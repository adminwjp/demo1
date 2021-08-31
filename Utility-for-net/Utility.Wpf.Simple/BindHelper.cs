using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Utility.Wpf
{
    /// <summary>
    /// bind helper
    /// </summary>
    public class BindHelper
    {
        /// <summary>
        ///  依赖 属性绑定 
        /// </summary>
        /// <param name="path">绑定 属性</param>
        /// <param name="target">绑定目标</param>
        /// <param name="property">依赖绑定属性</param>
        public static void SetBind(string path, DependencyObject target, DependencyProperty property)
        {
            SetBind(null, path, target, property);
        }

        /// <summary>
        /// 依赖 属性绑定 
        /// </summary>
        /// <param name="path">绑定 属性</param>
        /// <param name="target">绑定目标</param>
        /// <param name="property">依赖绑定属性</param>
        /// <param name="bindingMode">绑定方式</param>
        /// <param name="updateSourceTrigger">触发 数据源方式</param>
        /// <param name="validationRules">验证规则</param>
        public static void SetBind(string path, DependencyObject target, DependencyProperty property,  BindingMode bindingMode = BindingMode.Default,
      UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default, IList<ValidationRule> validationRules = null)
        {
            SetBind(null, path, target, property, null, bindingMode, updateSourceTrigger, validationRules);
        }


        /// <summary>
        /// 依赖 属性绑定 
        /// </summary>
        /// <param name="path">绑定 属性</param>
        /// <param name="target">绑定目标</param>
        /// <param name="property">依赖绑定属性</param>
        /// <param name="stringFormat">绑定 属性格式</param>
        /// <param name="bindingMode">绑定方式</param>
        /// <param name="updateSourceTrigger">触发 数据源方式</param>
        /// <param name="validationRules">验证规则</param>
        public static void SetBind( string path, DependencyObject target, DependencyProperty property, string stringFormat = null, BindingMode bindingMode = BindingMode.Default,
        UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default, IList<ValidationRule> validationRules = null)
        {
            SetBind(null, path, target, property, stringFormat, bindingMode, updateSourceTrigger, validationRules);
        }

        /// <summary>
        /// 依赖 属性绑定 
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="path">绑定 属性</param>
        /// <param name="target">绑定目标</param>
        /// <param name="property">依赖绑定属性</param>
        /// <param name="stringFormat">绑定 属性格式</param>
        /// <param name="bindingMode">绑定方式</param>
        /// <param name="updateSourceTrigger">触发 数据源方式</param>
        /// <param name="validationRules">验证规则</param>
        public static void SetBind(object source, string path, DependencyObject target, DependencyProperty property, string stringFormat = null, BindingMode bindingMode= BindingMode.Default, 
            UpdateSourceTrigger updateSourceTrigger= UpdateSourceTrigger.Default, IList<ValidationRule> validationRules=null)
        {
            Binding binding = new Binding();
            if (source!=null) binding.Source = source;
            if (!string.IsNullOrEmpty(path)) binding.Path = new PropertyPath(path);
            binding.Mode = bindingMode;
            binding.UpdateSourceTrigger = updateSourceTrigger;
            if(!string.IsNullOrEmpty(stringFormat))binding.StringFormat = stringFormat;
            if (validationRules != null)
            {
                foreach (var item in validationRules)
                {
                    binding.ValidationRules.Add(item);
                }
            }
            BindingOperations.SetBinding(target, property, binding);
        }
    }
}
