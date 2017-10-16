﻿using System;
using doLittle.Domain;


namespace doLittle.Specs.Commands
{
    public class StatefulAggregatedRoot : AggregateRoot
    {
        public string Value { get; set; }
        public bool EventApplied { get; private set; }

        public StatefulAggregatedRoot(Guid id) : base(id)
        {
        }

        void On(SimpleEvent simpleEvent)
        {
            EventApplied = true;
            Value = simpleEvent.Content;
        }


        public bool CommitCalled = false;
        public override void Commit()
        {
            CommitCalled = true;
            base.Commit();
        }

        public bool RollbackCalled = false;
        public override void Rollback()
        {
            RollbackCalled = true;
            base.Rollback();
        }
    }
}