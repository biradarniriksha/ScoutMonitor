using ScoutMonitor.App;
using ScoutMonitor.Core.Interfaces;
using ScoutMonitor.Core.Models;
using System.Text;
using System.Text.Json;

namespace ScoutMonitor.Infrastructure.Plugins
{
    public class ApiPosterPlugin : IMonitorPlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public ApiPosterPlugin(HttpClient httpClient, Config config)
        {
            _httpClient = httpClient;
            _endpointUrl = config.ApiPlugin.EndpointUrl;
        }

        public async Task OnMetricsCollected(SystemMetrics metrics)
        {
            var payload = new
            {
                cpu = metrics.Cpu,
                ram_used = metrics.RamUsedMb,
                disk_used = metrics.DiskUsedMb
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(_endpointUrl, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API Plugin] Failed to POST metrics: {ex.Message}");
            }
        }
    }
}
