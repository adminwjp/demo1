using System.Windows.Controls;

namespace Tool.Ctrls
{
    public partial class PlatCtrl:UserControl
    {
        public PlatCtrl()
        {
            Grid grid = new Grid();
            this.Content = grid;
            this.Initial(grid);
            this.LoadData();
        }
    }
}
