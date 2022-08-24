using System;
using System.Collections.Generic;
using System.Reflection;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.Core.Attributes.Handlers;

public class CollectionsHandler<T> where T : BaseProduct, new()
{
    public List<NamedCollection> GetCollections(T product)
    {
        var resListOfCollections = new List<NamedCollection>();

        string CollectionName = null;
        
            var type = typeof(T);       
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(HardCollectionAttribute)))
                {
                    var propsValue = prop.GetValue(product);
                    var valueType = propsValue.GetType().GetProperty("CategoryName", 
                        BindingFlags.Instance 
                        | BindingFlags.NonPublic
                        | BindingFlags.DeclaredOnly);

                  
                    if (valueType != null && valueType.PropertyType == typeof(stringField))
                    {
                        var nameFromValue = valueType.GetValue(propsValue);
                        if (nameFromValue != null)
                        {
                            var typeOfNameOfValue = nameFromValue.GetType().GetProperty("Field", 
                                BindingFlags.Instance 
                                | BindingFlags.NonPublic
                                | BindingFlags.DeclaredOnly);
                            
                                CollectionName = typeOfNameOfValue.GetValue(nameFromValue).ToString();
                        }
                    }

                    var newCollection = new Collection(
                        slug: new slugField(CollectionName),
                        id: 0,
                        name: CollectionName
                    );
                    
                    resListOfCollections.Add(new NamedCollection(
                            collection: newCollection,
                            name:  prop.GetCustomAttribute<HardCollectionAttribute>()?.CollectionName
                        )
                    );
                }
                else
                    continue;
            }

            return resListOfCollections;
    }
    public void SetCollection(T product, NamedCollection collection)
    {
        var newCollection = collection.GetCollection();
        var newCollectionName = collection.GetName();
        string CollectionName = null;
        
        var type = typeof(T);       
        var props = type.GetProperties();
        
        foreach (var prop in props)
        {
            if (Attribute.IsDefined(prop, typeof(HardCollectionAttribute)))
            {
                var collectionName = prop.GetCustomAttribute<HardCollectionAttribute>()?.CollectionName;
                if (newCollectionName == collectionName)
                {
                    var arg = new Object[]
                    {
                        newCollection.Id,
                        newCollection.Slug,
                        newCollection.CategoryName.GetValue()
                    };
                    
                    var instance = Activator.CreateInstance(
                        prop.PropertyType, arg
                    );
                    
                    if (instance != null)
                    {
                        prop.SetValue(product, instance, null);
                    }
                    
                }
            }
        }
    }

    public NamedCollection CollectionsMatching<InputCollection>
        (T product, NamedCollection collection, List<InputCollection> allCollections)
    where InputCollection : Collection
    {
        var productCollection = collection.GetCollection();
        slugField productCollectionSlug = productCollection.GetSlug();
    
        foreach (var item in allCollections)
        {
            if (productCollectionSlug.GetValue() == item.Slug.GetValue())
            {
                productCollection.setId(item.Id);
                var updatedCollection = new NamedCollection(collection.GetName(), productCollection);
                SetCollection(product, updatedCollection);
                // return updatedCollection;
            }
        }
    
        return collection;
    }

}