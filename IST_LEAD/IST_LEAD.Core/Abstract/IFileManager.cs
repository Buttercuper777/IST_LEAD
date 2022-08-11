using System.Threading.Tasks;
using IST_LEAD.Core.Models;
using Microsoft.AspNetCore.Http;

namespace IST_LEAD.Core.Abstract
{
    public interface IFileManager
    {

        public Task<FileEntity> WriteFile(IFormFile file);
        public string GetFormat(IFormFile file);
        public string Base64Encode(string plainText);
        public string Base64Decode(string EncodedText);
    }
}
