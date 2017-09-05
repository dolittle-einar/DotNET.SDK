﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using doLittle.Applications;
using doLittle.Concepts;
using doLittle.DependencyInversion;
using doLittle.Reflection;
using doLittle.JSON.Application;
using doLittle.JSON.Concepts;
using doLittle.JSON.Events;
using doLittle.Logging;
using doLittle.Serialization;
using doLittle.Strings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace doLittle.JSON.Serialization
{
    /// <summary>
    /// Represents a <see cref="ISerializer"/>
    /// </summary>
    public class Serializer : ISerializer
    {
        readonly IContainer _container;
        readonly IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        ConcurrentDictionary<ISerializationOptions, JsonSerializer> _cacheAutoTypeName;
        ConcurrentDictionary<ISerializationOptions, JsonSerializer> _cacheAutoTypeNameReadOnly;
        ConcurrentDictionary<ISerializationOptions, JsonSerializer> _cacheNoneTypeName;
        ConcurrentDictionary<ISerializationOptions, JsonSerializer> _cacheNoneTypeNameReadOnly;

        /// <summary>
        /// Initializes a new instance of <see cref="Serializer"/>
        /// </summary>
        /// <param name="container">A <see cref="IContainer"/> used to create instances of types during serialization</param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> for converting string representations of <see cref="IApplicationResourceIdentifier"/></param>
        public Serializer(
            IContainer container,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter)
        {
            _container = container;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _cacheAutoTypeName = new ConcurrentDictionary<ISerializationOptions, JsonSerializer>();
            _cacheNoneTypeName = new ConcurrentDictionary<ISerializationOptions, JsonSerializer>();
            _cacheAutoTypeNameReadOnly = new ConcurrentDictionary<ISerializationOptions, JsonSerializer>();
            _cacheNoneTypeNameReadOnly = new ConcurrentDictionary<ISerializationOptions, JsonSerializer>();
        }

        /// <inheritdoc/>
        public T FromJson<T>(string json, ISerializationOptions options = null)
        {
            return (T)FromJson(typeof(T), json, options);
        }

        /// <inheritdoc/>
        public object FromJson(Type type, string json, ISerializationOptions options = null)
        {
            var serializer = CreateSerializerForDeserialization(options);
            using (var textReader = new StringReader(json))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    object instance;

                    if (type.IsConcept())
                    {
                        var genericArgumentType = type.GetConceptValueType();
                        var value = serializer.Deserialize(reader, genericArgumentType);
                        return ConceptFactory.CreateConceptInstance(type, value);
                    }

                    if (type.GetTypeInfo().IsValueType ||
                        type.HasInterface<IEnumerable>())
                        instance = serializer.Deserialize(reader, type);
                    else
                    {
                        IEnumerable<string> propertiesMatched;
                        instance = CreateInstanceOf(type, json, out propertiesMatched);

                        DeserializeRemaindingProperties(type, serializer, reader, instance, propertiesMatched);
                    }
                    return instance;
                }
            }
        }

        /// <inheritdoc/>
        public void FromJson(object instance, string json, ISerializationOptions options = null)
        {
            var serializer = CreateSerializerForDeserialization(options);
            using (var textReader = new StringReader(json))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    serializer.Populate(reader, instance);
                }
            }
        }

        /// <inheritdoc/>
        public string ToJson(object instance, ISerializationOptions options = null)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = CreateSerializerForSerialization(options);
                serializer.Serialize(stringWriter, instance);
                var serialized = stringWriter.ToString();
                return serialized;
            }
        }

        /// <inheritdoc/>
        public Stream ToJsonStream(object instance, ISerializationOptions options = null)
        {
            var serialized = ToJson(instance, options);

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(serialized);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        /// <inheritdoc/>
        public IDictionary<string, object> GetKeyValuesFromJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }



        object CreateInstanceOf(Type type, string json, out IEnumerable<string> propertiesMatched)
        {
            propertiesMatched = new string[0];
            if (type.HasDefaultConstructor())
                return Activator.CreateInstance(type);
            else
            {
                if (DoesPropertiesMatchConstructor(type, json))
                    return CreateInstanceByPropertiesMatchingConstructor(type, json, out propertiesMatched);
                else
                    return _container.Get(type);
            }
        }


        bool DoesPropertiesMatchConstructor(Type type, string json)
        {
            var hash = JObject.Load(new JsonTextReader(new StringReader(json)));
            var constructor = type.GetNonDefaultConstructor();
            var parameters = constructor.GetParameters();
            var properties = hash.Properties();
            var matchingParameters = parameters.Where(cp => properties.Select(p => p.Name.ToCamelCase()).Contains(cp.Name.ToCamelCase()));
            return matchingParameters.Count() == parameters.Length;
        }

        object CreateInstanceByPropertiesMatchingConstructor(Type type, string json, out IEnumerable<string> propertiesMatched)
        {
            var propertiesFound = new List<string>();
            var hash = JObject.Load(new JsonTextReader(new StringReader(json)));
            var properties = hash.Properties();

            var constructor = type.GetNonDefaultConstructor();

            var parameters = constructor.GetParameters();
            var parameterInstances = new List<object>();

            var toObjectMethod = typeof(JToken).GetTypeInfo().GetMethod("ToObject", new Type[] { typeof(JsonSerializer) });
            var serializer = CreateSerializerForDeserialization(SerializationOptions.CamelCase);

            foreach (var parameter in parameters)
            {
                var property = properties.Single(p => p.Name.ToCamelCase() == parameter.Name.ToCamelCase());
                propertiesFound.Add(property.Name);

                object parameterInstance = null;
                if (parameter.ParameterType == typeof(object))
                {
                    parameterInstance = serializer.Deserialize(new JsonTextReader(new StringReader(property.Value.ToString())), typeof(ExpandoObject));
                }
                else
                {
                    var genericToObjectMethod = toObjectMethod.MakeGenericMethod(parameter.ParameterType);
                    parameterInstance = genericToObjectMethod.Invoke(property.Value, new[] { serializer });
                }

                parameterInstances.Add(parameterInstance);
            }
            propertiesMatched = propertiesFound;
            var instance = constructor.Invoke(parameterInstances.ToArray());
            return instance;
        }


        void DeserializeRemaindingProperties(Type type, JsonSerializer serializer, JsonTextReader reader, object instance, IEnumerable<string> propertiesMatched)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = (string)reader.Value;

                    reader.Read();

                    if (!propertiesMatched.Contains(propertyName))
                    {
                        var typeInfo = type.GetTypeInfo();
                        var property = typeInfo.GetProperty(propertyName);
                        if (property == null) property = typeInfo.GetProperty(propertyName.ToPascalCase());
                        if (property != null && property.CanWrite )
                        {
                            var deserialized = serializer.Deserialize(reader, property.PropertyType);
                            property.SetValue(instance, deserialized);
                        }
                    }
                }
            }
        }


        JsonSerializer CreateSerializerForDeserialization(ISerializationOptions options = null)
        {
            return RetrieveSerializer(options ?? SerializationOptions.Default, true);
        }

        JsonSerializer CreateSerializerForSerialization(ISerializationOptions options = null)
        {
            return RetrieveSerializer(options ?? SerializationOptions.Default, false);
        }

        JsonSerializer RetrieveSerializer(ISerializationOptions options, bool ignoreReadOnlyProperties = false)
        {
            if (options.Flags.HasFlag(SerializationOptionsFlags.IncludeTypeNames))
            {
                if( ignoreReadOnlyProperties ) return _cacheAutoTypeNameReadOnly.GetOrAdd(options, _ => CreateSerializer(options, TypeNameHandling.Auto, ignoreReadOnlyProperties));
                return _cacheAutoTypeName.GetOrAdd(options, _ => CreateSerializer(options, TypeNameHandling.Auto, ignoreReadOnlyProperties));
            }
            else
            {
                if( ignoreReadOnlyProperties ) return _cacheNoneTypeNameReadOnly.GetOrAdd(options, _ => CreateSerializer(options, TypeNameHandling.None, ignoreReadOnlyProperties));
                return _cacheNoneTypeName.GetOrAdd(options, _ => CreateSerializer(options, TypeNameHandling.None, ignoreReadOnlyProperties));
            }
        }

        JsonSerializer CreateSerializer(ISerializationOptions options, TypeNameHandling typeNameHandling, bool ignoreReadOnlyProperties = false)
        {
            var contractResolver = new SerializerContractResolver(_container, options, ignoreReadOnlyProperties);

            var serializer = new JsonSerializer
            {
                TypeNameHandling = typeNameHandling,
                ContractResolver = contractResolver,
            };
            serializer.Converters.Add(new ApplicationResourceIdentifierJsonConverter(_applicationResourceIdentifierConverter));
            serializer.Converters.Add(new ExceptionConverter());
            serializer.Converters.Add(new ConceptConverter());
            serializer.Converters.Add(new ConceptDictionaryConverter());
            serializer.Converters.Add(new EventSourceVersionConverter());
            serializer.Converters.Add(new CamelCaseToPascalCaseExpandoObjectConverter());

            return serializer;
        }
    }
}