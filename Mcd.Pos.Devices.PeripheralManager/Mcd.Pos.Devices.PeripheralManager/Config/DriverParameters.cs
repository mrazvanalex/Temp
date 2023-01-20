namespace Mcd.Pos.Devices.PeripheralManager.Config
{
    public class CashDrawerConfig
    {
        public DriverParameters DriverParameters { get; set; }
        public LogConfiguration LogConfiguration { get; set; }
        public AppConfig AppConfig { get; set; }
        public MessageHubConfiguration MessageHubConfiguration {get;set;}
        public HttpCommChannelConfiguration HttpCommChannelConfiguration { get; set; }
    }

    public class DriverParameters
    {
        public string LibraryPath { get; set; }
        public string HostAddress { get; set; }
        public string NodeName { get; set; }
        public string PortNumber { get; set; }
        public string LogicalName { get; set; }
    }

    public class LogConfiguration
    {
        public string LogDirectory { get; set; }
    }

    public class AppConfig
    {
        public string CommChannel { get; set; }
    }

    public class MessageHubConfiguration
    {
        public string SubscribeEndpoint { get; set; }
        public string ListenEndpoint { get; set; }
        public string PublishEndpoint { get; set; }
        public string UnsubscribeEndpoint { get; set; }
        public string Topic { get; set; }
        public string SubTopic { get; set; }
    }

    public class HttpCommChannelConfiguration
    {
        public string BaseUrl { get; set; }
    }
}
