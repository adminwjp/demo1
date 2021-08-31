using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.AspNetCore.Controllers;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Json;

namespace SocialContact.Admin
{
    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}/icon")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactIconController : BaseController<IconAppService, IconEntity, long>
    {
       public SocialContactIconController(IconAppService service)
        {
            this.ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public override ResponseApi<ResultDto<IconEntity>> FindResultByPage([FromForm,FromBody] IconEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
}
