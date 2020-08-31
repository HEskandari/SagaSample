using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace SagaEndpoint
{
    public class MyTestSaga : Saga<MySagaData>
        , IAmStartedByMessages<TestCommand>
        , IHandleMessages<FinishSaga>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<TestCommand>(x => x.ID).ToSaga(s => s.SagaID);
            mapper.ConfigureMapping<FinishSaga>(x => x.ID).ToSaga(s => s.SagaID);
        }

        public Task Handle(TestCommand message, IMessageHandlerContext context)
        {
            if (!Data.ShouldStart)
            {
                throw new Exception("Should start not set.");
            }

            if (message.ShouldFail)
            {
                throw new Exception("Should fail.");
            }

            Data.MoreData = new AdditionalData
            {
                Text = SagaDataFaker.AdditionalDataFaker.Text, 
                BooleanValue = SagaDataFaker.AdditionalDataFaker.BooleanValue,
                SentDate = SagaDataFaker.AdditionalDataFaker.SentDate,
                Image = SagaDataFaker.AdditionalDataFaker.Image
            };

            var sendOptions = new SendOptions();
            var deliverAt = DateTimeOffset.Now + message.ExecutionData.WaitForSeconds;
            sendOptions.SetHeader("MyCustomHeader", "My custom value");
            sendOptions.DoNotDeliverBefore(deliverAt);
            sendOptions.RouteToThisEndpoint();
            
            Console.WriteLine("TestCommand received.");

            return context.Send(new DoWorkCommand
            {
                ID = Data.SagaID
            });
        }

        public Task Handle(FinishSaga message, IMessageHandlerContext context)
        {
            if (message.ShouldFail)
            {
                throw new Exception("Should fail.");
            }

            Data.MoreData.Text = "Updated";

            Console.WriteLine($"MyCustomHeader value is {context.MessageHeaders["MyCustomHeader"]}" );
            Console.WriteLine("Saga finished.");

            MarkAsComplete();

            return Task.CompletedTask;
        }
    }
}