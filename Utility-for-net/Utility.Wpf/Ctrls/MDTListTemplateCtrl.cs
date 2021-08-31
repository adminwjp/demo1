using MaterialDesignThemes.Wpf;
using Utility.Wpf.Components;
using Utility.Wpf.Entries;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    /// MaterialDesignThemes 表格 数据(dialog 表单)
    /// </summary>
    public class MDTListTemplateCtrl: ListTemplateCtrl
    {
        DialogHost dialogHost = new DialogHost() { };
        /// <summary>
        /// MaterialDesignThemes 表格 数据 (dialog 表单)
        /// </summary>
        public MDTListTemplateCtrl()
        {

        }
        /// <summary>
        /// MaterialDesignThemes 表格 数据 (dialog 表单)
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="methodTemplate"></param>
        public  MDTListTemplateCtrl(ListEntry listModel, MethodTemplateEntry methodTemplate)
        {
            Set(listModel, methodTemplate);
            Show();
        }

        /// <summary>
        /// 东西 一动 出现 问题 坑
        /// </summary>
        private   void Show()
        {
           // var result =await dialogHost.ShowDialog(this);//test not pass
        }

        ///// <summary>
        ///// 初始化 布局 以及 数据展示 
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
        //        var result = dialogHost.ShowDialog(materialDesignThemesDialogTemplate);//dialog 显示 不出来 不知道 啥 原因  难道 包装 过度 导致 显示 不出来(获取不到 window 对象) ？
        //    };
        //    mDTListTemplateComponent.Dialog = materialDesignThemesDialogTemplate;
        //    dialogTemplateComponent.Resources = materialDesignThemesDialogTemplate.Resources;
        //    this.DataContext = mDTListTemplateComponent.DataListViewModel;
        //    mDTListTemplateComponent.Initial(listModel, methodTemplate);//初始化
        //    ListTemplateComponent = mDTListTemplateComponent;

        //    mDTListTemplateComponent.DialogBind += (it) => {
        //        materialDesignThemesDialogTemplate.DataContext = it;
        //    };
        //    dialogTemplateComponent.Initial();//初始化 dialog

        //    this.Content = mDTListTemplateComponent.DockPanelList;
        //}
    }
}
