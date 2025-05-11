using ScoutMonitor.Core.Models;

namespace ScoutMonitor.Core.Interfaces
{
    public interface IMonitorPlugin
    {
        Task OnMetricsCollected(SystemMetrics metrics);
    }
}
