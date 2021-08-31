using Config.GrpcService;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Config.GrpcClient
{
    public class ConfigClient
    {
        static void Main(string[] args)
        {
            //https://docs.microsoft.com/zh-cn/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1#call-a-grpc-service-with-an-untrustedinvalid-certificate
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);//http 才可以 其他不支持
            //https://www.cnblogs.com/hulizhong/p/11586053.html
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:50001", new GrpcChannelOptions { HttpHandler = httpClientHandler });
            var client = new Greeter.GreeterClient(channel);
            var reply = client.SayHello(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
