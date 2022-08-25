using IST_LEAD.Integrations.Directus.Models;
using IST_LEAD.Integrations.Directus.Models.Relations;

namespace IST_LEAD.Integrations.Directus.Abstract;

public interface IDirectusRelationsManager
{
    public Task<RelationsObject> GetRelations();
    public Task<RelationsObject> GetRelations(string collection);
        
    public Task<OneRelationObject> FindRelationWithField(string field);
    public string GetRelatedCollection(RelationsObject relations, string relationItem);
}