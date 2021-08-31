using Company.Domain.Entities;
using Company.Ef;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Ef.Repositories;
using Utility.Helpers;

namespace Company.Api.Admin
{
    [Area("company")]
    [Route("company/admin/api/v{version:apiVersion}/category")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class CatagoryController : BaseController<CompanyCatagoryEntity>
    {
        public CatagoryController(IHttpContextAccessor contextAccessor) :base(contextAccessor)//(BaseEfRepository<CompanyDbContext, CompanyCatagoryEntity, long> repository) : base(repository)
        {

        }
        [HttpPost("insert")]
        public override ResponseApi Insert([ FromForm, FromBody] CompanyCatagoryEntity obj)
        {
            if (Request.Form.Files !=null&& Request.Form.Files.Count > 0)
            {

            }
            return base.Insert(obj);
        }
        [HttpPost("Update")]
        public override ResponseApi Update([FromBody, FromForm] CompanyCatagoryEntity obj)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {

            }
            return base.Update(obj);
        }

        [HttpGet("flag")]
        public IActionResult Flag()
        {
            var t = typeof(CompanyCatagoryFlag);
            var data=TypeHelper.GetFieldsEnum(CompanyCatagoryFlag.Nav);
            return new JsonResult(data);
        }
    }
}
