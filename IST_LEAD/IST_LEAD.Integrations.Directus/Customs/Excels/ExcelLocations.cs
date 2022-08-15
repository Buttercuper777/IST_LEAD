using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Customs.Excels;

public class ExcelLocations
{
    public string FileId { get; set; }
    public List<Field> Fields { get; set; }
}

public class Field
{
    [JsonProperty("field_name")]
    [JsonPropertyName("field_name")]
    public string FieldName { get; set; }
    public Location location { get; set; }

}

