using Utility.Application.Services;
using Utility.Mappers;
using Utility.Domain.Repositories;
using SocialContact.Domain.Entities;
using Utility.Attributes;

namespace SocialContact.Application.Services
{

    [Transtation]
    public class IconAppService:CrudAppService<IconEntity, long>
    {
        public IconAppService(IRepository<IconEntity, long> repository//, IMapper objectMapper
            ) : base(repository)
        {
            //this.Mapper = objectMapper;           
        }

    }
}
