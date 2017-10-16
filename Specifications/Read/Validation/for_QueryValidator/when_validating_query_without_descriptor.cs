﻿using doLittle.Read.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Read.Validation.for_QueryValidator
{
    public class when_validating_query_without_descriptor : given.a_query_validator
    {
        static SomeQuery query;
        static QueryValidationResult result;

        Establish context = () =>
        {
            query = new SomeQuery();
            query_validation_descriptors_mock.Setup(q => q.HasDescriptorFor<SomeQuery>()).Returns(false);
        };

        Because of = () => result = query_validator.Validate(query);

        It should_return_a_valid_result = () => result.Success.ShouldBeTrue();
        It should_check_if_has_descriptors = () => query_validation_descriptors_mock.Verify(q => q.HasDescriptorFor<SomeQuery>(), Times.Once());
        It should_not_get_descriptor = () => query_validation_descriptors_mock.Verify(q => q.GetDescriptorFor<SomeQuery>(), Times.Never());
    }
}
