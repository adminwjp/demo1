using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinForm.Process
{
    public class ProcessUtils
    {
        static Process.ProcessMainForm processMain = (Process.ProcessMainForm)null;
        static Process.ProcessBarForm processBar =  (Process.ProcessBarForm)null;
        public static void Show()
        {
            processMain =  new Process.ProcessMainForm();
            processMain.Show();
            processBar =  new Process.ProcessBarForm();
            processBar.ShowDialog();
        }
        public static void Close()
        {
            Thread.Sleep(2 * 1000);
            var temp=processBar.TopLevelControl;
            processBar?.CloseForm();
            processMain?.CloseForm();
        }
    }
}
