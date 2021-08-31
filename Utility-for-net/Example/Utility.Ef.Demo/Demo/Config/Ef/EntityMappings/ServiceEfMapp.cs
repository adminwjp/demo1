#if  NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
namespace Config.Ef.EntityMappings
{
    /// <summary> 服务信息 </summary>
    public  class ServiceEfMapp : BaseEfMapp<ServiceEntity> 
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceEfMapp() :base(ServiceEntity.TableName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Set()
        {

            Property(it => it.Status);//状态

        }
    }

}

#endif

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Config.Domain.Entities;
namespace Config.Ef.EntityMappings
{
    /// <summary> 服务信息 </summary>
    public  class ServiceEfMapp : BasEfMapp<ServiceEntity> 
    {
        public ServiceEfMapp() 
        {
            this.TableName = ServiceEntity.TableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ServiceEntity> builder)
        {
            base.Configure(builder);

            builder.Property(it => it.Status);//状态

           

        }
    }

}
#endif
