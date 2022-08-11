using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Implementation;
using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.Integrations.Directus;

public class DirectusProvider
{
    private string AccessToken { get; }

    public Relations Relations { get; set; }
    public Items Items { get; set; }

    public DirectusProvider(string accessToken, string basePath = "http://localhost:8055")
    {
        this.AccessToken = accessToken;
        
        this.Relations = new Relations(accessToken, basePath);
        this.Items = new Items(accessToken, basePath);
    }
    
    
}