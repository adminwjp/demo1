
using Config.Application.Services;
using Config.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.AspNetCore.Controllers;

namespace Config.Api.Controllers
{
    //继承 基类 async  无法访问 一直 请求
    // Conflicting method/path combination "POST api/Config/Insert" for actions - Config.Api.Controllers.ConfigController.InsertAsync 
    // (Config.Example.Api),Config.Api.Controllers.ConfigController.Insert (Config.Example.Api). Actions require a unique method/path combination for Swagger/OpenAPI 3.0
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : BaseController<ConfigResponseApiService, ConfigService, ConfigEntity, string>
    {
        public ConfigController(ConfigResponseApiService apiBLL)
        {
            this.ApiService = apiBLL;
        }
    }
}
