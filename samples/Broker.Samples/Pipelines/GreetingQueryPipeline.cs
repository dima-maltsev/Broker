using System;
using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Pipelines
{
    public class GreetingQueryPipeline : IPipeline<GreetingMessage, string>
    {
        public async Task<string> ExecuteAsync(GreetingMessage message, Func<Task<string>> next)
        {
            Console.WriteLine("Before typed");
            var result = await next();
            result += "_Added";
            Console.WriteLine(result);
            Console.WriteLine("After typed");
            return result;
        }
    }
}
