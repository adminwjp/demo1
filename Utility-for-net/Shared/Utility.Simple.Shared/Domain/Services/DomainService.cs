using Utility.Domain.Uow;
using Utility.Ioc;
using Utility.Mappers;
using Utility.Cache;
using System.Collections.Generic;
using System;

namespace Utility.Domain.Services
{
    /// <summary>
    /// 实体基础服务 接口 实现
    /// </summary>
    public abstract class DomainService:IDomainService
    {
     
        /// <summary>
        /// 
        /// </summary>
        public DomainService()
        {
            IocManager = EmptyIocManager.Empty;
            Mapper = DefaultMapper.Empty;
        }
        public bool UseAopTransactionAsync { get; set; } = false;
        /// <summary>
        /// unit work
        /// </summary>
        public IUnitWork UnitWork { get; set; }
        /// <summary>
        ///  ioc 
        /// </summary>
        public IIocManager IocManager { get; set; }
        /// <summary>
        /// object mapper
        /// </summary>
        public IMapper Mapper { get; set; }
        /// <summary>
        /// 缓存
        /// </summary>
        public ICacheContent Cache { get; set; }

 
    }
}
