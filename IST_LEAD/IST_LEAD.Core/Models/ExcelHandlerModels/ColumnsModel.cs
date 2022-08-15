using System.Collections.Generic;
using IST_LEAD.Core.Models.Common;

namespace IST_LEAD.Core.Models.ExcelHandlerModels
{
    public class Columns
    {
        public string title { get; set; }
        public Location Location { get; set; }
        public List<string> Helpers { get; set; }
        
    }
}