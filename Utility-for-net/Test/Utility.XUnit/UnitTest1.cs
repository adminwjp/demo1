using System;
using Xunit;
using Xunit.Abstractions;

namespace Utility.Ef.Xunit
{
    //������� �� û���� �� ���� vs ��Ҫ ������
    //ɱ�� ���� ���� ���� ���� ���� ���� ��� go python  php �������п�� ���� λ�� ������
    //�Լ� д�� ���� ���� ���߰��� �Ķ� �鷳
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
