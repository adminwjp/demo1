using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Pages
{
    /// <summary>
    ///MaterialDesignThemes  dialog page 
    /// </summary>

    public class MDTDialogTemplatePage : DialogTemplatePage
    {
        /// <summary>
        /// 无
        /// </summary>
        public MDTDialogTemplatePage()
        {

        }
        /// <summary>
        /// dialog page
        /// </summary>
        /// <param name="listEntry">数据格式</param>
        public MDTDialogTemplatePage(ListEntry listEntry)
        {
           // DialogTemplateComponent = new MDTDialogTemplateComponent(listEntry) { Resources = this.Resources };
            this.Content = DialogTemplateComponent.DockPanelDialog;
        }
    }
}
