using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST_LEAD.Core.Models
{
    public class FileForUpload : FileEntity
    {
        
        public override IFormFile File { get; set; }
        public string FileFullName { get; set; }

    }
}
