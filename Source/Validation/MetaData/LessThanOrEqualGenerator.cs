﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Validation.MetaData;
using FluentValidation.Validators;

namespace Dolittle.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="LessThanOrEqual"/> rule from
    /// a <see cref="LessThanOrEqualValidator"/>
    /// </summary>
    public class LessThanOrEqualGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(LessThanOrEqualValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new LessThanOrEqual
            {
                Value = ((LessThanOrEqualValidator)propertyValidator).ValueToCompare,
                Message = propertyValidator.GetErrorMessageFor(propertyName)
            };
        }
#pragma warning restore 1591 // Xml Comments

    }
}
