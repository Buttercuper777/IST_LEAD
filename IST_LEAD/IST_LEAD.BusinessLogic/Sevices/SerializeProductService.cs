using System.Collections.Generic;
using IST_LEAD.Core.Attributes.Handlers;
using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.BusinessLogic.Sevices;

public class SerializeProductService
{
    private Dictionary<string, string> valuesDictionary { get; set; }

    // public string SerializeProduct(List<BaseProduct> itemsList)
    // {
    //     foreach (var product in itemsList)
    //     {
    //         var fieldsSerializer = new FieldsSerializer<BaseProduct>();
    //     }
    // }
}