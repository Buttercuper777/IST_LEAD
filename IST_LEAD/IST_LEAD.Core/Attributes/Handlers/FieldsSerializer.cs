using System;
using System.Collections.Generic;
using System.Reflection;
using IST_LEAD.Core.ProductBuilder.Models.Product;

namespace IST_LEAD.Core.Attributes.Handlers;

public class FieldsSerializer<SerializeObject, OutputObject> where SerializeObject : BaseProduct
{
    // private Dictionary<string, bool> HaveAttributesDictionary { get; set; }
    private static OutputObject ForOut { get; set; }

    public FieldsSerializer(OutputObject obj)
    {
        ForOut = obj;
    }
    
    
    public OutputObject GetSerializationAttributes(SerializeObject item)
    {
   
        var itemType = typeof(SerializeObject);
        var itemProps = itemType.GetProperties();
        foreach (var prop in itemProps)
        {
            
            string nameForWrite = String.Empty;
            
            if (Attribute.IsDefined(prop, typeof(HardFieldAttribute)) && Attribute.IsDefined(prop, typeof(FieldNameForOutAttribute)))
                nameForWrite = prop.GetCustomAttribute<FieldNameForOutAttribute>()?.Field;
            else
            {
                if (Attribute.IsDefined(prop, typeof(HardFieldAttribute)))
                    nameForWrite = prop.GetCustomAttribute<HardFieldAttribute>()?.Field;
                if (Attribute.IsDefined(prop, typeof(FieldNameForOutAttribute)))
                    nameForWrite = prop.GetCustomAttribute<FieldNameForOutAttribute>()?.Field;
            }

            var outType = ForOut.GetType();
            var outProp = outType.GetProperty(nameForWrite, BindingFlags.Instance 
                                                            | BindingFlags.Public
                                                            | BindingFlags.DeclaredOnly);

            if (!Attribute.IsDefined(prop, typeof(HardCollectionAttribute)))
            {
                if (outProp != null)
                {

                    var propField = prop.GetValue(item, null);
                    var propFieldValue = propField.GetType().GetProperty("Field",
                        BindingFlags.Instance |
                        BindingFlags.DeclaredOnly |
                        BindingFlags.NonPublic);

                    if (propFieldValue != null)
                    {
                        var args = new Object[]
                        {
                            propFieldValue.GetValue(propField, null).ToString().ToCharArray()
                        };

                        var instance = Activator.CreateInstance(outProp.PropertyType, args);
                        if (instance != null)
                            outProp.SetValue(ForOut, instance, null);
                    }
                }
            }
            else
            {
                if (outProp != null)
                {
                    var propField = prop.GetValue(item, null);
                    var propFieldValueId = propField.GetType().GetProperty("Id",
                        BindingFlags.Instance |
                        BindingFlags.NonPublic);
                    
                    var propFieldValueName = propField.GetType().GetProperty("CategoryName",
                        BindingFlags.Instance |
                        BindingFlags.NonPublic);
                    
                    if (propFieldValueId != null && propFieldValueName != null)
                    {

                        var valueIdForAdd = propFieldValueId.GetValue(propField, null);
                        var valueNameForAdd = propFieldValueName.GetValue(propField, null);
                        
                        var IListRef = typeof (List<>);
                        Type[] IListParam = outProp.PropertyType.GetGenericArguments();

                        var ValueFieldForAdd = valueNameForAdd.GetType().GetProperty("Field",
                                BindingFlags.Instance |
                                BindingFlags.DeclaredOnly |
                                BindingFlags.NonPublic)
                            .GetValue(valueNameForAdd, null);
                        
                        var args = new Object[]
                        {
                            valueIdForAdd, 
                            ValueFieldForAdd
                        };

                        var ObjforAdd = Activator.CreateInstance(
                            outProp.PropertyType.GenericTypeArguments[0],
                            args);
                        
                        var Result = 
                            Activator.CreateInstance(IListRef.MakeGenericType(
                            IListParam));
                        
                        Result.GetType().GetMethod("Add").Invoke(Result, new[] {ObjforAdd});
                        
                        if (Result != null)
                            outProp.SetValue(ForOut, Result, null);
                    }
                }
            }

            if (Attribute.IsDefined(prop, typeof(HardSelectionAttribute)))
            {
                if (outProp != null)
                {
                    var propField = prop.GetValue(item, null);
                    var propFieldValue = propField.GetType().GetProperty("Selections",
                        BindingFlags.Instance |
                        BindingFlags.NonPublic);
                    
                    if (propFieldValue != null)
                    {
                        var valueForAdd = propFieldValue.GetValue(propField, null);
                        
                        
                        var IListRef = typeof(List<>);
                        Type[] IListParam = {typeof(string)};          
                        
                        object Result = Activator.CreateInstance(IListRef.MakeGenericType(IListParam));
                        Result.GetType().GetMethod("AddRange").Invoke(Result, new[] {valueForAdd});
                        
                        if (Result != null)
                            outProp.SetValue(ForOut, Result, null);
                    }
                }
            }

        }

        return ForOut;
    }
    
}

