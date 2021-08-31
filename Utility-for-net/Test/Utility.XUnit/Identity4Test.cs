//using Newtonsoft.Json.Linq;
//using System;
//using System.Net.Http;

//namespace NUnitTest
//{
//    public  class Identity4Test
//    {
//        //   [Fact]
//        public void Test1()
//        {
//            //包装有点厉害 identity4model 内部 包装了 好 几层(调来调去 依赖注入 可读性太差 )
//            //connection/token pass (password ef 不支持 基于 内存 应该 支持 没测试)
//            //connection/user fail(无法通过 )
//            //其他未测试 
//            // discover endpoints from metadata
//            var client = new HttpClient();

//            var disco =  client.GetDiscoveryDocumentAsync("https://localhost:44376").Result;
//            if (disco.IsError)
//            {
//                Console.WriteLine(disco.Error);
//                return;
//            }

//            // request token
//            var tokenResponse =  client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
//            {
//                Address = disco.TokenEndpoint,
//                ClientId = "client",
//                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",

//                Scope = "api1"
//            }).Result;

//            if (tokenResponse.IsError)
//            {
//                Console.WriteLine(tokenResponse.Error);
//                return;
//            }

//            Console.WriteLine(tokenResponse.Json);
//            Console.WriteLine("\n\n");

//            // call api
//            var apiClient = new HttpClient();
//            apiClient.SetBearerToken(tokenResponse.AccessToken);

//            var response =  apiClient.GetAsync("https://localhost:44376/identity").Result;
//            if (!response.IsSuccessStatusCode)
//            {
//                Console.WriteLine(response.StatusCode);
//            }
//            else
//            {
//                var content =  response.Content.ReadAsStringAsync().Result;
//                Console.WriteLine(JArray.Parse(content));
//            }
//        }
//    }
//}
