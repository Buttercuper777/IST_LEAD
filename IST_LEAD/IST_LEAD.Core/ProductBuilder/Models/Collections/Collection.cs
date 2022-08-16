using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public class Collection : BaseCollection
{
    internal override stringField CategoryName { get; set; }
    
    
    public Collection(int id, slugField slug) : base(id, slug){}
    public Collection(string name) : base(name)
    {
        CategoryName = new stringField(name);
    }

    public Collection(int id, slugField slug, string name) : base(id, slug, name)
    {
        CategoryName = new stringField(name);
    }
    
}
