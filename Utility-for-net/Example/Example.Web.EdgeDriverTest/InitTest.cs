using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Web.EdgeDriverTest
{
    [TestClass]
    public class InitTest
    {
        public static string url = "http://127.0.0.1:6002/";

        public static string SuccessRegex = "\"success\":true";
        public static string TokenRegex = "\"token\":\"(.*?)\",";
        [TestInitialize]
        public void Init()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
