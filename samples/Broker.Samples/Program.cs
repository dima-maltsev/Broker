using System;
using System.Threading.Tasks;
using Autofac;
using Broker.Extensions.Autofac.DependencyInjection;
using Broker.Extensions.Microsoft.DependencyInjection;
using Broker.Samples.Messages;
using Broker.Samples.Pipelines;
using Broker.Samples.Registars;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.Samples
{
    class Program
    {
        static async Task Main()
        {
            var registars = new IRegistar[]
            {
                new ServiceCollectionRegistar(),
                new AutofacRegistar()
            };

            var greetingMessage = new GreetingMessage { Name = "World", User = "User" };

            foreach (var registar in registars)
            {
                Console.WriteLine($"Running {registar.GetType().Name}");
                Console.WriteLine();

                var broker = registar.Register();

                await broker.SendAsync(greetingMessage);
                await broker.PublishAsync(greetingMessage);
                await broker.SendAsync<IAudit>(greetingMessage);

                var result = await broker.SendAsync<GreetingMessage, string>(greetingMessage);
                Console.WriteLine(result);

                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
