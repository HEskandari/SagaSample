using NServiceBus;

namespace Messages
{
    public class EndpointNames
    {
        public static string Sender = nameof(Sender);
        public static string SagaEndpoint = nameof(SagaEndpoint);
        public static string ExternalHandler = nameof(ExternalHandler);
    }
    
    public static class Routing
    {
        public static void ApplyRouting(this RoutingSettings config)
        {
            config.RouteToEndpoint(typeof(TestCommand), EndpointNames.SagaEndpoint);
            config.RouteToEndpoint(typeof(FinishSaga), EndpointNames.SagaEndpoint);
            config.RouteToEndpoint(typeof(TestMessage), EndpointNames.ExternalHandler);
            config.RouteToEndpoint(typeof(DoWorkCommand), EndpointNames.ExternalHandler);
            
        }
    }
}