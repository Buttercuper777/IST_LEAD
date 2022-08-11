using IST_LEAD.Core.Models;

namespace IST_LEAD.Core.Abstract
{
    public interface IFileEntityRepository
    {
        int AddNewFileEntity(FileEntity fileEntity);
    }
}
