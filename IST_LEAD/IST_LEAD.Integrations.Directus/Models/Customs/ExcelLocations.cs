using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models.Customs;

public class ExcelLocations
{
    public string FileId { get; set; }
    public List<Field> Fields { get; set; }
}

public class Field
{
    [JsonProperty("field_name")]
    public string FieldName { get; set; }
    public Location location { get; set; }

}

