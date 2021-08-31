using System;
using Xunit;
using Xunit.Abstractions;

namespace Utility.Ef.Xunit
{
    //编译错误 还 没结束 ？ 垃圾 vs 非要 我重启
    //杀死 进程 重新 启动 老是 出现 这种 情况 go python  php 启动运行快点 报错 位置 不好找
    //自己 写的 东西 乱七 乱七八糟 改动 麻烦
    public class UnitTest1
    {
        ITestOutputHelper testOutput;

        public UnitTest1(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }
        //[Fact]
        public void Test1()
        {
            //app.config null
            // testOutput.WriteLine(System.Configuration.ConfigurationManager.AppSettings["a"]);
            //web.config null
            //testOutput.WriteLine(System.Configuration.ConfigurationManager.AppSettings["b"]);
        }
    }
}
