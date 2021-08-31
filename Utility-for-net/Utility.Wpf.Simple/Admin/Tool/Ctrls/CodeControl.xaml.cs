using System.Text;
using System.Windows;
using System.Windows.Controls;
using Utility.Wpf;

namespace Tool.Ctrls
{
    /// <summary>
    /// CodeControl.xaml 的交互逻辑
    /// </summary>
    public partial class CodeControl : UserControl
    {
        public CodeControl()
        {
            InitializeComponent();
        }

        private void GeneratorClcik(object sender, RoutedEventArgs e)
        {
            var origin = WpfHelper.GetRichTexboxTextFormat(this.originVal);
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(origin))
            {
                var strs = origin.Split('\n');
            }
        }

        private void CleanClcik(object sender, RoutedEventArgs e)
        {
            this.originVal.Document.Blocks.Clear();
            this.newVal.Document.Blocks.Clear();
        }
    }
}
