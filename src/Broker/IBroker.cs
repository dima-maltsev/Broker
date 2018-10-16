using System.Threading.Tasks;

namespace Broker
{
    public interface IBroker
    {
        Task Send<TMessage>(TMessage message);

        Task Publish<TMessage>(TMessage message);
    }
}
