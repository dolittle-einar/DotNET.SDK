﻿using Biförost.Specs.Security.Fakes;
using doLittle.Security;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_checking_can_authorize_with_target_that_can_authorize
    {
        static ISecurityAction action_that_can_authorize;
        static ISecurityAction action_that_cannot_authorize;
        static BaseSecurityDescriptor descriptor;
        static bool can_authorize;

        public when_checking_can_authorize_with_target_that_can_authorize()
        {
            action_that_can_authorize = new MySecurityAction(o => true, o => null);
            action_that_cannot_authorize = new MySecurityAction(o => true, ObjectDisposedException => null);

            descriptor = new BaseSecurityDescriptor();
            descriptor.AddAction(action_that_cannot_authorize);
            descriptor.AddAction(action_that_can_authorize);
        }

        Because of = () => can_authorize = descriptor.CanAuthorize<MySecurityAction>(new object());

        It should_be_authorizable = () => can_authorize.ShouldBeTrue();
    }
}