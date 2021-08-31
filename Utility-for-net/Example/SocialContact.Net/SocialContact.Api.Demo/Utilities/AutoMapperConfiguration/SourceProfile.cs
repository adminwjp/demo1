using AutoMapper;
using System;
using Tunynet.Post;

namespace Tunynet.Common
{
    /// <summary>
    /// 基础转换类配置
    /// </summary>
    public class SourceProfile : Profile
    {
        /// <summary>
        /// 基础配置类
        /// </summary>
        [Obsolete("6.0会过时 但是目前是最新")]
        public SourceProfile()
        {
            //评论转换
            CreateMap<Comment, CommentEditModel>();
            CreateMap<CommentEditModel, Comment>();
            //贴子转换
            CreateMap<ThreadEditModel, Thread>();
            CreateMap<Thread, ThreadEditModel>();

            //CreateMap<Source, Destination2>().ForMember(d => d.AnotherValue2, opt =>
            //{
            //    opt.MapFrom(s => s.AnotherValue);
            //});
        }
    }
}