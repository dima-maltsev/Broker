namespace Broker
{
    public class MessageContext<TMessage>
    {
        public MessageContext(TMessage message)
        {
            Message = message;
        }

        public TMessage Message { get; }
    }

    public class QueryContext<TMessage, TResult> : MessageContext<TMessage>
    {
        public QueryContext(TMessage message) : base(message)
        {
        }

        public TResult Result { get; set; }
    }
}
