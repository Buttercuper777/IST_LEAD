using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models.Fields;

public class MetaData
{
    [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
    public string? note { get; set; }
    
    [JsonProperty("field")]
    public string? field { get; set; }

}