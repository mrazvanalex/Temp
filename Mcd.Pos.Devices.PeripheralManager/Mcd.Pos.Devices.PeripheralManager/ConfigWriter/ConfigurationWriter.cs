using Mcd.Pos.Devices.PeripheralManager.Config;
using System.Text.Json;

namespace Mcd.Pos.Devices.PeripheralManager.ConfigWriter
{
    public class ConfigurationWriter
    {
        private string _configLocation { get; set; }
        public ConfigurationWriter(string configLocation)
        {
            _configLocation = configLocation;
        }

        public void WriteConfiguration()
        {
            ConfigurationLoader configurationLoader = new ConfigurationLoader();
            configurationLoader.LoadConfigurations(_configLocation);

            var config = configurationLoader.CashDrawerConfig;
            var jsonConfig = JsonSerializer.Serialize(config);

            FileInfo file = new FileInfo($"{configurationLoader.ManagerConfiguration.ConfigurationLocation}/{configurationLoader.ManagerConfiguration.ConfigurationName}");
            file.Directory.Create();
            File.WriteAllText($"{configurationLoader.ManagerConfiguration.ConfigurationLocation}/{configurationLoader.ManagerConfiguration.ConfigurationName}", jsonConfig);
        }
    }
}
