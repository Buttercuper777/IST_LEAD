using System.Collections.Generic;
using IST_LEAD.Core.Models.Common;

namespace IST_LEAD.Core.Models.ExcelMatchingModels;

public class ExcelColumnsList
{
    public string ColName { get; set; }
    public List<string> Values { get; set; }
}

public class Values
{
    public string Value { get; set; }
}