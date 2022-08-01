using System;

namespace IST_LEAD.Core.Models.ExcelHandlerModels
{
    public abstract class ResultModel
    {
        public bool missings { get; set; }
        public Columns[] columns { get; set; }
    }
}