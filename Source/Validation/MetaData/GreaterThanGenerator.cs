﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Validation.MetaData;
using FluentValidation.Validators;

namespace doLittle.FluentValidation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="GreaterThan"/> rule from
    /// a <see cref="GreaterThanValidator"/>
    /// </summary>
    public class GreaterThanGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(GreaterThanValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new GreaterThan
            {
                Value = ((GreaterThanValidator)propertyValidator).ValueToCompare,
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
#pragma warning restore 1591 // Xml Comments

    }
}
