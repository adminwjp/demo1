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
    public class FamousController : BaseController<FamousRaceEntity>
    {
        //public FamousController(ILogger<FamousController> logger, IRepository<FamousRaceEntity,long> repository) : base(logger,repository)
        //{

        //}
        public FamousController(ILogger<FamousController> logger) : base(logger, null)
        {

        }
        protected override ResponseApi Edited(FamousRaceEntity obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new FamousRaceEntity() { Name = obj.Name, UpdateDate = DateTime.Now });
            return ResponseApi.CreateSuccess();
        }
    }
}