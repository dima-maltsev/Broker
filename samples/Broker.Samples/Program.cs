using System;
using Broker.Extensions.Microsoft.DependencyInjection;
using Broker.Samples.Messages;
using Broker.Samples.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.Samples
{
    class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddBroker();
            services.AddTransient(typeof(IPipeline<>), typeof(GenericPipeline<>));
            services.AddTransient(typeof(IPipeline<GreetingMessage>), typeof(GreetingPipeline));

            var provider = services.BuildServiceProvider();
            var broker = provider.GetService<IBroker>();

            var greetingMessage = new GreetingMessage { Name = "World", User = "User" };

            broker.SendAsync(greetingMessage);
            broker.PublishAsync(greetingMessage);
            broker.SendAsync<IAudit>(greetingMessage);

            Console.ReadKey();
        }
    }
}
