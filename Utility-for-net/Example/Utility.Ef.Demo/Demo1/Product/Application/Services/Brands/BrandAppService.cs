using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Cache;
using Utility.Domain.Repositories;
using Utility.Interceptors;
using Utility.Mappers;

namespace Product.Application.Services.Brands
{
    public interface IBrandAppService: ICrudAppService<IBrandRepository, BrandDto, BrandDto, BrandDto, BrandDto, BrandEntity, long>
    {

    }
    [Component(typeof(IBrandAppService), AutofacScope = AutofacScope.InstancePerDependency
         , Interceptor = typeof(IocTranstationAopInterceptor), EnableAspect = true, 
        InterceptorType = InterceptorType.Interface
         )]
    public class BrandAppService : CrudAppService<IBrandRepository, BrandDto, BrandDto, BrandDto, BrandDto, BrandEntity, long>, IBrandAppService
    {
        public BrandAppService(IBrandRepository repository, IMapper objectMapper, ICacheContent cache)
            : base(repository)
        {
            this.Mapper = objectMapper;
            this.Cache = cache;
        }
    }
}
