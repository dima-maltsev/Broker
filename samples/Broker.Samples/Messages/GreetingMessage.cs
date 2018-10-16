namespace Broker.Samples.Messages
{
    public class GreetingMessage : IAudit
    {
        public string Name { get; set; }

        public string User { get; set; }
    }
}
