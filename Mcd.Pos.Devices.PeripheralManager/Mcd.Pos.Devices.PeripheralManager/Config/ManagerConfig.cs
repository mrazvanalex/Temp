namespace Mcd.Pos.Devices.PeripheralManager.Config
{
    public interface IManagerConfig
    {
        public string ConfigurationLocation { get; set; }
        public string ConfigurationName { get; set; }
        public string CashDrawerLocation { get; set; }
        public bool Enabled { get; set; }
    }

    public class ManagerConfig : IManagerConfig
    {
        public string ConfigurationLocation { get; set; }
        public string ConfigurationName { get; set; }
        public string CashDrawerLocation { get; set; }
        public bool Enabled { get; set; }
    }
}
