using System.Text.Json.Serialization;
using IST_LEAD.Integrations.Directus.Abstract;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models;

public class OneItemObject : IOneItemObject
{
    [JsonProperty("id" , NullValueHandling = NullValueHandling.Ignore)]
    public override int Id { get; set; }
    
    [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
    public override string Slug { get; set; }
    
}