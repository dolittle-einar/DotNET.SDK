﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using doLittle.Execution;
using doLittle.Validation;
using doLittle.Types;

namespace doLittle.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandValidators"/> 
    /// </summary>
    public class CommandValidators : ICommandValidators
    {
        IInstancesOf<ICommandValidator> _validators;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandValidators"/>
        /// </summary>
        /// <param name="validators">Instances of validators to use</param>
        public CommandValidators(IInstancesOf<ICommandValidator> validators)
        {
            _validators = validators;
        }

        /// <inheritdoc/>
        public CommandValidationResult Validate(CommandRequest command)
        {
            var errorMessages = new List<string>();
            var validationResults = new List<ValidationResult>();

            foreach (var validator in _validators)
            {
                var validatorResult = validator.Validate(command);
                errorMessages.AddRange(validatorResult.CommandErrorMessages);
                validationResults.AddRange(validatorResult.ValidationResults);
            }
            var result = new CommandValidationResult
            {
                CommandErrorMessages = errorMessages,
                ValidationResults = validationResults
            };
            return result;
        }
    }
}
