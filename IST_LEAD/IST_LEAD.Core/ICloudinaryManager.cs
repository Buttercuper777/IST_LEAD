using IST_LEAD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST_LEAD.Core
{
    public interface ICloudinaryManager
    {
        UploadedFile UploadFile(FileForUpload newFile);
        bool DeleteFile(string pulicId);

    }
}
