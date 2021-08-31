using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Demo.Domain.Entities;

namespace Utility.Demo
{
    public abstract class SourceMaterialController : Utility.AspNetCore.Controllers.BaseController<CrudAppService<SourceMaterialEntity, long>, SourceMaterialEntity, long>
    {
        public SourceMaterialController(CrudAppService<SourceMaterialEntity, long> service)
        {
            ApiService = service;
            this.Service = service;
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public override ResponseApi<ResultDto<SourceMaterialEntity>> FindResultByPage([FromForm, FromBody] SourceMaterialEntity obj, int page, int size)
        {
            return base.FindResultByPage(obj, page, size);
        }
    }
}
