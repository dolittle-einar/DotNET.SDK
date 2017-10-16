﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Conventions;

namespace doLittle.Read
{
    /// <summary>
    /// Defines a query for a specified type of <see cref="IReadModel"/>.
    /// </summary>
    /// <typeparam name="T">The type to query.</typeparam>
    /// <remarks>
    /// Types inheriting from this interface will be picked up proxy generation, deserialized and dispatched to the
    /// correct instance of <see cref="IQueryProviderFor{T}"/>.
    /// </remarks>
    public interface IQueryFor<T> : IQuery, IConvention where T : IReadModel
    {
    }
}
