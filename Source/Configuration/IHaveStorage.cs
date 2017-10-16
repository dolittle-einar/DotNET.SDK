﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Configuration
{
    /// <summary>
    /// Marker interface that to indicate the need of storage configuration
    /// </summary>
    public interface IHaveStorage
    {
        /// <summary>
        /// Gets or sets the entity context configuration
        /// </summary>
        IEntityContextConfiguration EntityContextConfiguration { get; set; }
    }
}
