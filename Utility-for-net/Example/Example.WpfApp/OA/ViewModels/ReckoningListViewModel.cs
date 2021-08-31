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
    ///账套 人员设置
    /// </summary>
    [MappTypeAttribute(typeof(ReckoningListEntity))]
    public class ReckoningListViewModel : ReckoningListEntity, INotifyPropertyChanged, IIsSelectedViewModel
    {
        public void CreateByNullInstance()
        {
            Record ??= new RecordViewModel();
            ReckoningName ??= new ReckoningNameViewModel();
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
        private ReckoningNameViewModel _reckoningName;

        public new RecordViewModel Record
        {
            get { return this._record; }
            set { base.Set(ref this._record, value, "Record"); }
        }
        public new ReckoningNameViewModel ReckoningName
        {
            get { return this._reckoningName; }
            set { base.Set(ref this._reckoningName, value, "ReckoningName"); }
        }
    }
}
