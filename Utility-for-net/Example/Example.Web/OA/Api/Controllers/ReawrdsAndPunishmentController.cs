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
    [Route("oa/admin/api/v{version:apiVersion}/reawrds_and_punishment")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ReawrdsAndPunishmentController : BaseController<ReawrdsAndPunishmentEntity>
    {
        //public ReawrdsAndPunishmentController(ILogger<ReawrdsAndPunishmentController> logger, IRepository<ReawrdsAndPunishmentEntity,long> repository) : base(logger, repository)
        //{

        //}
        public ReawrdsAndPunishmentController(ILogger<ReawrdsAndPunishmentController> logger) : base(logger, null)
        {

        }
    }
}