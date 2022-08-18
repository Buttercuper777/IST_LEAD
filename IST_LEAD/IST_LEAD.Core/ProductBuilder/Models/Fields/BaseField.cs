using System.Reflection;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace IST_LEAD.Core.ProductBuilder.Models.Fields;

public abstract class BaseField
{
    protected abstract string Field { get; set; }

    internal BaseField(string field)
    {
        Field = field;
    }

    public BaseField(BaseField item)
    {
        this.Field = item.Field;
    }
    
    public string GetValue()
    {
        return Field;
    }
    
    protected void SetValue(string val)
    {
        Field = val;
    }
    
    public virtual bool AddSlug(string field) => false;
    public virtual string CreateSlug(string field) => null;
    public virtual bool AddUrl(string url) => false;
    public virtual string CreateUrl(string url) => null;
    
    

    
}