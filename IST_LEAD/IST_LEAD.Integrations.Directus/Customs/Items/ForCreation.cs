using IST_LEAD.Integrations.Directus.Models;
using IST_LEAD.Integrations.Directus.Models.Items;

namespace IST_LEAD.Integrations.Directus.Customs.Items;

public class ForCreation
{
    public string creationType { get; set; }
    public ItemsObject data { get; set; }
}

