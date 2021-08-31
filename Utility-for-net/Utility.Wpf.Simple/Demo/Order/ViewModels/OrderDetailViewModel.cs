//using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Shop.Wpf.ViewModel
{
    public class OrderDetailViewModel : AbstractNotifyPropertyChanged,IIsSelectedViewModel, INotifyPropertyChanged
    {
        private Int64 id;
        private Int64? productID;
        private Decimal? fee;
        private string isComment;
        private Int64? sellerID;
        private string productName;
        private string giftID;
        private Int32 orderID;
        private string specInfo;
        private Int32 score;
        private Int32 number;
        private string lowStocks;
        private Decimal? price;
        private Decimal? total;
        public virtual Int64 Id
        {
            get { return this.id; }
            set { this.id = value; if (id > 0) { OnPropertyChanged("Id"); } }
        }
        public virtual Int64? ProductID
        {
            get { return this.productID; }
            set { this.productID = value; if (productID.HasValue && productID.Value > 0) { OnPropertyChanged("ProductID"); } }
        }
        public virtual Decimal? Fee
        {
            get { return this.fee; }
            set { this.fee = value; if (fee.HasValue && fee.Value > 0) { OnPropertyChanged("Fee"); } }
        }
        public virtual string IsComment
        {
            get { return this.isComment; }
            set { this.isComment = value; if (!string.IsNullOrEmpty(isComment)) { OnPropertyChanged("IsComment"); } }
        }
        public virtual Int64? SellerID
        {
            get { return this.sellerID; }
            set { this.sellerID = value; if (sellerID.HasValue && sellerID.Value > 0) { OnPropertyChanged("SellerID"); } }
        }
      public virtual string ProductName
        {
            get { return this.productName; }
            set { this.productName = value; if (!string.IsNullOrEmpty(productName)) { OnPropertyChanged("ProductName"); } }
        }
        public virtual string GiftID
        {
            get { return this.giftID; }
            set { this.giftID = value; if (!string.IsNullOrEmpty(giftID)) { OnPropertyChanged("GiftID"); } }
        }
        public virtual Int32 OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; if (orderID > 0) { OnPropertyChanged("OrderID"); } }
        }
        public virtual string SpecInfo
        {
            get { return this.specInfo; }
            set { this.specInfo = value; if (!string.IsNullOrEmpty(specInfo)) { OnPropertyChanged("SpecInfo"); } }
        }
        public virtual Int32 Score
        {
            get { return this.score; }
            set { this.score = value; if (score > 0) { OnPropertyChanged("Score"); } }
        }
        public virtual Int32 Number
        {
            get { return this.number; }
            set { this.number = value; if (number > 0) { OnPropertyChanged("Number"); } }
        }
       public virtual string LowStocks
        {
            get { return this.lowStocks; }
            set { this.lowStocks = value; if (!string.IsNullOrEmpty(lowStocks)) { OnPropertyChanged("LowStocks"); } }
        }
        public virtual Decimal? Price
        {
            get { return this.price; }
            set { this.price = value; if (price.HasValue && price.Value > 0) { OnPropertyChanged("Price"); } }
        }
        public virtual Decimal? Total
        {
            get { return this.total; }
            set { this.total = value; if (total.HasValue && total.Value > 0) { OnPropertyChanged("Total"); } }
        }

               
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;

        public virtual void Clean()
        {
         
        }
        public event PropertyChangedEventHandler PropertyChanged;


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OrderDetailResultViewModel : DataListViewModel<OrderDetailViewModel,long>
    {
        public OrderDetailResultViewModel()
        {
            this.Query = new OrderDetailViewModel();
            this.Form = new OrderDetailViewModel();
            this.TableForm = new OrderDetailViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }

    }


}



