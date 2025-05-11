namespace ScoutMonitor.App
{
    public class Config
    {
        public int MonitoringIntervalSeconds { get; set; }
        public ApiPluginConfig ApiPlugin { get; set; }
    }
    public class ApiPluginConfig
    {
        public string EndpointUrl { get; set; }
    }
}
