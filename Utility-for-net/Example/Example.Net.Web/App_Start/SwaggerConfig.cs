

using Example.Web;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Utility.AspNet;

[assembly: PreApplicationStartMethod(typeof(ExampleWebSwaggerConfig), "Register")]
namespace Example.Web
{
    public class ExampleWebSwaggerConfig
    {
        public static void Register()
        {
           
        }


    }
}