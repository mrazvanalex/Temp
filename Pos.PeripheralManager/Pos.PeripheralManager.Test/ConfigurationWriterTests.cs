using Pos.PeripheralManager.Config;

namespace Pos.PeripheralManager.Test
{
    public class ConfigurationWriterTests
    {
        public const string CONFIG_FILE_LOCATION = "appsettings.json";
        private ConfigurationLoader _configLoader { get; set; }
        [SetUp]
        public void Setup()
        {
            // 
            var configWriter = new ConfigurationWriter.ConfigurationWriter(CONFIG_FILE_LOCATION);
            configWriter.WriteConfiguration();

            _configLoader = new ConfigurationLoader();
            _configLoader.LoadConfigurations(CONFIG_FILE_LOCATION);
        }

        [Test]
        public void ShouldSaveConfigurationToFile()
        {


            Assert.That($"{_configLoader.ManagerConfiguration.ConfigurationLocation}/{_configLoader.ManagerConfiguration.ConfigurationName}", Does.Exist);
        }
    }
}