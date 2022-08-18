using System;

namespace IST_LEAD.Core.ProductBuilder.Models.Fields;

public sealed class urlField : BaseField
{
    protected override string Field { get; set; }

    public urlField(string field) : base(field)
    {
        this.CreateUrl(field);
    }

    public urlField(urlField item) : base(item) { }
    
    
    
    public override string CreateUrl(string url)
    {
        Uri uriResult = null;
        bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (uriResult != null)
            return uriResult.ToString();
        else
            return null;
    }
}