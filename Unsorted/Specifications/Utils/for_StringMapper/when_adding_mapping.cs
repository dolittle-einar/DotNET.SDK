﻿using System.Linq;
using doLittle.Utils;
using Machine.Specifications;

namespace doLittle.Specs.Utils.for_StringMapper
{
    public class when_adding_mapping
    {
        static StringMapper mapper = new StringMapper();

        Because of = () => mapper.AddMapping("Something", "else");

        It should_add_a_mapping = () => mapper.Mappings.Count().ShouldEqual(1);
        It should_pass_the_format_to_the_mapping = () => mapper.Mappings.First().Format.ShouldEqual("Something");
        It should_pass_the_mapped_format_to_the_mapping = () => mapper.Mappings.First().MappedFormat.ShouldEqual("else");
    }
}
