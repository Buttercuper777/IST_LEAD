using IST_LEAD.Core.Models;

namespace IST_LEAD.Core.Abstract
{
    public interface ICloudinaryManager
    {
        UploadedFile UploadFile(FileForUpload newFile);
        bool DeleteFile(string pulicId);

        string GetFilesFromFolder(string folder, int maxFileNum);

    }
}
