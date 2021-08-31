using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Application
{
    public class CommentManager
    {
        public static ActorSystem ActorSystem { get;  set; }
        public static IActorRef RouterActor { get;  set; }
    }
}
