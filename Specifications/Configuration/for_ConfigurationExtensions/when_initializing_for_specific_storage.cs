﻿using doLittle.Configuration;
using doLittle.Entities;
using Machine.Specifications;
using Moq;
using System;
using It = Machine.Specifications.It;

namespace doLittle.Specs.Configuration.for_ConfigurationExtensions
{
    [Subject(typeof(ConfigurationStorageElement))]
    public class when_initializing_for_specific_storage : given.a_configuration_element_with_storage
    {
        Because of = () => configuration.BindEntityContextTo<SomeType>(container.Object);

        It should_bind_the_specific_connection = () => container.Verify(c => c.Bind(typeof(EntityContextConnection), connection));
        It should_bind_specific_storage_for_type = () => container.Verify(c => c.Bind(typeof(IEntityContext<SomeType>), typeof(EntityContext<SomeType>)));
        It should_not_set_the_default_storage = () => container.Verify(c => c.Bind(typeof(IEntityContext<>),Moq.It.IsAny<Type>()),Times.Never());
    }

}
