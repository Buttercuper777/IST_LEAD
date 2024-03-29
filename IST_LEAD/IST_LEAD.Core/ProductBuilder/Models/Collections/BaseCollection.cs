﻿using IST_LEAD.Core.ProductBuilder.Models.Fields;

namespace IST_LEAD.Core.ProductBuilder.Models.Collections;

public class BaseCollection
{
    internal int Id { get; set; }
    internal slugField Slug { get; }
    internal virtual stringField CategoryName { get; set; }
    
    
    internal BaseCollection(int id, slugField slug)
    {
        this.Id = id;
        this.Slug = slug;
    }
    internal BaseCollection(string name)
    {
        CategoryName = new stringField(name);
    }
    public BaseCollection(int id, slugField slug, string name)
    {
        this.Id = id;
        this.Slug = slug;
        this.CategoryName = new stringField(name);
    }

    public int GetId() => Id;
    public slugField GetSlug() => Slug;

    public void setId(int id)
    {
        Id = id;
    }
    
    

    
    



}