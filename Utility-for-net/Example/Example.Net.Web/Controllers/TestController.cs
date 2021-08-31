using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Web.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
    public class Test1Controller : ApiController
    {
        // GET: Test
       [System.Web.Http.HttpGet]
        public string Index()
        {
            return "test";
        }
    }
}