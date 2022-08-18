using System.ComponentModel.DataAnnotations;
using IST_LEAD.Core.Attributes;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.Integrations.Directus.Customs.Products;

public class OneProduct : BaseProduct
{
    
    [Required]
    [FieldNameForOut("slug")]
    [HardField("product_name_ru")]
    public override slugField Slug { get; set; }
    
    [HardField("product_name_ru")]
    public override stringField ProductNameRu { get; set; } = new stringField("Содержимое заполняется администрацией");
    
    [HardField("product_name")]
    public override stringField ProductName { get; set; } = new stringField("Content is filling");
    
    [HardField("vend_code")]
    [ImageQualifier]
    public override stringField VendCode { get; set; } = new stringField("Check by phone");
    
    [HardField("weight")]
    public override stringField Weight { get; set; } = new stringField("Check by phone");
    
    [HardField("product_name_ru")]
    [FieldNameForOut("text_description")]
    public override stringField TextDescription { get; set; } = new stringField("Check by phone");
    
    [HardField("price")]
    public override stringField Price { get; set; } = new stringField("Check by phone");
    
    [HardField("sizes")]
    public override stringField Sizes { get; set; } = new stringField("Check by phone");
    
    [HardField("analogue_text")]
    public override stringField Analogue { get; set; } = new stringField("Check by phone");
    
    [HardField("replacement_text")]
    public override stringField Replacement { get; set; } = new stringField("Check by phone");
    
    [HardField("included_text")]
    public override stringField Included { get; set; } = new stringField("Check by phone");
    
    [HardImage]
    [FieldNameForOut("image_url")]
    public override urlField ImageUrl { get; set; } =
        new urlField("https://res.cloudinary.com/dv9xitsjg/image/upload/v1660219704/Empty_Prod_image_hgbdyr.svg");
    
    
    // [FieldNameForOut("manufacturer_name")]      //  Field name for serialization [Manufacturer -> manufacturer_name]
    [HardCollection("product_manufacturer")]    //  Set name of NamedCollection here
    [HardField("product_manufacturer")]         //  To set value during mapping  [Data from product_manufacturer -> to this]
    public override Collection Manufacturer { get; set; } 
    

    // [FieldNameForOut("type_name")]
    [HardCollection("product_type")]
    [HardField("product_type")]
    public override Collection Type { get; set; }
    

    // [FieldNameForOut("type_name")]
    [HardCollection("product_unit")]
    [HardField("product_unit")]
    public override Collection Unit { get; set; }
}