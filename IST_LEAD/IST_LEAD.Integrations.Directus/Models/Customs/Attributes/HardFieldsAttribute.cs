namespace IST_LEAD.Integrations.Directus.Models.Customs.Attributes;

public class HardFieldsAttribute : Attribute
{
    public string Field { get; set; }
    
    public HardFieldsAttribute(string field)
    {
        Field = field;
    }
    
}