using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Demo.Application.Services;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Domain.Repositories;

namespace Utility.Demo
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    [Transtation]
    public  class MenuController : Utility.AspNetCore.Controllers.BaseController<MenuService, MenuEntity, long>
    {
        public MenuController(MenuService service) 
        {
            ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public virtual ResponseApi<ResultDto<MenuEntity>> List([FromForm,FromBody ] MenuEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
}
