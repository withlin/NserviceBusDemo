using Messages;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "邮递";

            var endpointConfiguration = new EndpointConfiguration("Shipping");
            //也可以用mssql做存储
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();

            var routing = transport.Routing();
            routing.RegisterPublisher(typeof(OrderPlaced), "Sales");
            routing.RegisterPublisher(typeof(OrderBilled), "Billing");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("按Enter 键退出... ");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}