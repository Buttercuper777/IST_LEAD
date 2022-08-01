﻿using System;
using AutoMapper;
using IST_LEAD.BusinessLogic.Sevices;
using IST_LEAD.Core;
using IST_LEAD.Core.Models;
using IST_LEAD.DAL.Entities;
using IST_LEAD.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IST_LEAD.LEAD_API.Common.Mapping;


namespace IST_LEAD.LEAD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public static IMapper Mapper { get; set; } = null!;
    
        //private readonly INewFileActions _newFileActions;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IDbRepository _dbRepository;

        public FilesController(ICloudinaryManager cloudinaryManager, IDbRepository dbRepository)
        {
            //_newFileActions = newFileActions;
            _cloudinaryManager = cloudinaryManager;
            _dbRepository = dbRepository;

        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile newFile, CancellationToken cancellationToken)
        {

            if (newFile == null)
                return BadRequest("Request must contain a file. Empty request");
            
            var fileForUpload = new FileForUpload
            {
                File = newFile,
                FileFullName = newFile.FileName
            };

            var uploadedFile = _cloudinaryManager.UploadFile(fileForUpload);

            if (uploadedFile.UploadedResult)
            {
                var mapperConfiguration = new MapperConfiguration(x =>
                {
                    x.AddProfile<BaseMappingProfile>();
                });

                mapperConfiguration.AssertConfigurationIsValid();
                Mapper = mapperConfiguration.CreateMapper();

                var model = Mapper.Map<ExcelEntity>(uploadedFile);

                var result = await _dbRepository.Add(model);
                // await _dbRepository.SaveChangesAsync();

                return Ok(result);
            }

            else
            {
                return BadRequest(uploadedFile.FilePath);
            }

        }
    }
}