namespace IST_LEAD.Integrations.Directus.Abstract;

public abstract class IOneItemObject
{
    public abstract int Id { get; set; }
    public abstract string Slug { get; set;  }
    public abstract string Name { get; set;  }
}