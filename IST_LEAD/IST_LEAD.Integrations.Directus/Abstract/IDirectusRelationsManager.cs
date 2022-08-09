using IST_LEAD.Integrations.Directus.Models;

namespace IST_LEAD.Integrations.Directus.Abstract;

public interface IDirectusRelationsManager
{
    public Task<List<RelationsObject>> GetRelations();
    public List<RelationsObject> GetRelations(string collection);
        
    public List<RelationsObject> FindRelationCollection(string collection);
    public string GetRelatedCollection(RelationsObject withRelatedCollection, RelationsObject withRelatedField);
}