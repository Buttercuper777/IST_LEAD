using IST_LEAD.Core.Models.Common;

namespace IST_LEAD.Integrations.Directus.Customs.Products;

public class SerializationProduct
{
    public List<string> type_of_equipment { get; set; }

    public string slug { get; set; }
    public string product_name_ru { get; set; }
    public string product_name { get; set; }
    public string weight { get; set; }
    public string text_description { get; set; }
    public string price { get; set; }
    public string sizes { get; set; }
    public string analogue_text { get; set; }
    public string replacement_text { get; set; }
    public string included_text { get; set; }
    public string image_url { get; set; }

    public List<SerializationCollection> product_manufacturer { get; set; }
    public List<SerializationCollection> product_type { get; set; }
    public List<SerializationCollection> product_unit { get; set; }

    public string vend_code { get; set; }
    
}

public class SerializationCollection
{

    public SerializationCollection(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
}