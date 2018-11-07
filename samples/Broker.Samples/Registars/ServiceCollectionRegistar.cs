using Broker.Extensions.Microsoft.DependencyInjection;
using Broker.Samples.Messages;
using Broker.Samples.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.Samples.Registars
{
    internal class ServiceCollectionRegistar : IRegistar
    {
        public IBroker Register()
        {
            var services = new ServiceCollection();
            services.AddBroker();
            services.AddTransient(typeof(IPipeline<>), typeof(GenericPipeline<>));
            services.AddTransient(typeof(IPipeline<GreetingMessage>), typeof(GreetingPipeline));
            services.AddTransient(typeof(IPipeline<,>), typeof(GenericQueryPipeline<,>));
            services.AddTransient(typeof(IPipeline<GreetingMessage, string>), typeof(GreetingQueryPipeline));

            var provider = services.BuildServiceProvider();
            var broker = provider.GetService<IBroker>();

            return broker;
        }
    }
}
