﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using doLittle.Execution;
using doLittle.Types;

namespace doLittle.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandHandlerManager">ICommandHandlerManager</see>
    /// </summary>
    /// <remarks>
    /// The manager will automatically import any <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see>
    /// and use them when handling
    /// </remarks>
    [Singleton]
    public class CommandHandlerManager : ICommandHandlerManager
    {
        readonly IEnumerable<ICommandHandlerInvoker> _invokers;

        /// <summary>
        /// Initializes a new instance of a <see cref="CommandHandlerManager">CommandHandlerManager</see>
        /// </summary>
        /// <param name="invokers">
        /// <see cref="IInstancesOf{ICommandHandlerInvoker}">Invokers</see> to use for discovering the 
        /// <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see>'s to use
        /// </param>
        public CommandHandlerManager(IInstancesOf<ICommandHandlerInvoker> invokers)
        {
            _invokers = invokers;
        }

        /// <inheritdoc/>
        public void Handle(CommandRequest command)
        {
            var handled = false;

            foreach (var invoker in _invokers)
            {
                if (invoker.TryHandle(command))
                {
                    handled = true;
                }
            }

            ThrowIfNotHandled(command, handled);
        }

        void ThrowIfNotHandled(CommandRequest command, bool handled)
        {
            if (!handled) throw new CommandWasNotHandled(command);
        }
    }
}