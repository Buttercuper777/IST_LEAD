using IST_LEAD.Integrations.Directus.Extensions;

namespace IST_LEAD.Integrations.Directus.Customs.Items;

public class DirectusSendModel 
{
    [DirectusJsonStandIn]
    public string name { get; set; }
    public string slug { get; set; }
}