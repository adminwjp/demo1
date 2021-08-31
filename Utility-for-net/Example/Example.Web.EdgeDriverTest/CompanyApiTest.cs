using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Net.Http;

namespace Example.Web.EdgeDriverTest
{
    [TestClass]
    public class CompanyApiTest
    {
        string url = InitTest.url + "company/admin/api/v1/";
        [TestMethod]
        public void TestClass()
        {
            var res=HttpHelper.Get(url + "category/test");
            Console.WriteLine(res);
        }
    }
}
