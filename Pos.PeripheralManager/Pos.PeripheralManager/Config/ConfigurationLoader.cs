using Pos.PeripheralManager.Config.CashDrawer;

namespace Pos.PeripheralManager.Config
{
    public class ConfigurationLoader
    {
        private List<CashDrawerConfig> _cashDrawerConfig;
        private Logging _logging;
        private ManagerConfig _managerconfig;
        public List<CashDrawerConfig> CashDrawerConfig { get => _cashDrawerConfig; }
        public Logging Logging { get => _logging; }
        public ManagerConfig ManagerConfiguration { get => _managerconfig; }


        public void LoadConfigurations(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                throw new Exception($"File not found at {fileLocation}");

            var builder = new ConfigurationBuilder()
                   .AddJsonFile(fileLocation, optional: false);

            IConfiguration config = builder.Build();

            _logging = config.GetSection("VirtualCashDrawerConfig:Logging").Get<Logging>();
            _managerconfig = config.GetSection("VirtualCashDrawerConfig:ManagerConfig").Get<ManagerConfig>();
            _cashDrawerConfig = config.GetSection("VirtualCashDrawerConfig:CashDrawerConfig").Get<List<CashDrawerConfig>>();
        }
    }
}
