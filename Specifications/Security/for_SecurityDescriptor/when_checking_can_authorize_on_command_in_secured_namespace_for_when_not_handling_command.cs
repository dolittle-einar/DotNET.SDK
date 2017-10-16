﻿using Biförost.Specs.Security.Fakes;
using doLittle.Security;
using Machine.Specifications;

namespace doLittle.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_checking_can_authorize_on_command_in_secured_namespace_for_when_not_handling_command : given.a_configured_security_descriptor
    {
        static bool can_authorize;

        Because of = () => can_authorize = security_descriptor.CanAuthorize<MySecurityAction>(command_that_has_namespace_rule);

        It should_not_be_able_to_authorize = () => can_authorize.ShouldBeFalse();
    }
}