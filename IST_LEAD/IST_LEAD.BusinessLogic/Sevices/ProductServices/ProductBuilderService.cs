using System;
using System.Collections.Generic;
using System.Linq;
using IST_LEAD.Core.Abstract;
using IST_LEAD.Core.Attributes.Handlers;
using IST_LEAD.Core.Models.ExcelMatchingModels;
using IST_LEAD.Core.ProductBuilder.Abstract;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Product;
using IST_LEAD.Integrations.Directus.Customs.Products;

namespace IST_LEAD.BusinessLogic.Sevices.ProductServices;

public class ProductBuilderService<T> : IProductBuilderService<T> 
    where T : BaseProduct, new()
{
    private static List<T> ProductList { get; set; }

    public GenericProductList<T> HardFieldsActions { get; set; }
    public CollectionsHandler<T> ProductCollections { get; set; }

    public List<T> InitProductsList(int size)
    {
        ProductList = Enumerable.Range(0, size)
            .Select(_ => new T()).ToList();

        HardFieldsActions = new GenericProductList<T>(ProductList);
        ProductCollections = new CollectionsHandler<T>();
        
        return ProductList;
    }

    public List<T> GetProducts() => ProductList;
    
}