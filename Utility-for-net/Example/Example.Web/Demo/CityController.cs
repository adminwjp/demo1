
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Demo.Domain.Entities;

namespace Utility.Demo
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    [Transtation]
    public class CityController : Utility.AspNetCore.Controllers.BaseController<CrudAppService<CityEntity, long>, CityEntity, long>
    {
        public CityController(CrudAppService<CityEntity, long> service)
        {
            ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public virtual ResponseApi<ResultDto<CityEntity>> List([FromForm, FromBody] CityEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
}
