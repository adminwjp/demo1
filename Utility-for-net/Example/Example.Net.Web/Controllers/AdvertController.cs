using Adverts;
using Adverts.Dto;
using Spring.Objects.Factory.Attributes;
using System.Web.Http;
namespace Example.Web.Controllers
{
    public class AdvertController : BaseController<AdvertAppService, IAdvertRepository, CreateAdvertInput, UpdateAdvertInput,
        GetAllAdvertDto, RequestAdvertDto, Advert>
    {
        [Autowired]
        public AdvertController(AdvertAppService service) : base(service)
        {
            this.service = service;
        }
        [HttpGet]
        public string Test()
        {
            return "test";
        }
    }
}