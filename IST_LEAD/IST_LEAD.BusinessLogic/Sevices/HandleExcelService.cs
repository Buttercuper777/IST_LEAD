using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Newtonsoft.Json;
using IST_LEAD.Core;

namespace IST_LEAD.BusinessLogic.Sevices
{
    public class HandleExcelService
    {
        private static string baseDirectory = @"..\";
        private static string pathString = System.IO.Path.Combine(baseDirectory, "LocalStorage");
        
        private static string FileUrl { get; set; }
        private static string FileName { get; set; }

        private static bool FileDownloadSuccess { get; set; } = false;

        public HandleExcelService(string filePath, string fileName)
        {
            FileUrl = filePath;
            FileName = fileName;
        }
        
        // === === === Point of entry === === ===
        public void HandleExcel()
        {
            var fileManager = new FileManager();
                Directory.CreateDirectory(pathString);

                pathString = System.IO.Path.Combine(pathString,
                    fileManager.Base64Encode(FileName));
                
                
                bool dlResult = GetFileByPath(pathString);

                if(dlResult)
                {
                    string res = OpenExelFile(pathString);
                    
                }
                else
                {
                    
                }
        }
        // === === === === [END] === === === ===
        
        

        // === === === Download File === === ===
        private bool GetFileByPath(string newFilePath)
        {
            if(!File.Exists(newFilePath))
            {
                using(WebClient webClient = new WebClient()){
                    try
                    {
                        webClient.DownloadFileTaskAsync(new Uri(FileUrl), newFilePath).Wait();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        // === === === === [END] === === === ===
        
        
        private string OpenExelFile(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);

            DataSet ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                }
            }) ;

            DataTable table = ds.Tables[0];
            
            string JSONString = JsonConvert.SerializeObject(table);
            return JSONString;   
            
        }
        
    }
}