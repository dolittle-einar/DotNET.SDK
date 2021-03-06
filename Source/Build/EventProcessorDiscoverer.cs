/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Collections;
using Dolittle.Events.Processing;
using Dolittle.Lifecycle;

namespace Dolittle.Build
{
    
    /// <summary>
    /// Represents a class that can discover event processors
    /// </summary>
    public class EventProcessorDiscoverer
    {
        readonly static Type EventProcessorCollectionType = typeof(ICanProcessEvents);
        
        readonly IAssemblyContext _assemblyContext;
        readonly IBuildMessages _buildMessages;

        MethodInfo[] _eventProcessors;
        
        /// <summary>
        /// Instantiates and instance of <see cref="EventProcessorDiscoverer"/>
        /// </summary>
        /// <param name="assemblyContext"></param>
        /// <param name="buildMessages"></param>
        public EventProcessorDiscoverer(IAssemblyContext assemblyContext, IBuildMessages buildMessages)
        {
            _assemblyContext = assemblyContext;
            _buildMessages = buildMessages;
        }


        /// <summary>
        /// Gets all the discovered event processors
        /// </summary>
        public IEnumerable<MethodInfo> GetAllEventProcessors() => 
            _eventProcessors
            ?? (_eventProcessors = DiscoverEventProcessors());
        /// <summary>
        /// Gets all the discovered event processors of a type
        /// </summary>
        /// <param name="type"></param>
        public IEnumerable<MethodInfo> GetEventProcessors(Type type) => 
            _eventProcessors.Where(_ => _.DeclaringType.Equals(type)) 
            ?? (_eventProcessors = DiscoverEventProcessors()).Where(_ => _.DeclaringType.Equals(type));

        MethodInfo[] DiscoverEventProcessors()
        {
            var types = GetTypesHoldingEventProcessorsFromAssembly();

            IList<MethodInfo> eventProcessors = new List<MethodInfo>();

            foreach (var type in types)
            {
                var found = false;
                type.GetMethods().ForEach(_ => 
                {
                    var eventProcessorId = _.EventProcessorId();
                    if (eventProcessorId.Value != null && ! eventProcessorId.Value.Equals(Guid.Empty)) 
                    {
                        found = true;
                        eventProcessors.Add(_);
                    }
                });
                if (! found)
                    _buildMessages.Warning($"No event processor methods found in Event Processor collection class '{type.FullName}'. All methods that'll process events has to be marked with '{typeof(EventProcessorAttribute).FullName}' giving it a unique Event Processor Id.");
            }
            
            return eventProcessors.ToArray();
        }
        IEnumerable<Type> GetTypesHoldingEventProcessorsFromAssembly()
        {
            return _assemblyContext
                .GetProjectReferencedAssemblies()
                .SelectMany(_ => _.ExportedTypes)
                .Where(_ =>
                    EventProcessorCollectionType.IsAssignableFrom(_));
        }
    }
}