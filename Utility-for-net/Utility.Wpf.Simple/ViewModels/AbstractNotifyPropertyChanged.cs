using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;

namespace Utility.Wpf.ViewModels
{
    /// <summary>
    /// Ĭ��ʵ��
    /// </summary>
    internal class EmptyNotifyPropertyChanged: AbstractNotifyPropertyChanged
    {

    }
    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public abstract class AbstractNotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Ĭ��ʵ��
        /// </summary>
        public static readonly AbstractNotifyPropertyChanged Empty = new EmptyNotifyPropertyChanged();

       
        /// <summary>
        /// ���Ըı� �¼� 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
       
        /// <summary>
        /// ���� ֵ �ı� ���� 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(ref T oldVal, T newVal, string propertyName = null)
        {
            Set(ref oldVal, newVal, propertyName);
        }
        /// <summary>
        /// ���� ֵ �ı� ���� 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <param name="propertyName"></param>
        protected virtual void Set<T>(ref T oldVal,T newVal, string propertyName = null)
        {
            oldVal = newVal;
            //ֵ ���� �Ƚ� ��Ч
            if (typeof(T).IsValueType)
            { //0==0 false
                if (oldVal.ToString().Equals(newVal.ToString()))
                {
                    return;
                }
            }
            else
            {
                if (EqualityComparer<T>.Default.Equals(oldVal, newVal))
                {
                    return;
                }
            }
            this.OnPropertyChanged(propertyName);
        }
 
        /// <summary>
        /// ���� ֵ �ı� ���� �¼�
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
