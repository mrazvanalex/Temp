namespace Mcd.Pos.Devices.PeripheralManager.Config
{
    public class Configuration
    {
        public Logging LogConfiguration { get; }
        public DriverParameters DriverParameters { get; }

        public Configuration(Logging lc, DriverParameters dp)
        {
            LogConfiguration = lc;
            DriverParameters = dp;
        }
    }
}
