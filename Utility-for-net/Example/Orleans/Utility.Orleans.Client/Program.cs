using Tasks.Interfaces;
using System;
using Orleans;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using Orleans.Configuration;

namespace Tasks.Client
{
    /// <summary>
    ///参考 Orleans 实现
    ///https://github.com/dotnet/Orleans
    ///https://dotnet.github.io/orleans/
    ///Phenix https://github.com/phenixiii/Phenix.NET7/blob/master/Phenix.Client/HttpClient.cs
    /// </summary>
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    IClusterClient client = new ClientBuilder()
        //       .UseLocalhostClustering()
        //       .UseStaticClustering(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40000))
        //       .Configure<ClusterOptions>(opt =>
        //       {
        //           opt.ClusterId = "TestCluster";
        //           opt.ServiceId = "TestServer";
        //       })
        //        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ITestGrain).Assembly))
        //       .Build();
        //    client.Connect(ex =>
        //    {
        //        if (ex != null)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        return Task.FromResult(true);
        //    }).GetAwaiter().GetResult();
        //    ITestGrain test = client.GetGrain<ITestGrain>("test");
        //    string name =  test.SayHello("1").Result;
        //    Console.WriteLine("name：" + name);
        //    string name1 =  test.SayHello("2").Result;
        //    Console.WriteLine("name1：" + name1);
        //    string name2 =  test.SayHello("3").Result;
        //    Console.WriteLine("name2：" + name2);
        //    Console.ReadKey();
        //}
        public static Task Main(string[] args)
        {
            //最好 在 git 上下载源码 使用 文档 不是最新的
            // GrainClient.Initialize();
            // //然后通过创建代理对象，并调用Grains的方法来发送消息
            // var grain = GrainClient.GrainFactory.GetGrain<ITestGrain>("grain1");
            //await grain.SayHello("World");
            return new HostBuilder()
         .ConfigureServices(services =>
         {
             services.AddSingleton<ClusterClientHostedService>();
             services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
             services.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);

             services.AddHostedService<TaskClientHostedService>();

             services.Configure<ConsoleLifetimeOptions>(options =>
             {
                 options.SuppressStatusMessages = true;
             });
         })
         .ConfigureLogging(builder =>
         {
             builder.AddConsole();
         })
         .RunConsoleAsync();
        }
    }
}
