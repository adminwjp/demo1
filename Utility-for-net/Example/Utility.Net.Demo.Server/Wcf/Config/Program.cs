using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config.Wcf.Server
{
   public  class Program
    {
        public static void Main(string[] args)
        {
            ConfigWcfHelper.Instance.RegisterServer();
            Console.WriteLine("wcf config starting,any key quit ");
            Console.ReadKey();
        }
    }
}
