using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Serialization;

namespace Messages
{
    public class EndpointStarter
    {
        public static EndpointConfiguration CreateCommonConfiguration(string name)
        {
            string connectionString = @"Server=192.168.0.114;Database=ServiceControlSql;User Id=sa;Password=GQI1qNeq0oEHlL;";
            EndpointConfiguration cfg = new EndpointConfiguration(name);
            PersistenceExtensions<SqlPersistence> persistence = cfg.UsePersistence<SqlPersistence>();
            TransportExtensions<SqlServerTransport> transport = cfg.UseTransport<SqlServerTransport>();
            SerializationExtensions<NewtonsoftSerializer> ser = cfg.UseSerialization<NewtonsoftSerializer>();

            cfg.AuditSagaStateChanges(serviceControlQueue: "Particular.ServiceControl.Sql");
            cfg.AuditProcessedMessagesTo("audit");
            cfg.SendFailedMessagesTo("error");
            cfg.EnableInstallers();
            cfg.DefineCriticalErrorAction(OnCriticalError);
            
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(() => new SqlConnection(connectionString));
            persistence.SubscriptionSettings().DisableCache();

            transport.ConnectionString(connectionString);
            transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);
            transport.Routing().ApplyRouting();
            
            RecoverabilitySettings recoverabilityConfig = cfg.Recoverability();
            recoverabilityConfig.Immediate(delegate (ImmediateRetriesSettings config)
            {
                config.NumberOfRetries(3);
            });
            recoverabilityConfig.Delayed(delegate (DelayedRetriesSettings config)
            {
                config.NumberOfRetries(3);
            });

            return cfg;
        }
        
        private static Task OnCriticalError(ICriticalErrorContext arg)
        {
            Console.WriteLine("Error: " + arg.Error);
            return Task.CompletedTask;
        }
    }
}