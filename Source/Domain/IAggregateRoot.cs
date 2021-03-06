﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Events;
using Dolittle.Runtime.Transactions;

namespace Dolittle.Domain
{
    /// <summary>
    /// Defines the very basic functionality needed for an aggregated root
    /// </summary>
    public interface IAggregateRoot : IEventSource, ITransaction
    {
    }
}