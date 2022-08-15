using System;

namespace IST_LEAD.Core.Attributes;

public class HardFieldAttribute : Attribute
{
    public string Field { get; set; }
    public HardFieldAttribute(string field)
    {
        Field = field;
    }
    
    
}