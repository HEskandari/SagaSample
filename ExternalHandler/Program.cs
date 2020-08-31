using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace ExternalHandler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IEndpointInstance endpoint = null;

            try
            {
                endpoint = await Endpoint.Start(EndpointStarter.CreateCommonConfiguration(EndpointNames.ExternalHandler));

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