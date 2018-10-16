using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBroker(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic);

            return services.AddBroker(assemblies);
        }

        public static IServiceCollection AddBroker(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddBroker(assemblies.AsEnumerable());
        }

        public static IServiceCollection AddBroker(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            RegisterRequiredServices(services);
            RegisterHandlers(services, assemblies);
            return services;
        }

        private static void RegisterRequiredServices(IServiceCollection services)
        {
            services.AddScoped<IServiceFactory>(p => new ServiceFactory(p));
            services.AddScoped<IBroker, Broker>();
        }

        private static void RegisterHandlers(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var descriptors =
                from a in assemblies
                from t in a.DefinedTypes
                where t.IsClass && !t.IsAbstract
                from i in t.ImplementedInterfaces
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>)
                select new { ServiceType = i, ImplementationType = t };

            foreach (var descriptor in descriptors)
            {
                services.AddTransient(descriptor.ServiceType, descriptor.ImplementationType);
            }
        }
    }
}
