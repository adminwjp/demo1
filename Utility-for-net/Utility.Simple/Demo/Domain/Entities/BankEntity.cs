using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{    
    /// <summary>银行卡 </summary>
    public class BankEntity: BankEntity<long>, IIsDefault
    {

    }
    /// <summary>银行卡 </summary>
    public class BankEntity<Key> : Entity<Key>, IIsDefault<Key>
    {
        private string bankId;
        private string bankName;
        private string bankPhoto1;
        private string bankPhoto2;
        private Key bankPhotoId1;
        private Key bankPhotoId2;
        private string bankAddress;
        private string bankUserName;
        private string bankUserAddress;
        private bool isDefault;
        private Key userId;

        /// <summary>银行卡卡号 </summary>
        public virtual string BankId { get => bankId; set { Set(ref bankId, value, "BankId"); } }
        /// <summary>银行卡名称 </summary>
        public virtual string BankName { get => bankName; set { Set(ref bankName, value, "BankName"); } }
        /// <summary>银行卡正面图片 </summary>
        public virtual string BankPhoto1 { get => bankPhoto1; set { Set(ref bankPhoto1, value, "BankPhoto1"); } }
        /// <summary>银行卡背面图片 </summary>
        public virtual string BankPhoto2 { get => bankPhoto2; set { Set(ref bankPhoto2, value, "BankPhoto2"); } }

        /// <summary>银行卡正面图片id </summary>
        public virtual Key BankPhotoId1 { get => bankPhotoId1; set { Set(ref bankPhotoId1, value, "BankPhotoId1"); } }
        /// <summary>银行卡背面图片id </summary>
        public virtual Key BankPhotoId2 { get => bankPhotoId2; set { Set(ref bankPhotoId2, value, "BankPhotoId2"); } }

        /// <summary>银行卡开户地址 </summary>
        public virtual string BankAddress { get => bankAddress; set { Set(ref bankAddress, value, "BankAddress"); } }
        /// <summary>银行卡持有人姓名 </summary>
        public virtual string BankUserName { get => bankUserName; set { Set(ref bankUserName, value, "BankUserName"); } }
        /// <summary>银行卡持有人地址 </summary>
        public virtual string BankUserAddress { get => bankUserAddress; set { Set(ref bankUserAddress, value, "BankUserAddress"); } }
        /// <summary>默认银行卡 </summary>
        public virtual bool IsDefault { get => isDefault; set { Set(ref isDefault, value, "IsDefault"); } }
        /// <summary>用户id </summary>
        public virtual Key UserId { get => userId; set { Set(ref userId, value, "UserId"); } }

    }
}
