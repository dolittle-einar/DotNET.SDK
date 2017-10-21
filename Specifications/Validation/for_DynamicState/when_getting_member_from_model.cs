﻿using Machine.Specifications;

namespace doLittle.FluentValidation.Specs.for_DynamicState
{
    public class when_getting_member_from_model
    {
        static Model model;
        static dynamic state;
        static string result;

        Establish context = () => 
        {
            model = new Model();
            state = new DynamicState(model, new[] { Model.TheStringProperty });
        };

        Because of = () => result = state.TheString;

        It should_call_get_on_model = () => model.TheStringGetCalled.ShouldBeTrue();
    }
}
