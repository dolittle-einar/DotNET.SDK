﻿using System;
using doLittle.Tasks;
using Machine.Specifications;

namespace doLittle.Specs.Tasks.for_TaskScheduler
{
    [Subject(typeof(TaskScheduler))]
    public class when_starting_asynchronous_task_with_two_operations_and_all_operations_are_done : given.a_task_scheduler
    {
        static TaskWithTwoOperations task;
        static bool done_called = false;

        Establish context = () =>
        {
            task = new TaskWithTwoOperations(true) { CurrentOperation = 2 };
        };

        Because of = () => task_scheduler.Start(task, t=>done_called = true);

        It should_not_start_any_operations = () => scheduler_mock.Verify(s => s.Start<Task>(Moq.It.IsAny<Action<Task>>(), task, Moq.It.IsAny<Action<Task>>()), Moq.Times.Never());
        It should_call_done = () => done_called.ShouldBeTrue();
    }
}
