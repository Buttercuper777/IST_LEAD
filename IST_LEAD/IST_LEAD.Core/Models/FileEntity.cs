using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST_LEAD.Core.Models
{
    public abstract class FileEntity
    {
        public FileEntity() => DateCreated = DateTime.UtcNow;

        public virtual IFormFile File { get; set; }
        public virtual string FileName { get; set; } 
        public virtual string Format { get; set; } 
        public virtual string FilePath { get; set; }
        public virtual double FileSize  { get; set; }
        public DateTime DateCreated { get; set; }  
    }
}
