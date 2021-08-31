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
    [Route("oa/admin/api/v{version:apiVersion}/reckoning_list")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ReckoningListController : BaseController<ReckoningListEntity>
    {
        //public ReckoningListController(ILogger<ReckoningListController> logger, IRepository<ReckoningListEntity,long> repository) : base(logger, repository)
        //{

        //}
        public ReckoningListController(ILogger<ReckoningListController> logger) : base(logger, null)
        {

        }
    }
}