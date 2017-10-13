﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Domain;
using doLittle.Runtime.Execution;
using doLittle.Runtime.Events.Coordination;
using doLittle.Runtime.Transactions;
using doLittle.Logging;

namespace doLittle.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandContext">ICommandContext</see>
    /// </summary>
    public class CommandContext : ICommandContext
    {
        readonly IUncommittedEventStreamCoordinator _uncommittedEventStreamCoordinator;
        readonly List<IAggregateRoot> _objectsTracked = new List<IAggregateRoot>();

        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new <see cref="CommandContext">CommandContext</see>
        /// </summary>
        /// <param name="command">The <see cref="CommandRequest">command</see> the context is for</param>
        /// <param name="executionContext">The <see cref="IExecutionContext"/> for the command</param>
        /// <param name="uncommittedEventStreamCoordinator">The <see cref="IUncommittedEventStreamCoordinator"/> to use for coordinating the committing of events</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        public CommandContext(
            CommandRequest command,
            IExecutionContext executionContext,
            IUncommittedEventStreamCoordinator uncommittedEventStreamCoordinator,
            ILogger logger)
        {
            Command = command;
            ExecutionContext = executionContext;
            _uncommittedEventStreamCoordinator = uncommittedEventStreamCoordinator;
            _logger = logger;

            // This should be exposed to the client somehow - maybe even coming from the client
            TransactionCorrelationId = Guid.NewGuid();
        }


        /// <inheritdoc/>
        public TransactionCorrelationId TransactionCorrelationId { get; }

        /// <inheritdoc/>
        public CommandRequest Command { get; }

        /// <inheritdoc/>
        public IExecutionContext ExecutionContext { get; }

        /// <inheritdoc/>
        public void RegisterForTracking(IAggregateRoot aggregateRoot)
        {
            if( _objectsTracked.Contains(aggregateRoot)) return;
            _objectsTracked.Add(aggregateRoot);
        }

        /// <inheritdoc/>
        public IEnumerable<IAggregateRoot> GetObjectsBeingTracked()
        {
            return _objectsTracked;
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            Commit();
        }

        /// <inheritdoc/>
        public void Commit()
        {
            _logger.Information("Commit transaction");
            var trackedObjects = GetObjectsBeingTracked();
            _logger.Trace($"Total number of objects tracked '{trackedObjects.Count()}");
            foreach (var trackedObject in trackedObjects)
            {
                _logger.Trace($"Committing events from {trackedObject.GetType().AssemblyQualifiedName}");
                var events = trackedObject.UncommittedEvents;
                if (events.HasEvents)
                {
                    _logger.Trace("Events present - send them to uncommitted eventstream coordinator");
                    _uncommittedEventStreamCoordinator.Commit(TransactionCorrelationId, events);
                    _logger.Trace("Commit object");
                    trackedObject.Commit();
                }
            }
        }

        /// <inheritdoc/>
        public void Rollback()
        {
            // Todo : Should rollback any aggregated roots that are being tracked - this should really only be allowed to happen if we have not stored the events yet
            // once the events are stored, we can't roll back

        }
#pragma warning restore 1591 // Xml Comments
    }
}
