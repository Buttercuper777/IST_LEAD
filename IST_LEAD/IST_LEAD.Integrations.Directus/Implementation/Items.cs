using System.Net.Http.Headers;
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
}