using IST_LEAD.Integrations.Directus.Models.Fields;

namespace IST_LEAD.Integrations.Directus.Abstract;

public interface IDirectusFieldsManager
{
    public Task<FieldsObject> GetFields(string collection);
    public string GetFieldName(FieldsObject fields);
}