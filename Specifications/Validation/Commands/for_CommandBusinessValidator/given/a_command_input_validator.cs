﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_CommandBusinessValidator.given
{
    public class a_command_business_validator
    {
        protected static ICanValidate simple_command_business_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
                                {
                                    simple_command_business_validator = new SimpleCommandBusinessValidator();
                                    simple_command = new SimpleCommand();
                                };
    }

    public class a_command_business_validator_with_ruleset
    {
        protected static ICanValidate simple_command_business_validator;
        protected static SimpleCommand simple_command;

        Establish context = () =>
        {
            simple_command_business_validator = new SimpleCommandInputValidatorWithRuleset();
            simple_command = new SimpleCommand();
        };
    }
}