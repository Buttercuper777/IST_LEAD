using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IST_LEAD.LEAD_API.Extensions;

public static class SessionExtensions
{
    public static void SetObjectInSession(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetCustomObjectFromSession<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}