using System.Windows.Controls;

namespace Tool.Ctrls
{
    public partial class StrCtrl : UserControl
    {
        public StrCtrl()
        {
            Grid grid = new Grid();
            this.Content = grid;
            this.Initial(grid);
            this.LoadData();
        }
    }
}
