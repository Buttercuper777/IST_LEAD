using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IST_LEAD.Core.Abstract.Services;
using IST_LEAD.Core.Models.Common;
using IST_LEAD.Integrations.Directus.Abstract;
using IST_LEAD.Integrations.Directus.Customs.Products;
using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.BusinessLogic.Sevices;

public class VendCoderService : IVendCoderService
{
    private string GenarateNewVend(int newId, string NewvendName)
    {
        var newNum = newId;
        int numOfChars = 0;
        while(newNum > 0) { numOfChars++; newNum /= 10; }

        var vendName = NewvendName;
        
        for (int i = 0; i < 5 - numOfChars; i++)
        { vendName += "0"; }

        vendName += newId.ToString();

        return vendName;
    }
    
    public TProduct setVendCode<TProduct>(TProduct product, string VendCodeFieldName, int newVendNum)
    {
        var prodType = typeof(TProduct);
        var prodProp = prodType.GetProperty(VendCodeFieldName,
            BindingFlags.Public |
            BindingFlags.DeclaredOnly | 
            BindingFlags.Instance);

        if (prodProp != null)
        {
            var prodPropValue = prodProp.GetValue(product, null);
            if (prodPropValue.GetType() == typeof(string))
            {
                var instance = Activator.CreateInstance(prodProp.PropertyType,
                    GenarateNewVend(newVendNum, "IST").ToCharArray()
                    );

                prodProp.SetValue(product, instance, null);
            }

        }

        return product;
    }
}