using System;
using Utility.Wpf.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Shop.Wpf.ViewModel
{
    public class OrderShipViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        private Int64 id;
        private string tel;
        private string cityCode;
        private string city;
        private string province;
        private string sex;
        private string areaCode;
        private string zip;
        private string shipname;
        private string orderId;
        private string provinceCode;
        private string remark;
        private string phone;
        private string shipaddress;
        private string area;
       public virtual Int64 Id
        {
            get { return this.id; }
            set { this.id = value; if (id > 0) { OnPropertyChanged("Id"); } }
        }
       public virtual string Tel
        {
            get { return this.tel; }
            set { this.tel = value; if (!string.IsNullOrEmpty(tel)) { OnPropertyChanged("Tel"); } }
        }
      public virtual string CityCode
        {
            get { return this.cityCode; }
            set { this.cityCode = value; if (!string.IsNullOrEmpty(cityCode)) { OnPropertyChanged("CityCode"); } }
        }
      public virtual string City
        {
            get { return this.city; }
            set { this.city = value; if (!string.IsNullOrEmpty(city)) { OnPropertyChanged("City"); } }
        }
       public virtual string Province
        {
            get { return this.province; }
            set { this.province = value; if (!string.IsNullOrEmpty(province)) { OnPropertyChanged("Province"); } }
        }
      public virtual string Sex
        {
            get { return this.sex; }
            set { this.sex = value; if (!string.IsNullOrEmpty(sex)) { OnPropertyChanged("Sex"); } }
        }
       public virtual string AreaCode
        {
            get { return this.areaCode; }
            set { this.areaCode = value; if (!string.IsNullOrEmpty(areaCode)) { OnPropertyChanged("AreaCode"); } }
        }
       public virtual string Zip
        {
            get { return this.zip; }
            set { this.zip = value; if (!string.IsNullOrEmpty(zip)) { OnPropertyChanged("Zip"); } }
        }
       public virtual string Shipname
        {
            get { return this.shipname; }
            set { this.shipname = value; if (!string.IsNullOrEmpty(shipname)) { OnPropertyChanged("Shipname"); } }
        }
      public virtual string OrderId
        {
            get { return this.orderId; }
            set { this.orderId = value; if (!string.IsNullOrEmpty(orderId)) { OnPropertyChanged("OrderId"); } }
        }
       public virtual string ProvinceCode
        {
            get { return this.provinceCode; }
            set { this.provinceCode = value; if (!string.IsNullOrEmpty(provinceCode)) { OnPropertyChanged("ProvinceCode"); } }
        }
       public virtual string Remark
        {
            get { return this.remark; }
            set { this.remark = value; if (!string.IsNullOrEmpty(remark)) { OnPropertyChanged("Remark"); } }
        }
      public virtual string Phone
        {
            get { return this.phone; }
            set { this.phone = value; if (!string.IsNullOrEmpty(phone)) { OnPropertyChanged("Phone"); } }
        }
       public virtual string Shipaddress
        {
            get { return this.shipaddress; }
            set { this.shipaddress = value; if (!string.IsNullOrEmpty(shipaddress)) { OnPropertyChanged("Shipaddress"); } }
        }
       public virtual string Area
        {
            get { return this.area; }
            set { this.area = value; if (!string.IsNullOrEmpty(area)) { OnPropertyChanged("Area"); } }
        }


               
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;

        public virtual void Clean()
        {
         
        }
 
    }

    public class OrderShipResultViewModel : DataListViewModel<OrderShipViewModel,long>
    {
        public OrderShipResultViewModel()
        {
            this.Query = new OrderShipViewModel();
            this.Form = new OrderShipViewModel();
            this.TableForm = new OrderShipViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }

    }


}



