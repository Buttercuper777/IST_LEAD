using System.Collections.Generic;

namespace IST_LEAD.Core.ProductBuilder.Models.Selections;

public class Selection : BaseSelection
{
    internal override List<string> Selections { get; } = new List<string>() { };
    
    public Selection(string value) : base(value) { }
}