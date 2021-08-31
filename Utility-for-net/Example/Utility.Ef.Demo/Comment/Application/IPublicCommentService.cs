using Comment.Domain.Entities;
//using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Comment.Application
{
    /// <summary>
    /// akka.net 支持 一个参数
    /// </summary>
    public interface IPublicCommentService//: IHostedService
    {
        Task<CommentReply> Add(Comments comment, CancellationToken token);

        Task<CommentReply> Update(Comments comment, CancellationToken token);

        Task<CommentReply> Delete(string id, CancellationToken token);

        Task<CommentReply> Delete(string[] ids, CancellationToken token);

        Task<CommentOutputReply> Find(Comments comment, CancellationToken token);

        
    }
}
