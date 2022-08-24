using IST_LEAD.Core.Models.Common;
using IST_LEAD.Core.Models.ExcelMatchingModels;

namespace IST_LEAD.Core.Abstract.Services;

public interface IHandleExcelService
{
    public IHandleExcelService Init(string filePath, string fileName);
    public string GetAllExcelColumns();
    public ExcelColumnValues GetColumnValues(Location location, ExcelColumnValues excelColumnValues);
    public void ExcelDelete();
    public int GetNumOfExcelRows();
}