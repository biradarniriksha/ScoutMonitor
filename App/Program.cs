using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScoutMonitor.Core.Interfaces;
using ScoutMonitor.Core.Services;
using ScoutMonitor.Infrastructure.Monitoring;
using ScoutMonitor.Infrastructure.Plugins;
using ScoutMonitor.App; 

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory) 
    .AddJsonFile("App/appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var appConfig = config.Get<Config>();

var services = new ServiceCollection();

services.AddSingleton(appConfig);
services.AddSingleton<HttpClient>();
services.AddSingleton<IMonitorPlugin, FileLoggerPlugin>();
services.AddSingleton<IMonitorPlugin, ApiPosterPlugin>();
services.AddSingleton<ISystemMonitor, WindowsSystemMonitor>();

var provider = services.BuildServiceProvider();

var plugins = provider.GetServices<IMonitorPlugin>();
var monitor = provider.GetService<ISystemMonitor>();

while (true)
{
    var metrics = monitor?.GetMetrics();
    if (metrics != null)
    {
        foreach (var plugin in plugins)
        {
            await plugin.OnMetricsCollected(metrics);
        }
        Console.WriteLine($"[System Monitor] CPU: {metrics.Cpu:F2}% | RAM: {metrics.RamUsedMb:F2} MB | Disk: {metrics.DiskUsedMb:F2} MB");
    }
    await Task.Delay(TimeSpan.FromSeconds(appConfig.MonitoringIntervalSeconds));
}
