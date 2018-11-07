using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Broker.Extensions.Autofac.DependencyInjection
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddBroker(this ContainerBuilder builder) =>
            AddBrokerInternal(builder, AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic));

        public static ContainerBuilder AddBroker(this ContainerBuilder builder, params Assembly[] assemblies) =>
            AddBrokerInternal(builder, assemblies.AsEnumerable());

        public static ContainerBuilder AddBroker(this ContainerBuilder builder, IEnumerable<Assembly> assemblies) =>
            AddBrokerInternal(builder, assemblies);

        private static ContainerBuilder AddBrokerInternal(ContainerBuilder builder, IEnumerable<Assembly> assemblies)
        {
            var assembliesToScan = (assemblies as Assembly[] ?? assemblies).Distinct().ToArray();
            builder.RegisterModule(new BrokerAutofacModule(assembliesToScan));
            return builder;
        }
    }
}
