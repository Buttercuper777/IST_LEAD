using System;

namespace IST_LEAD.Core.Attributes;

public class FieldNameForOutAttribute : Attribute
{
    public string Field { get; set; }

    public FieldNameForOutAttribute(string fieldName)
    {
        Field = fieldName;
    }
}