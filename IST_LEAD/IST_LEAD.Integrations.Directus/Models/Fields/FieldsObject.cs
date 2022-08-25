using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models.Fields;

public class FieldsObject
{
    [JsonProperty("data")]
    public List<OneFieldObject> fields { get; set; }
}