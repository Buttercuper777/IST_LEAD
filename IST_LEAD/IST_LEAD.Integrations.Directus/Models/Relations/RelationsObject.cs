using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models.Relations;

public class RelationsObject
{
    public RelationsObject()
    {
        Relations = new List<OneRelationObject>();
    }

    public void AddRelation(OneRelationObject relation)
    {
        Relations.Add(relation);
    }    
    
    [JsonProperty("data")]
    public List<OneRelationObject> Relations { get; set; } = null!;
}