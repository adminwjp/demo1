//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Example.ConsoleApp;
//using Microsoft.Owin;
//using Owin;

//[assembly: OwinStartup(typeof(OwinMain))]

//namespace Example
//{
//    public class OwinMain
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
//            app.Use((context, next) =>
//            {
//                TextWriter output = context.Get<TextWriter>("host.TraceOutput");
//                return next().ContinueWith(result =>
//                {
//                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
//                    context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
//                });
//            });

//            app.Run(async context =>
//            {
//                await context.Response.WriteAsync(getTime() + " My First OWIN App");
//            });
//        }
//        string getTime()
//        {
//            return DateTime.Now.Millisecond.ToString();
//        }
//    }
//}
