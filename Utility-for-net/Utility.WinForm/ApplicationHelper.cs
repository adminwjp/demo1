using System;
using System.Windows.Forms;

namespace Utility.WinForm
{
    public  class ApplicationHelper
    {
        public static void Start()
        {
            System.Windows.Forms.Application.ThreadException -= Application_ThreadException;
            System.Windows.Forms.Application.ApplicationExit -= Application_ApplicationExit;
            System.Windows.Forms.Application.Idle -= Application_Idle;
            System.Windows.Forms.Application.ThreadExit -= Application_ThreadExit;

            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
            System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
            System.Windows.Forms.Application.Idle += Application_Idle;
            System.Windows.Forms.Application.ThreadExit += Application_ThreadExit;
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0
            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        }
        private static void Application_ThreadExit(object sender, EventArgs e)
        {

        }

        private static void Application_Idle(object sender, EventArgs e)
        {


        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {


        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

        }
    }
}
