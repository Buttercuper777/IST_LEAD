namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public class NamedCollection
{
    private string CollectionName { get;  }
    private Collection Collection { get; set; }

    public NamedCollection(string name, Collection collection)
    {
        CollectionName = name;
        Collection = collection;
    }

    public string GetName() => CollectionName;
    public Collection GetCollection() => Collection;
    public void SetCollection(Collection newCollection)
    {
        Collection = newCollection;
    }
}