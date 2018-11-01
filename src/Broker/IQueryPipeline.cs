using System;
using System.Threading.Tasks;

namespace Broker
{
    public interface IQueryPipeline<in TMessage, TResult>
    {
        Task<TResult> ExecuteAsync(TMessage message, Func<Task<TResult>> next);
    }
}