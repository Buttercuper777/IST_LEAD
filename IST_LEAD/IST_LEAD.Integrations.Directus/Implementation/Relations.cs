using System.Net.Http.Headers;
using Newtonsoft.Json;
using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Models;
using IST_LEAD.Integrations.Directus.Models.Relations;

namespace IST_LEAD.Integrations.Directus.Implementation;

public class Relations : IDirectusRelationsManager
{
    private string AccessToket { get; }
    private string BaseUrl { get;  }
    private string RealtionsPath { get; set; } 

    public Relations(string accessToken, string baseUrl, string realtionsPath= "/relations/")
    {
        this.AccessToket = accessToken;
        this.BaseUrl = baseUrl;
        this.RealtionsPath = realtionsPath;
    }
    
    public async Task<RelationsObject> GetRelations()
    {
        var Relations = new RelationsObject();
        
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToket);
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + RealtionsPath))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            Relations = JsonConvert.DeserializeObject<RelationsObject>(data);
                        }
                    }
                }
            }
        }
        catch
        {
            Relations = null;
        }

        return Relations;
    }

    public async Task<RelationsObject> GetRelations(string collection)
    {
         var Relations = new RelationsObject();
                
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToket);
                        using (HttpResponseMessage res = await client.GetAsync(BaseUrl + RealtionsPath + collection))
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();
                                if (data != null)
                                {
                                    Relations = JsonConvert.DeserializeObject<RelationsObject>(data);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Relations = null;
                }
        
                return Relations;
    }

    public async Task<OneRelationObject> FindRelationWithField(string field)
    {
        var AllRelations = new RelationsObject();
        var RelationsWithField = new OneRelationObject();
        
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToket);
                using (HttpResponseMessage res = await client.GetAsync(BaseUrl + RealtionsPath))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            AllRelations = JsonConvert.DeserializeObject<RelationsObject>(data);
                            foreach (var relation in AllRelations.Relations)
                            {
                                if (relation.meta.OneField == field)
                                {
                                    RelationsWithField = relation;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            RelationsWithField = null;
        }
        
        return RelationsWithField;
    }

    public string GetRelatedCollection(RelationsObject relations, string relatedItem)
    {

        var OneOfRelations = new OneRelationObject();
        
        if (relations.Relations.Count == 2)
        {
            foreach (var relation in relations.Relations)
            {
                if ((relation.meta.OneField == null ||
                     relation.meta.OneField != relatedItem))
                {
                    OneOfRelations = relation;
                }
                else continue;
            }

            return OneOfRelations.RelatedCollection;

        }

        return null;
    }
}