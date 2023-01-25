namespace Pos.PeripheralManager.Config.CashDrawer
{
    public class MessageHubConfiguration
    {
        public string SubscribeEndpoint { get; set; }
        public string ListenEndpoint { get; set; }
        public string PublishEndpoint { get; set; }
        public string UnsubscribeEndpoint { get; set; }
        public string Topic { get; set; }
        public string SubTopic { get; set; }
    }
}
