using System;
using Utility.Wpf.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Shop.Wpf.ViewModel
{
    public class OrderPayViewModel: AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        private Int64 id;
        private string orderId;
        private DateTime confirmDate;
        private string tradeNo;
        private string remark;
        private string payMethod;
        private Decimal? payAmount;
        private string confirmUser;
        private string payStatus;
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
        public virtual DateTime ConfirmDate
        {
            get { return this.confirmDate; }
            set { this.confirmDate = value; if (confirmDate == default(DateTime)) { OnPropertyChanged("ConfirmDate"); } }
        }
      public virtual string TradeNo
        {
            get { return this.tradeNo; }
            set { this.tradeNo = value; if (!string.IsNullOrEmpty(tradeNo)) { OnPropertyChanged("TradeNo"); } }
        }
      public virtual string Remark
        {
            get { return this.remark; }
            set { this.remark = value; if (!string.IsNullOrEmpty(remark)) { OnPropertyChanged("Remark"); } }
        }
        public virtual string PayMethod
        {
            get { return this.payMethod; }
            set { this.payMethod = value; if (!string.IsNullOrEmpty(payMethod)) { OnPropertyChanged("PayMethod"); } }
        }
        public virtual Decimal? PayAmount
        {
            get { return this.payAmount; }
            set { this.payAmount = value; if (payAmount.HasValue && payAmount.Value > 0) { OnPropertyChanged("PayAmount"); } }
        }
       public virtual string ConfirmUser
        {
            get { return this.confirmUser; }
            set { this.confirmUser = value; if (!string.IsNullOrEmpty(confirmUser)) { OnPropertyChanged("ConfirmUser"); } }
        }
       public virtual string PayStatus
        {
            get { return this.payStatus; }
            set { this.payStatus = value; if (!string.IsNullOrEmpty(payStatus)) { OnPropertyChanged("PayStatus"); } }
        }

           
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;

        public virtual void Clean()
        {
         
        }
    }

    public class OrderPayResultViewModel : DataListViewModel<OrderPayViewModel,long>
    {
        public OrderPayResultViewModel()
        {
            this.Query = new OrderPayViewModel();
            this.Form = new OrderPayViewModel();
            this.TableForm = new OrderPayViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }

    }


}



