﻿using System.Linq;
using doLittle.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Diagnostics.for_ProblemsReporter
{
    public class when_clearing_after_reporting
    {
        static Mock<IProblems> problems_mock;
        static ProblemsReporter reporter;

        Establish context = () => 
        {
            problems_mock = new Mock<IProblems>();
            reporter = new ProblemsReporter();
            reporter.Report(problems_mock.Object);
        };

        Because of = () => reporter.Clear();

        It should_not_hold_any_problems = () => reporter.All.Count().ShouldEqual(0);
    }
}
