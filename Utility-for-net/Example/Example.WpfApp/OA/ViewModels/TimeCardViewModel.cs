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
    /// 考勤信息
    /// </summary>
    [MappTypeAttribute(typeof(TimeCardEntity))]
    public class TimeCardViewModel : TimeCardEntity, INotifyPropertyChanged, IIsSelectedViewModel
    {
        public void CreateByNullInstance()
        {
            Record ??= new RecordViewModel();
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
        private RecordViewModel _record;
        private RecordViewModel _ratifierRecord;//批准人

        public new RecordViewModel Record
        {
            get { return this._record; }
            set{base.Set(ref this._record, value, "Record"); }
        }
      
        /// <summary>
        /// 批准人
        /// </summary>
        public new RecordViewModel RatifierRecord
        {
            get { return this._ratifierRecord; }
            set { base.Set(ref this._ratifierRecord, value, "RatifierRecord");}
        }
    }
}
