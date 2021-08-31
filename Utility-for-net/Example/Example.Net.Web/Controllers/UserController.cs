
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
//using Utility.Demo.Domain.Entities;
//using Utility.Demo;
//using Utility.Demo.Application.Services;
using Microsoft.Extensions.Logging;

namespace Example.Web.Controllers
{
    public class UserController:Controller
    {
        public ActionResult Register()
        {
            return View();
        }
    }
    //[System.Web.Http.Route("api/v1/user")]
    //public class UserApiController : Utility.Demo.UserController<UserEntity,long>
    //{
    //    public UserApiController(UserAppService<UserEntity, long> userAppService, ILogger<UserApiController> logger)
    //        :base(userAppService,logger)
    //    {
    //    }
    //}
}