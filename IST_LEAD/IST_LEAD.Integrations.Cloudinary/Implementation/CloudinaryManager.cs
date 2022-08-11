using IST_LEAD.Core;
using IST_LEAD.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using IST_LEAD.BusinessLogic.Sevices;
using IST_LEAD.Core.Abstract;

namespace IST_LEAD.Integrations.Cloudinary.Implementation
{
    public class CloudinaryManager : ICloudinaryManager
    {
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        private readonly FileManager _fileManager;
        public CloudinaryManager(CloudinarySettings settings){
            _cloudinarySettings = settings;
            Account account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
            _fileManager = new FileManager();
        }

  
        public UploadedFile UploadFile(FileForUpload newFile)
        {
            var uploadResult = new RawUploadResult();
            string uploadingFileName = String.Empty;


            if(newFile.File.Length > 0) {
            
                uploadingFileName = ($"[{newFile.DateCreated}]_") + newFile.FileFullName;
            
                using (var stream = newFile.File.OpenReadStream()) 
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(newFile.FileFullName, stream),
                        PublicId = "IST_LEAD_EXCEL'S/" + uploadingFileName
                    };
            
                    uploadResult = _cloudinary.Upload(uploadParams);
            
                }
            
            }
       
            try
            {
                var returnFileEntity = new UploadedFile
                {
                    FileName = uploadingFileName,
                    FilePath = uploadResult.Url.ToString(),
                    Format =   _fileManager.GetFormat(newFile.File),
                    FileSize = newFile.File.Length / 1024,
                    UploadedResult = true,
                };
                return returnFileEntity;
            }
            
            catch (Exception ex)
            {
                return new UploadedFile
                {
                    FileName = uploadingFileName,
                    FilePath = ex.ToString(),
                    Format = null,
                    FileSize = 0,
                    UploadedResult = false,
                };
            }
            

        }

        public bool DeleteFile(string pulicId)
        {
            var deletionParams = new DeletionParams("IST_LEAD_EXCEL'S/" + pulicId);
            DeletionResult result = _cloudinary.Destroy(deletionParams);
            return true;
        }

    }
}
