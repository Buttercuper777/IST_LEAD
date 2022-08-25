using IST_LEAD.Integrations.Directus.Models;
using IST_LEAD.Integrations.Directus.Models.Items;

namespace IST_LEAD.Integrations.Directus.Abstract;

public interface IDirectusItemsManager
{
    public Task<ItemsObject> GetItems(string collection);

    public Task<int> AddNewItem(string query, string queryPath);
}