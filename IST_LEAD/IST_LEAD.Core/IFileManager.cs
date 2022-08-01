using IST_LEAD.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST_LEAD.Core
{
    public interface IFileManager
    {

        public Task<FileEntity> WriteFile(IFormFile file);
        public string GetFormat(IFormFile file);
        public string Base64Encode(string plainText);
        public string Base64Decode(string EncodedText);
    }
}
