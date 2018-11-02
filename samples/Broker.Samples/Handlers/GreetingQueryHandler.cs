using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Handlers
{
    public class GreetingHandleHandler : IHandle<GreetingMessage, string>
    {
        public Task<string> HandleAsync(GreetingMessage message)
        {
            return Task.FromResult($"Hello, {message.Name}");
        }
    }
}
