using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Utility.Wpf.Entries;
using Utility.Wpf.ViewModels;
using System.Collections.ObjectModel;

namespace Utility.Wpf.Components
{
    /// <summary>
    /// MaterialDesignThemes 表格模板
    /// </summary>
    public class MDTListTemplateComponent : ListTemplateComponent
    {

        /// <summary>
        /// dialog
        /// </summary>
        public UserControl Dialog { get; set; }

        //MaterialDesignThemes.Wpf.DialogHost dialogHost = new MaterialDesignThemes.Wpf.DialogHost() { };


        /// <summary>
        /// 默认 
        /// </summary>
        public MDTListTemplateComponent() : base(ComponentFlag.MaterialDesignThemesControl,false)
        {
        }

        /// <summary>
        /// 默认初始化
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="methodTemplateEntry"></param>

        public MDTListTemplateComponent(ListEntry listModel, MethodTemplateEntry methodTemplateEntry) : base(listModel, methodTemplateEntry, ComponentFlag.MaterialDesignThemesControl, false)
        {
            IsMaterialDesignTheme = true;
            DialogTemplateComponent = new MDTDialogTemplateComponent(listModel);
        }
        /// <summary>
        /// 默认初始化 
        /// </summary>
        /// <param name="muilDataEntry"></param>
        /// <param name="methodTemplateEntry"></param>
        public MDTListTemplateComponent(MuilDataEntry muilDataEntry, MethodTemplateEntry methodTemplateEntry) : base(muilDataEntry, methodTemplateEntry, ComponentFlag.MaterialDesignThemesControl, false)
        {

        }
        
        /// <summary>
        /// 表单 显示
        /// </summary>
        protected override  void FormShow()
        {
            if (FormDialogOpen != null)
            {
                FormDialogOpen.Invoke();
                return;
            }
           // var result = await MaterialDesignThemes.Wpf.DialogHostEx.ShowDialog(dialogHost,Dialog);//无法 显示
            //var result = await DialogHost.Show(Dialog, "RootDialog");
        }

        /// <summary>
        /// 设置 MaterialDataGridTextColumn
        /// </summary>
        /// <param name="column"></param>

        protected override void SeDataGridColumnText(ColumnEntry column)
        {
            MaterialDesignThemes.Wpf.DataGridTextColumn dataGridTextColumn = GetMdtDataGridColumnText(column);
            this.DataGridList.Columns.Add(dataGridTextColumn);
        }

        /// <summary>
        ///设置  DataGridTextColumn
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected virtual MaterialDesignThemes.Wpf.DataGridTextColumn GetMdtDataGridColumnText(ColumnEntry column)
        {
            MaterialDesignThemes.Wpf.DataGridTextColumn dataGridTextColumn = GetDataGridColumnText<MaterialDesignThemes.Wpf.DataGridTextColumn>(column);
            dataGridTextColumn.MaxLength = column.MaxLength;
            //dataGridTextColumn.EditingElementStyle = (Style)this.Resources["MaterialDesignDataGridTextColumnEditingStyle"];
            //dataGridTextColumn.EditingElementStyle = (Style)this.Resources["MaterialDesignDataGridTextColumnPopupEditingStyle"];
            return dataGridTextColumn;
        }

        /// <summary>
        /// 设置  MaterialDataGridTextColumn
        /// </summary>
        /// <param name="column"></param>
        protected virtual void SeDataGridColumnTextNumber(ColumnEntry column)
        {
            MaterialDesignThemes.Wpf.DataGridTextColumn dataGridTextColumn = GetMdtDataGridColumnText(column);
            //dataGridTextColumn.HeaderStyle = new Style() {TargetType=typeof(DataGridColumnHeader), /*BasedOn =(Style)this.Resources["MaterialDesignDataGridColumnHeader"]*/ };
            //dataGridTextColumn.HeaderStyle.Setters.Add(new Setter() { Property = DataGridColumnHeader.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center });
            //var textBlock = new TextBlock() { TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Right };
            //dataGridTextColumn.HeaderStyle.Setters.Add(new Setter() { 
            //    Property = MaterialDesignThemes.Wpf.DataGridTextColumn.HeaderTemplateProperty, 
            //    Value =new DataTemplate()
            //    {
            //        DataType = textBlock.GetType()

            //    }
            //});
            // dataGridTextColumn.ElementStyle = new Style() {/* TargetType=typeof(TextBlock)*/ };
            //dataGridTextColumn.ElementStyle.Setters.Add(new Setter() { Property = TextBlock.HorizontalAlignmentProperty, Value = HorizontalAlignment.Right });
            this.DataGridList.Columns.Add(dataGridTextColumn);
        }

        /// <summary>
        /// 设置 DataGridComboBoxColumn
        /// </summary>
        /// <param name="column"></param>
        protected override void SetDataGridColumnCombox(ColumnEntry column)
        {
            MaterialDesignThemes.Wpf.DataGridComboBoxColumn dataGridComboBoxColumn = GetDataGridColumnCombox<MaterialDesignThemes.Wpf.DataGridComboBoxColumn>(column);
            dataGridComboBoxColumn.IsEditable = !Disabled && column.Flag == ColumnEditFlag.Edit;
            this.DataGridList.Columns.Add(dataGridComboBoxColumn);
        }

    }

}
