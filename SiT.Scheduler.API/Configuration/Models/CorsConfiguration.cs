namespace SiT.Scheduler.API.Configuration.Models;

using System.Collections.Generic;

public class CorsConfiguration
{
    public IEnumerable<string> AllowedOrigins { get; set; }
    public IEnumerable<string> AllowedHeaders { get; set; }
    public IEnumerable<string> AllowedMethods { get; set; }
}
