using NHibernate.Mapping.Attributes;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 账套信息
    /// </summary>
    [Class(Table = "reckoning_info", Lazy = false)]
    public class ReckoningEntity : BaseEntity
    {
        private RecordEntity _record;
        private AccountItemEntity _accountItem;
        private decimal _money;
        [ManyToOne(Column = "reEntities")]
        public RecordEntity Record
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [ManyToOne(Column = "account_item_id")]
        public AccountItemEntity AccountItem
        {
            get { return this._accountItem; }
            set { Set(ref _accountItem, value, "AccountItem"); }
        }
        [Property(Column = "money")]
        public decimal Money
        {
            get { return this._money; }
            set { Set(ref _money, value, "Money"); }
        }
    }
}
