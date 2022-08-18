namespace IST_LEAD.Integrations.Directus.Abstract;

public abstract class IItemsObject
{
    public List<IOneItemObject> Items { get; set; } = null!;
}