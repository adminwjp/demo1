using System.Windows.Controls;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    /// dialog �û� �ؼ�
    /// </summary>
    public class DialogTemplateCtrl: UserControl
    {
        /// <summary>
        /// dialog ���
        /// </summary>
        public DialogTemplateComponent DialogTemplateComponent { get; set; }

        /// <summary>
        /// dialog �û� �ؼ�
        /// </summary>
        public DialogTemplateCtrl()
        {

        }
        /// <summary>
        /// dialog �û� �ؼ�
        /// </summary>
        /// <param name="listEntry">���ݸ�ʽ</param>
        public DialogTemplateCtrl(ListEntry listEntry)
        {
            DialogTemplateComponent=new DialogTemplateComponent(listEntry) { Resources=this.Resources};

        }
        /// <summary>
        ///  ��ʼ�� ���� �Լ� �¼� ��
        /// </summary>
        public virtual void Initail()
        {
            DialogTemplateComponent.Initial();
            this.Content = DialogTemplateComponent.DockPanelDialog;
        }
    }
}
