﻿using System.Dynamic;
using doLittle.Applications;
using doLittle.Commands;
using doLittle.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Commands.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_no_command_handlers : given.a_command_handler_invoker_with_no_command_handlers
    {
        protected static bool result;

        Because of = () =>
                         {
                             var command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
                             result = invoker.TryHandle(command);
                         };

        It should_return_false_when_trying_to_handle = () => result.ShouldBeFalse();
    }
}
