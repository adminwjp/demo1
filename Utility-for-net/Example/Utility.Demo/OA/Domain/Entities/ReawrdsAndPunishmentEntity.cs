using NHibernate.Mapping.Attributes;
using System;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 奖惩信息
    /// </summary>
    [Class(Table = "reawrds_and_punishment_info", Lazy = false)]
    public class ReawrdsAndPunishmentEntity:BaseEntity
    {
        private RecordEntity _record;
        private int _type;
        private string _reason;
        private string _content;
        private decimal _money;
        private DateTime _startDate;
        private DateTime _endDate;
        [ManyToOne(Column ="record_id")]
        public RecordEntity Record
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [Property(Column = "type")]
        public int Type
        {
            get { return this._type; }
            set { Set(ref _type, value, "Type"); }
        }
        [Property(Column = "reason",Length =500)]
        public string Reason
        {
            get { return this._reason; }
            set { Set(ref _reason, value, "Reason"); }
        }
        [Property(Column = "content", Length = 200)]
        public string Content
        {
            get { return this._content; }
            set { Set(ref _content, value, "Content"); }
        }
        [Property(Column = "money")]
        public decimal Money
        {
            get { return this._money; }
            set { Set(ref _money, value, "Money"); }
        }
        [Property(Column = "start_date")]
        public DateTime StartDate
        {
            get { return this._startDate; }
            set { Set(ref _startDate, value, "StartDate"); }
        }
        [Property(Column = "end_date")]
        public DateTime EndDate
        {
            get { return this._endDate; }
            set { Set(ref _endDate, value, "EndDate"); }
        }
    }
}
