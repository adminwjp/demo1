using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utility.Wpf.ViewModels;
using Utility.Wpf.Attributes;
using Utility.Wpf;
using Utility.Wpf.Entries;
using System;
using Config.Domain.Entities;

namespace Example.WpfApp
{
    [MappType(typeof(ConfigEntity))]
    public class ConfigViewModel : ConfigEntity, IIsSelectedViewModel, INotifyPropertyChanged
    {
        public static ConfigResultViewModel ConfigResult { get; set; }
        public static readonly ConfigEntity Empty = new ConfigEntity();
        public static readonly ConfigViewModel EmptyViewModel = new ConfigViewModel();
        private bool _isSelected;

        public override bool IsSelected
        {
            get => _isSelected;
            set
            {
                Set(ref _isSelected, value, "IsSelected");
                if (ConfigResult != null)
                {
                    if (ConfigResult.IsAllSelected != value)
                    {
                        if (ConfigResult.DataList != null && ConfigResult.DataList.Count > 0)
                        {
                            int selectCount = 0;
                            foreach (var item in ConfigResult.DataList)
                            {
                                if (item.IsSelected)
                                {
                                    selectCount++;
                                }
                            }
                            if (selectCount == ConfigResult.DataList.Count)
                            {
                                ConfigResult.IsAllSelected = true;
                            }
                            else
                            {
                                if (selectCount > 0)
                                {
                                    ConfigResult.AllChange = false;
                                    ConfigResult.IsAllSelected = false;
                                    ConfigResult.AllChange = true;
                                }
                                else
                                {
                                    ConfigResult.IsAllSelected = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IIsSelectedViewModel> AllSelectEvent;

        protected override void Set<T>(ref T oldVal, T newVal, string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldVal, newVal))
            {
                return;
            }
            oldVal = newVal;
            this.OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Clean()
        {
            this.Status = ConfigStatus.None;// Flag.None;
            this.AddressTemplate = string.Empty;
            this.Name = string.Empty;
            this.User = string.Empty;
            this.Pwd = string.Empty;
        }
        public ConfigEntity Parse()
        {
            ConfigViewModel.Empty.Id = this.Id;
            ConfigViewModel.Empty.Flag = this.Flag;
            ConfigViewModel.Empty.AddressTemplate = this.AddressTemplate;
            ConfigViewModel.Empty.Name = this.Name;
            ConfigViewModel.Empty.User = this.User;
            ConfigViewModel.Empty.Pwd = this.Pwd;
            return ConfigViewModel.Empty;
            // return ConfigHelper.MapperManager.MapTo<AddressInfo, AddressInfo>(this);
        }
        public ConfigViewModel Copy()
        {
            ConfigViewModel.EmptyViewModel.Id = this.Id;
            ConfigViewModel.EmptyViewModel.Flag = this.Flag;
            ConfigViewModel.EmptyViewModel.AddressTemplate = this.AddressTemplate;
            ConfigViewModel.EmptyViewModel.Name = this.Name;
            ConfigViewModel.EmptyViewModel.User = this.User;
            ConfigViewModel.EmptyViewModel.Pwd = this.Pwd;
            return ConfigViewModel.EmptyViewModel;
        }
    }
    public class ConfigResultViewModel : AbstractNotifyPropertyChanged
    {
        static ConfigResultViewModel()
        {
            CacheListModelManager.CacheDataSource["Config.Flag"] = it => { LoadFlag(it); return flags; };
        }
        private static readonly List<string> flags = new List<string>();
        private static void LoadFlag(bool load)
        {
            //if (!load || flags.Count == 0)
            //{
            //    flags.Clear();
            //    foreach (FieldInfo item in typeof(Flag).GetFields())
            //    {
            //        if (item.Name.Equals("value__"))
            //        {
            //            continue;
            //        }
            //        flags.Add(item.Name);
            //    }
            //}
        }
        public static readonly ListEntry Table = new ListEntry()
        {
            Title = "配置信息",
            Width = 500,
            Height = 440,
            Columns = new List<ColumnEntry>() {
             new ColumnEntry(){ ColumnType =ColumnType.None,Name= "Id",Header="编号",Flag= ColumnEditFlag.Disabled},
             new ColumnEntry(){ ColumnType =ColumnType.Combox,Name= "Flag",Header="标识",Key="Config.Flag", SingleItems=true},
             new ColumnEntry(){ ColumnType = ColumnType.TextBox,Name= "Address",Header="地址"},
             new ColumnEntry(){ ColumnType = ColumnType.TextBox,Name= "Name",Header="名称"},
             new ColumnEntry(){ ColumnType =ColumnType.TextBox,Name= "User",Header="用户名"},
             new ColumnEntry(){ ColumnType =ColumnType.TextBox,Name= "Pwd",Header="密码"},
            }
        };
        public static readonly ConfigResultViewModel Instance = new ConfigResultViewModel();
        private ObservableCollection<ConfigViewModel> _dataList;
        public ConfigViewModel _config = new ConfigViewModel();//查询信息
        public ConfigViewModel Config { get => _config; set => Set(ref _config, value, "Config"); }
        public bool AllChange { get; set; } = true;
        private bool? _isAllSelected = false;
        public object Id { get; set; }
        public bool? IsAllSelected
        {
            get { return _isAllSelected; }
            set
            {
                if (_isAllSelected != value)
                {
                    Set(ref _isAllSelected, value, "IsAllSelected");
                    if (value.HasValue && AllChange)
                    {
                        if (DataList != null && DataList.Count > 0)
                        {
                            foreach (var item in DataList)
                            {
                                item.IsSelected = value.Value;
                            }
                        }
                    }
                }

            }
        }
        public ConfigViewModel GetSelect()
        {
            ConfigViewModel obj = null;//只能 选中 一行 进行 编辑
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        if (obj != null)
                        {
                            return null;
                        }
                        obj = item;
                    }
                }
            }
            return obj;
        }
        public IEnumerable<ConfigViewModel> GetMulSelect()
        {
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                yield break;
            }
        }
        public IEnumerable<string> GetMulSelectId()
        {
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.IsSelected)
                    {
                        yield return item.Id;
                    }
                }
            }
            else
            {
                yield break;
            }
        }
        public ConfigViewModel GetSelect(string id)
        {
            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.Id == id)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        public ObservableCollection<ConfigViewModel> DataList { get => _dataList; set => Set(ref _dataList, value, "DataList"); }//数据源
    }

}
