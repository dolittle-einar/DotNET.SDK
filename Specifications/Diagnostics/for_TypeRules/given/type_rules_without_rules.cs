﻿using doLittle.Diagnostics;
using Machine.Specifications;

namespace doLittle.Specs.Diagnostics.for_TypeRules.given
{
    public class type_rules_without_rules : all_dependencies
    {
        protected static TypeRules type_rules;

        Establish context = () => 
            type_rules = new TypeRules(
                                type_finder.Object, 
                                container.Object, 
                                problems_factory_mock.Object,
                                problems_reporter_mock.Object
                             );
    }
}
