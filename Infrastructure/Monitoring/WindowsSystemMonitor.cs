using ScoutMonitor.Core.Models;
using ScoutMonitor.Core.Services;
using System.Diagnostics;
using System.Management;


namespace ScoutMonitor.Infrastructure.Monitoring
{
    public class WindowsSystemMonitor : ISystemMonitor
    {
        public SystemMetrics GetMetrics()
        {
            var cpu = GetCpuUsage();
            var ramUsed = GetRamUsageInMB();
            var diskUsed = GetDiskUsageInMB();

            return new SystemMetrics
            {
                Cpu = cpu,
                RamUsedMb = ramUsed,
                DiskUsedMb = diskUsed
            };
        }

        private float GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return cpuCounter.NextValue();
        }
        private float GetDiskUsageInMB()
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady && d.Name == "C:\\");
            if (drive == null) return 0;

            long used = drive.TotalSize - drive.TotalFreeSpace;
            return used / (1024f * 1024f);
        }
        private float GetRamUsageInMB()
        {
            float available = new PerformanceCounter("Memory", "Available MBytes").NextValue();
            float total = GetTotalMemoryInMB();
            return total - available;
        }

        private float GetTotalMemoryInMB()
        {
            using var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            foreach (var obj in searcher.Get())
            {
                return Convert.ToSingle(obj["TotalPhysicalMemory"]) / (1024 * 1024);
            }
            return 0;
        }
    }
}
