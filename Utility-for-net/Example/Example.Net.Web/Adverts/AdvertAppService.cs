using AutoMapper;
using Adverts.Dto;
using Spring.Objects.Factory.Attributes;
using Spring.Stereotype;

namespace Adverts
{
    /// <summary>
    /// 礼物 服务 
    /// </summary>
    [Service("service")]
    public class AdvertAppService : BaseAppService<IAdvertRepository, CreateAdvertInput, UpdateAdvertInput,
        GetAllAdvertDto, RequestAdvertDto, Advert>
    {
        [Autowired]
        public AdvertAppService(IAdvertRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
