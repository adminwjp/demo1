using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using OA.Api;
using N = NHibernate;
namespace OA.Api.Controllers
{
    [Area("oa")]
    [Route("oa/admin/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi),200)]
    public class RoleController : BaseController<RoleEntity>
    {
        //public RoleController(ILogger<RoleController> logger, IRepository<RoleEntity,long> repository) : base(logger,repository)
        //{

        //}
        public RoleController(ILogger<RoleController> logger) : base(logger, null)
        {

        }
        protected override ResponseApi Edited(RoleEntity obj)
        {
            base.Repository.Update(it => it.Id == obj.Id, it => new RoleEntity() { Name = obj.Name, UpdateDate = DateTime.Now });
            return ResponseApi.CreateSuccess();
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data=base.Repository.Query(null).Select(it=>new { it.Id,it.Name}).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}