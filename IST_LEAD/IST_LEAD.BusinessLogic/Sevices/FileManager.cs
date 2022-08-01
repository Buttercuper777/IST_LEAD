using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IST_LEAD.Core;
using IST_LEAD.Core.Models;

using Microsoft.AspNetCore.Http;

namespace IST_LEAD.BusinessLogic.Sevices
{
    public class FileManager : IFileManager
    {

        public string GetFormat(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return extension;
        }
        
        
        public string Base64Decode(string EncodedText) {
            byte[] data = System.Convert.FromBase64String(EncodedText);
            string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }
        
        public string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        
        
        public async Task<FileEntity> WriteFile(IFormFile file)
        {
            throw new NotImplementedException();
        }

    }
}
