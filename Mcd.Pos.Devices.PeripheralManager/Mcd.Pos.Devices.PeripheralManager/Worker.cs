using Mcd.Pos.Devices.PeripheralManager.CashDrawer;
using Mcd.Pos.Devices.PeripheralManager.Config;
using Mcd.Pos.Devices.PeripheralManager.ConfigWriter;
using System.Diagnostics;
using System.Reflection;

namespace Mcd.Pos.Devices.PeripheralManager
{
    public class WindowsBackgroundService : BackgroundService
    {
        private const int WAITING_TIME = 15_000; // 5 seconds
        private const string CASH_DRAWER_CONFIG_FILE_LOCATION = "appsettings.json";
        //private const string NPSHARP_PROCESSNAME = "NpSharp.App";
        //private const string NPWEBVIEW_PROCESSNAME = "NPWebView";
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                await StartWorker(scope.ServiceProvider, cancellationToken);
            }
        }

        private async Task StartWorker(IServiceProvider sp, CancellationToken cancellationToken)
        {
            IManagerConfig managerConfig = sp.GetRequiredService<IManagerConfig>();

            // create file based configuration for each of the CashDrawer apps
            var configWriter = new ConfigurationWriter($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{CASH_DRAWER_CONFIG_FILE_LOCATION}");
            configWriter.WriteConfiguration();

            // TODO: Move this to a dictionary of Ids when more CDs are available.
            int id = 0;
            ICashDrawerService cdService = sp.GetRequiredService<ICashDrawerService>();

            if (managerConfig.Enabled)
            {
                id = cdService.StartCashDrawer();


            }

            while (!cancellationToken.IsCancellationRequested)
            {
                var oldValue = managerConfig.Enabled;
                managerConfig = GetConfiguration();

                if (managerConfig.Enabled != oldValue)
                {
                    if (managerConfig.Enabled)
                    {
                        id = cdService.StartCashDrawer();
                        _logger.LogInformation("Cash Drawer Started");
                    }
                    else
                    {
                        cdService.StopCashDrawer(id);
                        _logger.LogInformation("Cash Drawer STopped");
                    }
                }
                Console.WriteLine($"DBG PMAnager: Enabled field for CashDrawer is : {managerConfig.Enabled}");
                _logger.LogInformation($"DBG PMAnager: Enabled field for CashDrawer is : {managerConfig.Enabled}");
                await Task.Delay(WAITING_TIME, cancellationToken);
            }
        }

        private ManagerConfig GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                 //.SetBasePath("E:\\git\\Tremend\\Mcd\\PManager V2\\Mcd.Pos.Devices.PeripheralManager\\Mcd.Pos.Devices.PeripheralManager\\bin\\Debug\\net6.0")
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var loggingConfig = configuration.GetSection("VirtualCashDrawerConfig:ManagerConfig").Get<ManagerConfig>();
            return loggingConfig;
        }
    }
}