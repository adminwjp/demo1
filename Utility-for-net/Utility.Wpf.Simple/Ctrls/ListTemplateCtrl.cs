using System.Windows.Controls;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    /// ��� ���� (dialog ��)
    /// </summary>
    public class ListTemplateCtrl : UserControl
    {
        /// <summary>
        /// ��� ����
        /// </summary>
        public ListTemplateComponent ListTemplateComponent { get; set; }// = new ListTemplateComponent();
        /// <summary>
        /// ��� ����
        /// </summary>
        public ListTemplateCtrl()
        {

        }
        /// <summary>
        /// ���� ��� ���� ���
        /// </summary>
        /// <param name="muilDataEntry">���� ��� ����</param>
        /// <param name="methodTemplateEntry"></param>
        public ListTemplateCtrl(MuilDataEntry muilDataEntry,MethodTemplateEntry methodTemplateEntry)
        {
            ListTemplateComponent = new ListTemplateComponent(muilDataEntry, methodTemplateEntry, ComponentFlag.Control,false);
            this.Content = ListTemplateComponent.DockPanelList;
            this.DataContext = ListTemplateComponent.DataListViewModel;

            //init layou 
            Set(muilDataEntry.Data[0].Lists[0],methodTemplateEntry);
        }

        /// <summary>
        /// ��ʼ�� ���� �Լ� ����չʾ 
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="methodTemplate"></param>
        public virtual void Set(ListEntry listModel, MethodTemplateEntry methodTemplate)
        {

            var listTemplateComponent = new ListTemplateComponent(listModel, methodTemplate, ComponentFlag.Control,false);

            var dialogTemplateComponent = listTemplateComponent.DialogTemplateComponent;
            listTemplateComponent.DialogTemplateComponent = dialogTemplateComponent;
            listTemplateComponent.FormDialog.Content = dialogTemplateComponent.DockPanelDialog;


            listTemplateComponent.Resources = base.Resources;

            this.DataContext = listTemplateComponent.DataListViewModel;
            listTemplateComponent.Initial(listModel, methodTemplate);//��ʼ��
            ListTemplateComponent = listTemplateComponent;

            listTemplateComponent.DialogBind += (it) => {
                listTemplateComponent.FormDialog.DataContext = it;
            };
            dialogTemplateComponent.Initial();

            this.Content = listTemplateComponent.DockPanelList;

        }

    }
}
