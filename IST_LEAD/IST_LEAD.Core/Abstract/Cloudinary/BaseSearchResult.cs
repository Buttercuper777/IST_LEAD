using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IST_LEAD.Core.Abstract.Cloudinary;

public abstract class BaseSearchResult
{
    [JsonProperty("resources")]
    public List<BaseSearchResItem> resources { get; set; }
}

public class BaseSearchResItem 
{
    [JsonProperty("filename")]
    public string filename { get; set; }
    
    [JsonProperty("url")]
    public string url { get; set; }
}