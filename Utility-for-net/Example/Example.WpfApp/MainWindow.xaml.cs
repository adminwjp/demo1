using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Example.WpfApp.ViewModels;

namespace Example.WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Snackbar Snackbar;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue.Enqueue("欢迎 来到 wpf 世界 !");
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Snackbar = this.MainSnackbar;
        }
        public void Initial()
        {
          
        }
        //个人中心
        private void PersonCenter_OnClick(object sender, RoutedEventArgs e)
        {

        }
        //个人设置
        private void PersonSetting_OnClick(object sender, RoutedEventArgs e)
        {

        }
        //注册账号
        private void Register_OnClick(object sender, RoutedEventArgs e)
        {

        }
        //修改密码
        private void ModifyPwd_OnClick(object sender, RoutedEventArgs e)
        {

        }
        //退出登录
        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
