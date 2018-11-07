using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Broker.Extensions.Autofac.DependencyInjection
{
    public class BrokerAutofacModule : Module
    {
        private readonly Assembly[] _assemblies;

        public BrokerAutofacModule(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterRequiredServices(builder);
            RegisterHandlers(builder, _assemblies);
        }

        private void RegisterRequiredServices(ContainerBuilder builder)
        {
            builder.RegisterType<Broker>().As<IBroker>().InstancePerLifetimeScope();
            builder.RegisterType<ServiceFactory>().As<IServiceFactory>().InstancePerLifetimeScope();
        }

        private void RegisterHandlers(ContainerBuilder builder, Assembly[] assemblies)
        {
            var handlerTypes = new[] { typeof(IHandle<>), typeof(IHandle<,>) };

            foreach (var handlerType in handlerTypes)
            {
                builder.RegisterAssemblyTypes(assemblies)
                    .AsClosedTypesOf(handlerType)
                    .AsImplementedInterfaces();
            }
        }
    }
}
