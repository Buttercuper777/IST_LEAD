using System.Reflection;

namespace IST_LEAD.Core.ProductBuilder.Models.Fields;

public class stringField : BaseField
{
    protected override string Field { get; set; }
    // internal string sField { get; set; }

    public stringField(string value) : base(value)
    {
        // sField = value;
    }
    
}