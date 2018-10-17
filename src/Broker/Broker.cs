using System;
using System.Linq;
using System.Threading.Tasks;

namespace Broker
{
    public class Broker : IBroker
    {
        private readonly IServiceFactory _factory;

        public Broker(IServiceFactory factory)
        {
            _factory = factory;
        }

        public async Task SendAsync<TMessage>(TMessage message)
        {
            var handler = _factory.GetService<IHandle<TMessage>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"Message {message.GetType()} has no handlers registered");
            }

            var pipelines = _factory.GetServices<IPipeline<TMessage>>();

            Task HandlerAction() => handler.HandleAsync(message);

            var runner = pipelines
                .Reverse()
                .Aggregate((Func<Task>) HandlerAction,
                    (next, pipeline) => () => pipeline.ExecuteAsync(message, next));

            await runner().ConfigureAwait(false);
        }

        public async Task PublishAsync<TMessage>(TMessage message)
        {
            var handlers = _factory.GetServices<IHandle<TMessage>>();
            var pipelines = _factory.GetServices<IPipeline<TMessage>>().Reverse().ToList();

            foreach (var handler in handlers)
            {
                Task HandlerAction() => handler.HandleAsync(message);

                var runner = pipelines
                    .Aggregate((Func<Task>) HandlerAction,
                        (next, pipeline) => () => pipeline.ExecuteAsync(message, next));

                await runner().ConfigureAwait(false);
            }
        }
    }
}
