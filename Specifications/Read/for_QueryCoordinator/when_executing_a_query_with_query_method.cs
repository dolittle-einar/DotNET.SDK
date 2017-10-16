﻿using System;
using doLittle.Read;
using Machine.Specifications;

namespace doLittle.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_with_query_method : given.a_query_coordinator
    {
        static IQuery query;
        static PagingInfo paging;
        static Exception exception;

        Establish   context = () => 
        {
            query = new QueryWithQueryMethod();
            paging = new PagingInfo();
        };

        Because of = () => exception = Catch.Exception(() => coordinator.Execute(query, paging));

        It should_throw_the_no_query_property_exception = () => exception.ShouldBeOfExactType<NoQueryPropertyException>();
    }
}
