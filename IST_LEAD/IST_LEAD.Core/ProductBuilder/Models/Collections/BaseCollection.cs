using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public class BaseCollection
{
    
    internal BaseCollection(int id, slugField slug)
    {
        this.Id = id;
        this.Slug = slug;
    }
    
    internal BaseCollection(string name)
    {
        CategoryName = new stringField(name);
    }

    public int GetId() => Id;
    public slugField GetSlug() => Slug;
    
    
    internal int Id { get; }
    internal slugField Slug { get; }
    
    internal virtual stringField CategoryName { get; set; }
    
    



}