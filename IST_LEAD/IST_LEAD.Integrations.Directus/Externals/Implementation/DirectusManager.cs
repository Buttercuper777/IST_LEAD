using IST_LEAD.Core.Abstract;
using IST_LEAD.Integrations.Directus.Customs.Excels;
using IST_LEAD.Integrations.Directus.Externals.Abstract;
using Newtonsoft.Json;

namespace IST_LEAD.Integrations.Directus.Externals.Implementation;

public class DirectusManager : IDirectusManager
{
    public DirectusProvider _provider;
    
    public DirectusManager(DirectusProvider newProvider)
    {
        _provider = newProvider;
    }

    public DirectusProvider GetProvider()
    {
        return _provider;
    }
    
    public T GetExcelLocationsObject<T>(string json, T obj)
    {
        var newExcelLocationsObject = new ExcelLocations();
        
        if (typeof(T).Name == nameof(ExcelLocations))
        {
            obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        return default(T);

    }
}