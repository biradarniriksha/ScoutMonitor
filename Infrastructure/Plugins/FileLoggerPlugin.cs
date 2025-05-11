using ScoutMonitor.Core.Interfaces;
using ScoutMonitor.Core.Models;

namespace ScoutMonitor.Infrastructure.Plugins
{
    public class FileLoggerPlugin : IMonitorPlugin
    {
        private readonly string _logFilePath = "metrics_log.txt";

        public async Task OnMetricsCollected(SystemMetrics metrics)
        {
            string logEntry = $"{DateTime.Now}: CPU = {metrics.Cpu:F2}% | RAM = {metrics.RamUsedMb:F2} MB | Disk = {metrics.DiskUsedMb:F2} MB";
            await File.AppendAllTextAsync(_logFilePath, logEntry + Environment.NewLine);
        }
    }
}
