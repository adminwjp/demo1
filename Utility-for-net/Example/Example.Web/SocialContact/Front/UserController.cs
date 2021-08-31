using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Attributes;
using Utility.Json;
using Utility.Demo.Application.Services.Dtos;
using Utility.Extensions;
using Utility.Demo.Application.Services;
using Utility.Demo;

namespace SocialContact.Front
{
    [Area("social_contact")]
    [Route("social_contact/front/api/v{version:apiVersion}/user")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactUserController : UserController<UserEntity, long>
    {

        public SocialContactUserController(UserAppService<UserEntity, long> userAppService, IHttpContextAccessor contextAccessor, ILogger logger)
            :base(userAppService,contextAccessor,logger)
        {
        }

       
      
    }
}
