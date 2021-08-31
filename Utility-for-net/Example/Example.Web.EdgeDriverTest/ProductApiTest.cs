using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Helpers;
using Utility.Json;
using Utility.Net.Http;

namespace Example.Web.EdgeDriverTest
{
    [TestClass]
    public class ProductApiTest
    {
        String url = InitTest.url + "admin/api/v1/";
        //[TestMethod]
        public void TestProduct()
        {
           var  res = HttpHelper.PostJson(url  + "product/add", JsonHelper.ToJson(new { Title = "123", SearchKey = "123" },
               JsonHelper.JsonSerializerSettings));
            if (RegexHelper.IsMatch(res, InitTest.SuccessRegex))
            {
                Console.WriteLine("add product success");
            }
            else
            {
                Console.WriteLine(res);
                Console.WriteLine("add product fail");
            }
          
        }
    }
}
