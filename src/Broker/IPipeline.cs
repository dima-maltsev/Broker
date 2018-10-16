using System;
using System.Threading.Tasks;

namespace Broker
{
    public interface IPipeline<in TMessage>
    {
        Task Execute(TMessage message, Func<Task> next);
    }
}
