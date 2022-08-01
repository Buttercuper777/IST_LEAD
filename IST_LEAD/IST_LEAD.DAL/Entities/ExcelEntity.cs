using Newtonsoft.Json;

namespace IST_LEAD.DAL.Entities
{
    public class ExcelEntity : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
