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
    public class DutyController : BaseController<DutyEntity>
    {
        //public DutyController(ILogger<DutyController> logger, IRepository<DutyEntity,long> repository) : base(logger, repository)
        //{

        //}
        public DutyController(ILogger<DutyController> logger) : base(logger, null)
        {

        }
    }
}