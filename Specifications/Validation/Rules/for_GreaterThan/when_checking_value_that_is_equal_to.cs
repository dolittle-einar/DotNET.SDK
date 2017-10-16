﻿using doLittle.Rules;
using doLittle.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Validation.Rules.for_GreaterThan
{
    public class when_checking_value_that_is_equal_to
    {
        static double value = 42.0;
        static GreaterThan<double>  rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new GreaterThan<double>(null, value);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_fail_with_value_is_equal_reason = () => rule_context_mock.Verify(r => r.Fail(rule, value, Reasons.ValueIsEqual), Times.Once());
    }
}
