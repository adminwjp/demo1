using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Utility.Database.Entities
{
    /// <summary> 
    /// mapping entity information  to table structure information
    /// primary key collection, 
    /// mysql   can only has one   primary key,
    /// sqlite  can many   primary key
    /// </summary>
    public class ClassEntity
    {
        /// <summary>
        /// no param constractor
        /// </summary>
        public ClassEntity()
        {

        }

        /// <summary>
        /// has param constractor
        /// </summary>
        /// <param name="classType">entity type</param>
        public ClassEntity(Type classType)
        {
            this.ClassType = classType;
        }

        /// <summary>table name </summary>
        public string Table { get; set; }
        /// <summary>
        /// 所有 属性 都存在 不清理 
        /// </summary>
        public IList<PropertyEntity> PropertyEntities { get; set; }
        /// <summary>
        ///主键 缓存 外键 关联 时 要 用到  每次 遍历  没必要 (可能是联合主键 优先级 用第一个关联)
        /// </summary>
        public virtual IList<PropertyEntity> IdEntities{get;set;}

        public virtual void UpdatePkAndFk(){
            if (IdEntities == null && PkQuantity > 0)
            {
                RefreshPrimaryKey();
            }
                if (FkEntities == null && FkQuantity > 0)
            {
                RefreshForeignKey();
            }

        }
        /// <summary>
        ///外键 缓存 整理普通外键列 时 用到 
        /// </summary>
        public virtual IList<PropertyEntity> FkEntities{get;set;}
        /// <summary>
        /// 强制更新主键 缓存信息
        /// </summary>
        /// <param name="refresh">true:强制更新</param>
        public void RefreshPrimaryKey(bool refresh = false)
        {
            if (PropertyEntities == null || PropertyEntities.Count == 0)
                return;
            if (PkQuantity > 0)
            {
                if (IdEntities != null)
                {
                    if (!refresh)
                    {
                        return;
                    }
                }
                IdEntities = IdEntities ?? new Utility.Collections.Array<PropertyEntity>(PkQuantity);
                for (int i = 0; i < PropertyEntities.Count; i++)
                {
                    if (PropertyEntities[i].Flag == ColumnFlag.PrimaryKey)
                    {
                        IdEntities.Add(PropertyEntities[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 强制更新外键 缓存信息
        /// </summary>
        /// <param name="refresh">true:强制更新</param>
        public void RefreshForeignKey(bool refresh = false)
        {
            if (PropertyEntities == null || PropertyEntities.Count == 0)
                return;
            if (FkQuantity > 0)
            {
                if (FkEntities != null)
                {
                    if (!refresh)
                    {
                        return;
                    }
                }
                FkEntities = FkEntities ?? new Utility.Collections.Array<PropertyEntity>(FkQuantity);
                for (int i = 0; i < PropertyEntities.Count; i++)
                {
                    if (PropertyEntities[i].Flag == ColumnFlag.ForeignKey)
                    {
                        FkEntities.Add(PropertyEntities[i]);
                    }
                }
            }
        }
        /// <summary>
        /// annnotation,sqlite invalid
        /// </summary>
        public string Comment { get; set; }
        /// <summary>entity type </summary>
        public Type ClassType { get; set; }

        public void ValidateColumn(string column)
        {
            if (PropertyEntities != null)
            {
                for (int i = 0; i < PropertyEntities.Count; i++)
                {
                    var item = PropertyEntities[i];
                    if (item.Column.Equals(column))
                    {
                        throw new Exception($"class type {ClassType} property {item.Property}  column {column} has exists ");
                    }
                }
            }
        }
        /// <summary>普通sql语句缓存 </summary>
        public readonly SqlEntity SqlEntry = new SqlEntity();

        public StringBuilder SqlServerCommentBuilder { get; set; }
        /// <summary>
        /// primary key quantity
        /// </summary>
        internal int PkQuantity { get; set; }
        /// <summary>
        /// foreigin key quantity
        /// </summary>
        internal int FkQuantity { get; set; }


      
        /// <summary>
        /// 是否为自增 id 级联 时要用到
        /// </summary>
        public virtual bool IsIdentity { get; set; }
        /// <summary>
        /// true:创建表 时 不创建主键,使用时 当主键使用
        /// </summary>
        public virtual bool IgnorePk { get; set; }
        /// <summary>
        /// true:创建表 时 不创建外键,使用时 当外键使用 (先写死,外键删除时可能有问题(得获取到外键名称 还得排序(删除有优先级,关联外键的优先级)))
        /// </summary>
        public virtual bool IgnoreFk { get; set; } = true;
       

        /// <summary>根据列名获取外键信息</summary>
        /// <param name="column">列名</param>
        /// <returns></returns>
        public FKColumnEntity GetForeignKey(string column)
        {
            for (int i = 0; i < PropertyEntities.Count; i++)
            {
                var columnEntry = PropertyEntities[i];
                if (columnEntry.Column.Equals(column))
                {
                    return columnEntry.FKColumnEntity;
                }
            }
            return null;
        }

        public static bool IsPk(Type type)
        {
            return TypeHelper.IsInterger(type) || TypeHelper.IsGuid(type) || TypeHelper.IsString(type);
        }

        /// <summary>根据外键属性获取外键分组信息</summary>
        /// <param name="fKEntry">外键</param>
        /// <returns></returns>
        public FKColumnEntity GetFk(BaseEntity fKEntry)
        {
            foreach (var item in PropertyEntities)
            {
                if (item.Flag == ColumnFlag.ForeignKey &&
                    fKEntry.Equals(item.FKColumnEntity.Basic) ||
                    fKEntry.Equals(item.FKColumnEntity.Single) ||
                    fKEntry.Equals(item.FKColumnEntity.Many))
                    return item.FKColumnEntity;
            }
            return null;
        }
    }
}
