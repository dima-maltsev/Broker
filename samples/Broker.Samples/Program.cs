using System;
using System.Threading.Tasks;
using Broker.Extensions.Microsoft.DependencyInjection;
using Broker.Samples.Messages;
using Broker.Samples.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.Samples
{
    class Program
    {
        static async Task Main()
        {
            var services = new ServiceCollection();
            services.AddBroker();
            services.AddTransient(typeof(IPipeline<>), typeof(GenericPipeline<>));
            services.AddTransient(typeof(IPipeline<GreetingMessage>), typeof(GreetingPipeline));
            services.AddTransient(typeof(IPipeline<GreetingMessage>), typeof(GreetingQueryPipeline));

            var provider = services.BuildServiceProvider();
            var broker = provider.GetService<IBroker>();

            var greetingMessage = new GreetingMessage { Name = "World", User = "User" };

            await broker.SendAsync(greetingMessage);
            await broker.PublishAsync(greetingMessage);
            await broker.SendAsync<IAudit>(greetingMessage);

            var result = await broker.QueryAsync<GreetingMessage, string>(greetingMessage);
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
