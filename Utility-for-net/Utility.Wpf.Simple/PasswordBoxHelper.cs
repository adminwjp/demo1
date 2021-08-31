using System.Windows;
using System.Windows.Controls;

namespace Utility.Wpf
{
    /// <summary>
    /// PasswordBox ˫��� ���� ����
    /// </summary>
    public static class PasswordBoxHelper
    {

        /// <summary>
        /// ���� ˫��� ��������
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper),
                                                                                                          new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        /// <summary>
        /// ����׷�� ˫��� ��������
        /// </summary>
        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, OnAttachPropertyChanged));


        /// <summary>
        /// ������� ˫��� ��������
        /// </summary>
        private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper));

        /// <summary>
        ///���� ����׷��  
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }


        /// <summary>
        /// ��ȡ ����׷��  
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        /// <summary>
        /// ��ȡ ����
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        /// <summary>
        ///���� ����
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>
        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }


        /// <summary>
        /// ��ȡ ���� �Ƿ� ����
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        /// <summary>
        ///���� ���� �Ƿ� ����
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        /// <summary>
        /// ���� �ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        /// ���� ׷�� �ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnAttachPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox == null)
            {
                return;
            }
            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }
            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        /// <summary>
        /// ���� �ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }
}
