using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.Extensions.Microsoft.DependencyInjection
{
    internal class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;

        public ServiceFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public T GetService<T>()
        {
            return _provider.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return _provider.GetServices<T>();
        }
    }
}
