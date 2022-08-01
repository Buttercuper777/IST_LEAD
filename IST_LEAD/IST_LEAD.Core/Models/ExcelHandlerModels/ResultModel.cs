using System;
using System.Collections.Generic;

namespace IST_LEAD.Core.Models.ExcelHandlerModels
{
    public class ResultModel
    {
        public ResultModel()
        {
            missings = false;
            columns = new List<Columns>();
        }
        
        public bool missings { get; set; }
        public List<Columns> columns { get; set; }
    }
}