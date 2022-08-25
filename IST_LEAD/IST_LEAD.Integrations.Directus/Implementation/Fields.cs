using System.Net.Http.Headers;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Models.Fields;
using IST_LEAD.Integrations.Directus.Models.Items;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Implementation;

public class Fields 
    : IDirectusFieldsManager
{
    private string AccessToket { get; }
    private string BaseUrl { get;  }
    private string ItemssPath { get; set; }
    
    public Fields(string accessToket, string baseUrl, string itemsPath="/fields/")
    {
        this.AccessToket = accessToket;
        this.BaseUrl = baseUrl;
        this.ItemssPath = itemsPath;
    }
    
    public async Task<FieldsObject> GetFields(string collection)
    {
        var Items = new FieldsObject();
        
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
                            Items = JsonConvert.DeserializeObject<FieldsObject>(data);
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

    public string GetFieldName(FieldsObject fields)
    {
        foreach (var field in fields.fields)
        {
            if (field.meta.note == "CREATION_NAME")
                return field.meta != null ? field.meta.field : null;
        }
        
        return null;
    }
    
}