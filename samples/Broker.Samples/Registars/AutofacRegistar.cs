using Autofac;
using Broker.Extensions.Autofac.DependencyInjection;
using Broker.Samples.Messages;
using Broker.Samples.Pipelines;

namespace Broker.Samples.Registars
{
    internal class AutofacRegistar : IRegistar
    {
        public IBroker Register()
        {
            var builder = new ContainerBuilder();
            builder.AddBroker();
            builder.RegisterGeneric(typeof(GenericPipeline<>)).As(typeof(IPipeline<>));
            builder.RegisterType(typeof(GreetingPipeline)).As(typeof(IPipeline<GreetingMessage>));
            builder.RegisterGeneric(typeof(GenericQueryPipeline<,>)).As(typeof(IPipeline<,>));
            builder.RegisterType(typeof(GreetingQueryPipeline)).As(typeof(IPipeline<GreetingMessage, string>));

            var container = builder.Build();
            var broker = container.Resolve<IBroker>();

            return broker;
        }
    }
}
