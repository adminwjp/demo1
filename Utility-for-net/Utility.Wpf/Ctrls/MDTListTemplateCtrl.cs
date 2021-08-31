using MaterialDesignThemes.Wpf;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    /// MaterialDesignThemes ��� ����(dialog ��)
    /// </summary>
    public class MDTListTemplateCtrl: ListTemplateCtrl
    {
        DialogHost dialogHost = new DialogHost() { };
        /// <summary>
        /// MaterialDesignThemes ��� ���� (dialog ��)
        /// </summary>
        public MDTListTemplateCtrl()
        {

        }
        /// <summary>
        /// MaterialDesignThemes ��� ���� (dialog ��)
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="methodTemplate"></param>
        public  MDTListTemplateCtrl(ListEntry listModel, MethodTemplateEntry methodTemplate)
        {
            Set(listModel, methodTemplate);
            Show();
        }

        /// <summary>
        /// ���� һ�� ���� ���� ��
        /// </summary>
        private   void Show()
        {
           // var result =await dialogHost.ShowDialog(this);//test not pass
        }

        ///// <summary>
        ///// ��ʼ�� ���� �Լ� ����չʾ 
        ///// </summary>
        ///// <param name="listModel"></param>
        ///// <param name="methodTemplate"></param>
        //public override void Set(ListEntry listModel, MethodTemplateEntry methodTemplate)
        //{

        //    var mDTListTemplateComponent = new MDTListTemplateComponent(listModel, methodTemplate);

        //    var dialogTemplateComponent = mDTListTemplateComponent.DialogTemplateComponent;

        //    mDTListTemplateComponent.Resources = base.Resources;
        //    var materialDesignThemesDialogTemplate = new MDTDialogTemplateCtrl();
        //    mDTListTemplateComponent.FormDialogOpen = () => {
        //        //var result=DialogHost.Show(materialDesignThemesDialogTemplate).Result;
        //        var result = dialogHost.ShowDialog(materialDesignThemesDialogTemplate);//dialog ��ʾ ������ ��֪�� ɶ ԭ��  �ѵ� ��װ ���� ���� ��ʾ ������(��ȡ���� window ����) ��
        //    };
        //    mDTListTemplateComponent.Dialog = materialDesignThemesDialogTemplate;
        //    dialogTemplateComponent.Resources = materialDesignThemesDialogTemplate.Resources;
        //    this.DataContext = mDTListTemplateComponent.DataListViewModel;
        //    mDTListTemplateComponent.Initial(listModel, methodTemplate);//��ʼ��
        //    ListTemplateComponent = mDTListTemplateComponent;

        //    mDTListTemplateComponent.DialogBind += (it) => {
        //        materialDesignThemesDialogTemplate.DataContext = it;
        //    };
        //    dialogTemplateComponent.Initial();//��ʼ�� dialog

        //    this.Content = mDTListTemplateComponent.DockPanelList;
        //}
    }
}
