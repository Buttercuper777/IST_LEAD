using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Implementation;
using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.Integrations.Directus;

public class DirectusProvider
{
    private string AccessToken { get; }

    public Relations Relations { get; set; }

    public DirectusProvider(string accessToken)
    {
        this.AccessToken = accessToken;
        this.Relations = new Relations(accessToken, "https://admin.istlift.com");
    }
    
    
}