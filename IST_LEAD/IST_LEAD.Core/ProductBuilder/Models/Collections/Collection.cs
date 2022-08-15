using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public class Collection : BaseCollection
{
    public Collection(int id, slugField slug) : base(id, slug){}

    public Collection(string name) : base(name)
    {
        CategoryName = new stringField(name);
    }
    
    internal override stringField CategoryName { get; set; }
}
