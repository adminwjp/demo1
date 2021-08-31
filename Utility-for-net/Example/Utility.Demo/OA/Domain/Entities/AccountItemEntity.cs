using NHibernate.Mapping.Attributes;
using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 考勤/账套项目
    /// </summary>
    [Class(Table = "account_item_info", NameType = typeof(AccountItemEntity), Lazy = false)]
    public class AccountItemEntity:BaseEntity
    {
        private string _name;
        private string _type;//1 考勤 / 2 账套
        private string _utit;
        [Property(Column = "name", Length = 20)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [Property(Column = "type", Length = 4)]
        ///1 考勤 / 2 账套
        public string Type
        {
            get { return this._type; }
            set { Set(ref _type, value, "Type"); }
        }
        [Property(Column = "utit", Length = 20)]
        public string Utit
        {
            get { return this._utit; }
            set { Set(ref _utit, value, "Utit"); }
        }
    }
}
