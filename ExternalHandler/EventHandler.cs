using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace ExternalHandler
{
    public class EventHandlerTwo : IHandleMessages<TestEvent>
    {
        public Task Handle(TestEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine($"TestEvent received in {nameof(EventHandlerTwo)}.");
    
            return Task.CompletedTask;
        }
    }

}