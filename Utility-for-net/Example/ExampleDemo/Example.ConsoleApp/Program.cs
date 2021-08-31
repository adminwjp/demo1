using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Net;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            
            HttpServerHelper httpServer = new HttpServerHelper();
            httpServer.Listener.Prefixes.Add("http://*:8081/");
            httpServer.Start(null);
            Console.ReadKey();
        }

        public static void TestQuestion2()
        {
            //Question2.InitConnect();
            //Question2.InitData();
            //Question2.ListByGroupByTypeAndMaxNum();
        }

        public static void TestQuestion3()
        {
            //Question2.InitConnect();
            //Question3.InitData();
            //Question3.ListByYearAndQuarterBy1Or2Or3Or4();
        }

        public static void TestQuestion4()
        {
            //Question2.InitConnect();
            //Question4.InitData();
            //Question4.DeleteRepeat();
        }
    }
}
