using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utility.Wpf.Attributes;
using Utility.Wpf.ViewModels;

namespace OA.Wpf.ViewModels
{
    /// <summary>
    /// 档案信息
    /// </summary>
    [MappTypeAttribute(typeof(RecordEntity))]
    public class RecordViewModel : RecordEntity, INotifyPropertyChanged, IIsSelectedViewModel
    {
        public void CreateByNullInstance()
        {
          //防止 双向绑定时改对象为null 参数 传递不过来
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    Set(ref _isSelected, value, "IsSelected");
                    if (AllSelectEvent != null)
                    {
                        AllSelectEvent(this);
                    }
                }
           
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IIsSelectedViewModel> AllSelectEvent;

        protected override void Set<T>(ref T oldVal, T newVal, string propertyName = null)
        {
            //值 类型 比较 无效
            if (typeof(T).IsValueType)
            {
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
            oldVal = newVal;
            this.OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      
    }
}
