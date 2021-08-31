using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility.WinForm
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationHelper.Start();
            //Application.Run(new MainForm());
            System.Windows.Forms.Application.Run(new Tool.MainForm());
        }
    }
}
