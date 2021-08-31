using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Application.Services;
using SocialContact.Application.Services.Dtos;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.AspNetCore.Controllers;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Json;

namespace SocialContact.Admin
{
    
    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}/work")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactWorkController : BaseController<WorkAppService, IRepository<WorkEntity, long>, CreateWorkInput, UpdateWorkInput, WorkInput, WorkDto, WorkEntity, long>
    {
        public SocialContactWorkController(WorkAppService service) 
        {
            this.ApiService = service;
            this.Service = service;

        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public override ResponseApi<ResultDto<WorkDto>> FindResultByPage([FromForm, FromBody] WorkInput obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }

    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}/relation")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactRelationController : BaseController<RelationAppService,  RelationEntity, long>
    {
        public SocialContactRelationController(RelationAppService service)
        {
            this.ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public override ResponseApi<ResultDto<RelationEntity>> FindResultByPage([FromForm, FromBody] RelationEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }

    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}/user_menu")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactUserMenuController : BaseController<CrudAppService<UserMenuEntity, long>,  UserMenuEntity, long>
    {
        public SocialContactUserMenuController(CrudAppService<UserMenuEntity, long> service)
        {
            this.ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public override ResponseApi<ResultDto<UserMenuEntity>> FindResultByPage([FromForm, FromBody] UserMenuEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
}
