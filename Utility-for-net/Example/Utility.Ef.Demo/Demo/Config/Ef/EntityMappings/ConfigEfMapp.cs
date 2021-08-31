#if  NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
namespace Config.Ef.EntityMappings
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigEfMapp : BaseEfMapp<ConfigEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigEfMapp():base(ConfigEntity.TableName)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Set()
        {
             Property(x => x.AddressTemplate).HasMaxLength(ConfigEntity.AddressTemplateMaxLength);//地址

            Property(x => x.Flag).HasMaxLength(ConfigEntity.FlagMaxLength);//标识

            Property(it => it.Status);//状态

            Property(x => x.User).HasMaxLength(ConfigEntity.UserMaxLength);//用户名

            Property(x => x.Pwd).HasMaxLength(ConfigEntity.PwdMaxLength);//密码

             Property(x => x.IsPwd);//是否需要密码

        }
    }
}
#endif

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Config.Domain.Entities;
using Config.Ef.EntityMappings;

namespace Config.Ef.EntityMappings
{


    public class ConfigEfMapp : BasEfMapp<ConfigEntity>
    {
        public ConfigEfMapp()
        {
            this.TableName = ConfigEntity.TableName;
        }

        protected override void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ConfigEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.AddressTemplate).HasMaxLength(ConfigEntity.AddressTemplateMaxLength);//地址

            builder.Property(x => x.Flag).HasMaxLength(ConfigEntity.FlagMaxLength);//标识

            builder.Property(it => it.Status);//状态

            builder.Property(x => x.User).HasMaxLength(ConfigEntity.UserMaxLength);//用户名

            builder.Property(x => x.Pwd).HasMaxLength(ConfigEntity.PwdMaxLength);//密码

            builder.Property(x => x.IsPwd);//是否需要密码
        }
    }
}
#endif