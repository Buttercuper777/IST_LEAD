using System.Collections.Generic;
using Newtonsoft.Json;

namespace IST_LEAD.Core.Abstract.Cloudinary;

public abstract class BaseSearchResult
{
    [JsonProperty("resources")]
    public List<searchRes> resources { get; set; }
}
public class searchRes
{
    [JsonProperty("filename")]
    internal string filename { get; set; }
    
    [JsonProperty("url")]
    internal string url { get; set; }
}