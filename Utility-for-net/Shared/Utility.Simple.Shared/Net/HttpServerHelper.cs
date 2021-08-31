#if  ! (NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Json;
//using System.Web.Http;

namespace Utility.Net
{
    //public class HttpServerHelper: HttpServer
    //{
    //}

    public class HttpServerHelper
    {
        public  readonly HttpListener Listener = new HttpListener();
        public virtual void Start(Action<HttpListenerContext> routers=null)
        {
            Listener.Start();
            Listener.IgnoreWriteExceptions = true;
            while (true)
            {
                var httpListenerContext = Listener.GetContext();
                Console.WriteLine(httpListenerContext.Request.RawUrl);
                httpListenerContext.Response.KeepAlive = true;
                //路由器
                //设置 Response 参数 无效
                if (routers == null)
                {
                    switch (httpListenerContext.Request.RawUrl)
                    {
                        case "/api/v1/user/login":
                            {
                                httpListenerContext.Response.StatusCode = 200;
                                var data = new { success = true, msg = "success", data = "login", code = 200 };
                                var buffer = Encoding.UTF8.GetBytes(JsonHelper.ToJson(data));
                                httpListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
                                httpListenerContext.Response.Cookies.Add(new Cookie("user_id", "123"));// not used
                                httpListenerContext.Response.AddHeader("Server", "Nignx");// not used
                                httpListenerContext.Response.OutputStream.Close();
                                break;
                            }
                        case "/api/v1/user/register":
                            {
                                httpListenerContext.Response.StatusCode = 200;
                                var data = new { success = true, msg = "success", data = "register", code = 200 };
                                var buffer = Encoding.UTF8.GetBytes(JsonHelper.ToJson(data));
                                httpListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
                                httpListenerContext.Response.Cookies.Add(new Cookie("register", "123")); // not used
                                httpListenerContext.Response.AppendCookie(new Cookie("register", "123")); // not used
                                httpListenerContext.Response.AddHeader("Server", "Nignx");// not used
                                httpListenerContext.Response.Headers["Server"] = "Nignx";// not used
                                httpListenerContext.Response.OutputStream.Close();
                                break;
                            }
                        case "/api/v1/user/update_pwd":
                            {
                                httpListenerContext.Response.StatusCode = 200;
                                var data = new { success = true, msg = "success", data = "update_pwd", code = 200 };
                                var buffer = Encoding.UTF8.GetBytes(JsonHelper.ToJson(data));
                                httpListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
                                httpListenerContext.Response.Cookies.Add(new Cookie("update_pwd", "123"));// not used
                                httpListenerContext.Response.AddHeader("Server", "Nignx");// not used
                                httpListenerContext.Response.OutputStream.Close();
                                break;
                            }
                        case "/":
                        default:
                            {
                                httpListenerContext.Response.StatusCode = 200;
                                var data = new { success = true, msg = "success", data = "test", code = 200 };
                                var buffer = Encoding.UTF8.GetBytes(JsonHelper.ToJson(data));
                                httpListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
                                httpListenerContext.Response.Cookies.Add(new Cookie("test", "123"));// not used
                                httpListenerContext.Response.OutputStream.Close();
                                break;
                            }
                    }
                }
                else
                {
                    routers(httpListenerContext);
                }
            }
        }

        public virtual void Stop()
        {
            Listener.Stop();
        }
    }
}
#endif