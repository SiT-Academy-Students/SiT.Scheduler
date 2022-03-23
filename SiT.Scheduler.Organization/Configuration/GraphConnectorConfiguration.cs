namespace SiT.Scheduler.Organization.Configuration;

public class GraphConnectorConfiguration
{
    public const string Section = "GraphConnector";

    public string TenantId { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }
}