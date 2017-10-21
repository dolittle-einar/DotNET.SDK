﻿using doLittle.FluentValidation.MetaData;
using doLittle.Validation.MetaData;
using FluentValidation.Validators;
using Machine.Specifications;

namespace doLittle.FluentValidation.Specs.MetaData.for_GreaterThanGenerator
{
    public class when_generating
    {
        static GreaterThanValidator validator;
        static GreaterThanGenerator generator;
        static GreaterThan result;

        Establish context = () =>
        {
            validator = new GreaterThanValidator(5.7f);
            generator = new GreaterThanGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty", validator) as GreaterThan;

        It should_create_a_rule = () => result.ShouldNotBeNull();
        It should_pass_along_the_value = () => result.Value.ShouldEqual(validator.ValueToCompare);
    }
}
