//using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Template.ViewModel
{
    public class TemplateViewModel
    {
    }


    //列信息
    public class ColumnViewModel :// BindableBase, 
        ICleanViewModel,IIsSelectedViewModel
    {
        public static readonly ColumnViewModel Empty = new ColumnViewModel();
        private long id;
        private string cloumName;
        private string remark;
        private string propertyName;
        private string csharepPropertyType;
        private long length;
        private string title;
        private long tableId;

        public virtual long Id { get => id; set => SetProperty(ref id, value); }

        protected virtual void SetProperty<T>(ref T id, T value)
        {
           
        }

        public virtual String CloumName { get => cloumName; set => SetProperty(ref cloumName, value); }
        public virtual String PropertyName { get => propertyName; set => SetProperty(ref propertyName, value); }

        public virtual String Remark { get => remark; set => SetProperty(ref remark, value); }

        public virtual String CsharepPropertyType { get => csharepPropertyType; set => SetProperty(ref csharepPropertyType, value); }
        public virtual long Length { get => length; set => SetProperty(ref length, value); }

        public virtual String Title { get => title; set => SetProperty(ref title, value); }
        public virtual long TableId { get => tableId; set => SetProperty(ref tableId, value); }
        
        public virtual TableViewModel Table { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public  event Action<IIsSelectedViewModel> AllSelectEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Clean()
        {
            this.Id = 0;
            this.CloumName = null;
            this.PropertyName = null;
            this.Remark = null;
            this.CsharepPropertyType = null;
            this.Length = 0;
            this.Title = null;
            this.TableId = 0;
        }

        public virtual bool IsTransient()
        {
            return true;
        }
    }

    public class ColumnResultViewModel : DataListViewModel<ColumnViewModel,long>
    {
        public ColumnResultViewModel()
        {
            this.Query = new ColumnViewModel();
            this.Form = new ColumnViewModel();
            this.TableForm = new ColumnViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }

    }
    //数据库信息
    public class DatabaseViewModel :// BindableBase,
                                    ICleanViewModel, IIsSelectedViewModel
    {
        public static readonly DatabaseViewModel Empty = new DatabaseViewModel();
        private long id;
        private string name;
        private string programName;
        private string remark;
        protected virtual void SetProperty<T>(ref T id, T value)
        {

        }
        public virtual long Id { get => id; set => SetProperty(ref id, value); }
        public virtual String Name { get => name; set => SetProperty(ref name, value); }
        public virtual String ProgramName { get => programName; set => SetProperty(ref programName, value); }
        public virtual String Remark { get => remark; set => SetProperty(ref remark, value); }

        public virtual IList<TableViewModel> TableModels { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Clean()
        {
            this.Id = 0;
            this.Name = null;
            this.ProgramName = null;
            this.Remark = null;
        }
        public virtual bool IsTransient()
        {
            return true;
        }

    }

    public class DatabaseResultViewModel : DataListViewModel<DatabaseViewModel, long>
    {
        public DatabaseResultViewModel()
        {
            this.Query = new DatabaseViewModel();
            this.Form = new DatabaseViewModel();
            this.TableForm = new DatabaseViewModel();
        }
    }
    //表信息
    public class TableViewModel : //BindableBase, 
        ICleanViewModel, IIsSelectedViewModel
    {
        public static readonly TableViewModel Empty = new TableViewModel();
        private long id;
        private string className;
        private string tablemName;
        private string remark;
        private string title;
        private long databaseId;
        private ItemViewModel items;
        protected virtual void SetProperty<T>(ref T id, T value)
        {

        }
        public virtual long Id { get => id; set => id = value; }
        public virtual String ClassName { get => className; set => className = value; }
        public virtual String TablemName { get => tablemName; set => tablemName = value; }

        public virtual String Remark { get => remark; set => remark = value; }

        public virtual String Title { get => title; set => title = value; }

        public virtual long DatabaseId { get => databaseId; set => databaseId = value; }
        public virtual DatabaseViewModel Database { get; set; }

        public virtual IList<ColumnViewModel> ColumnModels { get; set; }
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Clean()
        {
            this.Id = 0;
            this.ClassName = null;
            this.TablemName = null;
            this.Remark = null;
            this.Title = null;
            this.DatabaseId = 0;
        }
        public virtual bool IsTransient()
        {
            return true;
        }
    }

    public class TableResultViewModel : DataListViewModel<TableViewModel,long>
    {
        public TableResultViewModel()
        {
            this.Query = new TableViewModel();
            this.Form = new TableViewModel();
            this.TableForm = new TableViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }
    }
}



