using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Infrastructure
{
    /// <summary>待办事项 </summary>
    public class NeedAuditNumDto
    {
        /// <summary>获取资讯需审核内容计数 </summary>
        public int CmsNeedAuditNum { get; set; }

        /// <summary>获取贴子需审核内容计数 </summary>
        public int PostNeedAuditNum { get; set; }

        /// <summary>获取评论需审核内容计数 </summary>
        public int CommentNeedAuditNum { get; set; }

        /// <summary>获取勋章需审核内容计数 </summary>
        public int MedalNeedAuditNum{ get; set; }

        /// <summary>获取积分任务需审核内容计数 </summary>
        public int TaskNeedAuditNum { get; set; }

        public static NeedAuditNumDto GetAuditNum(ISession session)
        {
            return new NeedAuditNumDto() {
                CmsNeedAuditNum=DataStatisticsHelper.GetContentItemCountByAudit(session),
                PostNeedAuditNum=DataStatisticsHelper.GetThreadCountByAudit(session),
                CommentNeedAuditNum=DataStatisticsHelper.GetCommentCountByAudit(session),
                MedalNeedAuditNum=DataStatisticsHelper.GetsMedalToUserCountByApplying(session),
                TaskNeedAuditNum=DataStatisticsHelper.GetRecordsCountByApplying(session)
            };
        }
    }

    /// <summary> 资讯总计数和每日计数 </summary>
    public class CMSCountDto
    {
        /// <summary>资讯总计数 </summary>
        public int CmsCountAll { get; set; }

        /// <summary>资讯每日计数 </summary>
        public int CmsCountPerDay { get; set; }

        public static CMSCountDto GetCMSCount(ISession session)
        {
            return new CMSCountDto { 
                CmsCountAll= DataStatisticsHelper.GetContentItemCount(session),
                CmsCountPerDay= DataStatisticsHelper.GetContentItemCount(session,is24Hours:true)
            };
        }
    }

    /// <summary> 贴子总计数和每日计数 </summary>
    public class ThreadCountDto
    {
        /// <summary>贴子总计数 </summary>
        public int ThreadCountAll { get; set; }

        /// <summary>贴子每日计数 </summary>
        public int ThreadCountPerDay { get; set; }

        public static ThreadCountDto GeThreadCount(ISession session)
        {
            return new ThreadCountDto
            {
                ThreadCountAll = DataStatisticsHelper.GetThreadCount(session),
                ThreadCountPerDay = DataStatisticsHelper.GetThreadCount(session, is24Hours: true)
            };
        }
    }

    /// <summary> 评论总计数和每日计数 </summary>
    public class CommentCountDto
    {
        /// <summary>评论总计数 </summary>
        public int CommentCountAll { get; set; }

        /// <summary>评论每日计数 </summary>
        public int CommentCountPerDay { get; set; }

        public static CommentCountDto GetCommentCount(ISession session)
        {
            return new CommentCountDto
            {
                CommentCountAll = DataStatisticsHelper.GetThreadCount(session),
                CommentCountPerDay = DataStatisticsHelper.GetThreadCount(session, is24Hours: true)
            };
        }
    }

    /// <summary>总计数和每日计数 </summary>
    public class DataSatisticsDto
    {
        public CMSCountDto CMSCount { get; set; }

        public ThreadCountDto ThreadCount { get; set; }

        public CommentCountDto CommentCount { get; set; }

        public static DataSatisticsDto GetDataSatistics(ISession session)
        {
            return new DataSatisticsDto() { 
                CMSCount=CMSCountDto.GetCMSCount(session),
                ThreadCount=ThreadCountDto.GeThreadCount(session),
                CommentCount=CommentCountDto.GetCommentCount(session)
            };
        }
    }

    /// <summary> 总计数和每日计数 待办事项 </summary>
    public class TotalCountDto
    {
        public  DataSatisticsDto DataSatistics { get; set; }

        public NeedAuditNumDto NeedAuditNum { get; set; }

        public static TotalCountDto GetTotalCount(ISession session)
        {
            return new TotalCountDto
            {
                DataSatistics = DataSatisticsDto.GetDataSatistics(session),
                NeedAuditNum = NeedAuditNumDto.GetAuditNum(session)
            };
        }
    }

}
