using System.Collections.Generic;
using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Selections;

public abstract class BaseSelection
{
    internal abstract List<string> Selections { get; }
    
    internal BaseSelection(string value)
    {
        var consts = new SelectionConstants();
        var constValue = consts.GetConstByName(value);
        
        Selections.AddRange(constValue);
    }
    
    
    public List<string> GetSelections()
    {
        return Selections;
    }

    public List<string> AddSelection(string value)
    {
        Selections.Add(value);
        return Selections;
    }
}