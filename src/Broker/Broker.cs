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
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var handler = _factory.GetService<IHandle<TMessage>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"Message {message.GetType()} has no handlers registered");
            }

            var pipelines = _factory.GetServices<IPipeline<TMessage>>();

            var context = new MessageContext<TMessage>(message);

            Task HandlerAction() => handler.HandleAsync(message);

            var runner = pipelines
                .Reverse()
                .Aggregate((Func<Task>) HandlerAction,
                    (next, pipeline) => () => pipeline.ExecuteAsync(context, next));

            await runner().ConfigureAwait(false);
        }

        public async Task PublishAsync<TMessage>(TMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var handlers = _factory.GetServices<IHandle<TMessage>>();
            var pipelines = _factory.GetServices<IPipeline<TMessage>>().Reverse().ToList();

            var context = new MessageContext<TMessage>(message);

            foreach (var handler in handlers)
            {
                Task HandlerAction() => handler.HandleAsync(message);

                var runner = pipelines
                    .Aggregate((Func<Task>) HandlerAction,
                        (next, pipeline) => () => pipeline.ExecuteAsync(context, next));

                await runner().ConfigureAwait(false);
            }
        }

        public async Task<TResult> QueryAsync<TMessage, TResult>(TMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var handler = _factory.GetService<IQuery<TMessage, TResult>>();
            if (handler == null)
            {
                throw new InvalidOperationException($"Message {message.GetType()} has no handlers registered");
            }

            var result = await handler.QueryAsync(message).ConfigureAwait(false);
            return result;
        }
    }
}
