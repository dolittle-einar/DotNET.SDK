/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Applications;
using doLittle.Runtime.Execution;
using doLittle.Time;
using doLittle.Runtime.Transactions;
using doLittle.Runtime.Events.Migration;
using doLittle.Events;
using doLittle.Runtime.Events.Storage;
using doLittle.Runtime.Events;

namespace doLittle.Events.Storage
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventEnvelopes"/>
    /// </summary>
    public class EventEnvelopes : IEventEnvelopes
    {
        IApplicationArtifacts _applicationResources;
        ISystemClock _systemClock;
        IExecutionContext _executionContext;
        IEventMigrationHierarchyManager _eventMigrationHierarchyManager;

        /// <summary>
        /// Initializes a new instance of <see cref="EventEnvelopes"/>
        /// </summary>
        /// <param name="applicationArtifacts"><see cref="IApplicationArtifacts"/> for identifying artifacts</param>
        /// <param name="systemClock"><see cref="ISystemClock"/> for working with time</param>
        /// <param name="executionContext"><see cref="IExecutionContext"/> for working with metadata related to current execution context</param>
        /// <param name="eventMigrationHierarchyManager"><see cref="IEventMigrationHierarchyManager"/> for working with the migration levels of <see cref="IEvent">events</see></param>
        public EventEnvelopes(
            IApplicationArtifacts applicationArtifacts, 
            ISystemClock systemClock, 
            IExecutionContext executionContext, 
            IEventMigrationHierarchyManager eventMigrationHierarchyManager)
        {
            _applicationResources = applicationArtifacts;
            _systemClock = systemClock;
            _executionContext = executionContext;
            _eventMigrationHierarchyManager = eventMigrationHierarchyManager;
        }

        /// <inheritdoc/>
        public IEventEnvelope CreateFrom(IEventSource eventSource, IEvent @event, EventSourceVersion version)
        {
            var envelope = new EventEnvelope(
                TransactionCorrelationId.NotSet,
                Guid.NewGuid(),
                EventSequenceNumber.Zero, 
                EventSequenceNumber.Zero, 
                _eventMigrationHierarchyManager.GetCurrentGenerationFor(@event.GetType()),
                _applicationResources.Identify(@event),
                eventSource.EventSourceId,
                _applicationResources.Identify(eventSource),
                version,
                _executionContext.Principal.Identity.Name,
                _systemClock.GetCurrentTime()
            );

            return envelope;
        }

        /// <inheritdoc/>
        public IEnumerable<IEventEnvelope> CreateFrom(IEventSource eventSource, IEnumerable<EventAndVersion> eventsAndVersion)
        {
            return eventsAndVersion.Select(e => CreateFrom(eventSource, e.Event, e.Version));
        }
    }
}
