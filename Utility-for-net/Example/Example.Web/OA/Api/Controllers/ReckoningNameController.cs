using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("oa/admin/api/v{version:apiVersion}/reckoning_name")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ReckoningNameController : BaseController<ReckoningNameEntity>
    {
        //public ReckoningNameController(ILogger<ReckoningNameController> logger, IRepository<ReckoningNameEntity,long> repository) : base(logger, repository)
        //{

        //}
        public ReckoningNameController(ILogger<ReckoningNameController> logger) : base(logger, null)
        {

        }
        protected override ResponseApi Edited(ReckoningNameEntity obj)
        {
            base.Repository.Update(it => it.Id == obj.Id, it => new ReckoningNameEntity() { Name = obj.Name,Explain=obj.Explain, UpdateDate = DateTime.Now });
            return ResponseApi.CreateSuccess();
        }
	    [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Query(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}