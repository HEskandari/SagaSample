using System;
using System.Threading.Tasks;
using Bogus;
using Messages;
using NServiceBus;

namespace Sender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IEndpointInstance endpoint = await Endpoint.Start(EndpointStarter.CreateCommonConfiguration(EndpointNames.Sender));

            Console.WriteLine("Started.");
            Console.WriteLine("Press [E], [C], [M], [S] or press Enter to exit.");
            ConsoleKeyInfo key;
            bool shouldFail = false;
            Randomizer.Seed = new Random(8675309);

            while ((key = Console.ReadKey()).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.S)
                {
                    shouldFail = !shouldFail;
                    Console.WriteLine("Should fail is now: " + shouldFail);
                }

                if (key.Key == ConsoleKey.E)
                {
                    await endpoint.Publish(new TestEvent { ID = Guid.NewGuid(), ShouldFail = shouldFail});
                }

                if (key.Key == ConsoleKey.C)
                {
                    await endpoint.Send(
                        new TestCommand
                        {
                            ID = Guid.NewGuid(),
                            ShouldFail = shouldFail,
                            ExecutionData = new SagaExecutionData
                            {
                                Category = DataFaker.ExecutionDataFaker.Category,
                                Color = DataFaker.ExecutionDataFaker.Color,
                                ProductName = DataFaker.ExecutionDataFaker.ProductName,
                                WaitForSeconds = DataFaker.ExecutionDataFaker.WaitForSeconds
                            }
                        });
                }

                if (key.Key == ConsoleKey.M)
                {
                    await endpoint.SendLocal(new TestMessage { ID = Guid.NewGuid(), ShouldFail = shouldFail });
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    await endpoint.Stop();
                    break;
                }
            }
        }
    }
}