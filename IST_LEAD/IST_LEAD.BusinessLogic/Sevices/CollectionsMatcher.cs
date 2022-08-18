using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.BusinessLogic.Sevices;

public class CollectionsMatcher
{

    public NamedCollection CollectionsMatching(NamedCollection namedCollection, ItemsObject allCollections)
    {
        var productCollection = namedCollection.GetCollection();
        slugField productCollectionSlug = productCollection.GetSlug();
        
        foreach (var item in allCollections.GetItems())
        {
            if (productCollectionSlug.GetValue() == item.Slug)
            {
                productCollection.setId(item.Id);
                var updatedCollection = new NamedCollection(namedCollection.GetName(), productCollection);
                return updatedCollection;
            }
        }
        
        return namedCollection;
    }
}
    
