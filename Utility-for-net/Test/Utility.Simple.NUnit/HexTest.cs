using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Utility.Test
{
    /// <summary>
    /// 单元测试 最好 写下 之前写的 算了  迁移过来 可能有各种问题 出现问题再解决
    /// </summary>
   public class HexTest
    {
        [Test]
        public void TestHex()
        {
            //每个 语言 包装 的使用方式不同 
            
            //js 61
            //"a".charCodeAt(0).toString("16")
            var str = HexHelper.ToHex(Encoding.UTF8.GetBytes("a"));
            Console.WriteLine(str);
            Assert.IsTrue(str == "61");
        }

    }
}
