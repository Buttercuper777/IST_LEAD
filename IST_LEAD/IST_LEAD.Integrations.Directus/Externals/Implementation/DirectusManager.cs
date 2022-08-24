using IST_LEAD.Core.Abstract;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Integrations.Directus.Customs.Collections;
using IST_LEAD.Integrations.Directus.Customs.Excels;
using IST_LEAD.Integrations.Directus.Externals.Abstract;
using IST_LEAD.Integrations.Directus.Models;
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

    public async Task<string> GetBaseCollectionByRelated(string relatedCollection)
    {
        var relationWithField = 
           await _provider.Relations.FindRelationWithField(relatedCollection);
        
        var targetRelations = 
           await _provider.Relations.GetRelations(relationWithField.Collection);
        
        var related =
            _provider.Relations.GetRelatedCollection(targetRelations, relatedCollection);

        return related;
    }

    public async Task<List<OutCollection>> GetItemsAsCollections(string itemsSrc)
    {

        var outCollections = new List<OutCollection>() {};
        ItemsObject allItemsOfDirectusCollection = null;
        allItemsOfDirectusCollection = await _provider.Items.GetItems(itemsSrc);

        foreach (var collection in allItemsOfDirectusCollection.GetItems())
        {
            var newItem = new OutCollection(collection.Id, new slugField(collection.Slug));
            outCollections.Add(newItem);
        }

        return outCollections;
    }
    
    public T GetExcelLocationsObject<T>(string json, T obj)
    {
        
        if (typeof(T).Name == nameof(ExcelLocations))
        {
            obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        return default(T);

    }
}