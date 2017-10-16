﻿using System;
using doLittle.Read;
using Machine.Specifications;

namespace doLittle.Specs.Read.for_ReadModelFilters.given
{
    public class read_model_filters_with_one_filter_that_filters_and_one_that_does_not : all_dependencies
    {
        protected static ReadModelFilters   filters;
        protected static FilterThatFilters filtering_filter;
        protected static FilterThatDoesNotFilter non_filtering_filter;

        Establish context = () =>
        {
            type_discoverer.Setup(t => t.FindMultiple<ICanFilterReadModels>()).Returns(new Type[] {
                typeof(FilterThatFilters),
                typeof(FilterThatDoesNotFilter)
            });
            filtering_filter = new FilterThatFilters();
            container.Setup(c => c.Get(typeof(FilterThatFilters))).Returns(filtering_filter);

            non_filtering_filter = new FilterThatDoesNotFilter();
            container.Setup(c => c.Get(typeof(FilterThatDoesNotFilter))).Returns(non_filtering_filter);

            filters = new ReadModelFilters(type_discoverer.Object, container.Object);
        };
    }
}
