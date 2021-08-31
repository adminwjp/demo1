using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Utility.AspNet;

namespace Example.Web
{
    public static class ExampleWebWebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            WebApiConfig.Register(config);
        }
    }
 
}
