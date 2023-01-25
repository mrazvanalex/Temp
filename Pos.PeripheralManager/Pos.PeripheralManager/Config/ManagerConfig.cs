namespace Pos.PeripheralManager.Config
{
    public class ManagerConfig : IManagerConfig
    {
        public string ConfigurationLocation { get; set; }
        public string ConfigurationName { get; set; }
        public string CashDrawerLocation { get; set; }
        public bool Enabled { get; set; }
    }
}
