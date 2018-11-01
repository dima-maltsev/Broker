using System.Threading.Tasks;

namespace Broker
{
    public interface IHandle<in TMessage>
    {
        Task HandleAsync(TMessage message);
    }

    public interface IHandle<in TMessage, TResult>
    {
        Task<TResult> HandleAsync(TMessage message);
    }
}
