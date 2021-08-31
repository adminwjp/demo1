
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Attributes;

namespace SocialContact.Front
{
    [Area("social_contact")]
    [Route("social_contact/front/api/v{version:apiVersion}/catalog")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactCatagoryController : ControllerBase
    {
        private CatagoryAppService catagoryAppService;

        //Object reference not set to an instance of an object 注释 去掉
        public SocialContactCatagoryController(CatagoryAppService catagoryAppService)
        {
            this.catagoryAppService = catagoryAppService;
        }

        [HttpGet("Job")]
        public virtual ResponseApi Job()
        {
            return ResponseApi.CreateSuccess().SetData(catagoryAppService.Catagory(CatalogFlag.Job));
        }
        [HttpGet("Tag")]
        public virtual ResponseApi Tag()
        {
            return ResponseApi.CreateSuccess().SetData(catagoryAppService.Catagory(CatalogFlag.Tag));
        }
        [HttpGet("Skill")]
        public virtual ResponseApi Skill()
        {
            return ResponseApi.CreateSuccess().SetData(catagoryAppService.Catagory(CatalogFlag.Skill));
        }
        [HttpGet("Like")]
        public virtual ResponseApi Like()
        {
            return ResponseApi.CreateSuccess().SetData(catagoryAppService.Catagory(CatalogFlag.Like));
        }
     
    }
}
