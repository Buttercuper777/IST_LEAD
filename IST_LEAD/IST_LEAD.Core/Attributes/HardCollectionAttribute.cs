using System;

namespace IST_LEAD.Core.Attributes;

public class HardCollectionAttribute : Attribute
{

    public string CollectionName { get; set; }

    public HardCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }

}