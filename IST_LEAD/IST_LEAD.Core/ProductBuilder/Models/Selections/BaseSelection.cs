using System.Collections.Generic;

namespace IST_LEAD.Core.ProductBuilder.Models.Selections;

public abstract class BaseSelection
{
    private List<string> Selections { get; }

    internal BaseSelection(string value)
    {
        Selections.Add(value);
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