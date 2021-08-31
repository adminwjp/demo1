#if NETCOREAPP3_1 || NET5_0  || NET6_0
//using ShowMeTheXAML;
#endif
using Utility.Wpf;
using Example.WpfApp.Common;
using Utility.Ioc;
using Utility.Wpf.Demo;

namespace Example.WpfApp
{
    public class AppStart: WpfApplication
    {
        public AppStart()
        {
            IocManager = AutofacIocManager.Instance;
            //invalide
            //this.StartupUri = new System.Uri("Demo/Test.xaml", System.UriKind.Relative);
            //this.StartupUri = new System.Uri("Login.xaml", System.UriKind.Relative);
        }
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
#if NETCOREAPP3_1 || NET5_0  || NET6_0
            //XamlDisplay.Init();
#endif
            Wpf.AppStart app = new Wpf.AppStart();
            StartManager.Start();
            app.Run(new Login());
        }
    }
}
