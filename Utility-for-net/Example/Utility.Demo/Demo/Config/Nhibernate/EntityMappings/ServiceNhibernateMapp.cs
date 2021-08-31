//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;

namespace Config.Nhibernate.EntityMappings
{
    /// <summary> 服务信息 nhibernate映射  </summary>
    public  class ServiceNhibernateMapp : BaseNhibernateMapp<ServiceEntity> 
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceNhibernateMapp():base(ServiceEntity.TableName)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Set()
        {
            Property(x => x.Name, it => {  it.Length(10); });//服务名称

            Property(x => x.Ip, it => {  it.Length(15); });//ip地址

            Property(x => x.Port);//端口

            Property(x => x.Status);//状态
        }
    }
}
#endif