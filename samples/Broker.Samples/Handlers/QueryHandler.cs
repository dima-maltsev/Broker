using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Handlers
{
    public class QueryHandler : IQuery<GreetingMessage, string>
    {
        public Task<string> QueryAsync(GreetingMessage message)
        {
            return Task.FromResult($"Hello, {message.Name}");
        }
    }
}
