using System;
using System.Threading.Tasks;

namespace Broker.Samples.Pipelines
{
    public class GenericQueryPipeline<TMessage, TResult> : IPipeline<TMessage, TResult>
    {
        public async Task<TResult> ExecuteAsync(TMessage message, Func<Task<TResult>> next)
        {
            Console.WriteLine("Before generic");
            var result = await next();
            Console.WriteLine("After generic");
            return result;
        }
    }
}
