using System;
using Utility.IO;

namespace Utility.Test
{
    class Program
    {

         class a
        {
            public a()
            {
                Console.WriteLine("a");
            }
            public void show()
            {
                Console.WriteLine("a.show");
            }

            // public abstract void show1();
            public virtual void show1()
            {
                Console.WriteLine("a.show1");
            }
        }

        class b:a
        {
            public new  void show()
            {
                Console.WriteLine("b.show");
            }
            public override void show1()
            {
                Console.WriteLine("b.show1");
            }
        }
        class c : b
        {
            public c()
            {
                Console.WriteLine("c");
            }

            public new void show()
            {
                Console.WriteLine("c.show");
            }

            public override void show1()
            {
                Console.WriteLine("c.show1");
            }
        }
        class d : a
        {
            public d():base()
            {
                Console.WriteLine("d");
            }
            public new void show()
            {
                base.show();
                Console.WriteLine("d.show");
            }

            public override void show1()
            {
                base.show1();
                Console.WriteLine("d.show1");
            }
            public int cc { get; set; }
        }

        [STAThread]
        static void Main1(string[] args)
        {
            string str = FileHelper.ReadFile(@"E:\work\csharp\src\Utility\Utility\Example\Config\Model\BaseModel.cs").Replace("\"","\\\"");
            //DatabaseTest.Test();
            // System.Linq.Expressions.Expression<Func<d, int>> expression = it => it.cc;

            // Elasticsearch.Net.IElasticLowLevelClient elasticLowLevelClient=null;
            //var response= elasticLowLevelClient.DeleteByQuery<Elasticsearch.Net.StringResponse>("","", Elasticsearch.Net.PostData.String(""));
            // Nest.IElasticClient client = null;
            //Task.FromCanceled
            //a a1 = new a();//a
            //a1.show();//a.show
            //a1.show1();//a.show1
            //a a2 = new b();//a
            //a2.show(); ;//a.show
            //a2.show1(); ;//b.show1
            //a a3 = new c();//a c
            //a3.show(); ;//a.show
            //a3.show1(); ;//c.show1
            //a a4 = new d();//a d
            //a4.show(); ;//a.show
            //a4.show1(); ;//a.show1 d.show1

            //a a1 = new a();//a
            //a1.show();//a.show
            //a1.show1();//a.show1
            //b a2 = new b();//a
            //a2.show(); //b.show
            //a2.show1(); //b.show1
            //c a3 = new c();//a c
            //a3.show(); //c.show
            //a3.show1();//c.show1
            //d a4 = new d();//a d
            //a4.show();//a.show d.show
            //a4.show1();//a.show1 d.show1

            //var b = "\"b\"c=aa\"\"";
            //        Console.WriteLine("Hello World!");
            //        string pa= @"E:\work\csharp\src\Utility\Shared\Utility.Shared\Example\Config\Resources\ThreeLayers\Model\BaseModel.tpl";
            //        var a=FileUtils.ReadFile(pa);
            //        var str = "var str=\""+a.Replace("\"","\\\"")+"\";";
            // FileUtils.WriteFile("e:\\a.txt", str.Replace("'","\""));
            Console.WriteLine(11);
            Console.ReadKey();
        }

        //[STAThread]
        //static void Main2(string[] args)
        //{
        //    Service.BLL.ServiceStart.Reg( Service.BLL.ServiceStart.AutoWay.Autofac);
        //    //Service.Wcf.ServiceWcfHelper.ServiceManager = Service.BLL.ServiceStart.AutofacManager.Resolve<ServiceNhibernateManager>();
        //    ////Service.Wcf.ServiceWcfHelper.StopServer();
        //    //Service.Wcf.ServiceWcfHelper.RegisterServer();

        //    Service.Remote.ServiceRemoteHelper.ServiceManager = Service.BLL.ServiceStart.AutofacManager.Resolve<ServiceNhibernateManager>();
        //    Service.Remote.ServiceRemoteHelper.RegisterServer();

        //    //WcfConfig.WcfConfigHelper.ConfigInit();
        //    //WcfConfig.WcfConfigHelper.Start();
        //    //WcfConfig.WcfConfigManager.ServiceStart();
        //    // Main1();
        //    Console.WriteLine("Hello World!");
        //    Console.ReadKey();
        //}


        //static void Main1()
        //{
        //   //SocketTest();
        //    WcfTest();
        //    //RemoteTest();
        //    Console.WriteLine(11);
        //    Console.ReadKey();
        //}
        //private static void SocketTest()
        //{
        //    //SocketManager.TcpListenerConnect("127.0.0.1", 13000);
        //    //return;

        //    SocketServer socketServer = new SocketServer(10, 500);
        //    socketServer.Init();
        //    socketServer.Start(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 20001));
        //}
        //private static void WcfTest()
        //{
        //    try
        //    {
        //        using (Wcf.WcfHost wcfHost = new Wcf.WcfHost())
        //        {
        //            wcfHost.Open();
        //            Console.Read();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message + ex.StackTrace);
        //    }
        //}
        //private static void RemoteTest()
        //{
        //    //1.tcp：
        //    //第一种方式
        //    RemoteManager.RegisterActivatedServiceTcp<RemoteObject>(20001, "test");//http 不支持
        //    //第2种方式
        //    // RemoteManager.RegisterServiceTcp<RemoteObject>(20001, "test");
        //    //第3种方式 配置的
        //    // RemoteManager.RegisterServiceTcpConfig("Remote.Server.exe.config");

        //    //2.Http：
        //    //第一种方式
        //    // RemoteManager.RegisterServiceHttp<RemoteObject>(20001, "test");
        //    //第2种方式 配置的
        //    //RemoteManager.RegisterServiceHttpConfig("Remote.Server.exe.config");
        //    RemoteObject.Action = (it) => { Console.WriteLine("service {0}", it); };
        //}
    }
}
