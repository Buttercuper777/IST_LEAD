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
    
    public void MapHardFields(ExcelColumnValues list, List<MapObject> products)
    {
        // var newTItem = new MapObject();

        int index = 0;
        foreach (var entity in products)
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
    
    public void SetHardImage(BaseSearchResult images, List<MapObject> list)
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
    
}
