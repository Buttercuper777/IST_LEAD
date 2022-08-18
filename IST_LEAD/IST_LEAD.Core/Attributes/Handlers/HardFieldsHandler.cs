using System;
using System.Collections.Generic;
using System.Reflection;
using IST_LEAD.Core.Abstract.Cloudinary;
using IST_LEAD.Core.Models.ExcelMatchingModels;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.Core.Attributes.Handlers;

public class HardFieldsHandler<MapObject> where MapObject :  BaseProduct
{

    private static List<MapObject> ListOfEntities { get; set; }
  

    public HardFieldsHandler(List<MapObject> list)
    {
        ListOfEntities = list;
    }

    public List<MapObject> GetList()
    {
        return ListOfEntities;
    }
    
    private string CheckContains(BaseSearchResult list, string value)
    {
        foreach (var entity in list.resources)
        {
            if (entity.filename.Contains(value))
            {
                return entity.url;
            }
        }

        return null;
    }
    
    public void MapHardFields(ExcelColumnsList list, int numOfItems)
    {
        // var newTItem = new MapObject();

        int index = 0;
        foreach (var entity in ListOfEntities)
        {
            
            var type = typeof(MapObject);       
            var props = type.GetProperties();


            foreach (var prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(HardFieldAttribute)))
                {
                    var fieldName = prop.GetCustomAttribute<HardFieldAttribute>()?.Field;
                    if (fieldName != null && fieldName == list.ColName)
                    {
                        if (prop.CanWrite)
                        {
                            var arg = new Object[] { list.Values[index] };
                            var instance = Activator.CreateInstance(
                                prop.PropertyType, arg
                            );

                            var newField = new object();

                            if (instance != null)
                            {
                                prop.SetValue(entity, instance, null);
                            }
                        }
                        
                    }
                    
                }
                
            }
            index++;
        }
    }

    public List<NamedCollection> GetCollections(MapObject list)
    {
        var resListOfCollections = new List<NamedCollection>();

        string CollectionName = null;
        
            var type = typeof(MapObject);       
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(HardCollectionAttribute)))
                {
                    var propsValue = prop.GetValue(list);
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
    
    public void SetHardImage(List<MapObject> list,  BaseSearchResult images) 
    {
        foreach (var obj in list)
        {
            var type = typeof(MapObject);       
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(HardImageAttribute)))
                {
                    var entityType = obj.GetType();
                    var entityFields = entityType.GetProperties();
                    
                    foreach (var field in entityFields)
                    {
                        if (Attribute.IsDefined(field, typeof(ImageQualifierAttribute)))
                        {
                            var fieldStringValue = field.GetValue(obj);
                            
                            if (fieldStringValue != null)
                            {
                                string fieldValue = null;
                                Type fieldStringType = fieldStringValue.GetType();
                                IList<PropertyInfo> fieldProps = new List<PropertyInfo>(fieldStringType.GetProperties(
                                    BindingFlags.Instance 
                                    | BindingFlags.NonPublic
                                    | BindingFlags.DeclaredOnly));

                                foreach (PropertyInfo fProp in fieldProps)
                                {
                                    object propValue = fProp.GetValue(fieldStringValue, null);
                                    if (propValue != null && (propValue.GetType() == typeof(string)))
                                    {
                                        fieldValue = propValue.ToString();
                                        break;
                                    }
                                }

                                if (fieldValue != null)
                                {
                                    foreach (var image in images.resources)
                                    {
                                        var imageStringFinder = fieldValue.Replace(" ", "");
                                        var url = CheckContains(images, imageStringFinder);
                                        if (url != null)
                                        {
                                            var instance = Activator.CreateInstance(prop.PropertyType, url);
                                            if(instance != null)
                                                prop.SetValue(obj, instance, null);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }

    public void SetCollection(MapObject product, NamedCollection collection)
    {
        var newCollection = collection.GetCollection();
        var newCollectionName = collection.GetName();
        string CollectionName = null;
        
        var type = typeof(MapObject);       
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
    
}

