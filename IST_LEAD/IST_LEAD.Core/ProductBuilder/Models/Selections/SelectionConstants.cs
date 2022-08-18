using System.Collections.Generic;

namespace IST_LEAD.Core.ProductBuilder.Models.Selections;

public readonly struct SelectionConstants
{
    public Dictionary<string, string> selectionConstants { get; } = null;

    private string escalatorBP { get; } = "escalator";
    private string elevatorBP { get; } = "elevator";

    public SelectionConstants()
    {
        selectionConstants = new Dictionary<string, string>()
        {
            { escalatorBP, "эскалатор" },
            { elevatorBP, "лифт" }
        };
    }

    public List<string> GetConstByName(string name)
    {
        List<string> outArr = new List<string>(){};
        var lowerName = name.ToLower();
        
        foreach (var item in selectionConstants)
        {
            if(item.Value.Contains(lowerName))
                outArr.Add(item.Key);    
        }
        
        return outArr;
    }
}