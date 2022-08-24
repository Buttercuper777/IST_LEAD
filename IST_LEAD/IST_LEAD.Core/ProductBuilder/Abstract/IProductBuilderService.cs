using System.Collections.Generic;
using System.Linq;
using IST_LEAD.Core.Attributes.Handlers;
using IST_LEAD.Core.Models.ExcelMatchingModels;
using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.Core.ProductBuilder.Abstract;

public interface IProductBuilderService<T> where T : BaseProduct, new()
{
    public GenericProductList<T> HardFieldsActions { get; set; } 
    public CollectionsHandler<T> ProductCollections { get; set; }
    
    
    public List<T> InitProductsList(int size);
    public List<T> GetProducts();

    // public GenericProductList<TObject> InitProductsList<TObject>(int size)
    //     where TObject : BaseProduct, new();

}