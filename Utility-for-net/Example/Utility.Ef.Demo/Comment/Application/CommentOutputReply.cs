using Akka.Actor;
using Comment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;

namespace Comment.Application
{
    public class CommentOutputReply
    {
        public CommentOutputReply(ResultDto<Comments> rsult, IActorRef hasher)
        {
            Result = rsult;
            Comment = hasher;
        }

        public ResultDto<Comments> Result { get; }
        public IActorRef Comment { get; }
    }
}
