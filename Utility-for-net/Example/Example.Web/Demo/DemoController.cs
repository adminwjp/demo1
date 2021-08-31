using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Demo.Domain.Entities;

namespace Example.Web.Demo
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    [Transtation]
    public class EmailController : Utility.AspNetCore.Controllers.BaseController<CrudAppService<EmailEntity, long>, EmailEntity, long>
    {
        public EmailController(CrudAppService<EmailEntity, long> service)
        {
            ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public virtual ResponseApi<ResultDto<EmailEntity>> List([FromForm, FromBody] EmailEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    [Transtation]
    public class SmsController : Utility.AspNetCore.Controllers.BaseController<CrudAppService<SmsEntity, long>, SmsEntity, long>
    {
        public SmsController(CrudAppService<SmsEntity, long> service)
        {
            ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public virtual ResponseApi<ResultDto<SmsEntity>> List([FromForm, FromBody] SmsEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
    }
}
