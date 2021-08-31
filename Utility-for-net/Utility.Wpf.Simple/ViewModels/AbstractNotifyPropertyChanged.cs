using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;

namespace Utility.Wpf.ViewModels
{
    /// <summary>
    /// 默认实现
    /// </summary>
    internal class EmptyNotifyPropertyChanged: AbstractNotifyPropertyChanged
    {

    }
    /// <summary>
    /// 依赖 属性 基类
    /// </summary>
    public abstract class AbstractNotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// 默认实现
        /// </summary>
        public static readonly AbstractNotifyPropertyChanged Empty = new EmptyNotifyPropertyChanged();

       
        /// <summary>
        /// 属性改变 事件 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
       
        /// <summary>
        /// 属性 值 改变 触发 
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
        /// 属性 值 改变 触发 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <param name="propertyName"></param>
        protected virtual void Set<T>(ref T oldVal,T newVal, string propertyName = null)
        {
            oldVal = newVal;
            //值 类型 比较 无效
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
        /// 属性 值 改变 触发 事件
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
