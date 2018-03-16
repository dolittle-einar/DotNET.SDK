﻿using System.Dynamic;
using System.Linq;
using Dolittle.Commands;
using Dolittle.Runtime.Commands;
using Dolittle.Commands.Validation;
using Dolittle.Validation;
using Dolittle.Applications;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.Runtime.Commands.Validation;

namespace Dolittle.FluentValidation.Commands.for_CommandValidator
{
    public class when_validating_a_command_that_has_model_rule_error_on_input_validator : given.a_command_validation_service
    {
        const string ErrorMessage = "Something went wrong";

        static CommandValidationResult result;
        static CommandRequest command;
        static ICommand command_instance;
        static Mock<ICommandInputValidator> command_input_validator;

        Establish context = () =>
        {
            command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationArtifactIdentifier>(), new ExpandoObject());
            command_instance = Mock.Of<ICommand>();
            command_request_converter.Setup(c => c.Convert(command)).Returns(command_instance);
            command_input_validator = new Mock<ICommandInputValidator>();
            command_input_validator.Setup(c => c.ValidateFor(command_instance)).Returns(new[] {
                new ValidationResult(ErrorMessage,new[] { ModelRule<object>.ModelRulePropertyName })
            });

            command_validator_provider_mock.Setup(c => c.GetInputValidatorFor(command_instance)).Returns(command_input_validator.Object);
        };

        Because of = () => result = command_validator.Validate(command);

        It should_have_a_command_error_message = () => result.CommandErrorMessages.First().ShouldEqual(ErrorMessage);
    }
}
