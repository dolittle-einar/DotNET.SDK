﻿using System.Collections.Generic;
using System.Linq;
using doLittle.Utils;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Utils.for_StringMapper
{
    [Subject(typeof(StringMapper))]
    public class when_getting_all_mappings_for_a_string_with_no_mappings_matching
    {
        const string input = "something";
        static StringMapper mapper = new StringMapper();
        static Mock<IStringMapping> first_mapping_mock;
        static Mock<IStringMapping> second_mapping_mock;
        static IEnumerable<IStringMapping> result;

        Establish context = () =>
            {
                first_mapping_mock = new Mock<IStringMapping>();
                first_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
                second_mapping_mock = new Mock<IStringMapping>();
                second_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
                mapper.AddMapping(first_mapping_mock.Object);
                mapper.AddMapping(second_mapping_mock.Object);
            };

        Because of = () => result = mapper.GetAllMatchingMappingsFor(input);

        It should_return_no_mappings = () => result.Any().ShouldBeFalse();
    }
}