﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Execution;
using doLittle.Collections;
using doLittle.Types;

namespace doLittle.Tenancy
{
    /// <summary>
    /// Represents an implentation of <see cref="ITenantPopulator"/> capable of working with 
    /// <see cref="ICanPopulateTenant"/>
    /// </summary>
    /// <remarks>
    /// You can have as many implementations of <see cref="ICanPopulateTenant"/> - they will all
    /// be called. There is no guarantee in ordering of when they're called
    /// </remarks>
    [Singleton]
    public class TenantPopulator : ITenantPopulator
    {
        IInstancesOf<ICanPopulateTenant> _populators;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="populators"></param>
        public TenantPopulator(IInstancesOf<ICanPopulateTenant> populators)
        {
            _populators = populators;
        }


        /// <inheritdoc/>
        public void Populate(ITenant tenant, dynamic details)
        {
            _populators.ForEach(p => p.Populate(tenant, details));
        }
    }
}
