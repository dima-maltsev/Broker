using System;
using System.Threading.Tasks;

namespace Broker
{
    public interface IPipeline<in TMessage>
    {
        Task ExecuteAsync(TMessage message, Func<Task> next);
    }

    public interface IPipeline<in TMessage, TResult>
    {
        Task<TResult> ExecuteAsync(TMessage message, Func<Task<TResult>> next);
    }
}
