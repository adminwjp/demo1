using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.Domain.Uow;
using Utility;
using Utility.Extensions;
using Utility.Mappers;
using Comment.Domain.Entities;
using Utility.Attributes;
using Utility.AspNetCore.Controllers;
using Comment.Ef;
using Utility.Ef.Uow;
using Utility.Ef;

namespace Comment
{
    [Route("api/v{version:apiVersion}/admin/comment_type")]
    [ApiController]
    [ApiVersion("1.0")]
    [Transtation]
    public class CommentTypeController : BaseController
    {

        public CommentTypeController(CommentDbContent commentDbContent)
        {
            UnitWork = new EfUnitWork(new DbContextProvider(commentDbContent));
        }
        /// <summary>
        /// 添加 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Insert([FromForm, FromBody] CommentType create)
        {
            create.Id = Guid.NewGuid().ToString("N");
            create.CreationTime = DateTime.Now;
            UnitWork.Insert(create); 
            int res = 1;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.AddSuccess : Code.AddFail);
        }

        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Update([FromForm, FromBody] CommentType update)
        {
            update.LastModificationTime = DateTime.Now;
            UnitWork.Update(update);
            int res = 1;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.ModifySuccess : Code.ModifyFail);
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Delete(string id)
        {
            UnitWork.Delete<CommentType>(id);
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        [HttpPost("delete_list")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi DeleteList([FromForm, FromBody] DeleteEntity<string> ids)
        {
            Expression<Func<CommentType, bool>> where=null;
            foreach (var item in ids.Ids)
            {
                where = LinqExpression.Or(where, it => it.Id == item);
            }
            UnitWork.Update<CommentType>(where,it=>new CommentType() {
                LastModificationTime=DateTime.Now,
                IsDeleted=true});
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        

        [HttpPost("find/{page}/{size}")]
        public virtual ResponseApi<ResultDto<CommentType>> FindResultDtoByPage([FromForm, FromBody] CommentType input, int page = 1, int size = 10)
        {
            var data = UnitWork.FindListByPageOrEntity<CommentType>(null, page, size).ToList();
            var count = UnitWork.Count<CommentType>();
            var res= new ResultDto<CommentType>()
            {
                Data = data,
                Result = new PageResultDto(page, size,
                (int)(count / size == 0 ? count / size : count / size + 1), count)
            };
            return ResponseApi<ResultDto<CommentType>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        }

        [HttpGet("List/{page}/{size}")]
        public virtual ResponseApi<ResultDto<CommentType>> List( int page = 1, int size = 10)
        {
            return FindResultDtoByPage(null,page,size);
        }

    }
}
