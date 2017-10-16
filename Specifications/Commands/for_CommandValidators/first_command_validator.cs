﻿using doLittle.Commands;

namespace doLittle.Specs.Commands.for_CommandValidators
{
    public class first_command_validator : ICommandValidator
    {
        public CommandValidationResult result_to_return;
        public bool validate_called;
        public CommandRequest command_passed_to_validate;

        public CommandValidationResult Validate(CommandRequest command)
        {
            validate_called = true;
            command_passed_to_validate = command;
            return result_to_return;
        }
    }
}
