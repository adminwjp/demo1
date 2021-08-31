using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public async Task<string> Get()
        {
            var t= await Get3();
            return t;
        }

        async Task<string> Get3()
        {
            var t = await Get2();
            return t;
        }
        async Task<string> Get1()
        {
            //await new Task<string>(()=>"test async await")//error
            var t = await Task.FromResult("test async await");//pass
            return t;
        }
        async Task<string> Get2()
        {
            var t = await Get1();
            return t;
        }
    }
}
