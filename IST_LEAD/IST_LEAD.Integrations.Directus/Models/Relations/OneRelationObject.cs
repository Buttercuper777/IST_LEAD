using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models.Relations
{
    public class OneRelationObject
    {
        [JsonProperty("collection")]
        public string Collection { get; set; } = null!;
        [JsonProperty("related_collection")]
        public string RelatedCollection { get; set; } = null!;
        public Meta meta {get; set; }
        
    }

    public class Meta
    {
        [JsonProperty("one_field")]
        public string OneField { get; set; } = null!;
    }
}