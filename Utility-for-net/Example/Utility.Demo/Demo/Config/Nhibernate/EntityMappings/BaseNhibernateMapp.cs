//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )


using Config.Domain.Entities;

namespace Config.Nhibernate.EntityMappings
{
    /// <summary>nhibernate 基类 xml mapp 必须虚方法(属性)不然错误 ,注解可以不需要 虚方法(属性) </summary>
    public abstract class BaseNhibernateMapp<T> : Utility.Nhibernate.EntityMappings.BaseNhibernateMapp<T, string> where T : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public BaseNhibernateMapp(string tableName):base(tableName)
        {
            Property(x => x.Description, it => { it.Length(int.MaxValue); });//描述

            Property(x => x.CreateDate, it => { it.Column("CreateDate"); });//创建时间

            Property(x => x.LastDate, it => { it.Column("LastDate"); });//修改时间
        }
    }
}
#endif