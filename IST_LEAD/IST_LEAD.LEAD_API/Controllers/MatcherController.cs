using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using IST_LEAD.BusinessLogic.Sevices;
using IST_LEAD.BusinessLogic.Sevices.ProductServices;
using IST_LEAD.Core.Abstract;
using IST_LEAD.Core.Abstract.Services;
using IST_LEAD.Core.Attributes.Handlers;
using IST_LEAD.Core.Models.ExcelMatchingModels;
using IST_LEAD.Core.ProductBuilder.Abstract;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.DAL.Entities;
using IST_LEAD.DAL.Repository;
using IST_LEAD.Integrations.Cloudinary.Models;
using IST_LEAD.Integrations.Directus.Customs.Collections;
using IST_LEAD.Integrations.Directus.Customs.Excels;
using IST_LEAD.Integrations.Directus.Customs.Products;
using IST_LEAD.Integrations.Directus.Externals.Abstract;
using IST_LEAD.Integrations.Directus.Models;
using IST_LEAD.LEAD_API.Common.Mapping;
using IST_LEAD.LEAD_API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace IST_LEAD.LEAD_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatcherController : ControllerBase
{
    private IMapper Mapper { get; set; } = null!;

    private readonly IDirectusManager _directusManager;
    private readonly ICloudinaryManager _cloudinaryManager;
    private readonly IDbRepository _dbRepository;
    private readonly IVendCoderService _vendCoder;
    private readonly IHandleExcelService _handleExcelService;
    private readonly IProductBuilderService<OneProduct> _productsService;

    public MatcherController(
        IDirectusManager directusManager,
        ICloudinaryManager cloudinaryManager,
        IDbRepository dbRepository,
        IHandleExcelService handleExcelService,
        IProductBuilderService<OneProduct> productsService,

        IVendCoderService vendCoder
    )
    {
        _directusManager = directusManager;
        _cloudinaryManager = cloudinaryManager;
        _dbRepository = dbRepository;
        _handleExcelService = handleExcelService;
        _productsService = productsService;
        
        _vendCoder = vendCoder;
    }

    [HttpPost]
    public async Task<IActionResult> GetExcelValues([FromBody] ExcelLocations json)
    {
        if (json ==  null)
            return BadRequest();
        
        Guid fileId = Guid.Parse(json.FileId);
        ExcelEntity entity = null;
        
        try
        {
            entity = _dbRepository.Get<ExcelEntity>().FirstOrDefault(x => x.Id == fileId);
        }
        catch (Exception ex) { throw new Exception(ex.Message);}
        
        if (entity == null)
            throw new Exception("Unexpected error. There is no record with the file in the database");
        
        
        var excelColumnsValuesList = new List<ExcelColumnValues>();
        var excelHandler = _handleExcelService.Init(entity.FilePath, entity.FileName);
        
        foreach (var el in json.Fields)
        {
            var newColumn = new ExcelColumnValues
            {
                Values = new List<string>(),
                ColName = el.FieldName
            };
            
            excelColumnsValuesList.Add(
                excelHandler.GetColumnValues(el.location, newColumn
                ));
        }
        
        var ExcelRows = excelHandler.GetNumOfExcelRows();
        excelHandler.ExcelDelete();
        
        var directusProvider = _directusManager.GetProvider();
        var ProdImages = _cloudinaryManager.GetFilesFromFolder("ProductsImages", 100);
        var ImagesResult = JsonConvert.DeserializeObject<CloudinarySearch>(ProdImages);

        _productsService.InitProductsList(ExcelRows);
        _productsService.HardFieldsActions.SetHardAttributes(excelColumnsValuesList);
        _productsService.HardFieldsActions.ApplyImageQualifierAttributes(ImagesResult);

        var outSerializedProductsList = new List<SerializationProduct>();
        
        var productList = _productsService.GetProducts();
        foreach (var product in productList)
        {
            var listOfProductCollections = 
                _productsService.ProductCollections.GetCollections(product);
            
            foreach (var collection in listOfProductCollections)
            {
                var collectionName = collection.GetName();
                var related = await _directusManager.GetBaseCollectionByRelated(collectionName);
                var OutCollections = await _directusManager.GetItemsAsCollections(related);

                _productsService.ProductCollections.CollectionsMatching
                    (product, collection, OutCollections);

            }
            
            //SerializationProduct Just leave it for now and don't change it.
            // Reflection needs to be redone. 😅
            
            var outProd = new SerializationProduct();
            var fieldsSer = new FieldsSerializer<OneProduct, SerializationProduct>(outProd);
            var serdProd = fieldsSer.GetSerializationAttributes(product);
            outSerializedProductsList.Add(serdProd);
            // ------------------------------------------------------------
        }
        
    
        SessionExtensions.SetObjectInSession(
            HttpContext.Session, HttpContext.Session.Id,outSerializedProductsList);    
            
        var sessionData = Microsoft.AspNetCore.Http.SessionExtensions
            .GetString(HttpContext.Session, HttpContext.Session.Id);
        
        return Ok(sessionData);
    }
    
    
    [HttpGet]
    [Route("get-session")]
    public IActionResult SessionGet()
    {

        var res = SessionExtensions.GetCustomObjectFromSession<List<SerializationProduct>>
            (HttpContext.Session, HttpContext.Session.Id);
        
        return Ok(Microsoft.AspNetCore.Http.SessionExtensions
            .GetString(HttpContext.Session, HttpContext.Session.Id));
    }
    
    
    // var serdProdList = new List<SerializationProduct>();
    //     foreach (var product in newProducts)
    // {
    //     var outProd = new SerializationProduct();
    //     var fieldsSer = new FieldsSerializer<OneProduct, SerializationProduct>(outProd);
    //
    //     var serdProd = fieldsSer.GetSerializationAttributes(product);
    //     var allProducts = await directusProvider.Items.GetItems("Products");
    //     var MaxProductIndex = allProducts.Items.Max(x => x.Id) + 1;
    //     
    //     _vendCoder.setVendCode(serdProd, "vend_code", MaxProductIndex);
    //     
    //     serdProdList.Add(serdProd);
    // }


}