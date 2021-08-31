using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Example.WpfApp.ViewModels;

namespace Example.WpfApp
{
    public class MainWindow1 : Window
    {
        public static Snackbar Snackbar=new Snackbar() { MessageQueue= new SnackbarMessageQueue()};

        Grid _grid = new Grid();
        public MainWindow1()
        {
            //麻烦  dialog 通信
      /*      Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                MainWindow1.Snackbar.MessageQueue.Enqueue("欢迎 来到 wpf 世界 !");
            }, TaskScheduler.FromCurrentSynchronizationContext());*/

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this._grid.Children.Add(MainViewModel.mainViewModel.Main);
            this.Content = this._grid;
        }
    }
}
