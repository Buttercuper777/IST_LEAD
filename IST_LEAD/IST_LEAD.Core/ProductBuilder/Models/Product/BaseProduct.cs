using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Core.ProductBuilder.Models.Selections;

namespace IST_LEAD.Core.ProductBuilder.Models.Product;

public abstract class BaseProduct
{
    public virtual Selection TypeOfEquipment { get; set; }
    public virtual urlField ImageUrl { get; set; }
    public virtual slugField Slug { get; set; }
    
    public virtual stringField ProductNameRu { get; set; }
    public virtual stringField ProductName { get; set; }
    public virtual stringField VendCode { get; set; }
    public virtual stringField Weight { get; set; }
    public virtual stringField TextDescription { get; set; }
    public virtual stringField Price { get; set; }
    public virtual stringField Sizes { get; set; }
    
    
    public virtual stringField Analogue { get; set; }
    public virtual stringField Replacement { get; set; }
    public virtual stringField Included { get; set; }
    
    
    public virtual Collection Manufacturer { get; set; }
    public virtual Collection Type { get; set; }
    public virtual Collection Unit { get; set; }

    // public virtual stringField ProductNameRu { get; set; }
    // public virtual stringField Weight { get; set; }
    // public virtual stringField Price { get; set; }
    // public virtual stringField Sizes { get; set; }
    

}