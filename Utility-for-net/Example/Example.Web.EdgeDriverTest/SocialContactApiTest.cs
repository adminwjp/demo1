using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Utility.Helpers;
using Utility.Json;
using Utility.Net.Http;

namespace Example.Web.EdgeDriverTest
{
    [TestClass]
    public class SocialContactApiTest
    {

        HttpClient httpClient;
        HttpRequestMessage httpRequestMessage;
        string url = InitTest.url+"social_contact/admin/api/v1/";
        [TestInitialize]
        public void Init()
        {
            httpClient = new HttpClient();
            httpRequestMessage = new HttpRequestMessage();
        }
        
        //[TestMethod]
        public void TestUser()
        {
            Console.WriteLine("admin");
            TestUser("admin");
            this.TestClean();

            Init(); 
            Console.WriteLine("user");
            TestUser("user");
        }
       
        public void TestUser(string user)
        {

            httpClient.BaseAddress = new Uri(url + user+ "/register");
            httpRequestMessage.Method = System.Net.Http.HttpMethod.Post;
            httpRequestMessage.Content = new StringContent(
                JsonHelper.ToJson(new { account = "123", pwd = "123" },
                JsonHelper.JsonSerializerSettings),
                Encoding.UTF8, "application/json"
                );
           var  res = httpClient.SendAsync(httpRequestMessage).Result
                .Content.ReadAsStringAsync().Result;
            if (RegexHelper.IsMatch(res, InitTest.SuccessRegex))
            {
                Console.WriteLine("register success");
            }
            else
            {
                Console.WriteLine("register fail");
            }
            // httpClient.Dispose();

            
            res=HttpHelper.PostJson(url + user + "/login", JsonHelper.ToJson(new { account = "123", pwd = "123" },
                JsonHelper.JsonSerializerSettings));
            if (RegexHelper.IsMatch(res, InitTest.SuccessRegex))
            {
                Console.WriteLine("login success");
            }
            else
            {
                Console.WriteLine("login fail");
            }
            var token = RegexHelper.GetValue(res, InitTest.TokenRegex);

            res = HttpHelper.Get(url + user + "/list/1/10?token="+token);
            if (RegexHelper.IsMatch(res, InitTest.SuccessRegex))
            {
                Console.WriteLine("find success");
            }
            else
            {
                if (!res.Contains("token"))
                {
                    Console.WriteLine("token err,find fail");
                }
                else
                {
                    Console.WriteLine("find fail");
                }
            }
        }

        [TestCleanup]
        public void TestClean()
        {
            httpRequestMessage.Dispose();
            httpClient.Dispose();
        }
    }
}
