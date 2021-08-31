

using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using Tool;
using Utility.Wpf;
using Utility.Wpf.Ctrls;

namespace Wpf
{
    public class AppStart: WpfApplication
    {
        public AppStart()
        {
            //this.StartupUri = new System.Uri("Demo/Test.xaml", System.UriKind.Relative);
            //this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
        }
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
#if NETCOREAPP3_1
            //XamlDisplay.Init();
#endif
            //方案1 
            //Tool.MainWindow.Start();

            //StartManager.Initial();
           // ConfigManager.Load();

            //方案2
            HomeCtrl homeCtrl = new HomeCtrl("Config//Menu.Xml");
            Window mainWindow = new Window() { Content = homeCtrl };
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.ShowDialog();



            //Wpf.AppStart app = new Wpf.AppStart();
            //app.Run();
        }
    }
}
