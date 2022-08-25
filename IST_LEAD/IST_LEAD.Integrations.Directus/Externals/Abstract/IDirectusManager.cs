using IST_LEAD.Integrations.Directus.Customs.Collections;

namespace IST_LEAD.Integrations.Directus.Externals.Abstract;

public interface IDirectusManager
{
    public excelLocations GetExcelLocationsObject<excelLocations>(string json, excelLocations obj);
    public Task<string> GetBaseCollectionByRelated(string relatedCollection);
    public Task<List<OutCollection>> GetItemsAsCollections(string itemsSrc);
    public string GetDirectusCreationJson<T>(T outModel, string directusJsonPropValue);
    
    
    public DirectusProvider GetProvider();
}