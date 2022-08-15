namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public class NamedCollection
{
    internal string CollectionName { get; set; }
    internal Collection Collection { get; set; }

    public NamedCollection(string name, Collection collection)
    {
        CollectionName = name;
        Collection = collection;
    }
}