namespace IST_LEAD.Integrations.Directus.Models.Customs;

public class ExcelData
{
    public string ColName { get; set; }
    public List<Values> Values { get; set; }
}

public class Values
{
    public string value { get; set; }
    public Location location { get; set; }
}