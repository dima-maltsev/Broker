using System;
using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Pipelines
{
    public class GreetingQueryPipeline : QueryPipeline<GreetingMessage, string>
    {
        protected override async Task ExecuteQueryAsync(QueryContext<GreetingMessage, string> context, Func<Task> next)
        {
            await next();
            context.Result += "_Added";
            Console.WriteLine(context.Result);
        }
    }
}
