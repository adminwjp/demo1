//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Config.Nhibernate.EntityMappings
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigNhibernateMapp : BaseNhibernateMapp<ConfigEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigNhibernateMapp() : base(ConfigEntity.TableName)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Set()
        {
            Property(x => x.AddressTemplate, it => {  it.Length(50); });//地址

            Property(x => x.Name, it => { it.Length(10); });//名称

            Property(x => x.Flag);//标识

            Property(x => x.Status);//状态

            Property(x => x.User, it => {  it.Length(10); });//用户名

            Property(x => x.Pwd, it => { ; it.Length(10); });//密码
        }
    }
}
#endif