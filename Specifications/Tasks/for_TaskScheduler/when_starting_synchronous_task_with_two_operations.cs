﻿using System;
using Machine.Specifications;
using doLittle.Tasks;

namespace doLittle.Specs.Tasks.for_TaskScheduler
{
    [Subject(typeof(TaskScheduler))]
    public class when_starting_synchronous_task_with_two_operations : given.a_task_scheduler
    {
        static TaskWithTwoOperations task;
        static bool done_called;

        Establish context = () =>
        {
            task = new TaskWithTwoOperations(false);
            scheduler_mock.Setup(s => 
                s.Start<Task>(Moq.It.IsAny<Action<Task>>(), task, Moq.It.IsAny<Action<Task>>())).Callback((Action<Task> a, Task t,Action<Task> d) => a(t));
            
        };

        Because of = () => task_scheduler.Start(task, t=>done_called=true);

        It should_schedule_a_single_operation = () => scheduler_mock.Verify(s => s.Start<Task>(Moq.It.IsAny<Action<Task>>(), task, Moq.It.IsAny<Action<Task>>()), Moq.Times.Exactly(1));
        It should_get_correct_index_for_first_operation = () => task.FirstOperationIndex.ShouldEqual(0);
        It should_get_correct_index_for_second_operation = () => task.SecondOperationIndex.ShouldEqual(1);
        It should_update_the_tasks_current_operation = () => task.CurrentOperation.ShouldEqual(2);
        It should_call_done = () => done_called.ShouldBeTrue();
    }
}
