﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Runtime.Events;

namespace doLittle.Domain
{
    /// <summary>
    /// Defines the basic functionality for finding and getting aggregated roots
    /// </summary>
    /// <typeparam name="T">Type of aggregated root</typeparam>
    public interface IAggregateRootRepository<T>
        where T : AggregateRoot
    {
        /// <summary>
        /// Get an aggregated root by id
        /// </summary>
        /// <param name="id"><see cref="EventSourceId"/> of aggregated root to get</param>
        /// <returns>An instance of the aggregated root</returns>
        T Get(EventSourceId id);
    }
}
