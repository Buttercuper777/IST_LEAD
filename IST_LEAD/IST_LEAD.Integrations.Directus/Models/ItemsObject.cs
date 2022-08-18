using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Implementation;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Models;

public class ItemsObject : IItemsObject
{

        public ItemsObject()
        {
            Items = new List<OneItemObject>();
        }

        public void AddItem(OneItemObject item)
        {
            Items.Add(item);
        }

        public List<OneItemObject> GetItems()
        {
            return Items;
        }
    
        [JsonProperty("data")]
        public List<OneItemObject> Items { get; set; } = null!;
    
}