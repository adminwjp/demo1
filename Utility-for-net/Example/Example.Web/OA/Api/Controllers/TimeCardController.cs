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
    [Route("oa/admin/api/v{version:apiVersion}/time_card")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class TimeCardController : BaseController<TimeCardEntity>
    {
        //public TimeCardController(ILogger<TimeCardController> logger, IRepository<TimeCardEntity,long> repository) : base(logger, repository)
        //{

        //}
        public TimeCardController(ILogger<TimeCardController> logger) : base(logger, null)
        {

        }
    }
}