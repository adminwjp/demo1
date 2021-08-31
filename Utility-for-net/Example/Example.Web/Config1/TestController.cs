using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Config.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        public string Test()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }
        [HttpGet("test1")]
        public async Task<string> TestAsync()
        {
            return await new Task<string>(() => "async success"); ;
        }
    }
}
