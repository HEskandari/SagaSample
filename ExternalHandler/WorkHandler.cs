using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace ExternalHandler
{
    public class WorkHandler : 
        IHandleMessages<TestMessage>,
        IHandleMessages<TestEvent>,
        IHandleMessages<DoWorkCommand>
    {
        public Task Handle(TestMessage message, IMessageHandlerContext context)
        {
            if (message.ShouldFail)
            {
                throw new Exception("Should fail.");
            }

            Console.WriteLine("TestMessage received.");

            return Task.CompletedTask;
        }

        public Task Handle(TestEvent message, IMessageHandlerContext context)
        {
            if (message.ShouldFail)
            {
                throw new Exception("Should fail.");
            }
        
            Console.WriteLine($"TestEvent received in {nameof(WorkHandler)}.");
        
            return Task.CompletedTask;
        }

        public async Task Handle(DoWorkCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine("Doing work...");

            await Task.Delay(1000);
            
            var header = new SendOptions();
            header.SetHeader("MyCustomHeader", "some header value");
            
            await context.Send(new FinishSaga
            {
                ID = message.ID
            }, header);
        }
    }
}