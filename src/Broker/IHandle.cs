using System.Threading.Tasks;

namespace Broker
{
    public interface IHandle<in TMessage>
    {
        Task HandleAsync(TMessage message);
    }

    public interface IQuery<in TMessage, TResult>
    {
        Task<TResult> QueryAsync(TMessage message);
    }
}
