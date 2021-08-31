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
        /// ��
        /// </summary>
        public MDTDialogTemplatePage()
        {

        }
        /// <summary>
        /// dialog page
        /// </summary>
        /// <param name="listEntry">���ݸ�ʽ</param>
        public MDTDialogTemplatePage(ListEntry listEntry)
        {
           // DialogTemplateComponent = new MDTDialogTemplateComponent(listEntry) { Resources = this.Resources };
            this.Content = DialogTemplateComponent.DockPanelDialog;
        }
    }
}
