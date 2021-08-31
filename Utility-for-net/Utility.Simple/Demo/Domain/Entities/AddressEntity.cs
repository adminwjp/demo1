using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{ 
    /// <summary>地址 </summary>
    public class AddressEntity: AddressEntity<long>, IIsDefault
    {

    }
    /// <summary>地址 </summary>
    /// <typeparam name="Key"></typeparam>
    public class AddressEntity<Key> : Entity<Key>, IIsDefault<Key>
    {
        private string contactName;
        private string address;
        private string city;
        private string country;
        private string province;
        private string memo;
        private string phone;
        private string postCode;
        private bool isDefault;
        private Key userId;
        private string area;

        /// <summary>联系人 </summary>
        public virtual string ContactName { get => contactName; set { Set(ref contactName, value, "ContactName"); } }
        /// <summary>地址 </summary>
        public virtual string Address { get => address; set { Set(ref address, value, "Address"); } }
        /// <summary>地区 </summary>
        public virtual string Area { get => area; set { Set(ref area, value, "Area"); } }
        /// <summary>城市 </summary>
        public virtual string City { get => city; set { Set(ref city, value, "City"); } }
        /// <summary>省 </summary>
        public virtual string Province { get => province; set { Set(ref province, value, "Province"); } }
        /// <summary>国家 </summary>
        public virtual string Country { get => country; set { Set(ref country, value, "Country"); } }
        /// <summary>备注 </summary>
        public virtual string Memo { get => memo; set { Set(ref memo, value, "Memo"); } }
        /// <summary>联系人手机号 </summary>
        public virtual string Phone { get => phone; set { Set(ref phone, value, "Phone"); } }
        /// <summary>邮政编码 </summary>
        public virtual string PostCode { get => postCode; set { Set(ref postCode, value, "PostCode"); } }
        /// <summary>联系人 默认地址</summary>
        public virtual bool IsDefault { get => isDefault; set { Set(ref isDefault, value, "IsDefault"); } }
        /// <summary>用户id </summary>
        public virtual Key UserId { get => userId; set { Set(ref userId, value, "UserId"); } }
    }

    public class SellerAddressEntity : AddressEntity
    {

    }

    public class AgentAddressEntity : AddressEntity
    {

    }

    public class ManufacturerAddressEntity : AddressEntity
    {

    }
}
