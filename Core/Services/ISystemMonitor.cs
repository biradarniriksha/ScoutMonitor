using ScoutMonitor.Core.Models;

namespace ScoutMonitor.Core.Services
{
    public interface ISystemMonitor
    {
        SystemMetrics GetMetrics();
    }
}
