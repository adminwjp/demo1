using System;
using Utility.Wpf.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Shop.Wpf.ViewModel
{
    public class OrderLogViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        private Int64 id;
        private string orderId;
        private string content;
        private string accountType;
        private string account;
       public virtual Int64 Id
        {
            get { return this.id; }
            set { this.id = value; if (id > 0) { OnPropertyChanged("Id"); } }
        }
      public virtual string OrderId
        {
            get { return this.orderId; }
            set { this.orderId = value; if (!string.IsNullOrEmpty(orderId)) { OnPropertyChanged("OrderId"); } }
        }
       public virtual string Content
        {
            get { return this.content; }
            set { this.content = value; if (!string.IsNullOrEmpty(content)) { OnPropertyChanged("Content"); } }
        }
      public virtual string AccountType
        {
            get { return this.accountType; }
            set { this.accountType = value; if (!string.IsNullOrEmpty(accountType)) { OnPropertyChanged("AccountType"); } }
        }
       public virtual string Account
        {
            get { return this.account; }
            set { this.account = value; if (!string.IsNullOrEmpty(account)) { OnPropertyChanged("Account"); } }
        }

   
               
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;

        public virtual void Clean()
        {
         
        }
    
    }

    public class OrderLogResultViewModel : DataListViewModel<OrderLogViewModel,long>
    {
        public OrderLogResultViewModel()
        {
            this.Query = new OrderLogViewModel();
            this.Form = new OrderLogViewModel();
            this.TableForm = new OrderLogViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }

    }


}



