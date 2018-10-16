using System.Threading.Tasks;
using Broker.Samples.Messages;

namespace Broker.Samples.Handlers
{
    public abstract class BaseHandler : IHandle<GreetingMessage>
    {
        public abstract Task Handle(GreetingMessage message);
    }
}
