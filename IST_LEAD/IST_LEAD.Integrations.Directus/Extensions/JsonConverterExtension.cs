using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IST_LEAD.Integrations.Directus.Extensions;

public class JsonConverterExtension : JsonConverter
{
    private readonly Type[] _types;
    private readonly string _standIn;
    
    public JsonConverterExtension(string standIn, params Type[] types)
    {
        _types = types;
        _standIn = standIn;
    }
    
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JToken t = JToken.FromObject(value);
        if (t.Type != JTokenType.Object)
        {
            t.WriteTo(writer);
        }
        else
        {
            JObject jObject = (JObject)t;

            var valueProps = value.GetType().GetProperties();
            foreach (var prop in valueProps)
            {
                if (Attribute.IsDefined(prop, typeof(DirectusJsonStandInAttribute)))
                {
                    var baseProp = jObject.Properties().FirstOrDefault(p => p.Name == prop.Name);
                    if (baseProp != null)
                    {
                        var newProp = new JProperty(_standIn, baseProp.Value);
                        jObject.Remove(baseProp.Name);
                        jObject.Add(newProp);
                        jObject.WriteTo(writer);
                    }
                }
            }
            
        }
        
    }
    
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
    }
    
    public override bool CanRead
    {
        get { return false; }
    }

    public override bool CanConvert(Type objectType)
    {
        return _types.Any(t => t == objectType);
    }
}

