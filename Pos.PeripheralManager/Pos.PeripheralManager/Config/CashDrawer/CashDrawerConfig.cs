namespace Pos.PeripheralManager.Config.CashDrawer
{
    public class CashDrawerConfig
    {
        public DriverParameters DriverParameters { get; set; }
        public LogConfiguration LogConfiguration { get; set; }
        public AppConfig AppConfig { get; set; }
        public MessageHubConfiguration MessageHubConfiguration { get; set; }
        public HttpCommChannelConfiguration HttpCommChannelConfiguration { get; set; }
    }
}
