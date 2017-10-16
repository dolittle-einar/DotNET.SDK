﻿using Machine.Specifications;

namespace doLittle.Specs.Read.Validation.for_QueryValidationResult
{
    public class when_asking_for_success_on_result_null_passed_as_broken_rules : given.a_query_validation_result_with_null_passed_as_broken_rules
    {
        static bool success;

        Because of = () => success = result.Success;

        It should_be_considered_successful = () => success.ShouldBeTrue();
    }
}
