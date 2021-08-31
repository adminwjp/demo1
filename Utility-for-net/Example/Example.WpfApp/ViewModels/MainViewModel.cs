using System.Collections.Generic;
using System.Windows.Controls;
using Utility.Wpf.Ctrls;

namespace Example.WpfApp.ViewModels
{
    public class MainViewModel
    {
       public  LinkedList<UserControl> UserControls = new LinkedList<UserControl>() { 
        
       };
        public HomeCtrl Main = new HomeCtrl("config/main.xml") { IsShow=true};
        public static readonly MainViewModel mainViewModel = new MainViewModel();
    }
}
