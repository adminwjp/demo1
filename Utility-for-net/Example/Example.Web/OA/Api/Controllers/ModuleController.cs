using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ModuleController : BaseController<ModuleEntity>
    {
        //public ModuleController(ILogger<ModuleController> logger, IRepository<ModuleEntity,long> repository) : base(logger, repository)
        //{

        //}
        public ModuleController(ILogger<ModuleController> logger) : base(logger, null)
        {

        }
        protected override ResponseApi Edited(ModuleEntity obj)
		{
			base.Repository.Update(it => it.Id == obj.Id, it => new ModuleEntity() { Name = obj.Name, Href = obj.Href, Parent = obj.Parent, UpdateDate = DateTime.Now });
			return ResponseApi.CreateSuccess();
		}
		[HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Query(it=>it.Parent.Id>0).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}