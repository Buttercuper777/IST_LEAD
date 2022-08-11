using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public abstract class BaseCollection
{
    
    protected BaseCollection(string id, slugField slug)
    {
        this.Id = id;
        this.Slug = slug;
    }

    public string GetId() => Id;
    public slugField GetSlug() => Slug;
    
    
    private string Id { get; }
    private slugField Slug { get; }
    
    public virtual stringField CategoryName { get; set; }
    
    



}