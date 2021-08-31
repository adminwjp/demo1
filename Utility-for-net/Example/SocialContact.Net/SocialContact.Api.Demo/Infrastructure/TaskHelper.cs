using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Tasks;

namespace SocialContact.Infrastructure
{
    public class TaskHelper
    {
        /// <summary>
        /// 直接运行任务
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public static dynamic RunTask(TaskService taskService, int id)
        {
            TaskDetail td = taskService.Get(id);
            if (td == null)
            {
                return new { status = false, message = "执行失败!" };
            }

            if (td.RunAtServer == RunAtServer.Search)
            {
                try
                {
                    var url = ConfigurationManager.AppSettings["Search"];
                    var secret = ConfigurationManager.AppSettings["ApiAccessSecret"];

                    if (string.IsNullOrWhiteSpace(url))
                    {
                        return new { status = false, message = "执行失败!" };
                    }
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("BasicAuth {0}", secret));
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(string.Format("{0}/Api/Search/RunTask?id={1}", url, id)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return new { success = true, message = "执行成功!" };
                    }
                    else
                    {
                        return new { success = false, message = "执行失败!" };
                    }
                }
                catch (Exception)
                {
                    return new { status = false, message = "执行失败!" };
                }
            }
            else
            {
                TaskSchedulerFactory.GetScheduler().Run(id);
            }
            return new { success = true, message = "执行成功!" };
        }
    }
}
