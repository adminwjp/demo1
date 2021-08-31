using System.Windows.Controls;
using Utility.Wpf.Components;

namespace Utility.Wpf.Pages
{
    /// <summary>
    /// 表格 page
    /// </summary>
    public class ListTemplatePage : System.Windows.Controls.Page
    {
        /// <summary>
        /// 表格 组件
        /// </summary>
        public ListTemplateComponent ListTemplateComponent = new ListTemplateComponent( ComponentFlag.Page,false);

    }
}
