using System.Windows.Controls;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    /// 表格 数据 (dialog 表单)
    /// </summary>
    public class ListTemplateCtrl : UserControl
    {
        /// <summary>
        /// 表格 数据
        /// </summary>
        public ListTemplateComponent ListTemplateComponent { get; set; }// = new ListTemplateComponent();
        /// <summary>
        /// 表格 数据
        /// </summary>
        public ListTemplateCtrl()
        {

        }
        /// <summary>
        /// 多组 表格 数据 组合
        /// </summary>
        /// <param name="muilDataEntry">多组 表格 数据</param>
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
        /// 初始化 布局 以及 数据展示 
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
            listTemplateComponent.Initial(listModel, methodTemplate);//初始化
            ListTemplateComponent = listTemplateComponent;

            listTemplateComponent.DialogBind += (it) => {
                listTemplateComponent.FormDialog.DataContext = it;
            };
            dialogTemplateComponent.Initial();

            this.Content = listTemplateComponent.DockPanelList;

        }

    }
}
