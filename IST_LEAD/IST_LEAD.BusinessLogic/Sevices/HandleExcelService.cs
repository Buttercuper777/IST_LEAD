using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Newtonsoft.Json;
using IST_LEAD.Core;
using IST_LEAD.Core.Models.Common;
using IST_LEAD.Core.Models.ExcelHandlerModels;
using IST_LEAD.Core.Models.ExcelMatchingModels;

namespace IST_LEAD.BusinessLogic.Sevices
{
    public class HandleExcelService
    {
        private static string baseDirectory = @"..\";
        private static string pathString = System.IO.Path.Combine(baseDirectory, "LocalStorage");

        private static string FileUrl { get; set; } = "";
        private static string FileName { get; set; } = "";
        

        public HandleExcelService(string filePath, string fileName)
        {
            FileUrl = filePath;
            FileName = fileName;
        }

        
        // GetAllExcelColumns
        public string GetAllExcelColumns()
        {
            var fileManager = new FileManager();
            
            Directory.CreateDirectory(pathString);
            
            var fileString = System.IO.Path.Combine(pathString,
                    fileManager.Base64Encode(FileName));
                
                
                var dlResult = fileManager.GetFileByPath(fileString, FileUrl);

                if(dlResult.Result)
                {
                    var res = GetColumnsLocations(fileString);
                    return res;
                }
                else
                {
                    return null;
                }
            
        }
        
        
        private string GetColumnsLocations(string path)
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

            var res = new ResultModel
            {
                missings = false
            };

            for (int i = 0; i < table.Columns.Count; i++)
            {
                res.columns.Add(
                    new Columns
                    {
                        title = table.Columns[i].Caption,
                        Location = new Location
                        {
                            col = i,
                            row = 0
                        },
                        
                        Helpers = new List<string>(new []
                        {
                            table.Rows[0][i].ToString(),
                            table.Rows[1][i].ToString()
                        })
                        
                    }
                );
            }
            
            string JSONString = JsonConvert.SerializeObject(res);
            
            fileStream.Close();
            File.Delete(path);
            
            return JSONString;
        }


        public ExcelColumnsList GetColumnValues(Location location, ExcelColumnsList excelColumnsList)
        {
            
            var fileManager = new FileManager();
            
            var fileString = System.IO.Path.Combine(pathString,
                fileManager.Base64Encode(FileName));

            var GetFileRes = fileManager.GetFileByPath(fileString, FileUrl);

            if (!GetFileRes.Result)
                return null;
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            FileStream fileStream = File.Open(fileString, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);

            DataSet ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                }
            }) ;

            DataTable table = ds.Tables[0];

            if (excelColumnsList != null)
            {
                var columnValues = excelColumnsList;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    columnValues.Values.Add(
                        table.Rows[i][location.col].ToString()
                    );
                }
                
                fileStream.Close();
                return columnValues;
            }
            fileStream.Close();
            return null;
            
        }

        public void ExcelDelete()
        {
            var fileManager = new FileManager();
            var fileString = System.IO.Path.Combine(pathString,
                fileManager.Base64Encode(FileName));
            
            File.Delete(fileString);
        }

        public int GetNumOfExcelRows()
        {
            var fileManager = new FileManager();
            var fileString = System.IO.Path.Combine(pathString,
                fileManager.Base64Encode(FileName));
            
            FileStream fileStream = File.Open(fileString, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);

            DataSet ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                }
            }) ;
            
            DataTable table = ds.Tables[0];

            int RowsVal = 0;
            RowsVal = table.Rows.Count;
            fileStream.Close();

            return RowsVal;
        }
        
        
        
    }
}