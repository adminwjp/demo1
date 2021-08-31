using System;
using System.Windows;
using System.Windows.Input;
using Utility.Wpf;
using Utility.Wpf.Ctrls;

namespace Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainFactoty _mainFactoty;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this._mainFactoty = new MainFactoty();
            this._mainFactoty.TabControl = this.tabs;
            this._mainFactoty.MainMenu(this.mainMenu);
            this._mainFactoty.MainTree(this.mainTree);
            this._mainFactoty.Start(this.show);
            Console.SetOut(new RichTextBoxWriter(this.RichTextBoxLog));
        
        }
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tabs.Items.Refresh();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var show = MessageBox.Show("是否关闭应用程序?", "提示：", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (show == MessageBoxResult.OK)
            {
                this._mainFactoty.Stop();
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static void Start()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.ShowDialog();
        }
    }
}
