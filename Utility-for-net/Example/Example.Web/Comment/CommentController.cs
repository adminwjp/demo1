using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility;
using Comment.Application;
using Comment.Domain.Entities;
using Utility.Attributes;

namespace Comment
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Transtation]
    public class CommentController : ControllerBase
    {
        private IPublicCommentService commentService;

        public CommentController(IPublicCommentService commentService)
        {
            this.commentService = commentService;
        }
        /// <summary>
        /// 添加  A task was canceled
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Insert([FromForm, FromBody] Comments create)
        {
            create.Save = false;
            create.EnablePage = false;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Add(create, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.AddSuccess : Code.AddFail);
        }

        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Update([FromForm, FromBody] Comments update)
        {
            update.Save = true;
            update.EnablePage = false;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Update(update, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.ModifySuccess : Code.ModifyFail);
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi Delete(string id)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Delete(id, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

        [HttpPost("delete_list")]
        [ProducesResponseType(typeof(ResponseApi), 200)]
        public virtual ResponseApi DeleteList([FromForm, FromBody] DeleteEntity<string> ids)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            int res = commentService.Delete(ids.Ids, cts.Token).Result.Result;
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }

     

        [HttpPost("find/{page}/{size}")]
        public virtual ResponseApi<ResultDto<Comments>> FindResultDtoByPage([FromForm,FromBody]Comments input, int page = 1, int size = 10)
        {
            input.Save = false;
            input.EnablePage = true;
            input.Page = page;
            input.Size = size;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var res = commentService.Find(input, cts.Token).Result.Result;
            return ResponseApi<ResultDto<Comments>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        }

        [HttpGet("List/{page}/{size}")]
        public virtual ResponseApi<ResultDto<Comments>> List(int page = 1, int size = 10)
        {
            return FindResultDtoByPage(new Comments(),page,size);
        }


    }
}
