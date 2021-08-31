using NHibernate.Mapping.Attributes;
using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 账套 名称 信息
    /// </summary>
    [Class(Table = "reckoning_name_info", Lazy = false)]
    public class ReckoningNameEntity:BaseEntity
    {
        private string _name;
        private string _explain;//说明
        [Property(Column = "name", Length = 20)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        /// <summary>
        /// 说明
        /// </summary>
        [Property(Column = "`explain`", Length = 200)]
        public string Explain
        {
            get { return this._explain; }
            set { Set(ref _explain, value, "Explain"); }
        }
    }
}
