using NHibernate.Mapping.Attributes;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 培训人员信息
    /// </summary>
    [Class(Table = "bring_up_person_info", Name = "BringUpPersonInfo", NameType = typeof(BringUpPersonEntity), Lazy = false)]
    public class BringUpPersonEntity : BaseEntity
    {
        private BringUpContentEntity _bringUpContent;
        private RecordEntity _record;
        private int _sEntities;
        /// <summary>
        /// 培训等级
        /// </summary>
        private string _upToGrate;
        private string _remark;
        [ManyToOne(2, ClassType = typeof(BringUpContentEntity), Column = "bring_up_content_id")]
        public virtual BringUpContentEntity BringUpContent
        {
            get { return this._bringUpContent; }
            set { Set(ref _bringUpContent, value, "BringUpContent"); }
        }
        [ManyToOne(2, ClassType = typeof(RecordEntity), Column = "record_id")]
        public virtual RecordEntity Record
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [Property(Column = "sEntities", TypeType = typeof(int))]
        public int SEntities
        {
            get { return this._sEntities; }
            set { Set(ref _sEntities, value, "SEntities"); }
        }
        /// <summary>
        /// 培训等级
        /// </summary>
        [Property(Column = "up_to_grate",  Length = 2)]
        public string UpToGrate
        {
            get { return this._upToGrate; }
            set { Set(ref _upToGrate, value, "UpToGrate"); }
        }
        [Property(Column = "remark", NotNull = true, Length = 200)]
        public string Remark
        {
            get { return this._remark; }
            set { Set(ref _remark, value, "Remark"); }
        }
    }
}
