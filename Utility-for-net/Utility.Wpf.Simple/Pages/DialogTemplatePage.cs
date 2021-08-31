using System.Windows.Controls;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Pages
{
    /// <summary>
    /// dialog page
    /// </summary>
    public  class DialogTemplatePage: System.Windows.Controls.Page
    {
        /// <summary>
        /// dialog 组件
        /// </summary>
        public DialogTemplateComponent DialogTemplateComponent { get; set; }

        /// <summary>
        /// 无
        /// </summary>
        public DialogTemplatePage()
        {

        }
        /// <summary>
        /// dialog page
        /// </summary>
        /// <param name="listEntry">数据格式</param>
        public DialogTemplatePage(ListEntry listEntry)
        {
            DialogTemplateComponent = new DialogTemplateComponent(listEntry) { Resources = this.Resources };
            this.Content = DialogTemplateComponent.DockPanelDialog;
        }
    }
}
