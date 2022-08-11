using System.ComponentModel.DataAnnotations;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Product;

public class Product : BaseProduct
{
    public override urlField ImageUrl { get; set; } =
        new urlField("https://res.cloudinary.com/dv9xitsjg/image/upload/v1660219704/Empty_Prod_image_hgbdyr.svg");
    
    [Required]
    public override slugField Slug { get; set; }
    
    public override stringField ProductNameRu { get; set; } = new stringField("Содержимое заполняется администрацией");
    public override stringField ProductName { get; set; } = new stringField("Content is filling");
    public override stringField VendCode { get; set; } = new stringField("Check by phone");
    public override stringField Weight { get; set; } = new stringField("Check by phone");
    public override stringField TextDescription { get; set; } = new stringField("Check by phone");
    public override stringField Price { get; set; } = new stringField("Check by phone");
    public override stringField Sizes { get; set; } = new stringField("Check by phone");
    
    public override stringField Analogue { get; set; } = new stringField("Check by phone");
    public override stringField Replacement { get; set; } = new stringField("Check by phone");
    public override stringField Included { get; set; } = new stringField("Check by phone");
    
    [Required]
    public override Collection Manufacturer { get; set; }
    [Required]
    public override Collection Type { get; set; }
    [Required]
    public override Collection Unit { get; set; }
}