using Utility.Application.Services;
using Utility.Mappers;
using Utility.Domain.Repositories;
using SocialContact.Domain.Entities;
using Utility.Attributes;
using SocialContact.Application.Services.Dtos;
using Utility.Helpers;
using System.Collections.Generic;

namespace SocialContact.Application.Services
{
    [Transtation]
    public class WorkAppService:CrudAppService<IRepository<WorkEntity, long>,CreateWorkInput, UpdateWorkInput, WorkInput, WorkDto,  WorkEntity, long>
    {
        public WorkAppService(IRepository<WorkEntity, long> repository//, IMapper objectMapper
            ) : base(repository)
        {
            //this.Mapper = objectMapper;           
        }
        //protected override void AddBindAfter(CreateWorkInput create, WorkEntity entity)
        //{
        //    entity.CreateDate = CommonHelper.TotalMilliseconds(create.CreateDate, true);
        //    entity.StartDate = CommonHelper.TotalMilliseconds(create.StartDate, true);
        //    entity.EndDate = CommonHelper.TotalMilliseconds(create.EndDate, true);
        //    base.AddBindAfter(create, entity);
        //}
        //protected override void UpdateBindAfter(UpdateWorkInput update, WorkEntity entity)
        //{
        //    entity.UpdateDate = CommonHelper.TotalMilliseconds(update.UpdateDate, true);
        //    entity.StartDate = CommonHelper.TotalMilliseconds(update.StartDate, true);
        //    entity.EndDate = CommonHelper.TotalMilliseconds(update.EndDate , true);
        //    base.UpdateBindAfter(update, entity);
        //}

        //protected override void QueryOutputBindAfter(WorkEntity entity, WorkDto output)
        //{
        //    output.UpdateDate = CommonHelper.ToDate(entity.UpdateDate, true);
        //    output.CreateDate = CommonHelper.ToDate(entity.CreateDate, true);
        //    output.StartDate = CommonHelper.ToDate(entity.StartDate, true);
        //    output.EndDate = CommonHelper.ToDate(entity.EndDate ?? 0, true);
        //    base.QueryOutputBindAfter(entity, output);
        //}
    }
}
