using System.Reflection;

using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.Integrations.Directus.Models.Customs;

public class OneProduct
{
    
    public string ImageUrl { get; set; }
    
    public string Slug { get; set; }
    
    public string ProductNameRu { get; set; } 
    public string ProductName { get; set; } 
    public string VendCode { get; set; } 
    public string Weight { get; set; } 
    public string TextDescription { get; set; } 
    public string Price { get; set; } 
    public string Sizes { get; set; } 
    
    public string Analogue { get; set; } 
    public string Replacement { get; set; } 
    public string Included { get; set; }

    public int[] Manufacturer { get; set; } = { 1 };

    public int[] Type { get; set; } = { 1 };

    public int[] Unit { get; set; } = { 1 };
}