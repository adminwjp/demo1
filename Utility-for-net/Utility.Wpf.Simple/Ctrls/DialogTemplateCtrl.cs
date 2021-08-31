using System.Windows.Controls;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    /// dialog 用户 控件
    /// </summary>
    public class DialogTemplateCtrl: UserControl
    {
        /// <summary>
        /// dialog 组件
        /// </summary>
        public DialogTemplateComponent DialogTemplateComponent { get; set; }

        /// <summary>
        /// dialog 用户 控件
        /// </summary>
        public DialogTemplateCtrl()
        {

        }
        /// <summary>
        /// dialog 用户 控件
        /// </summary>
        /// <param name="listEntry">数据格式</param>
        public DialogTemplateCtrl(ListEntry listEntry)
        {
            DialogTemplateComponent=new DialogTemplateComponent(listEntry) { Resources=this.Resources};

        }
        /// <summary>
        ///  初始化 布局 以及 事件 绑定
        /// </summary>
        public virtual void Initail()
        {
            DialogTemplateComponent.Initial();
            this.Content = DialogTemplateComponent.DockPanelDialog;
        }
    }
}
