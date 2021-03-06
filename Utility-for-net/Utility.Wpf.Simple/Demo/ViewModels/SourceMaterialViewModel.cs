using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility.Demo.Domain.Entities;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.ViewModels
{
    public class SourceMaterialViewModel : SourceMaterialViewModel<long>
    {
     
    }
    public class SourceMaterialViewModel<Key> : SourceMaterialEntity<Key>, IIsSelectedViewModel, INotifyPropertyChanged
    {
        private bool isSelected;

        public bool IsSelected { get => isSelected; set { Set(ref isSelected, value, "IsSelected"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IIsSelectedViewModel> AllSelectEvent;
        /// <summary>
        /// 属性 值 改变 触发 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <param name="propertyName"></param>
        protected override void Set<T>(ref T oldVal, T newVal, string propertyName = null)
        {
            oldVal = newVal;
            //值 类型 比较 无效
            if (typeof(T).IsValueType)
            {
                //0==0 false
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
