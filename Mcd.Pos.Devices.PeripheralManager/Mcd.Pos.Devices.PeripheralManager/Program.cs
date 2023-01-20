using Mcd.Pos.Devices.PeripheralManager;
using Mcd.Pos.Devices.PeripheralManager.CashDrawer;
using Mcd.Pos.Devices.PeripheralManager.Config;
using Serilog;
using System.Reflection;

var builder = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();
var managerConfig = configuration.GetSection("VirtualCashDrawerConfig:ManagerConfig").Get<ManagerConfig>();
var logConfig = configuration.GetSection("VirtualCashDrawerConfig:Logging").Get<LogConfiguration>();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Pos.Devices.PeripheralManager";
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<ICashDrawerService, CashDrawerService>();
        services.AddHostedService<WindowsBackgroundService>();
        services.AddSingleton<IManagerConfig>(managerConfig);
    })
    .ConfigureLogging((context, logging) =>
    {
        Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.File(logConfig.LogDirectory).WriteTo.EventLog("PeripheralManager", manageEventSource: true)
      .CreateLogger();

        logging.AddSerilog().AddEventSourceLogger().AddEventLog();
        logging.AddConfiguration(
            context.Configuration.GetSection("VirtualCashDrawerConfig:Logging"));
    })
    .Build();

await host.RunAsync();
