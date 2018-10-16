using System.Collections.Generic;

namespace Broker
{
    public interface IServiceFactory
    {
        T GetService<T>();

        IEnumerable<T> GetServices<T>();
    }
}
