using System;

namespace IST_LEAD.Integrations.Cloudinary
{
    public class CloudinarySettings
    {
        public string ApiKey { get; }
        public string ApiSecret { get; }
        public string CloudName{ get; }
        
        public CloudinarySettings(string key, string secret, string name)
        {
            this.ApiKey = key;
            this.ApiSecret = secret;
            this.CloudName = name ;
        }

    }
}
