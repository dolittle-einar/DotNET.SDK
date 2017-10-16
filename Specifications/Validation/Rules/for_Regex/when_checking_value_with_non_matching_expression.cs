﻿using System;
using doLittle.Rules;
using doLittle.Validation;
using doLittle.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Validation.Rules.for_Regex
{
    public class when_checking_value_with_non_matching_expression
    {
        static Regex rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new Regex(null, "\\w");
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, string.Empty);

        It should_fail_with_not_conforming_to_expression_as_reason = () => rule_context_mock.Verify(r => r.Fail(rule, string.Empty, Regex.NotConformingToExpression), Times.Once());
    }
}
