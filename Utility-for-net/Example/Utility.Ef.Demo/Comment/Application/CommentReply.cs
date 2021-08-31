using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;

namespace Comment.Application
{
    public class CommentReply
    {
        public CommentReply(int result,IActorRef hasher)
        {
            Result = result;
            Comment = hasher;
        }
        public int Result { get;  }
        public IActorRef Comment { get; }
    }
}
