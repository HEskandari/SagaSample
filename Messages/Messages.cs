using System;
using NServiceBus;

namespace Messages
{
    public class TestMessage : IMessage
    {
        public Guid ID { get; set; }
        public bool ShouldFail { get; set; }
    }

    public class SagaExecutionData
    {
        public string ProductName { get; set; }
        public TimeSpan WaitForSeconds { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
    }

    public class TestCommand : ICommand
    {
        public Guid ID { get; set; }
        public bool ShouldFail { get; set; }
        public SagaExecutionData ExecutionData { get; set; }
    }

    public class DoWorkCommand : ICommand
    {
        public Guid ID { get; set; }
    }

    public class TestEvent : IEvent
    {
        public Guid ID { get; set; }
        public bool ShouldFail { get; set; }
    }

    public class FinishSaga : ICommand
    {
        public Guid ID { get; set; }
        public bool ShouldFail { get; set; }
    }
}