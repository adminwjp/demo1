using NHibernate.Mapping.Attributes;

namespace OA.Domain.Entities
{
    /// <summary>
    ///账套 人员设置
    /// </summary>
    [Class(Table = "reckoning_list_info", NameType = typeof(ReckoningListEntity), Lazy = false)]
    public class ReckoningListEntity:BaseEntity
    {
        private RecordEntity _record;
        private ReckoningNameEntity _reckoningName;
        [ManyToOne(Column ="record_id")]
        public RecordEntity Record
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [ManyToOne(Column = "reckoning_name_id")]
        public ReckoningNameEntity ReckoningName
        {
            get { return this._reckoningName; }
            set { Set(ref _reckoningName, value, "ReckoningName"); }
        }
    }
}
