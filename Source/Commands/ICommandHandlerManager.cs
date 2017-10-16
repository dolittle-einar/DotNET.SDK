﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Commands
{
    /// <summary>
    /// Defines the functionality for a manager that handles commands
    /// 
    /// Handles a <see cref="ICommand">command</see> by calling any
    /// command handlers that can handle the specific command
    /// </summary>
    public interface ICommandHandlerManager
    {
        /// <summary>
        /// Handle a command
        /// </summary>
        /// <param name="command"><see cref="CommandRequest">Command</see> to handle</param>
        void Handle(CommandRequest command);
    }
}