using System;
using System.Threading.Tasks;

namespace Broker
{
    public abstract class QueryPipeline<TMessage, TResult> : IPipeline<TMessage>
    {
        public Task ExecuteAsync(MessageContext<TMessage> context, Func<Task> next)
        {
            if (!(context is QueryContext<TMessage, TResult> queryContext))
            {
                throw new InvalidOperationException();
            }

            return ExecuteQueryAsync(queryContext, next);
        }

        protected abstract Task ExecuteQueryAsync(QueryContext<TMessage, TResult> context, Func<Task> next);
    }
}
