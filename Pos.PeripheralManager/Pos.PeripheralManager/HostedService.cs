using Pos.PeripheralManager.Config;

namespace Pos.PeripheralManager
{
    public class HostedService : BackgroundService
    {
        private const int WAITING_TIME = 15_000; // 5 seconds
        private const string CONFIG_FILE_LOCATION = "appsettings.json";
        private const string NPSHARP_PROCESSNAME = "NpSharp.App";
        private const string NPWEBVIEW_PROCESSNAME = "NPWebView";
        private readonly IServiceProvider _serviceProvider;

        public HostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                await StartWorker(scope, cancellationToken);
            }
        }

        private async Task StartWorker(IServiceScope scope, CancellationToken cancellationToken)
        {

            IManagerConfig managerConfig = scope.ServiceProvider.GetRequiredService<IManagerConfig>();

            // create file based configuration
            var configWriter = new ConfigurationWriter.ConfigurationWriter(CONFIG_FILE_LOCATION);
            configWriter.WriteConfiguration();

            // TODO: Move this to a dictionary of Ids when more CDs are available.
            int id = 0;
            ICashDrawerService cdService = scope.ServiceProvider.GetRequiredService<ICashDrawerService>();

            if (managerConfig.Enabled)
            {
                id = cdService.StartCashDrawer();
            }
            var isFirstRun = true;
            while (!cancellationToken.IsCancellationRequested)
            {
                var oldValue = managerConfig.Enabled;
                managerConfig = GetConfiguration();

                if (managerConfig.Enabled != oldValue || isFirstRun)
                {
                    if (managerConfig.Enabled)
                    {
                        id = cdService.StartCashDrawer();
                    }
                    else
                    {
                        cdService.StopCashDrawer(id);
                    }
                    isFirstRun = false;
                }
                Console.WriteLine($"DBG PMAnager: Enabled field for CashDrawer is : {managerConfig.Enabled}");
                await Task.Delay(WAITING_TIME, cancellationToken);
            }
        }

        private ManagerConfig GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var loggingConfig = configuration.GetSection("VirtualCashDrawerConfig:ManagerConfig").Get<ManagerConfig>();
            return loggingConfig;
        }
    }
}
