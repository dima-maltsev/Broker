using System;
using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Pipelines
{
    public class GreetingPipeline : IPipeline<GreetingMessage>
    {
        public async Task ExecuteAsync(MessageContext<GreetingMessage> context, Func<Task> next)
        {
            Console.WriteLine("Before greeting");
            await next().ConfigureAwait(false);
            Console.WriteLine("After greeting");
        }
    }
}
