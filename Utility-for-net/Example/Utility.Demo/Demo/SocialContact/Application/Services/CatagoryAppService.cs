using Utility.Application.Services;
using Utility.Mappers;
using Utility.Domain.Repositories;
using SocialContact.Domain.Entities;
using Utility.Json;
using Utility.Application.Services.Dtos;
using System.Linq;
using System.Collections.Generic;
using Utility.Attributes;

namespace SocialContact.Application.Services
{
    [Transtation]
    public class CatagoryAppService:CrudAppService<IRepository<CatagoryEntity, long>, CatagoryEntity, long>
    {
        public CatagoryAppService(IRepository<CatagoryEntity, long> repository//, IMapper objectMapper
            ) : base(repository)
        {
            //this.Mapper = objectMapper;           
        }

        public   virtual ResultDto<CatagoryEntity> List(CatalogFlag flag,int page, int size)
        {
            var data = base.Repository.QueryByPage(it => it.Flag == flag, page, size).ToList();
            var count = base.Repository.Count(it => it.Flag == flag);
            return new ResultDto<CatagoryEntity>(data,page,size,count);
        }
        public virtual List<CatagoryEntity> Catagory(CatalogFlag flag)
        {
            return base.Repository.Query(it => it.Flag == flag).ToList();
          
        }
        public virtual int Delete(CatalogFlag flag,long id)
        {
            return base.Repository.Delete(it => it.Flag == flag && it.Id == id);
        }
        public virtual int Delete(CatalogFlag flag, long[] ids)
        {
            foreach (var item in ids)
            {
               int res= Delete(flag,item);
                if (res < 0)
                {
                    (UnitWork.WriteTransaction??UnitWork.ReadTransaction).RollBack();
                }
            }
            return 1;
            //base.Repository.Delete(it => it.Flag == flag && it.Id.Value == id);
        }
    }
}
