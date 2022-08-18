using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using IST_LEAD.BusinessLogic.Sevices;
using IST_LEAD.Core.Abstract;
using IST_LEAD.Core.Abstract.Services;
using IST_LEAD.Core.Attributes;
using IST_LEAD.Core.Attributes.Handlers;
using IST_LEAD.Core.Models.Common;
using IST_LEAD.Core.Models.ExcelMatchingModels;
using IST_LEAD.Core.ProductBuilder.Models.Collections;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.DAL.Entities;
using IST_LEAD.DAL.Repository;
using IST_LEAD.Integrations.Cloudinary.Models;
using IST_LEAD.Integrations.Directus.Customs.Excels;
using IST_LEAD.Integrations.Directus.Customs.Products;
using IST_LEAD.Integrations.Directus.Externals.Abstract;
using IST_LEAD.Integrations.Directus.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IST_LEAD.LEAD_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatcherController : ControllerBase
{

    private readonly IDirectusManager _directusManager;
    private readonly ICloudinaryManager _cloudinaryManager;
    private readonly IDbRepository _dbRepository;
    private readonly IFileManager _fileManager;
    private readonly IVendCoderService _vendCoder;

    public MatcherController(IDirectusManager directusManager, ICloudinaryManager cloudinaryManager,
        IDbRepository dbRepository, IFileManager fileManager, IVendCoderService vendCoder)
    {
        _directusManager = directusManager;
        _cloudinaryManager = cloudinaryManager;
        _dbRepository = dbRepository;
        _fileManager = fileManager;
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
            return Ok(null);
        
        var excelHandler = new HandleExcelService(entity.FilePath, entity.FileName);
        var ExcelColumnsValuesList = new List<ExcelColumnsList>();

        foreach (var el in json.Fields)
        {
            var newColumn = new ExcelColumnsList()
            {
                Values = new List<string>()
            };
          
            newColumn.ColName = el.FieldName;

            newColumn = excelHandler.GetColumnValues(el.location, newColumn);
            
            ExcelColumnsValuesList.Add(newColumn);
            
        }

        var ExcelRows = excelHandler.GetNumOfExcelRows();
        excelHandler.ExcelDelete();

        var newProducts = new List<OneProduct>();
        
        for (int i = 0; i < ExcelRows; i++)
        {
            newProducts.Add(new OneProduct());
        }
        
        
        
        //New product Mapper
        var ProductMapper = new HardFieldsHandler<OneProduct>(newProducts);

        //Set fields & slugs[PRODUCT_NAME -> SLUG] from Ex-Cols to Products 
        foreach (var Column in ExcelColumnsValuesList)
        {
            ProductMapper.MapHardFields(Column, Column.Values.Count);
        }
        newProducts = ProductMapper.GetList();

        //Get Images From Cloudinary
        var ProdImages = _cloudinaryManager.GetFilesFromFolder("ProductsImages", 100);
        var ImagesResult = JsonConvert.DeserializeObject<CloudinarySearch>(ProdImages);
        
        //Set Images [MFG_VEND_CODE -> Image]
        ProductMapper.SetHardImage(newProducts, ImagesResult);

        var directusProvider = _directusManager.GetProvider();
        
        foreach (var product in newProducts)
        {
            var listOfCollections = ProductMapper.GetCollections(product);
            
            foreach (var collection in listOfCollections)
            {

                var collectionName = collection.GetName();
                
                var relationWithField = directusProvider.Relations.FindRelationWithField(collectionName);
                var targetRelations = await directusProvider.Relations.GetRelations(relationWithField.Result.Collection);
                var relatredCollection =
                    directusProvider.Relations.GetRelatedCollection(targetRelations, collectionName);

                ItemsObject AllItemsOfDirectusCollection = null;
                if (relatredCollection != null)
                {
                    AllItemsOfDirectusCollection = await directusProvider.Items.GetItems(relatredCollection);
                }

                if (AllItemsOfDirectusCollection != null)
                {
                    var newCollectionsMatcher = new CollectionsMatcher();
                    var MatchedNamedCollection = newCollectionsMatcher.CollectionsMatching(collection, AllItemsOfDirectusCollection);
                    
                    var CollectionOfMatchedCollection = MatchedNamedCollection.GetCollection();
                    
                    collection.SetCollection(CollectionOfMatchedCollection);
                    
                }
            
                ProductMapper.SetCollection(product, collection);
            }
        }
        
        
        foreach (var product in newProducts)
        {
            var outProd = new SerializationProduct();
            var fieldsSer = new FieldsSerializer<OneProduct, SerializationProduct>(outProd);
            var serdProd = fieldsSer.GetSerializationAttributes(product);

            var allProducts = await directusProvider.Items.GetItems("Products");
            var MaxProductIndex = allProducts.Items.Max(x => x.Id) + 1;

            _vendCoder.setVendCode(serdProd, "vend_code", MaxProductIndex);

        }
        
        
        
        var jsonResult = "";
        if (newProducts != null)
        {
            jsonResult = JsonConvert.SerializeObject(newProducts);
        }

        var JsonObj = json;
        return Ok(JsonObj);
        
    }
    
}