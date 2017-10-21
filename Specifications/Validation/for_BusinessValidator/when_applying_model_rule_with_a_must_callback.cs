﻿using Machine.Specifications;

namespace doLittle.FluentValidation.Specs.for_BusinessValidator
{
    public class when_applying_model_rule_with_a_must_callback
    {
        static ValidatorWithModelRuleWithOneMustClause validator;
        static SimpleObject object_to_validate;

        Establish context = () =>
        {
            validator = new ValidatorWithModelRuleWithOneMustClause();
            object_to_validate = new SimpleObject();
        };

        Because of = () => validator.Validate(object_to_validate);

        It should_call_the_callback = () => validator.CallbackCalled.ShouldBeTrue();
    }
}
