using NHibernate;
using NHibernate.Criterion;
using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Post;

namespace SocialContact.Infrastructure
{
    /// <summary>
    /// 数据统计
    /// </summary>
    public class DataStatisticsHelper
    {
        /// <summary>
        /// 资讯计数获取(后台用)
        /// </summary>
        /// <param name="approvalStatus">审核状态</param>
        /// <param name="is24Hours">是否24小时之内</param>
        /// <returns></returns>
        public static int GetContentItemCount(ISession session, AuditStatus? approvalStatus = null, bool is24Hours = false)
        {
            var criteria=session.CreateCriteria<ContentItem>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            if (approvalStatus.HasValue)
                ands.Add(Expression.Eq("ApprovalStatus", (int)approvalStatus.Value));
            if (is24Hours)
                ands.Add(Expression.Gt("DateCreated", DateTime.Now.AddHours(-24)));
            AbstractCriterion where = null;
            foreach (var item in ands)
            {
                where = where ? item : Expression.And(where, item);
            }
            return (where == null?criteria:criteria.Add(where)).SetProjection(Projections.RowCount()).UniqueResult<int>();
        }

        /// <summary>
        /// 资讯计数获取(后台用) 待审核 or 审核
        /// </summary>
        /// <returns></returns>
        public static int GetContentItemCountByAudit(ISession session)
        {
            return session.Query<ContentItem>().Where(it=>it.ApprovalStatus== AuditStatus.Pending||it.ApprovalStatus==AuditStatus.Again).Count();
        }


        /// <summary>
        /// 贴子计数获取(后台用)
        /// </summary>
        /// <param name="approvalStatus">审核状态</param>
        /// <param name="is24Hours">是否24小时之内</param>
        /// <returns></returns>
        public static int GetThreadCount(ISession session, AuditStatus? approvalStatus=null, bool is24Hours=false)
        {
            var criteria = session.CreateCriteria<Thread>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            if (approvalStatus.HasValue)
                ands.Add(Expression.Eq("ApprovalStatus", (int)approvalStatus.Value));
            if (is24Hours)
                ands.Add(Expression.Gt("DateCreated", DateTime.Now.AddHours(-24)));
            AbstractCriterion where = null;
            foreach (var item in ands)
            {
                where = where ? item : Expression.And(where, item);
            }
            return (criteria == null ? criteria : criteria.Add(where)).SetProjection(Projections.RowCount()).UniqueResult<int>();
        }

        /// <summary>
        /// 贴子计数获取(后台用) 待审核 or 审核
        /// </summary>
        /// <returns></returns>
        public static int GetThreadCountByAudit(ISession session)
        {
            return session.Query<Thread>().Where(it => it.ApprovalStatus == AuditStatus.Pending || it.ApprovalStatus == AuditStatus.Again).Count();
        }

        /// <summary>
        /// 评论计数获取(后台用)
        /// </summary>
        /// <param name="approvalStatus">审核状态</param>
        /// <param name="is24Hours">是否24小时之内</param>
        /// <param name="tenantTypeId"></param>
        /// <param name="commentedObjectId"></param>
        /// <returns></returns>
        public int GetCommentCount(ISession session,  AuditStatus? approvalStatus = null, bool is24Hours = false, string tenantTypeId = "", long? commentedObjectId = null)
        {
            var criteria = session.CreateCriteria<Comment>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            if (approvalStatus.HasValue)
                ands.Add(Expression.Eq("ApprovalStatus", (int)approvalStatus.Value));
            if (is24Hours)
                ands.Add(Expression.Gt("DateCreated", DateTime.Now.AddHours(-24)));
            if (!string.IsNullOrEmpty(tenantTypeId))
                ands.Add(Expression.Eq("TenantTypeId", tenantTypeId));
            if (commentedObjectId.HasValue)
                ands.Add(Expression.Eq("CommentedObjectId", commentedObjectId.Value));
            AbstractCriterion where = null;
            foreach (var item in ands)
            {
                where = where ? item : Expression.And(where, item);
            }
            return (where == null ? criteria : criteria.Add(where)).SetProjection(Projections.RowCount()).UniqueResult<int>();
        }

        /// <summary>
        /// 评论计数获取(后台用) 待审核 or 审核
        /// </summary>
        /// <returns></returns>
        public static int GetCommentCountByAudit(ISession session)
        {
            return session.Query<Comment>().Where(it => it.ApprovalStatus == AuditStatus.Pending || it.ApprovalStatus == AuditStatus.Again).Count();
        }

        /// <summary>
        /// 获取勋章授予记录（后台管理）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="medalId">勋章Id</param>
        /// <param name="userAwardStatus">授予勋章状态</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public static int GetsMedalToUserCount(ISession session,List<string> userId = null, long medalId = 0, UserAwardStatus? userAwardStatus = null)
        {
            var criteria = session.CreateCriteria<MedalToUser>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            if (userId != null && userId.Count > 0)
                ands.Add(Expression.In("UserId", userId));
            if (medalId > 0)
                ands.Add(Expression.Eq("MedalId", medalId));
            if (userAwardStatus.HasValue)
                ands.Add(Expression.Gt("DateCreated", userAwardStatus));
            AbstractCriterion where = null;
            foreach (var item in ands)
            {
                where = where ? item : Expression.And(where, item);
            }
            criteria = (where == null ? criteria : criteria.Add(where));
            criteria.AddOrder(Order.Desc("DateCreated")); 
            return (criteria == null ? criteria : criteria.Add(where)).SetProjection(Projections.RowCount()).UniqueResult<int>();
        }

        /// <summary>
        /// 获取勋章授予记录（后台管理）
        /// </summary>
        /// <returns></returns>
        public static int GetsMedalToUserCountByApplying(ISession session)
        {
            var criteria = session.CreateCriteria<MedalToUser>();
            criteria.Add(Expression.Eq("UserAwardStatus", UserAwardStatus.Applying));
            return criteria.SetProjection(Projections.RowCount()).UniqueResult<int>();
        }

        /// <summary>
        /// 获取积分任务需审核内容计数
        /// </summary>
        /// <param name="taskRecordStatus">领取状态</param>
        /// <param name="taskId">任务Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static int GetRecordsCount(ISession session,TaskRecordStatus? taskRecordStatus = null, long taskId = 0, List<string> userId = null)
        {
            var criteria = session.CreateCriteria<PointRecord>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            if (userId != null && userId.Count > 0)
                ands.Add(Expression.In("UserId", userId));
            if (taskRecordStatus.HasValue)
                ands.Add(Expression.Eq("Status", taskRecordStatus));
            if (taskId > 0)
                ands.Add(Expression.Eq("TaskId", taskId));
            AbstractCriterion where = null;
            foreach (var item in ands)
            {
                where = where ? item : Expression.And(where, item);
            }
            criteria = (where == null ? criteria : criteria.Add(where));
            criteria.AddOrder(Order.Desc("DateCreated"));
            return (criteria == null ? criteria : criteria.Add(where)).SetProjection(Projections.RowCount()).UniqueResult<int>();

        
        }
        
        /// <summary>
        /// 获取积分任务需审核内容计数
        /// </summary>
        /// <returns></returns>
        public static int GetRecordsCountByApplying(ISession session)
        {
            var criteria = session.CreateCriteria<PointRecord>();
            criteria.Add(Expression.Eq("UserAwardStatus", UserAwardStatus.Applying));
            return criteria.SetProjection(Projections.RowCount()).UniqueResult<int>();

        }
    }
}
