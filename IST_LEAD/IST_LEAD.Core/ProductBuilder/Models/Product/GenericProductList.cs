using System;
using System.Collections.Generic;
using IST_LEAD.Core.Abstract.Cloudinary;
using IST_LEAD.Core.Attributes.Handlers;
using IST_LEAD.Core.Models.ExcelMatchingModels;
using IST_LEAD.Core.ProductBuilder.Models.Collections;

namespace IST_LEAD.Core.ProductBuilder.Models.Product;

public class GenericProductList<T> where T : BaseProduct
{
    private static List<T> Products { get; set; }
    private HardFieldsHandler<T> AttributesHandler { get; set; }

    
    public GenericProductList(List<T> initList)
    {
        Products = initList;
        AttributesHandler = new HardFieldsHandler<T>();
    }
    
    public List<T> SetHardAttributes(List<ExcelColumnValues> src)
    {
        foreach (var column in src)
        {
            AttributesHandler.MapHardFields(column, Products);
        }
        
        return Products;
    }

    public List<T> ApplyImageQualifierAttributes(BaseSearchResult newImages)
    {
        AttributesHandler.SetHardImage(newImages, Products);
        return Products;
    }
    
}