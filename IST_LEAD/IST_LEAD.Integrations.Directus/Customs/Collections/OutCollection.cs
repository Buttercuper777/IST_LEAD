using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Integrations.Directus.Customs.Collections;

public class OutCollection : Collection
{
    public OutCollection(int id, slugField slug) : base(id, slug)
    {
        
    }
}