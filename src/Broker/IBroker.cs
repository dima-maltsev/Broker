using System.Threading.Tasks;

namespace Broker
{
    public interface IBroker
    {
        Task SendAsync<TMessage>(TMessage message);

        Task PublishAsync<TMessage>(TMessage message);

        Task<TResult> QueryAsync<TMessage, TResult>(TMessage message);
    }
}
