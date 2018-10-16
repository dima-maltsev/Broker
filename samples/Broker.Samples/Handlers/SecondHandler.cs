using System;
using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Handlers
{
    public class SecondHandler : IHandle<GreetingMessage>
    {
        public Task Handle(GreetingMessage message)
        {
            Console.WriteLine("Second");
            return Task.CompletedTask;
        }
    }
}
