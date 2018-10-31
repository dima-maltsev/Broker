using System;
using System.Threading.Tasks;

namespace Broker
{
    public interface IPipeline<TMessage>
    {
        Task ExecuteAsync(MessageContext<TMessage> context, Func<Task> next);
    }
}
