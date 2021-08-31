using System;
using System.Windows;
using Utility;
using Utility.Wpf.Demo.ViewModels;
#if !NETCOREAPP3_0 || !NETCOREAPP3_1 || !NET5_0
//using System.Windows.Forms;
#endif

namespace Utility.Wpf.Demo
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
       public  UserLoginViewModel LoginViewModel=>new UserLoginViewModel()
        {
            Account = "admin",
            Pwd = "123456",
            Rember = true,
            AutoLogin = false
        };
#if !NETCOREAPP3_0 || !NETCOREAPP3_1 || !NET5_0
        //NotifyIcon _notifyIcon;
        #endif
        bool _auto = true;

        public Action LoginEvent { get; set; }
        public Login()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.ShowInTaskbar = false;
            this.DataContext = this.LoginViewModel;
            this.StateChanged += Login_StateChanged;
#if !NETCOREAPP3_0 || !NETCOREAPP3_1 || !NET5_0
            //_notifyIcon = NotifyIconHelper.NotifyIcon;
#endif
            if(LoginViewModel.AutoLogin)
            {
                LoginClick(null, null);
            }
            else
            {
                _auto = false;
            }
        }

        private void Login_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                //this.Hide();
            }
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            //if (!LoginViewModel.ValidateLogin(_auto))
            //{
            //    _auto = false;
            //    return;
            //}

            //_auto = false;
            //var mainWindow = new MainWindow1();
            //this.Close();
            //mainWindow.ShowDialog();
            LoginEvent?.Invoke();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
