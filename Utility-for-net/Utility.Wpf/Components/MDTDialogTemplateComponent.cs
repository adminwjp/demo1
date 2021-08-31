using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Utility.Wpf.Entries;
using Utility.Wpf.Validations;

namespace Utility.Wpf.Components
{
    /// <summary>
    /// MaterialDesignThemes dialog 组装
    /// </summary>
    public class MDTDialogTemplateComponent : DialogTemplateComponent
    {

        /// <summary>
        /// dialog 数据源 双向绑定
        /// </summary>
        /// <param name="listEntry"></param>
        public MDTDialogTemplateComponent(ListEntry listEntry) : base(listEntry)
        {
            IsMaterialDesignTheme = true;
        }
        /// <summary>
        /// 无
        /// </summary>
        public MDTDialogTemplateComponent()
        {
            IsMaterialDesignTheme = true;
        }
        /// <summary>
        /// ComboBox 提示
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="tip"></param>
        protected override void ComboxHit(ComboBox comboBox, string tip)
        {
            HintAssist.SetHint(comboBox, tip);
        }
        /// <summary>
        /// ComboBox 错误提示
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="name"></param>
        protected override void ComboxErrorTip(ComboBox comboBox, string name)
        {
            HintAssist.SetHelperText(comboBox, $"{name} 不能为空 !");
        }
        /// <summary>
        /// TextBox 提示
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="tip"></param>
        protected override void TextboxHit(TextBox textBox, string tip)
        {
            HintAssist.SetHint(textBox, tip);
        }
        /// <summary>
        ///  TextBox 错误提示
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="name"></param>
        protected override void TextboxErrorTip(TextBox textBox, string name)
        {
            HintAssist.SetHelperText(textBox, $"{name} 不能为空 !");
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="stackPanel"></param>
        protected override void CloseIcon(StackPanel stackPanel)
        {
            PackIcon packIcon = new PackIcon() { Margin = new Thickness(10, 0, 10, 0), Kind = PackIconKind.WindowClose };
            stackPanel.Children.Add(packIcon);
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="button"></param>
        protected override void CloseButton(Button button)
        {
            button.Command = DialogHost.CloseDialogCommand;
        }
    }
}


