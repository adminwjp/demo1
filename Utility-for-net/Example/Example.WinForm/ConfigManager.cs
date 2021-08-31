using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.WinForm
{
    public class ConfigManager
    {
        public static readonly string templateApi =ConfigurationManager.AppSettings["templateApi"];

      
    }
}
