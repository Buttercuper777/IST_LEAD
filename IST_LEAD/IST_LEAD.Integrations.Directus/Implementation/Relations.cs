using System.Net.Http.Headers;
using Newtonsoft.Json;
using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.Integrations.Directus.Implementation;

public class Relations : IDirectusRelationsManager
{
    private string AccessToket { get; }
    private string BaseUrl { get;  }
    
    private string _relationsPath = "/relations/";
    
    public Relations(string accessToken, string baseUrl)
    {
        this.AccessToket = accessToken;
        this.BaseUrl = baseUrl;
    }
    
    public async Task<List<RelationsObject>> GetRelations()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToket);
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + _relationsPath))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            
                        }
                    }
                }
            }
        }
        catch
        {
            throw new Exception();
        }

        
        var resp = new List<RelationsObject>();
        return resp;
    }

    public List<RelationsObject> GetRelations(string collection)
    {
        throw new NotImplementedException();
    }

    public List<RelationsObject> FindRelationCollection(string collection)
    {
        throw new NotImplementedException();
    }

    public string GetRelatedCollection(RelationsObject withRelatedCollection, RelationsObject withRelatedField)
    {
        throw new NotImplementedException();
    }
}