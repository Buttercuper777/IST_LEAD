using IST_LEAD.Integrations.Directus.Implementation;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models;

public class ItemsObject
{

        public ItemsObject()
        {
            Items = new List<OneItemObject>();
        }

        public void AddRelation(OneItemObject item)
        {
            Items.Add(item);
        }    
    
        [JsonProperty("data")]
        public List<OneItemObject> Items { get; set; } = null!;
    
}