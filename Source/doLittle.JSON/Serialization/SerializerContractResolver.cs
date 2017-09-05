﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using doLittle.DependencyInversion;
using doLittle.Strings;
using doLittle.Serialization;
using doLittle.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace doLittle.JSON.Serialization
{
    /// <summary>
    /// Represents a <see cref="IContractResolver"/> based on the <see cref="DefaultContractResolver"/> for resolving contracts for serialization
    /// </summary>
    public class SerializerContractResolver : DefaultContractResolver
    {

        readonly IContainer _container;
        readonly ISerializationOptions _options;
        readonly bool _ignoreReadOnlyProperties;

        /// <summary>
        /// Initializes a new instance of <see cref="SerializerContractResolver"/>
        /// </summary>
        /// <param name="container">A <see cref="IContainer"/> to use for creating instances of types</param>
        /// <param name="options"><see cref="ISerializationOptions"/> to use during resolving</param>
        /// <param name="ignoreReadOnlyProperties">Wether or not to ignore read only properties - default false</param>
        
        public SerializerContractResolver(IContainer container, ISerializationOptions options, bool ignoreReadOnlyProperties = false)
        {
            _container = container;
            _options = options;
            _ignoreReadOnlyProperties = ignoreReadOnlyProperties;
        }


#pragma warning disable 1591 // Xml Comments
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            if( _ignoreReadOnlyProperties ) 
                properties = properties.Where(p => p.Writable == true).ToList();

            if (_options != null)
                return properties.Where(p => _options.ShouldSerializeProperty(type, p.PropertyName)).ToList();

            return properties;
        }


        public override JsonContract ResolveContract(Type type)
        {
            var contract = base.ResolveContract(type);

            if (contract is JsonObjectContract &&
                !type.GetTypeInfo().IsValueType &&
                !type.HasDefaultConstructor())
            {
                var defaultCreator = contract.DefaultCreator;
                contract.DefaultCreator = () =>
                                              {
                                                  try
                                                  {
                                                      if (type.HasDefaultConstructor()) return Activator.CreateInstance(type);
                                                      // Todo: Structs without default constructor will fail with this and that will then try using the defaultCreator in the catch
                                                      return _container.Get(type);
                                                  }
                                                  catch
                                                  {
                                                      if (defaultCreator != null)
                                                          return defaultCreator();
                                                      else
                                                          return null;
                                                  }
                                              };
            }

            return contract;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var result = base.ResolvePropertyName(propertyName);
            if (_options != null &&
                _options.Flags.HasFlag(SerializationOptionsFlags.UseCamelCase))
                result = result.ToCamelCase();

            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}