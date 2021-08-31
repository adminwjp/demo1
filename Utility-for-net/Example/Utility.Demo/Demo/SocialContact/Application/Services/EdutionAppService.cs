using Utility.Application.Services;
using Utility.Mappers;
using Utility.Domain.Repositories;
using SocialContact.Domain.Entities;
using Utility.Attributes;
using SocialContact.Application.Services.Dtos;
using Utility.Helpers;

namespace SocialContact.Application.Services
{
    [Transtation]
    public class EdutionAppService:CrudAppService<IRepository<EdutionEntity, long>, CreateEdutionInput,UpdateEdutionInput,EdutionInput,EdutionDto, EdutionEntity, long>
    {
        public EdutionAppService(IRepository<EdutionEntity, long> repository//, IMapper objectMapper
            ) : base(repository)
        {
           // this.Mapper = objectMapper;           
        }
        //protected override void AddBindAfter(CreateEdutionInput create, EdutionEntity target)
        //{
        //    target.CreateDate = CommonHelper.TotalMilliseconds(create.CreateDate, true);
        //    base.AddBindAfter(create, target);
        //}
        //protected override void UpdateBindAfter(UpdateEdutionInput update, EdutionEntity entity)
        //{
        //    entity.UpdateDate = CommonHelper.TotalMilliseconds(update.UpdateDate, true);
        //    base.UpdateBindAfter(update, entity);
        //}

        //protected override void QueryOutputBindAfter(EdutionEntity entity, EdutionDto output)
        //{
        //    //自定义 mapp 数据 类型 不同 自动转换 
        //    output.FirstStartDate = CommonHelper.ToDate(entity.FirstStartDate, true);
        //    output.FirstEndDate = CommonHelper.ToDate(entity.FirstEndDate, true);
        //    output.SecondStartDate = CommonHelper.ToDate(entity.SecondStartDate, true);
        //    output.SecondEndDate = CommonHelper.ToDate(entity.SecondEndDate, true);
        //    output.ThreeStartDate = CommonHelper.ToDate(entity.ThreeStartDate, true);
        //    output.ThreeEndDate = CommonHelper.ToDate(entity.ThreeEndDate, true);
        //    output.FourStartDate = CommonHelper.ToDate(entity.FourStartDate, true);
        //    output.FourEndDate = CommonHelper.ToDate(entity.FourEndDate, true);
        //    output.FiveStartDate = CommonHelper.ToDate(entity.FiveStartDate, true);
        //    output.FiveEndDate = CommonHelper.ToDate(entity.FiveEndDate, true);
        //    base.QueryOutputBindAfter(entity, output);
        //}
    }
}
