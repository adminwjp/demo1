using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Application.Services;
using Utility.Attributes;
using Utility.Domain.Repositories;
using Utility.Mappers;

namespace SocialContact.Application.Services
{
    [Transtation]
    public class RelationAppService : CrudAppService<RelationEntity, long>
    {
        public RelationAppService(IRepository<RelationEntity, long> repository//, IMapper objectMapper
            ) : base(repository)
        {
            //this.Mapper = objectMapper;
        }
        public virtual List<RelationEntity> Catagory(CatalogFlag flag)
        {
            return base.Repository.Query(it => it.Flag == flag).ToList();

        }

        public virtual void Delete(CatalogFlag flag, long id)
        {
            base.Repository.Delete(it => it.Flag == flag && it.Id == id);
        }
    }
}
