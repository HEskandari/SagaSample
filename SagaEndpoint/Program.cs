using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace SagaEndpoint
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IEndpointInstance endpoint = null;

            try
            {
                endpoint = await Endpoint.Start(EndpointStarter.CreateCommonConfiguration(EndpointNames.SagaEndpoint));
                Console.WriteLine("Started.");
                Console.ReadLine();
            }
            finally
            {
                endpoint?.Stop();
            }
        }
    }
}