using System.Collections.Generic;
using Autofac;

namespace Broker.Extensions.Autofac.DependencyInjection
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IComponentContext _context;

        public ServiceFactory(IComponentContext context)
        {
            _context = context;
        }

        public T GetService<T>()
        {
            return _context.Resolve<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return _context.Resolve<IEnumerable<T>>();
        }
    }
}
