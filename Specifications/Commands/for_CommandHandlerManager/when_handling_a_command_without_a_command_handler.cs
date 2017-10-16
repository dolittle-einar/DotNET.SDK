﻿using System;
using System.Dynamic;
using doLittle.Applications;
using doLittle.Commands;
using doLittle.Lifecycle;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Commands.for_CommandHandlerManager
{
    public class when_handling_a_command_without_a_command_handler : given.a_command_handler_manager
    {
        static Exception thrown_exception;
        static CommandRequest handled_command;

        Because of = () =>
                         {
                             handled_command = new CommandRequest(TransactionCorrelationId.NotSet, Mock.Of<IApplicationResourceIdentifier>(), new ExpandoObject());
                             thrown_exception = Catch.Exception(() => manager.Handle(handled_command));
                         };

        It should_throw_unhandled_command_exception = () => thrown_exception.ShouldBeOfExactType<CommandWasNotHandled>();
    }
}
