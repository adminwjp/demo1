using Utility.Application.Services;
using Utility.Mappers;
using Utility.Domain.Repositories;
using SocialContact.Domain.Entities;
using Utility.Attributes;

namespace SocialContact.Application.Services
{
    [Transtation]
    public class UserMenuAppService:CrudAppService<IRepository<UserMenuEntity, long>, UserMenuEntity, long>
    {
        public UserMenuAppService(IRepository<UserMenuEntity, long> repository
            //, IMapper objectMapper
            ) : base(repository)
        {
            //this.Mapper = objectMapper;           
        }

    }
}
