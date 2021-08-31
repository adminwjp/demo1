using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database
{
    public class ModifyColumnModel
    {
        public string NewTable { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceColumn { get; set; }
        /// <summary>
        /// 外键名称
        /// </summary>
        public string ConstraintName { get; set; }
        /// <summary>
        /// 新列名
        /// </summary>
        public string NewColumn { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNotNull { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 是否自增 mysql 自增就是主键 不能有多个主键
        /// </summary>
        public bool AutoIncreMent { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
    }
}
