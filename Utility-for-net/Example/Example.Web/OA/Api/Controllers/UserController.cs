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
using Utility.Demo.Domain.Entities;
using Utility.Demo.Application.Services;

namespace OA.Api.Controllers
{

    [Area("oa")]
    [Route("oa/admin/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class UserController : Utility.Demo.UserController<UserEntity,long>
    {
        //public UserController(ILogger<UserController> logger, IRepository<UserEntity,long> repository) : base(logger, repository)
        //{

        //}
        public UserController(UserAppService<UserEntity, long> userAppService, IHttpContextAccessor contextAccessor, ILogger<UserController> logger) : base(userAppService, contextAccessor,logger)
        {

        }
     
    }
}