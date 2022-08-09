using System.Text.Json.Serialization;

namespace IST_LEAD.Integrations.Directus.Models
{
    public class RelationsObject
    {
        [JsonPropertyName("collection")]
        public string Collection { get; set; }
        [JsonPropertyName("related_collection")]
        public string RelatedCollection { get; set; }
        public Meta meta {get; set; }
        
    }

    public class Meta
    {
        [JsonPropertyName("one_field")]
        public string OneField { get; set; }
    }
}