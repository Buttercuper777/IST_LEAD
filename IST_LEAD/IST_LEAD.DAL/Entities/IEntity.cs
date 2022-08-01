using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace IST_LEAD.DAL.Entities
{
    public interface IEntity
    {
        [Key]
        [JsonIgnore]
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime DateCreated { get; set; }
        
    }
}
