using System;

namespace IST_LEAD.Core.ProductBuilder.Models.Fields;

public class urlField : BaseField
{
    internal urlField(string field) : base(field) { }
    
    
    public override bool AddUrl(string url)
    {
        Uri uriResult = null;
        bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (result && uriResult != null )
        {
            this.SetValue(uriResult.ToString());
        }

        return result;
    }

    
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