using System;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;

namespace Remote
{
    public class RemoteObject : MarshalByRefObject
    {
        TcpServerChannel tcpServerChannel;
        public static Action<int> Action { get; set; }
        //单向异步
        //[OneWay]
        [SoapMethod(XmlNamespace = "MessageMarshal", SoapAction = "MessageMarshal#Add")] //http
        public int Add(int a, int b)
        {
            var res = a + b;
            Console.WriteLine(res);//http service 不执行 client 执行   tcp service 执行 client 不执行  
            Action?.Invoke(res);//http 执行
            return res;
        }
    }
}
