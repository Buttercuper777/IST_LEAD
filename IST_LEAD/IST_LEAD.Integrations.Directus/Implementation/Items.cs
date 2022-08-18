using System.Net.Http.Headers;
using System.Text;
using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Models;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Implementation;

public class Items : IDirectusItemsManager
{
    private string AccessToket { get; }
    private string BaseUrl { get;  }
    private string ItemssPath { get; set; }

    public Items(string accessToket, string baseUrl, string itemsPath="/items/")
    {
        this.AccessToket = accessToket;
        this.BaseUrl = baseUrl;
        this.ItemssPath = itemsPath;
    }


    public async Task<ItemsObject> GetItems(string collection)
    {
        var Items = new ItemsObject();
        
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToket);
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + ItemssPath +collection ))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            Items = JsonConvert.DeserializeObject<ItemsObject>(data);
                        }
                    }
                }
            }
        }
        catch
        {
            Items = null;
        }

        return Items;
    }

    public async Task<int> AddNewItem(string query, string queryPath)
    {

        var newQuery = new StringContent(query, Encoding.UTF8, "application/json");
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", AccessToket);
                
                using (HttpResponseMessage res = await client.
                           PostAsync((BaseUrl + ItemssPath + queryPath), newQuery))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return 0;
                        }
                    }
                }
            }
        }
        catch
        {
            return 0;
        }

        return 0;

    }
}