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
        ///  ���� ���԰� 
        /// </summary>
        /// <param name="path">�� ����</param>
        /// <param name="target">��Ŀ��</param>
        /// <param name="property">����������</param>
        public static void SetBind(string path, DependencyObject target, DependencyProperty property)
        {
            SetBind(null, path, target, property);
        }

        /// <summary>
        /// ���� ���԰� 
        /// </summary>
        /// <param name="path">�� ����</param>
        /// <param name="target">��Ŀ��</param>
        /// <param name="property">����������</param>
        /// <param name="bindingMode">�󶨷�ʽ</param>
        /// <param name="updateSourceTrigger">���� ����Դ��ʽ</param>
        /// <param name="validationRules">��֤����</param>
        public static void SetBind(string path, DependencyObject target, DependencyProperty property,  BindingMode bindingMode = BindingMode.Default,
      UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default, IList<ValidationRule> validationRules = null)
        {
            SetBind(null, path, target, property, null, bindingMode, updateSourceTrigger, validationRules);
        }


        /// <summary>
        /// ���� ���԰� 
        /// </summary>
        /// <param name="path">�� ����</param>
        /// <param name="target">��Ŀ��</param>
        /// <param name="property">����������</param>
        /// <param name="stringFormat">�� ���Ը�ʽ</param>
        /// <param name="bindingMode">�󶨷�ʽ</param>
        /// <param name="updateSourceTrigger">���� ����Դ��ʽ</param>
        /// <param name="validationRules">��֤����</param>
        public static void SetBind( string path, DependencyObject target, DependencyProperty property, string stringFormat = null, BindingMode bindingMode = BindingMode.Default,
        UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default, IList<ValidationRule> validationRules = null)
        {
            SetBind(null, path, target, property, stringFormat, bindingMode, updateSourceTrigger, validationRules);
        }

        /// <summary>
        /// ���� ���԰� 
        /// </summary>
        /// <param name="source">����Դ</param>
        /// <param name="path">�� ����</param>
        /// <param name="target">��Ŀ��</param>
        /// <param name="property">����������</param>
        /// <param name="stringFormat">�� ���Ը�ʽ</param>
        /// <param name="bindingMode">�󶨷�ʽ</param>
        /// <param name="updateSourceTrigger">���� ����Դ��ʽ</param>
        /// <param name="validationRules">��֤����</param>
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
