﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Events.Processing.for_ProcessMethodEventProcessor.when_creating
{
    using System;
    using Dolittle.Artifacts;
    using Machine.Specifications;

    [Subject(typeof(ProcessMethodEventProcessor), "Create")]
    public class an_invoker_for_just_the_event_signature : given.event_processors
    {
        static ProcessMethodEventProcessor result;

        Because of = () => result = new ProcessMethodEventProcessor(object_factory.Object,container.Object,Guid.NewGuid(),new Artifact(Guid.NewGuid(),1),typeof(given.MyEvent),method_with_just_event, logger.Object);

        It should_create_the_process_method_event_processor = () => result.ShouldNotBeNull();
    }
}