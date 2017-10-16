﻿using doLittle.Read;
using Machine.Specifications;

namespace doLittle.Specs.Read.for_FetchingSecurityManager
{
    public class when_authorizing_a_query : given.a_fetching_security_manager
    {
        static SomeQueryFor query_for;

        Establish context = () => query_for = new SomeQueryFor();

        Because of = () => fetching_security_manager.Authorize((IQuery)query_for);

        It should_delegate_the_request_for_security_to_the_security_manager = () => security_manager_mock.Verify(s => s.Authorize<Fetching>(query_for), Moq.Times.Once());
    }
}
