﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Entities;
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IDefaultStorageConfiguration"/>
    /// </summary>
    public class DefaultStorageConfiguration : ConfigurationStorageElement, IDefaultStorageConfiguration
    {
        /// <inheritdoc/>
        public override void Initialize(IContainer container)
        {
            if (EntityContextConfiguration == null)
                EntityContextConfiguration = new NullEntityContextConfiguration();

            EntityContextConfiguration.BindDefaultEntityContext(container);
            base.Initialize(container);
        }

    }
}
