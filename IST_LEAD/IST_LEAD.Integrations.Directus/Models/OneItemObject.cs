using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models;

public class OneItemObject
{
    [JsonProperty("id" , NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; }
    
    [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
    public string Slug { get; set; }
    
}