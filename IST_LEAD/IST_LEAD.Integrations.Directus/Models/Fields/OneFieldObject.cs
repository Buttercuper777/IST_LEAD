using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models.Fields;

public class OneFieldObject
{
    public string collection { get; set; }
    
    [JsonProperty("meta")]
    public MetaData meta { get; set; } 
}

