using System;
using System.Linq;
using System.Threading.Tasks;
using IST_LEAD.BusinessLogic.Sevices;
using IST_LEAD.DAL.Entities;
using IST_LEAD.DAL.Repository;
using IST_LEAD.Integrations.Directus;
using IST_LEAD.Integrations.Directus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IST_LEAD.LEAD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : Controller
    {

        private readonly IDbRepository _dbRepository;
        
        public ExcelController(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        [HttpGet]
        [Route("GetJsonModel")]
        public async Task<IActionResult> GetJsonModel(Guid id)
        {
            var Entity = _dbRepository.Get<ExcelEntity>().FirstOrDefault(entity => entity.Id == id);
            var Resp = JsonConvert.SerializeObject(Entity,
                                                            Formatting.Indented,
                                                            new JsonSerializerSettings()
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                            });
            return Ok(Resp);
        }
        
        
        [HttpGet]
        [Route("HandleExcel")]
        public async Task<IActionResult> HandleExcel(Guid id)
        {
            var Entity = _dbRepository.Get<ExcelEntity>().FirstOrDefault(entity => entity.Id == id);
            if (Entity != null)
            {
                var filePath = Entity.FilePath;
                var fileName = Entity.FileName;
                var excelHandler = new HandleExcelService(filePath, fileName);
                var res = excelHandler.GetAllExcelColumns();
                
                return Content(res);
            }
            else
                return BadRequest("No entry found with this id");
            
        }
        
        // [HttpGet]
        // [Route("Try")]
        // public async Task<IActionResult> Try(string collection)
        // {
        //     var Directus = new DirectusProvider("");
        //     
        //     
        //     var rWF = Directus.Relations.FindRelationWithField(collection);
        //     var b = rWF.Result;
        //     
        //     var targetRelations = await Directus.Relations.GetRelations(b.Collection);
        //     
        //     
        //     var final_relation = Directus.Relations.GetRelatedCollection(targetRelations, collection);
        //     var Items = await Directus.Items.GetItems(final_relation);
        //
        //     if (Items != null)
        //     {
        //         // var JsonResp = JsonConvert.SerializeObject(Relations);
        //         return Ok(Items);
        //     }
        //     
        //     return BadRequest();
        //     
        // }
        
        
    }
}