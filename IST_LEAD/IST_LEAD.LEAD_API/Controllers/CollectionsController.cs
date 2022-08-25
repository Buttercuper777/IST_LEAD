using System;
using System.Threading.Tasks;
using IST_LEAD.Core.ProductBuilder.Models.Fields;
using IST_LEAD.Integrations.Directus.Customs.Items;
using IST_LEAD.Integrations.Directus.Externals.Abstract;
using IST_LEAD.Integrations.Directus.Models;
using IST_LEAD.Integrations.Directus.Models.Items;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IST_LEAD.LEAD_API.Controllers;

[Route("api/collections")]
[ApiController]
public class CollectionsController : ControllerBase
{

    private readonly IDirectusManager _directusManager;
    
    public CollectionsController(IDirectusManager directusManager)
    {
        _directusManager = directusManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNewCollections(ForCreation newItems)
    {
        if (newItems == null)
            return BadRequest();

        var directusProvider = _directusManager.GetProvider();
        
        var creationType = newItems.creationType;
        var collectionBase = await _directusManager
            .GetBaseCollectionByRelated(creationType);

        var collectionFields = await directusProvider.Fields.GetFields(collectionBase);
        var creationFieldName = directusProvider.Fields.GetFieldName(collectionFields);
    
        foreach (var item in newItems.data.Items)
        {
            var directusSendItem = new DirectusSendModel();
            var newSlug = new slugField(item.Name);
            directusSendItem.name = item.Name;
            directusSendItem.slug = newSlug.GetValue();

            var creationJson = _directusManager.GetDirectusCreationJson<DirectusSendModel>
                (directusSendItem, creationFieldName);

            await directusProvider.Items.AddNewItem(creationJson, collectionBase);
        }
        
        return Ok();
    }
}