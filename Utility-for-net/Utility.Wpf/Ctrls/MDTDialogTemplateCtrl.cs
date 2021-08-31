using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    ///MaterialDesignThemes dialog �û� �ؼ�
    /// </summary>
    public class MDTDialogTemplateCtrl: DialogTemplateCtrl
    {

        /// <summary>
        /// MaterialDesignThemes dialog �û� �ؼ�
        /// </summary>
        public MDTDialogTemplateCtrl()
        {

        }
        /// <summary>
        /// MaterialDesignThemes dialog �û� �ؼ�
        /// </summary>
        /// <param name="listEntry"></param>
        public MDTDialogTemplateCtrl(ListEntry listEntry)
        {
            base.DialogTemplateComponent = new MDTDialogTemplateComponent(listEntry) { Resources = this.Resources };
        }
    }
}
