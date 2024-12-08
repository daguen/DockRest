using Newtonsoft.Json;

namespace DockRest.Core.Models;

public class ContainerRequest
{
    [JsonProperty(PropertyName = "command")]
    public string Command { get; set; }
}
