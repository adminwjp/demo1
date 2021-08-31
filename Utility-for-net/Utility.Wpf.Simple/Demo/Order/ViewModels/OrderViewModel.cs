
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utility.Application.Services.Dtos;

namespace Shop.Wpf.ViewModel
{
    public class OrderViewModel : AbstractNotifyPropertyChanged, ICleanViewModel, IIsSelectedViewModel
    {
        private Int64 id;
        private Int64? cartId;
        private string status;
        private Int32 score;
        private Decimal? ptotal;
        private Decimal updateAmount;
        private string expressCompanyName;
        private Int32 payType;
        private Decimal? rebate;
        private Int32 carry;
        private string lowStocks;
        private string refundStatus;
        private Decimal? fee;
        private string otherRequirement;
        private string paystatus;
        private string remark;
        private string closedComment;
        private string expressNo;
        private string expressName;
        private Int32 quantity;
        private string confirmSendProductRemark;
        private string expressCode;
        private string account;
        private Int64? userId;
        private Decimal? amount;
        public virtual Int64 Id
        {
            get { return this.id; }
            set { this.id = value; if (id > 0) { OnPropertyChanged("Id"); } }
        }
        public virtual Int64? CartId
        {
            get { return this.cartId; }
            set { this.cartId = value; if (cartId.HasValue && cartId.Value > 0) { OnPropertyChanged("CartId"); } }
        }
        public virtual string Status
        {
            get { return this.status; }
            set { this.status = value; if (!string.IsNullOrEmpty(status)) { OnPropertyChanged("Status"); } }
        }
        public virtual Int32 Score
        {
            get { return this.score; }
            set { this.score = value; if (score > 0) { OnPropertyChanged("Score"); } }
        }
        public virtual Decimal? Ptotal
        {
            get { return this.ptotal; }
            set { this.ptotal = value; if (ptotal.HasValue && ptotal.Value > 0) { OnPropertyChanged("Ptotal"); } }
        }
        public virtual Decimal UpdateAmount
        {
            get { return this.updateAmount; }
            set { this.updateAmount = value; if (updateAmount > 0) { OnPropertyChanged("UpdateAmount"); } }
        }
        public virtual string ExpressCompanyName
        {
            get { return this.expressCompanyName; }
            set { this.expressCompanyName = value; if (!string.IsNullOrEmpty(expressCompanyName)) { OnPropertyChanged("ExpressCompanyName"); } }
        }
        public virtual Int32 PayType
        {
            get { return this.payType; }
            set { this.payType = value; if (payType > 0) { OnPropertyChanged("PayType"); } }
        }
        public virtual Decimal? Rebate
        {
            get { return this.rebate; }
            set { this.rebate = value; if (rebate.HasValue && rebate.Value > 0) { OnPropertyChanged("Rebate"); } }
        }
        public virtual Int32 Carry
        {
            get { return this.carry; }
            set { this.carry = value; if (carry > 0) { OnPropertyChanged("Carry"); } }
        }
        public virtual string LowStocks
        {
            get { return this.lowStocks; }
            set { this.lowStocks = value; if (!string.IsNullOrEmpty(lowStocks)) { OnPropertyChanged("LowStocks"); } }
        }
        public virtual string RefundStatus
        {
            get { return this.refundStatus; }
            set { this.refundStatus = value; if (!string.IsNullOrEmpty(refundStatus)) { OnPropertyChanged("RefundStatus"); } }
        }
        public virtual Decimal? Fee
        {
            get { return this.fee; }
            set { this.fee = value; if (fee.HasValue && fee.Value > 0) { OnPropertyChanged("Fee"); } }
        }
         public virtual string OtherRequirement
        {
            get { return this.otherRequirement; }
            set { this.otherRequirement = value; if (!string.IsNullOrEmpty(otherRequirement)) { OnPropertyChanged("OtherRequirement"); } }
        }
        public virtual string Paystatus
        {
            get { return this.paystatus; }
            set { this.paystatus = value; if (!string.IsNullOrEmpty(paystatus)) { OnPropertyChanged("Paystatus"); } }
        }
        public virtual string Remark
        {
            get { return this.remark; }
            set { this.remark = value; if (!string.IsNullOrEmpty(remark)) { OnPropertyChanged("Remark"); } }
        }
       public virtual string ClosedComment
        {
            get { return this.closedComment; }
            set { this.closedComment = value; if (!string.IsNullOrEmpty(closedComment)) { OnPropertyChanged("ClosedComment"); } }
        }
       public virtual string ExpressNo
        {
            get { return this.expressNo; }
            set { this.expressNo = value; if (!string.IsNullOrEmpty(expressNo)) { OnPropertyChanged("ExpressNo"); } }
        }
         public virtual string ExpressName
        {
            get { return this.expressName; }
            set { this.expressName = value; if (!string.IsNullOrEmpty(expressName)) { OnPropertyChanged("ExpressName"); } }
        }
        public virtual Int32 Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; if (quantity > 0) { OnPropertyChanged("Quantity"); } }
        }
        public virtual string ConfirmSendProductRemark
        {
            get { return this.confirmSendProductRemark; }
            set { this.confirmSendProductRemark = value; if (!string.IsNullOrEmpty(confirmSendProductRemark)) { OnPropertyChanged("ConfirmSendProductRemark"); } }
        }
        public virtual string ExpressCode
        {
            get { return this.expressCode; }
            set { this.expressCode = value; if (!string.IsNullOrEmpty(expressCode)) { OnPropertyChanged("ExpressCode"); } }
        }
       public virtual string Account
        {
            get { return this.account; }
            set { this.account = value; if (!string.IsNullOrEmpty(account)) { OnPropertyChanged("Account"); } }
        }
        public virtual Int64? UserId
        {
            get { return this.userId; }
            set { this.userId = value; if (userId.HasValue && userId.Value > 0) { OnPropertyChanged("UserId"); } }
        }
        public virtual Decimal? Amount
        {
            get { return this.amount; }
            set { this.amount = value; if (amount.HasValue && amount.Value > 0) { OnPropertyChanged("Amount"); } }
        }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;

        public virtual void Clean()
        {
         
        }
    
    }

    public class OrderResultViewModel : DataListViewModel<OrderViewModel,long>
    {
        public OrderResultViewModel()
        {
            this.Query = new OrderViewModel();
            this.Form = new OrderViewModel();
            this.TableForm = new OrderViewModel();
        }
        private ItemViewModel items;
        public virtual ItemViewModel Items { get => items; set => SetProperty(ref items, value); }

    }


}



