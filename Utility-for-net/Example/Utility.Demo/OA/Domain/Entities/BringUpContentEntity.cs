using NHibernate.Mapping.Attributes;
using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 培训内容信息
    /// </summary>
    [Class(Table = "bring_up_content_info", Name = "BringUpContentInfo", NameType = typeof(BringUpContentEntity), Lazy = false)]
    public class BringUpContentEntity:BaseEntity
    {
        private string _name;
        private string _content;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _unit;
        /// <summary>
        /// 讲课者
        /// </summary>
        private string _lecturer;
        private string _place;

        [Property( Column = "name",  Length = 50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [Property(Column = "content", Length = 100)]
        public string Content
        {
            get { return this._content; }
            set { Set(ref _content, value, "Content"); }
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
        [Property(Column = "unit", Length = 50)]
        public string Unit
        {
            get { return this._unit; }
            set { Set(ref _unit, value, "Unit"); }
        }
        /// <summary>
        /// 讲课者
        /// </summary>
        [Property( Column = "lecturer",  Length = 50)]
        public string Lecturer
        {
            get { return this._lecturer; }
            set { Set(ref _lecturer, value, "Lecturer"); }
        }
        [Property(Column = "place", Length = 100)]
        public string Place
        {
            get { return this._place; }
            set { Set(ref _place, value, "Place"); }
        }
    }
}
