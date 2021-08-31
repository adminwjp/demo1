using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using System.Xml.Serialization;
using Utility.Domain.Repositories;
using OA.Api;
using N = NHibernate;
namespace OA.Api.Controllers
{
    [Area("oa")]
    [Route("oa/admin/api/v{version:apiVersion}/bring_up_content")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class BringUpContentController : BaseController<BringUpContentEntity>
    {
        //public BringUpContentController(ILogger<BringUpContentController> logger, IRepository<BringUpContentEntity,long> repository) : base(logger, repository)
        //{

        //}
        public BringUpContentController(ILogger<BringUpContentController> logger) : base(logger, null)
        {

        }
        protected override ResponseApi Edited(BringUpContentEntity obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new BringUpContentEntity() { UpdateDate = DateTime.Now, Name = obj.Name, Content = obj.Content, Unit = obj.Unit, Place = obj.Place, StartDate = obj.StartDate, EndDate = obj.EndDate });
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