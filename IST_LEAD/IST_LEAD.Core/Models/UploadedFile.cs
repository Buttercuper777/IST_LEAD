using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST_LEAD.Core.Models
{
    public class UploadedFile : FileEntity
    {
        public override string FileName { get; set; }
        public override string FilePath { get; set; }
        public override string Format { get; set; }
        public override double FileSize { get; set; }
        public bool UploadedResult { get; set; }
    }
}
