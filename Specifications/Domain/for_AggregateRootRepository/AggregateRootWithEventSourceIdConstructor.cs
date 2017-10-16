﻿using doLittle.Domain;
using doLittle.Events;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithEventSourceIdConstructor : AggregateRoot
    {
        public AggregateRootWithEventSourceIdConstructor(EventSourceId id) : base(id)
        {

        }
    }
}
