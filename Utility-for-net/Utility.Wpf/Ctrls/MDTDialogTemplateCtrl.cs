using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    ///MaterialDesignThemes dialog 用户 控件
    /// </summary>
    public class MDTDialogTemplateCtrl: DialogTemplateCtrl
    {

        /// <summary>
        /// MaterialDesignThemes dialog 用户 控件
        /// </summary>
        public MDTDialogTemplateCtrl()
        {

        }
        /// <summary>
        /// MaterialDesignThemes dialog 用户 控件
        /// </summary>
        /// <param name="listEntry"></param>
        public MDTDialogTemplateCtrl(ListEntry listEntry)
        {
            base.DialogTemplateComponent = new MDTDialogTemplateComponent(listEntry) { Resources = this.Resources };
        }
    }
}
