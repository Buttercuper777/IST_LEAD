namespace IST_LEAD.Integrations.Directus.Externals.Abstract;

public interface IDirectusManager
{
    public excelLocations GetExcelLocationsObject<excelLocations>(string json, excelLocations obj);
}