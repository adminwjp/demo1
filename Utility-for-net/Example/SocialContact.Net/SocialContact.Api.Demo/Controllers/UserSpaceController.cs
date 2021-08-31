//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SocialContact.Application.Services;
using SocialContact.Domain.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Common.Configuration;
using Tunynet.Email;
using Tunynet.FileStore;
using Tunynet.PayServer;
using Tunynet.Post;
using Tunynet.Settings;
using Tunynet.Utilities;
using Utility.Web.Extensions;
namespace Tunynet.Spacebuilder
{
    //[FrontEndAuthorize]
    [UserAuthorize(IsAllowAnonymous = true)]
    public partial class UserSpaceController : Controller
    {
        private IUser _currentUser = UserContext.CurrentUser;
        private UserService userService;
        private AccountBindingService accountBindingService;
        private ValidateCodeService validateCodeService;
        private UserProfileService userProfileService;
        private MembershipService membershipService;
        private IAuthenticationService authenticationService;
        private SiteSettings siteSetting;
        private ContentItemService contentItemService;
        private CommentService commentService;
        private CountService TagCountService = new CountService(TenantTypeIds.Instance().Tag());

        //贴子
        private SectionService sectionService;

        private ThreadService threadService;

        //分类
        private CategoryService categoryService;

        //用户等级
        private UserRankService userRankService;

        private FollowService followService;

        //积分
        private PointService pointService;

        private UserSettings userSetting;

        //收藏
        private FavoriteService favoriteService;

        private AttachmentService attachmentService = new AttachmentService(TenantTypeIds.Instance().ContentItem());

        //邀请朋友
        private InviteFriendService inviteFriendService;

        //附件
        private AttachmentService newsAttachmentService = new AttachmentService(TenantTypeIds.Instance().CMS_Article());

        //标签
        private TagService tagService = new TagService(TenantTypeIds.Instance().ContentItem());

        //问答
        //栏目
        private ContentCategoryService contentCategoryService;

        //通知
        private NoticeService noticeService;

        private IKvStore kvStore;
        private INoticeSender noticeSender;
        private ContentModelService contentModelService;
        private InviteFriendSettings inviteFriendSetting;
        private Authorizer authorizer;



        //勋章
        private MedalService medalService;

        //积分任务
        private PointTaskService pointTaskService;

        private PointRechargeOrderService pointRechargeOrderService;
        private PayService payService;
     

        #region 用户空间

        /// <summary>
        /// 头像局部视图异步加载  5.0 未使用
        /// </summary>
        /// <param name="spaceKey">空间标示</param>
        public ActionResult _EditAvatarAsyn(string spaceKey)
        {
            var session = GlobalHelper.GetSession();
            using var tx = session.BeginTransaction();
            User user = UserService.GetUserByUserName(session, spaceKey);
            if (user == null)
                return new EmptyResult();
            IStoreFile iStoreFile = userService.GetAvatar(user.UserId, AvatarSizeType.Original);
            return View();
        }

        /// <summary>
        /// 上传头像分布页
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        public PartialViewResult _Avatar()
        {
            return PartialView();
        }

        #region 勋章

        /// <summary>
        ///  勋章馆
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MedalShop()
        {
            return View(medalService.GetTopsMedalToUser(10));
        }

        /// <summary>
        ///  勋章馆列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListMedalShop()
        {
            IEnumerable<Medal> medals = medalService.Gets();
            return PartialView(medals);
        }

        /// <summary>
        /// 授予用户勋章操作(勋章馆界面)
        /// </summary>
        /// <param name="medaltoUserId">勋章授予用户Id</param>
        /// <param name="method">方法名（Abandoned:放弃勋章，CancelApplying：取消申请，Applying:申请）</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeMedaltoUserStatus(long medaltoUserId, string method)
        {
            string msg = "";

            switch (method)
            {
                case "Abandoned":
                    {
                        var medaltoUser = medalService.GetMedalToUser(medaltoUserId);
                        medaltoUser.UserAwardStatusBefore = medaltoUser.UserAwardStatus;
                        medaltoUser.ManagerId = _currentUser.UserId;
                        medaltoUser.DateCreated = DateTime.Now;
                        medaltoUser.UserAwardStatus = UserAwardStatus.Abandoned;
                        medalService.UpdateMedalToUser(medaltoUser);
                        msg = "放弃勋章成功！";
                        break;
                    }
                case "CancelApplying":
                    {
                        medalService.DeleteMedalToUser(medaltoUserId);
                        msg = "取消申请成功！";
                        break;
                    }
                case "Applying":
                    {
                        var medalId = medaltoUserId;
                        var medalinConditions = medalService.GetMedalInConditionBymedalId(medalId);
                        var canUserApply = true;
                        foreach (var medalinCondition in medalinConditions)
                        {
                            var conditionInfo = medalinCondition.GetConditionInfo();
                            var checkNum = medalService.MedalRunMethod(conditionInfo.MethodName, _currentUser);

                            if (!(checkNum >= medalinCondition.MinCondition))
                            {
                                if (msg == "")
                                    msg = "申请条件不满足：<br/>申请需要的最小" + conditionInfo.ConditionName + "为" + medalinCondition.MinCondition + "，您当前的" + conditionInfo.ConditionName + "为" + checkNum;
                                else
                                    msg += ";<br/>申请需要的最小" + conditionInfo.ConditionName + "为" + medalinCondition.MinCondition + "，您当前的" + conditionInfo.ConditionName + "为" + checkNum;
                                canUserApply = false;
                            }
                        }
                        if (canUserApply)
                        {
                            MedalToUser medaltoUser = MedalToUser.New();
                            medaltoUser.ManagerId = _currentUser.UserId;
                            medaltoUser.UserAwardStatus = UserAwardStatus.Applying;
                            medaltoUser.UserId = _currentUser.UserId;
                            medaltoUser.UserDisplayName = string.IsNullOrEmpty(_currentUser.TrueName) ? _currentUser.UserName : _currentUser.TrueName;
                            medaltoUser.MedalId = medalId;
                            medalService.CreateMedalToUser(medaltoUser);
                            return Json(new StatusMessageData(StatusMessageType.Success, "申请成功！"));
                        }
                        else
                            return Json(new StatusMessageData(StatusMessageType.Error, msg));
                    }
                default:
                    break;
            }

            return Json(new StatusMessageData(StatusMessageType.Success, msg));
        }

        #endregion 勋章

        #region 积分任务

        /// <summary>
        ///  积分任务页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PointTask()
        {
            return View();
        }

        /// <summary>
        ///  积分任务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListPointTasks()
        {
            List<string> userId = new List<string>();
            userId.Add(_currentUser.UserId.ToString());
            string taskCompletedName = "";
            var tasks = pointTaskService.Gets(PointTaskStatus.Normal).Where(n => (!n.TaskType.IsSetDeadline || (n.TaskType.IsSetDeadline && n.Deadline > DateTime.Now)));
            //遍历完成的任务
            foreach (var task in tasks.Where(n => n.GetTaskToUser(_currentUser.UserId, TaskRecordStatus.Doing) != null).Where(n => n.CheckUserTask(_currentUser) == 100))
            {
                var record = task.GetTaskToUser(_currentUser.UserId, TaskRecordStatus.Doing);
                record.Status = TaskRecordStatus.Completed;
                record.DateCreated = DateTime.Now;
                pointTaskService.UpdateRecord(record);
                taskCompletedName += record.GetTaskInfo().TaskName + ",";
            }
            ViewData["taskCompletedName"] = taskCompletedName != "" ? taskCompletedName.Substring(0, taskCompletedName.Length - 1) : "";
            //金币经验名称
            ViewData["pointName"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().ExperiencePoints());
            ViewData["goldName"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().TradePoints());
            return PartialView(tasks);
        }

        /// <summary>
        ///  积分任务列表已完成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListCompletedPointTasks(int pageSize = 20, int pageIndex = 1)
        {
            List<string> userId = new List<string>();
            userId.Add(_currentUser.UserId.ToString());
            var taskCompleted = pointTaskService.GetRecords(TaskRecordStatus.Completed, 0, userId, pageSize, pageIndex);
            ViewData["taskCompleted"] = taskCompleted;
            //金币经验名称
            ViewData["pointName"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().ExperiencePoints());
            ViewData["goldName"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().TradePoints());
            return PartialView();
        }

        /// <summary>
        ///  积分任务详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _PointTaskDetail(long taskId)
        {
            var task = pointTaskService.Get(taskId);
            //金币经验名称
            ViewData["pointName"] = PointService.GetPointCategory(PointCategoryKeys.Instance().ExperiencePoints()).CategoryName;
            ViewData["goldName"] = PointService.GetPointCategory(PointCategoryKeys.Instance().TradePoints()).CategoryName;
            return PartialView(task);
        }

        /// <summary>
        ///  积分任务人工审核编辑页
        /// </summary>
        /// <param name="recordId">任务项Id</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _PointTaskEdit(long recordId)
        {
            var record = pointTaskService.GetRecord(recordId);
            var task = pointTaskService.Get(record.TaskId);
            ViewData["record"] = record;
            if (record != null && !string.IsNullOrEmpty(record.ResultContent))
            {
                //任务名组合
                ViewData["contents"] = JsonConvert.DeserializeObject<List<PointTaskSetting>>(record.ResultContent);
            }
            //任务名组合
            ViewData["options"] = JsonConvert.DeserializeObject<List<PointTaskSetting>>(task.TasksSettings);
            return PartialView(task);
        }

        /// <summary>
        /// 领取积分任务
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateTasktoUser(long taskId)
        {
            var task = pointTaskService.Get(taskId);
            if (_currentUser.Rank < task.MinUserRank)
                return Json(new StatusMessageData(StatusMessageType.Error, "用户等级不够，不能领取任务！"));
            List<string> userId = new List<string>();
            userId.Add(_currentUser.UserId.ToString());
            var records = pointTaskService.GetRecords(TaskRecordStatus.Abandoned, taskId, userId);
            foreach (var recordbefore in records)
            {
                pointTaskService.DeleteRecord(recordbefore.RecordId);
            }

            PointTaskRecord record = PointTaskRecord.New();
            if (!string.IsNullOrEmpty(task.TaskType.CheckMethodName))
                record.Status = TaskRecordStatus.Doing;
            else
                record.Status = TaskRecordStatus.Applying;
            record.UserId = _currentUser.UserId;
            record.UserDisplayName = string.IsNullOrEmpty(_currentUser.TrueName) ? _currentUser.UserName : _currentUser.TrueName;
            record.TaskId = taskId;
            pointTaskService.CreateRecord(record);
            return Json(new StatusMessageData(StatusMessageType.Success, "领取成功！"));
        }

        /// <summary>
        /// 放弃任务
        /// </summary>
        /// <param name="recordId">任务项Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AbandonedTask(long recordId)
        {
            var record = pointTaskService.GetRecord(recordId);
            if (record.Status == TaskRecordStatus.Applying || record.Status == TaskRecordStatus.Doing || record.Status == TaskRecordStatus.Refused)
            {
                record.Status = TaskRecordStatus.Abandoned;
                pointTaskService.UpdateRecord(record);
                return Json(new StatusMessageData(StatusMessageType.Success, "放弃成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "放弃失败！"));
        }


        /// <summary>
        /// 提交人工审核任务
        /// </summary>
        /// <param name="recordId">任务项Id</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PointTaskSubmit(long recordId, List<string> content)
        {
            //积分任务记录
            var record = pointTaskService.GetRecord(recordId);
            List<PointTaskSetting> contents = new List<PointTaskSetting>();
            List<string> contentStr = new List<string>();
            foreach (var item in content)
            {
                PointTaskSetting model = new PointTaskSetting();
                model.SettingName = item;
                contents.Add(model);
            }
            //json对象转为字符串
            record.ResultContent = JsonConvert.SerializeObject(contents);
            record.Status = TaskRecordStatus.Applying;
            record.DateCreated = DateTime.Now;
            pointTaskService.UpdateRecord(record);
            return Json(new StatusMessageData(StatusMessageType.Success, "提交成功！"));
        }

        /// <summary>
        /// 分享的内容
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <returns></returns>
        public ActionResult SharePage(long taskId)
        {
            PointTask task = pointTaskService.Get(taskId);
            PointTaskShareSetting setting = JsonConvert.DeserializeObject<PointTaskShareSetting>(task.TasksSettings);
            ViewData["setting"] = setting;
            return View(task);
        }

        /// <summary>
        /// 分享回调
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ShareCallBack(long taskId, long userId)
        {
            kvStore.Increase("ShareOut_UserId_" + userId + "_TaskId_" + taskId);
            return Json(new StatusMessageData(StatusMessageType.Success, "提交成功！"));
        }

        #endregion 积分任务

        /// <summary>
        ///  他、她的主页
        /// </summary>
        /// <param name="spaceKey">当前用户</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SpaceHomepage(string spaceKey)
        {
            User userHolder = UserService.GetUser(spaceKey);
            if (userHolder == null || userHolder.Status == UserStatus.Delete)
                return Redirect(SiteUrls.Instance().Error());
            if (_currentUser != null && _currentUser.UserId == userHolder.UserId)
                return RedirectToAction("MyHomepage", new { spaceKey = spaceKey });

            ViewData["userHolder"] = userHolder;

            //用户资料
            var userProfile = UserProfileService.Get(userHolder.UserId);

            //文章数量
            var cmsCount = 0;
            //评论个数
            int conmmentCount = 0;
            //贴子数
            int threadCount = 0;
            //提问计数
            int userAskAnswerCount = UserServiceExtension.GetUserQuestionCount(userHolder.UserId, true);
            ViewData["userAskAnswerCount"] = userAskAnswerCount > 0 ? userAskAnswerCount : 0;

            //文档计数
            int userDocumentCount = 0;
            kvStore.TryGet<int>(KvKeys.Instance().UserDocumentUploadCount(userHolder.UserId, AuditStatus.Success), out userDocumentCount);
            ViewData["userDocumentCount"] = userDocumentCount;

            //活动计数
            var eventCount = kvStore.Get(KvKeys.Instance().UserEventCount(userHolder.UserId, AuditStatus.Success));
            ViewData["userEventCount"] = eventCount == null ? "0" : eventCount.Value;

            cmsCount = UserServiceExtension.GetUserContentItemCount(userHolder.UserId, "");
            conmmentCount = UserServiceExtension.GetUserCommentCount(userHolder.UserId, null);
            threadCount = UserServiceExtension.GetUserThreadCount(userHolder.UserId, TenantTypeIds.Instance().Thread());

            if (_currentUser != null)
            {
                //关注信息
                var isMutualFollowed = followService.IsMutualFollowed(_currentUser.UserId, userHolder.UserId);
                ViewData["isMutualFollowed"] = isMutualFollowed;
                //是否为互相关注
                if (!isMutualFollowed)
                {
                    ViewData["isFollowed"] = followService.IsFollowed(_currentUser.UserId, userHolder.UserId);
                }
            }

            ViewData["userProfile"] = userProfile;
            ViewData["cmsCount"] = cmsCount;
            ViewData["conmmentCount"] = conmmentCount;
            ViewData["threadCount"] = threadCount;

            return View();
        }

        /// <summary>
        /// 我的主页
        /// </summary>
        /// <param name="spaceKey">当前用户</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyHomepage(string spaceKey)
        {
            User userHolder = UserServiceExtension.GetFullUser(spaceKey);
            if (userHolder == null || userHolder.Status == UserStatus.Delete)
            {
                return Redirect(SiteUrls.Instance().Error());
            }
            ViewData["userHolder"] = userHolder;

            if (_currentUser == null || _currentUser.UserId != userHolder.UserId)
                return RedirectToAction("SpaceHomepage", new { spaceKey });
            //用户资料
            var userProfile = UserProfileService.Get(userHolder.UserId);
            //文章数量
            int cmsCount = 0;
            //评论计数
            int conmmentCount = 0;
            //贴子计数
            int threadCount = 0;
            //收藏计数
            int favoriteCount = 0;
            //提问计数
            int userAskAnswerCount = UserServiceExtension.GetUserQuestionCount(userHolder.UserId, true);
            ViewData["userAskAnswerCount"] = userAskAnswerCount > 0 ? userAskAnswerCount : 0;
            //文档计数
            int userDocumentCount = 0;
            kvStore.TryGet<int>(KvKeys.Instance().UserDocumentUploadCount(userHolder.UserId), out userDocumentCount);
            ViewData["userDocumentCount"] = userDocumentCount;
            //活动计数
            var eventCount = kvStore.Get(KvKeys.Instance().UserEventCount(userHolder.UserId));
            ViewData["userEventCount"] = eventCount == null ? "0" : eventCount.Value;
            cmsCount = UserServiceExtension.GetUserContentItemCount(userHolder.UserId, "", true);
            conmmentCount = UserServiceExtension.GetUserCommentCount(userHolder.UserId, null, true);
            threadCount = UserServiceExtension.GetUserThreadCount(userHolder.UserId, TenantTypeIds.Instance().Thread(), true);
            kvStore.TryGet<int>(KvKeys.Instance().UserFavoriteCount(userHolder.UserId), out favoriteCount);
            ViewData["cmsCount"] = cmsCount;
            ViewData["favoriteCount"] = favoriteCount;
            ViewData["userProfile"] = userProfile;
            ViewData["conmmentCount"] = conmmentCount;
            ViewData["threadCount"] = threadCount;
            //todo by yangzd
            ViewData["DisplayName"] = userHolder.DisplayName;
            return View();
        }

        /// <summary>
        /// 我的主页（分页）
        /// </summary>
        /// <param name="spaceKey">当前用户</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _MyHomepage(string spaceKey)
        {
            User userHolder = UserServiceExtension.GetFullUser(spaceKey);
            if (userHolder == null || userHolder.Status == UserStatus.Delete)
                return Redirect(SiteUrls.Instance().Error());
            PagingDataSet<ContentItem> contentItems = new PagingDataSet<ContentItem>(new List<ContentItem>());

            if (_currentUser == userHolder)
                contentItems = ContentItemService.GetContentItems(null, true, userHolder.UserId, 10, 1, true, ContentItemSortBy.DatePublished_Desc, true);
            else
                contentItems = ContentItemService.GetContentItems(null, true, userHolder.UserId, 10, 1, true, ContentItemSortBy.DatePublished_Desc, false);

            var follow = followService.GetFollowerUserIds(userHolder.UserId, Follow_SortBy.DateCreated_Desc, 1);
            if (follow != null)
                ViewData["follow"] = UserServiceExtension.GetFullUsers(follow.Take(6));
            List<string> userIds = new List<string>();
            userIds.Add(userHolder.UserId.ToString());
            ViewData["medals"] = medalService.GetsMedalToUser(userIds, 0, UserAwardStatus.AlreadyAward, 9999, 1);

            ViewData["follows"] = followService.GetTopFollows(userHolder.UserId, 6, null);
            ViewData["contentItems"] = contentItems;
            ViewData["threads"] = ThreadService.GetUserThreads(TenantTypeIds.Instance().Thread(), userHolder.UserId, true, 10, 1);
            ViewData["userHolder"] = userHolder;
            //用户资料
            ViewData["userProfile"] = UserProfileService.Get(userHolder.UserId);

            return PartialView();
        }

        /// <summary>
        /// 我、他（她）的文章
        /// </summary>
        /// <param name="spaceKey">当前用户</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _MyCMS(string spaceKey)
        {
            var userHolder = UserService.GetUser(spaceKey);
            if (userHolder == null || userHolder.Status == UserStatus.Delete)
                return Redirect(SiteUrls.Instance().Error());
            ViewData["userHolder"] = userHolder;
            ViewData["categoryCount"] = GetCategories().Count();
            return PartialView();
        }

        /// <summary>
        /// 我、他（她）的文章分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _ListMyCMS(long userId, int pageSize = 6, int pageIndex = 1)
        {
            PagingDataSet<ContentItem> contentItems = new PagingDataSet<ContentItem>(new List<ContentItem>());
            var userHolder = userService.GetFullUser(userId);
            if (userHolder == null || userHolder.Status == UserStatus.Delete)
            {
                return Redirect(SiteUrls.Instance().Error());
            }
            contentItems = ContentItemService.GetContentItems(null, true, userId, pageSize, pageIndex, true, ContentItemSortBy.DatePublished_Desc, (_currentUser != null && userId == _currentUser.UserId), null);
            ViewData["userId"] = userId;

            return PartialView(contentItems);
        }

        /// <summary>
        /// 写文章
        /// </summary>
        /// <param name="contentItemId">资讯ID</param>
        /// <param name="contentCategoryId">栏目ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _EditCMS(long? contentItemId, int? contentCategoryId)
        {
            if (_currentUser == null)
            {
                return Redirect(SiteUrls.Instance().Error());
            }
            var contentItem = ContentItem.New();
            var contentItemEditModel = new ContentItemEditModel();

            if (contentItemId.HasValue && contentItemId > 0)
            {
                contentItem = ContentItemService.Get(contentItemId.Value);
                if (contentItem == null)
                    return Redirect(SiteUrls.Instance().Error());
                contentItem.MapTo(contentItemEditModel);
                contentItemEditModel.AuditStatus = (int)contentItem.ApprovalStatus;
                attachmentService = new AttachmentService(TenantTypeIds.Instance().CMS_Article());
                ViewData["attachmentList"] = AttachmentService.GetsByAssociateId(contentItemId.Value);
                //栏目
                if (contentCategoryId.HasValue && contentCategoryId.Value > 0)
                {
                    //获取栏目
                    var category = ContentCategoryService.Get(contentCategoryId.Value);
                    if (category == null)
                        return Redirect(SiteUrls.Instance().Error());
                    ViewData["category"] = category;
                }
                //获取标签
                if (contentItemId.HasValue && contentItemId.Value > 0)
                {
                    var tagsOfItem = tagService.attiGetItemInTagsOfItem(contentItemId.Value);
                    ViewData["tagsOfItem"] = tagsOfItem;
                }
            }
            ViewData["categorylist"] = GetCategories();
            contentItemEditModel.ContentModelId = contentModelService.GetContentModelsByContentModeKey(ContentModelKeys.Instance().Contribution()).ModelId;

            return PartialView(contentItemEditModel);
        }

        #region 资讯扩展

        public List<SelectListItem> GetCategories()
        {
            List<SelectListItem> categorylist = new List<SelectListItem>();
            var categories = ContentCategoryAppService.GetIndentedAllCategories(null).Where(n => n.ContentModelKeys.Contains(ContentModelKeys.Instance().Contribution())).Where(c => c.IsEnabled);
            for (int i = 0; i < categories.Count(); i++)
            {
                var folder = categories.ElementAt(i);
                var selecttext = string.Format("{0}", folder.CategoryName);
                if (folder.Depth == 1)
                {
                    selecttext = string.Format("{0}{1}", "-", folder.CategoryName);
                }
                if (folder.Depth > 1)
                {
                    selecttext = string.Format("{0}{1}", "──", folder.CategoryName);
                }

                categorylist.Add(new SelectListItem { Text = selecttext, Value = folder.CategoryId.ToString() });
            }
            return categorylist;
        }

        #endregion 资讯扩展

        /// <summary>
        /// 写文章
        /// </summary>
        /// <param name="contentItemEditModel">资讯</param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public ActionResult _EditCMS(ContentItemEditModel contentItemEditModel)
        {
            if (_currentUser == null)
            {
                return Redirect(SiteUrls.Instance().Error());
            }
            var setting = SiteSettings.Get() ?? new SiteSettings();

            Tag tag = Tag.New();
            var contentItem = ContentItem.New();
            contentItem = contentItemEditModel.AsContentItem(Request);
            var contentItemModel = new ContentItem();
            //编辑
            if (contentItemEditModel.ContentItemId > 0)
            {
                contentItemModel = contentItemService.Get(contentItemEditModel.ContentItemId);
                if (contentItemModel == null)
                    return Redirect(SiteUrls.Instance().Error());
                //处理标签计数         
                if ((int)contentItemModel.ApprovalStatus >= (int)setting.AuditStatus)
                {
                    var oldTags = tagService.GetTopTagsOfItem(contentItemModel.ContentItemId, 100);
                    foreach (var item in oldTags)
                    {
                        CountService.ChangeCount(new ChangeCountEvent()
                        {
                            countType = CountTypes.Instance().ItemCount(),
                            objectId = item.TagId,
                            ownerId = item.TagId,
                            changeCount = -1,
                            isRealTime = true
                        });
                    }
                }
                //编辑前是草稿
                if (contentItem.ApprovalStatus == 0)
                {
                    contentItem.IsDraft = true;
                }
                else
                {
                    contentItem.IsDraft = false;
                }

                contentItem.DatePublished = contentItemModel.DatePublished;

                //编辑后不是草稿
                if (contentItemEditModel.IsDraft == false && contentItem.ApprovalStatus == 0)
                {
                    contentItem.ApprovalStatus = AuditStatus.Pending;
                    contentItem.DatePublished = DateTime.Now;
                    contentItem.LastModified = DateTime.Now;
                }

                contentItem.IsVisible = true;
                ContentItemService.Update(contentItem, TenantTypeIds.Instance().CMS_Article(), authorizer.IsCategoryManager(TenantTypeIds.Instance().CMS_Article(), UserContext.CurrentUser, contentItemEditModel.CategoryId));
                //标签
                if (contentItem.ContentModel.ModelKey == ContentModelKeys.Instance().Contribution())
                {
                    tagService.ClearTagsFromItem(contentItemEditModel.ContentItemId);

                    if (contentItemEditModel.tagvalue != null)
                    {
                        tagService.AddTagsToItem(contentItemEditModel.tagvalue.ToArray(), contentItemEditModel.ContentItemId);
                        //处理标签计数         
                        if ((int)contentItem.ApprovalStatus >= (int)setting.AuditStatus)
                        {
                            foreach (var item in contentItemEditModel.tagvalue)
                            {
                                var newtag = tagService.Get(item);
                                CountService.ChangeCount(new ChangeCountEvent()
                                {
                                    countType = CountTypes.Instance().ItemCount(),
                                    objectId = newtag.TagId,
                                    ownerId = newtag.TagId,
                                    changeCount = 1,
                                    isRealTime = true
                                });
                            }
                        }
                    }
                }
            }
            //创建用户投稿
            else
            {
                contentItem.ApprovalStatus = AuditStatus.Fail;
                //草稿
                if (contentItemEditModel.IsDraft == true)
                    contentItem.ApprovalStatus = 0;
                contentItem.IsVisible = true;
                contentItem.IsAllowMobileEdit = false;
                if (!ContentItemService.Create(contentItem, TenantTypeIds.Instance().CMS_Article(), authorizer.IsCategoryManager(TenantTypeIds.Instance().ContentItem(), UserContext.CurrentUser, contentItemEditModel.CategoryId)))
                {
                    return Json(new StatusMessageData(StatusMessageType.Error, "发布文章过于频繁"));
                }
                if (contentItem.ContentModel.ModelKey == ContentModelKeys.Instance().Contribution())
                {
                    if (contentItemEditModel.tagvalue != null)
                    {
                        tagService.AddTagsToItem(contentItemEditModel.tagvalue.ToArray(), contentItem.ContentItemId);

                        foreach (var item in contentItemEditModel.tagvalue)
                        {
                            tag = tagService.Get(item);
                            //处理标签计数         
                            if ((int)contentItem.ApprovalStatus >= (int)setting.AuditStatus)
                            {
                                TagCountService.ChangeCount(CountTypes.Instance().ItemCount(), tag.TagId, tag.TagId, 1, true);
                            }
                        }
                    }
                }
            }
            return Json(new StatusMessageData(StatusMessageType.Success, "保存成功"));
            //return Redirect(SiteUrls.Instance().MyHome(_currentUser.UserId) + "#cms");
        }

        /// <summary>
        ///单个删除资讯
        /// </summary>
        /// <param name="contentItemId">内容项Id</param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _DeleteCMS(long contentItemId)
        {
            var contentItem = ContentItemService.Get(contentItemId);
            if (contentItem.IsAuthorizer())
            {
                ContentItemService.Delete(contentItem);
                return Json(new StatusMessageData(StatusMessageType.Success, "删除成功"));
            }
            else
                return Json(new StatusMessageData(StatusMessageType.Hint, "无权操作"));
        }

        ///<summary>
        ///草稿列表
        /// </summary>
        public ActionResult _ListDraft(long userId)
        {
            var userHolder = UserService.GetUser(userId);
            if (userHolder == null || userHolder.Status == UserStatus.Delete)
                return Redirect(SiteUrls.Instance().Error());

            var contentItems = ContentItemService.GetContentItems(null, null, userId, 20, 1, true, ContentItemSortBy.DatePublished_Desc, true, null, 0);
            return PartialView(contentItems);
        }

        #region 贴子

        /// <summary>
        /// 我的贴子
        /// </summary>
        /// <param name="spaceKey"></param>
        /// <returns></returns>
        public PartialViewResult _MyPost(string spaceKey)
        {

            var session = GlobalHelper.GetSession();
            var user = session.Query<User>().Where(it => it.UserName == spaceKey).FirstOrDefault();
            var userProfile = session.Query<UserProfile>().Where(it => it.UserId == user.UserId).FirstOrDefault();

            var sectionIds = favoriteService.GetPagingPartObjectIds(user.UserId, TenantTypeIds.Instance().Bar(), 1, null).Select(n => n.ObjectId).Distinct();

            var sectionList = sectionService.GetBarSections(sectionIds);

            ViewData["user"] = user;
            ViewData["userProfile"] = userProfile;
            ViewData["mysections"] = sectionList;

            return PartialView();
        }

        /// <summary>
        /// 我的贴子 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListMyPost(long userId, int pageSize = 10, int pageIndex = 1)
        {
            var threads = threadService.GetUserThreads(TenantTypeIds.Instance().Thread(), userId, _currentUser != null && _currentUser.UserId == userId, pageSize, pageIndex);

            ViewData["user"] = userService.GetUser(userId);

            return PartialView(threads);
        }

        #endregion 贴子

        #region 我的评论/Ta的评论

        /// <summary>
        /// 我的评论/Ta的评论
        /// </summary>
        /// <param name="spaceKey">用户空间传来的用户名</param>
        /// <param name="isReceived">isReceived=true 为收到的评论,=false 为发出的评论为</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _UserSpaceComments(string spaceKey)
        {
            User spaceUser = userService.GetFullUser(spaceKey);
            ViewData["spaceUser"] = spaceUser;

            return PartialView();
        }

        /// <summary>
        /// 我的评论/Ta的评论 列表
        /// </summary>
        /// <param name="spaceKey">用户空间传来的用户名</param>
        /// <param name="isReceived">isReceived=true 为收到的评论,=false 为发出的评论为</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListComments(string spaceKey, bool isReceived = true, int pageSize = 10, int pageIndex = 1)
        {
            PagingDataSet<Comment> userSpaceComments;

            User spaceUser = userService.GetFullUser(spaceKey);

            var spaceUserProfile = userProfileService.Get(spaceUser.UserId);

            //空间用户==当前用户，说明当前是我的空间
            if (_currentUser != null && spaceUser.UserId == _currentUser.UserId)
            {
                if (isReceived == true)
                {
                    //获取用户收到的评论
                    userSpaceComments = commentService.GetOwnerComments(_currentUser.UserId, null, null, null, pageSize, pageIndex);
                }
                else
                {
                    //获取用户发布的评论
                    userSpaceComments = commentService.GetComments(null, null, _currentUser.UserId, null, null, pageSize, pageIndex);
                }
            }
            else
            {
                //获取Ta发布的评论
                userSpaceComments = commentService.GetUserComments(spaceUser.UserId, null, null, null, pageSize, pageIndex);
            }

            Dictionary<string, string> tenantTypes = new Dictionary<string, string>
            {
                {TenantTypeIds.Instance().ContentItem(),"文章"},
                {TenantTypeIds.Instance().Thread() ,"贴子"}
            };

            ViewData["gender"] = spaceUserProfile?.Gender == GenderType.Male ? "他" : "她";
            ViewData["spaceUser"] = spaceUser;
            ViewData["tenantTypes"] = tenantTypes;
            ViewData["isReceived"] = isReceived;

            return PartialView(userSpaceComments);
        }

        #endregion 我的评论/Ta的评论

        #region 我收藏的文章/贴子

        /// <summary>
        /// 我收藏的文章/贴子
        /// </summary>
        /// <param name="isContentItem">isContentItem=true 为我收藏的文章,=false 为我收藏的贴子</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListMyFavorites(bool isContentItem = true, int pageIndex = 1, int pageSize = 20)
        {
            string tenantTypeId = (isContentItem == true ? TenantTypeIds.Instance().ContentItem() : TenantTypeIds.Instance().Thread());

            var myFavorites = favoriteService.GetPagingPartObjectIds(_currentUser.UserId, tenantTypeId, pageIndex, pageSize);
            var myFavoriteIds = myFavorites.Select(n => n.ObjectId).Distinct();

            if (myFavoriteIds.Any())
            {
                if (tenantTypeId == TenantTypeIds.Instance().ContentItem())
                {
                    //根据收藏Id获取收藏的资讯
                    ViewData["contentItems"] = ContentItemService.Gets(myFavoriteIds);
                }
                else
                {
                    //根据收藏Id获取收藏的贴子
                    ViewData["threads"] = threadService.Gets(myFavoriteIds);
                }
            }

            ViewData["isContentItem"] = isContentItem;

            return PartialView(myFavorites);
        }

        /// <summary>
        /// 文章/贴子 取消收藏
        /// </summary>
        /// <param name="objectId">文章/贴子ID</param>
        /// <param name="isContentItem">是否为文章</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CancelFavorite(long objectId, bool isContentItem = true)
        {
            string tenantTypeId = (isContentItem == true ? TenantTypeIds.Instance().ContentItem() : TenantTypeIds.Instance().Thread());
            var thisFavoriteService = new FavoriteService(tenantTypeId);
            if (thisFavoriteService.CancelFavorite(objectId, _currentUser.UserId))
                return Json(new StatusMessageData(StatusMessageType.Success, "取消收藏成功"));
            return Json(new StatusMessageData(StatusMessageType.Hint, "操作失败"));
        }

        #endregion 我收藏的文章/贴子

        #region 用户关注

        /// <summary>
        /// 关注、粉丝、邀请管理
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="follow">粉丝/关注列表标识 关注列表为"focus" 粉丝为"fans"默认为粉丝列表</param>
        /// <returns></returns>
        public PartialViewResult _ManageMyFollow(long userId, string follow)
        {
            ViewData["follow"] = follow;
            ViewData["userProfile"] = UserProfileService.Get(userId);
            ViewData["userId"] = userId;

            return PartialView();
        }

        /// <summary>
        /// 关注/取消关注用户
        /// </summary>
        /// <param name="targetUserId">需要关注/取消关注的用户Id</param>
        /// <returns></returns>
        public JsonResult _FollowUser(long targetUserId)
        {
            if (targetUserId == _currentUser.UserId)
            {
                return Json(new { state = 0, errormsg = "关注用户失败" });
            }

            if (followService.IsFollowed(_currentUser.UserId, targetUserId))
            {
                followService.CancelFollow(_currentUser.UserId, targetUserId);

                return Json(new { state = 1, successmsg = "取消关注成功", cancelfollow = true });
            }
            else
            {
                if (followService.Follow(_currentUser.UserId, targetUserId))
                {
                    //通知被关注用户
                    Notice notice = Notice.New();
                    notice.NoticeTypeKey = NoticeTypeKeys.Instance().FollowUser();
                    notice.Body = "";
                    notice.LeadingActor = _currentUser.DisplayName;
                    notice.LeadingActorUserId = _currentUser.UserId;
                    notice.ReceiverId = targetUserId;
                    notice.LeadingActorUrl = SiteUrls.Instance().SpaceHome(_currentUser.UserId);
                    noticeService.Create(notice);
                    noticeSender.Send(notice);

                    if (followService.IsMutualFollowed(_currentUser.UserId, targetUserId))
                    {
                        return Json(new { state = 1, successmsg = "关注用户成功", isMutualFollowed = true });
                    }
                    else
                    {
                        return Json(new { state = 1, successmsg = "关注用户成功" });
                    }
                }
                else
                {
                    return Json(new { state = 0, errormsg = "关注用户失败" });
                }
            }
        }

        /// <summary>
        /// 为用户设置备注名
        /// </summary>
        /// <param name="targetUserId">目标用户Id</param>
        /// <returns></returns>
        [UserAuthorize]
        public JsonResult _SetNoteName(long targetUserId, string noteName)
        {
            if (followService.IsFollowed(_currentUser.UserId, targetUserId))
            {
                FollowEntity follow = followService.Get(_currentUser.UserId, targetUserId);
                follow.NoteName = noteName;
                followService.Update(follow);

                return Json(new { state = 1 });
            }
            else
            {
                return Json(new { state = 0 });
            }
        }

        /// <summary>
        /// 关注列表分布页
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="groupId">用户分组Id</param>
        /// <param name="sortBy">排序条件</param>
        /// <param name="pageIndex">分页号</param>
        /// <returns></returns>
        public PartialViewResult _MyFocus(long userId, long? groupId, int pageIndex = 1, int pageSize = 10)
        {
            set(userId,groupId,pageIndex, pageSize,
            out PagingDataSet<long> followedUserIds, out IEnumerable<User> fullUserList, out IEnumerable<UserProfile> followedUserProfile);

            return PartialView(followedUserIds);
        }

        void set(long userId, long? groupId, int pageIndex , int pageSize ,
            out PagingDataSet<long> followedUserIds, out IEnumerable<User> fullUserList,out IEnumerable<UserProfile> followedUserProfile)
        {
            //关注的用户Id列表
             followedUserIds =groupId.HasValue? followService.GetFollowedUserIds(userId, groupId, Follow_SortBy.DateCreated_Desc, pageIndex, pageSize)
             : followService.GetFollowerUserIds(userId, Follow_SortBy.DateCreated_Desc, pageIndex, pageSize);

            //用户列表
            fullUserList = UserServiceExtension.GetFullUsers(followedUserIds);
            //关注用户信息列表
             followedUserProfile = UserProfileService.GetUserProfiles(followedUserIds);

            ViewData["fullUserList"] = fullUserList;
            ViewData["myUserId"] = userId;
            ViewData["followedUserProfile"] = followedUserProfile;
        }
        /// <summary>
        /// 粉丝列表分布页
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="pageIndex">分页号</param>
        /// <returns></returns>
        public PartialViewResult _MyFans(long userId, int pageIndex = 1, int pageSize = 10)
        {
            set(userId, null, pageIndex, pageSize,
             out PagingDataSet<long> followerIds, out IEnumerable<User> fullUserList, out IEnumerable<UserProfile> followerProfile);

            if (followerIds != null && followerIds.Any())
            {
                var deleteuser = followerIds.Except(fullUserList.Select(n => n.UserId));

                foreach (var item in deleteuser)
                {
                    followService.RemoveFollower(userId, item);
                    followerIds.ToList().Remove(item);
                }
            }


            //获取当前用户
            if (UserContext.CurrentUser != null)
            {
                ViewData["currentUser"] = UserServiceExtension.GetFullUser(UserContext.CurrentUser.UserId);
            }

            return PartialView(followerIds);
        }

        #endregion 用户关注

        #region 邀请用户

        /// <summary>
        /// 邀请朋友分布页
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public PartialViewResult _InviteFriend()
        {
            string inviteCode = inviteFriendService.GetInvitationCode(UserContext.CurrentUser.UserId);

            InvitationCode invitationCode = inviteFriendService.GetInvitationCodeEntity(inviteCode);
            ViewData["invitationCode"] = inviteCode;
            return PartialView();
        }

        /// <summary>
        /// 我邀请的朋友列表
        /// </summary>
        [UserAuthorize]
        public PartialViewResult _MyInvitedFriendsList(int pageSize = 5, int pageIndex = 1)
        {
            long totalRecords;
            //邀请朋友Id列表
            IEnumerable<long> friendsIds = inviteFriendService.GetMyInviteFriendRecords(UserContext.CurrentUser.UserId, pageSize, pageIndex, out totalRecords);
            //邀请朋友列表
            IEnumerable<User> friendsList = UserServiceExtension.GetFullUsers(friendsIds);
            //邀请朋友用户资料列表
            IEnumerable<UserProfile> friendsProfile = UserProfileService.GetUserProfiles(friendsIds);
            ViewData["friendsList"] = friendsList;
            ViewData["friendsProfile"] = friendsProfile;
            return PartialView(new PagingDataSet<long>(friendsIds)
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
        }

        #endregion 邀请用户

        #endregion 用户空间

        #region 用户资料设置

        /// <summary>
        /// 用户资料设置
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public ActionResult UserSetting()
        {
            UserProfileEditModel userProfileEditModel = new UserProfileEditModel();

            var userProfile = UserProfileService.Get(_currentUser.UserId);

            userProfile.MapTo(userProfileEditModel);

            userProfileEditModel.TrueName = _currentUser.TrueName;
            userProfileEditModel.UserName = _currentUser.UserName;
            userProfileEditModel.HasAvatar = _currentUser.HasAvatar;
            userProfileEditModel.Introduction = userProfileEditModel.Introduction?.Replace("<br />", "\r\n").Replace("&nbsp;", " ");
            return View(userProfileEditModel);
        }

        /// <summary>
        /// 用户资料设置
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public ActionResult UserSetting(UserProfileEditModel userProfileEditModel)
        {
            var fullUser = UserService.GetUser(_currentUser.UserId);
            if (fullUser != null)
            {
                var historyData = fullUser.Clone() as User;
                fullUser.TrueName = userProfileEditModel.TrueName;

                if (!UserServiceExtension.CanEditDisplayName(fullUser, historyData))
                {
                    return Json(-1);
                }
                var userProfile = UserProfileService.Get(_currentUser.UserId);
                if (userProfile == null)
                {
                    UserProfile newUserProfile = UserProfile.New(_currentUser.UserId);
                    set(newUserProfile, userProfileEditModel);
                    UserProfileService.Create(newUserProfile);
                }
                else
                {
                    set(userProfile, userProfileEditModel);
                    UserProfileService.Update(userProfile);
                }
                MembershipService.UpdateUser(fullUser, historyData);
                return Json(1);
            }

            return Json(0);
        }
        void set(UserProfile newUserProfile, UserProfileEditModel userProfileEditModel)
        {
            newUserProfile.Gender = userProfileEditModel.Gender;
            newUserProfile.NowAreaCode = userProfileEditModel.NowAreaCode;
            newUserProfile.Introduction = userProfileEditModel.Introduction;
        }
        /// <summary>
        /// 用户资料分布页
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _UserProfile()
        {
            ViewData["user"] = _currentUser;
            ViewData["RegisterType"] = userSetting.RegisterType;

            return PartialView();
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _ChangePassword()
        {
            ProfileEditModel profileEditModel = new ProfileEditModel();

            return PartialView(profileEditModel);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _ChangePassword(ProfileEditModel profileEditModel)
        {
            var username = string.Empty;
            username = _currentUser.UserName;

            var result = MembershipService.ChangePassword(username, profileEditModel.PassWord, profileEditModel.NewPassword);
            if (result)
                return Json(1);
            return Json(0);
        }

        /// <summary>
        /// 更改昵称
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _ChangeUserName()
        {
            ProfileEditModel profileEditModel = new ProfileEditModel();

            return PartialView(profileEditModel);
        }

        /// <summary>
        /// 更改昵称
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _ChangeUserName(ProfileEditModel profileEditModel)
        {
            var user = (User)UserContext.CurrentUser;
            var historyData = user.Clone() as User;

            var oldUserName = user.UserName;
            user.UserName = profileEditModel.UserName;
            MembershipService.UpdateUser(user, historyData);
            //移除字典中的ID与名字的关联
            UserIdToUserNameDictionary.RemoveUserId(user.UserId);
            UserIdToUserNameDictionary.RemoveUserName(oldUserName);
            return Json(1);
        }

        /// <summary>
        /// 绑定手机
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _ChangeMobile()
        {
            RegisterEditModel registerEditModel = new RegisterEditModel();

            return PartialView(registerEditModel);
        }

        /// <summary>
        /// 绑定手机
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _ChangeMobile(RegisterEditModel registerEditModel)
        {
            //手机注册
            var result = ValidateCodeService.Check(registerEditModel.AccountMobile, registerEditModel.VerfyCode);
            if (result != ValidateCodeStatus.Passed)
            {
                var errorMessage = ValidateCodeService.GetCodeError(result);
                return Json(new { state = 0, msg = errorMessage });
            }
            var bind = new UserBindPhoneEvent(_currentUser.UserId, registerEditModel.AccountMobile);
            GlobalHelper.Mediator.Publish(bind);
            return Json(new { state = 1, msg = "绑定成功" });
        }

        /// <summary>
        /// 绑定邮箱
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _ChangeEmail()
        {
            RegisterEditModel registerEditModel = new RegisterEditModel();

            return PartialView(registerEditModel);
        }

        /// <summary>
        /// 绑定邮箱
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _ChangeEmail(RegisterEditModel registerEditModel)
        {
            var iuser = UserService.GetUserByEmail(registerEditModel.AccountEmail);
            if (iuser != null && iuser.Status == UserStatus.IsActivated)
            {
                return Json(new { state = 0, msg = "发送失败，您发送的邮箱已经是注册用户" });
            }

            MailMessage model = EmailBuilder.Instance().RegisterValidateEmail(_currentUser, true);
            var result = ValidateCodeService.EmailSend(_currentUser, "绑定邮箱验证", model, true);

            var usre = UserServiceExtension.GetFullUser(_currentUser.UserId);
            usre.UserGuid = registerEditModel.AccountEmail;
            MembershipService.UpdateUser(usre);
            Dictionary<string, string> buttonLink = new Dictionary<string, string>();
            buttonLink.Add("用户设置页面", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().UserSetting()));
            TempData["SystemMessageViewModel"] = new SystemMessageViewModel() { Title = "帐号激活成功！", Body = $"你以后可以使用{_currentUser.AccountEmail}登录。<br/><span id='seconds'>5</span>秒后，自动跳转到", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };

            if (result)
                return Json(new { state = 1, msg = "发送邮件成功" });
            else
                return Json(new { state = 0, msg = "发送邮件失败" });
        }

        /// <summary>
        /// 发送激活邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [UserAuthorize()]
        private bool ActivateByEmail(IUser user)
        {
            MailMessage model = EmailBuilder.Instance().RegisterValidateEmail(user);
            var result = ValidateCodeService.EmailSend(user, "注册帐号邮箱验证", model);

            return result;
        }

        #endregion 用户资料设置

        #region 第三方绑定

        /// <summary>
        /// 帐号绑定
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public ActionResult AccountBinding()
        {
            var bindings = AccountBindingService.GetAccountBindings(_currentUser.UserId);

            ViewData["accountTypes"] = AccountBindingService.GetAccountTypes(true);

            return View(bindings);
        }

        /// <summary>
        /// 第三方授权绑定
        /// </summary>
        /// <param name="accountTypeKey"></param>
        /// <returns></returns>
        [UserAuthorize()]
        public ActionResult ThirdBinding(string accountTypeKey)
        {
            ThirdAccountGetter thirdAccountGetter = ThirdAccountGetterFactory.GetThirdAccountGetter(accountTypeKey);

            if (accountTypeKey == AccountTypeKeys.Instance().WeChat())
            {
                AccountType accountType = AccountBindingService.GetAccountType(accountTypeKey);

                ViewData["accountType"] = accountType;
                return View(thirdAccountGetter);
            }

            return Redirect(thirdAccountGetter.GetAuthorizationUrl(SiteUrls.FullUrl(null, SiteUrls.Instance().ThirdBindingCallBack(accountTypeKey))));
        }

        /// <summary>
        /// 第三方授权绑定返回页面
        /// </summary>
        /// <param name="accountTypeKey"></param>
        /// <returns></returns>
        [UserAuthorize()]
        public ActionResult ThirdBindingCallBack(string accountTypeKey)
        {
            ThirdAccountGetter thirdAccountGetter = ThirdAccountGetterFactory.GetThirdAccountGetter(accountTypeKey);
            int expires_in = 0;
            string accessToken = "";//thirdAccountGetter.GetAccessToken(Request, out expires_in);
            if (string.IsNullOrEmpty(accessToken))
            {
                TempData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Error, "授权失败,请稍后再试！");

                return Redirect(SiteUrls.Instance().AccountBinding());
            }

            //当前第三方帐号上用户标识
            var thirdCurrentUser = thirdAccountGetter.GetThirdUser(accessToken, thirdAccountGetter.OpenId);
            if (thirdCurrentUser != null)
            {
                //是否已绑定过其他帐号
                long userId = AccountBindingService.GetUserId(accountTypeKey, thirdCurrentUser.Identification);

                User systemUser = UserService.GetUser(userId);

                if (systemUser != null)
                {
                    if (_currentUser.UserId != systemUser.UserId)
                    {
                        TempData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Hint, "此帐号已在网站中绑定过，不可再绑定其他网站帐号");
                    }
                    else
                    {
                        AccountBindingService.UpdateAccessToken(systemUser.UserId, thirdCurrentUser.AccountTypeKey, thirdCurrentUser.Identification, thirdCurrentUser.AccessToken, expires_in);

                        TempData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Success, "更新授权成功");
                    }
                }
                else
                {
                    AccountBinding account = new AccountBinding()
                    {
                        AccountTypeKey = accountTypeKey,
                        Identification = thirdCurrentUser.Identification,
                        UserId = _currentUser.UserId,
                        AccessToken = accessToken
                    };

                    if (expires_in > 0)
                        account.ExpiredDate = DateTime.Now.AddSeconds(expires_in);
                    AccountBindingService.CreateAccountBinding(account);

                    TempData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Success, "绑定成功");
                }
            }

            return Redirect(SiteUrls.Instance().AccountBinding());
        }

        /// <summary>
        /// 解除绑定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public ActionResult CancelBinding(string accountTypeKey)
        {
            AccountBindingService.DeleteAccountBinding(_currentUser.UserId, accountTypeKey);

            return Json(new { state = 1, msg = "解除绑定成功" });
        }

        #endregion 第三方绑定

        #region 会员等级

        /// <summary>
        /// 我的积分
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        public ActionResult MyPoints()
        {
            return View();
        }

        /// <summary>
        /// 我的积分
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _MyPoints()
        {
            ViewData["experience"] = PointService.GetPointCategory(PointCategoryKeys.Instance().ExperiencePoints());
            ViewData["trade"] = PointService.GetPointCategory(PointCategoryKeys.Instance().TradePoints());
            ViewData["pointItems"] = PointService.GetPointItemsOfIncome();
            return PartialView();
        }

        /// <summary>
        /// 我的等级
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _MyRank()
        {
            List<UserRank> listUserRank = new List<UserRank>();
            var listUserRanks = UserRankService.GetAll();
            listUserRank.AddRange(listUserRanks.Select(n => n.Value));
            ViewData["listUserRank"] = listUserRank;
            var userRank = UserRankService.Get(_currentUser.Rank + 1);
            ViewData["experiencePoints"] = userRank == null ? 0 : (userRank.PointLower - _currentUser.ExperiencePoints);
            ViewData["experience"] = PointService.GetPointCategory(PointCategoryKeys.Instance().ExperiencePoints());
            ViewData["trade"] = PointService.GetPointCategory(PointCategoryKeys.Instance().TradePoints());
            return PartialView();
        }

        /// <summary>
        /// 积分记录
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _MyPointRecords(int pageSize = 15, int pageIndex = 1)
        {
            var pointRecord = PointService.GetPointRecords(_currentUser.UserId, null, null, null, pageSize, pageIndex);

            ViewData["Experience"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().ExperiencePoints());
            ViewData["Trade"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().TradePoints());

            return PartialView(pointRecord);
        }

        #endregion 会员等级

        #region 用户充值&&回调

        /// <summary>
        /// 充值 记录
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _MyPointRechargeOrder(int pageSize = 15, int pageIndex = 1)
        {
            ViewData["trade"] = PointService.GetPointCategoryName(PointCategoryKeys.Instance().TradePoints());
            var pointRecord = pointRechargeOrderService.Gets(UserContext.CurrentUser.UserId, null, null, pageSize, pageIndex);
            return PartialView(pointRecord);
        }

        /// <summary>
        ///创建充值订单
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _CreateOrder()
        {
            ViewData["trade"] = PointService.GetPointCategoryNameById(PointCategoryKeys.Instance().TradePoints());
            var pointRechargeSettings = pointRechargeOrderService.GetPointRechargeSettings();
            return PartialView(pointRechargeSettings);
        }

        /// <summary>
        ///创建充值订单
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _CreateOrder(string totalPrice)
        {
            var pointRechargeSettings = pointRechargeOrderService.GetPointRechargeSettings();
            var rechargeSetting = pointRechargeSettings.Where(n => (string)n.TotalPrice == totalPrice).FirstOrDefault();
            if (rechargeSetting == null)
            {
                return Json(new { state = 0, msg = "参数有误" });
            }
            PointRechargeOrder pointRechargeOrder = PointRechargeOrder.New();
            pointRechargeOrder.UserId = _currentUser.UserId;
            pointRechargeOrder.TotalPrice = rechargeSetting.TotalPrice;
            pointRechargeOrder.TradePoints = rechargeSetting.TradePoints;
            pointRechargeOrder.Description = PointService.GetPointCategoryName(PointCategoryKeys.Instance().TradePoints()) + "充值";
            pointRechargeOrder.Status = RechargeOrdeStatus.NotPay;
            pointRechargeOrderService.Create(pointRechargeOrder);
            return Json(new { state = 1, msg = pointRechargeOrder.Id.ToString() });
        }

        /// <summary>
        ///获取订单详情
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _OrderDetail(long orderId)
        {
            var pointRechargeOrder = pointRechargeOrderService.Get(orderId);
            if (pointRechargeOrder.UserId != _currentUser.UserId)
            {
                pointRechargeOrder = new PointRechargeOrder();
            }
            //foreach (Buyway item in Enum.GetValues(typeof(Buyway)))
            //{
            //    item.

            //}
            return PartialView(pointRechargeOrder);
        }

        /// <summary>
        ///删除订单
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult DeleteOrder(long orderId)
        {
            var pointRechargeOrder = pointRechargeOrderService.Get(orderId);
            if (pointRechargeOrder != null && (pointRechargeOrder.UserId != _currentUser.UserId || pointRechargeOrder.Status != RechargeOrdeStatus.NotPay))
            {
                return Json(new { state = 0, msg = "取消失败" });
            }
            pointRechargeOrderService.Delete(orderId);
            return Json(new { state = 1, msg = "取消成功" });
        }

        /// <summary>
        ///付款订单
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public ActionResult PaymentOrder(long orderId, Buyway buyway)
        {
            var pointRechargeOrder = pointRechargeOrderService.Get(orderId);
            if (pointRechargeOrder != null &&
                pointRechargeOrder.UserId == _currentUser.UserId)
            {
                PayData payModel = new PayData();
                switch (buyway)
                {
                    case Buyway.AliPay:

                        pointRechargeOrder.Buyway = buyway;
                        pointRechargeOrder.PayMediaType = PayMediaType.PC;
                        pointRechargeOrderService.Update(pointRechargeOrder);
                        payModel = pointRechargeOrder.PointRechargeOrderAsPayEditModel(
                                Request.Path + CachedUrlHelper.Action("AliNotifyUrl", "UserSpace"),
                                Request.Path + "/userspace/mypoints#rechargeorder");


                        //创建阿里请求
                        var fromHtml = payService.AliPayCreateOrder(payModel);
                        //Response.Write(fromHtml);
                        break;

                    case Buyway.WxPay:
                        var result = new StatusMessageData(StatusMessageType.Success, "");
                        if (pointRechargeOrder.Buyway == Buyway.WxPay)
                        {
                            if (pointRechargeOrder.PayMediaType == PayMediaType.WAP)
                            {
                                result.MessageType = StatusMessageType.Hint;
                                result.MessageContent = "请使用手机浏览器进行支付";
                                return Json(result);
                            }
                            else if (pointRechargeOrder.PayMediaType == PayMediaType.APP)
                            {
                                result.MessageType = StatusMessageType.Hint;
                                result.MessageContent = "请在APP中进行支付";
                                return Json(result);
                            }
                            else if (pointRechargeOrder.PayMediaType == PayMediaType.WeiXinMP)
                            {
                                result.MessageType = StatusMessageType.Hint;
                                result.MessageContent = "请在微信中打开页面进行支付";
                                return Json(result);
                            }
                        }
                        pointRechargeOrder.Buyway = buyway;
                        pointRechargeOrder.PayMediaType = PayMediaType.PC;
                        pointRechargeOrderService.Update(pointRechargeOrder);
                        //创建微信请求
                        payModel = pointRechargeOrder.PointRechargeOrderAsPayEditModel(
                                Request.Path + CachedUrlHelper.Action("WxNotifyUrl", "UserSpace"),
                                Request.Path);

                        string tips = "";
                        var url = payService.WxPayCreateOrder(payModel, out tips);
                        url = Tunynet.Utilities.WebUtility.ResolveUrl("~/UserSpace/_QrCode?Url=" + HttpUtility.UrlEncode(url));
                        result.MessageType = StatusMessageType.Success;
                        result.MessageContent = url;
                        return Json(result);
                }
            }
            return View();
        }

        /// <summary>
        ///阿里回调订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult AliNotifyUrl()
        {
            Dictionary<string, string> sArray = null;//payService.GetRequestPost(Request);
            if (sArray.Count < 1)
                return Content("fail");

            if (!payService.CheckAliPayRSA2Sign(sArray))
                return Content("fail");

            string trade_status = Request.Form.Get<string>("trade_status");
            long out_trade_no = Request.Form.Get<long>("out_trade_no");
            string buyer_logon_id = Request.Form.Get<string>("buyer_logon_id");
            string trade_no = Request.Form.Get<string>("trade_no");
            float total_amount = Request.Form.Get<float>("total_amount");

            if (trade_status == "TRADE_SUCCESS")
            {
                var pointRechargeOrder = pointRechargeOrderService.Get(out_trade_no);
                if (pointRechargeOrder == null)
                    return Content("fail");//订单不存在
                if (pointRechargeOrder.Status == RechargeOrdeStatus.Complete)
                    return Content("success");//订单已处理
                if (pointRechargeOrder.Status == RechargeOrdeStatus.NotPay)
                {
                    //业务参数判断
                    if (pointRechargeOrder.TotalPrice == total_amount)
                    {
                        pointRechargeOrder.Buyway = Buyway.AliPay;
                        pointRechargeOrder.Status = RechargeOrdeStatus.Complete;
                        pointRechargeOrder.TradeNo = trade_no;
                        pointRechargeOrder.TradingAccount = buyer_logon_id;
                        pointRechargeOrderService.Update(pointRechargeOrder);
                        //更新积分
                        PointService.Reward(pointRechargeOrder.UserId, pointRechargeOrder.UserId, 0, 0, pointRechargeOrder.TradePoints, pointRechargeOrder.Description);
                        return Content("success");
                    }
                }
            }
            return Content("fail");
        }
        /// <summary>
        ///微信回调订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult WxNotifyUrl()
        {
            PayQuery payinfo = null;//payService.GetWxPayNotifyData(Request);
            if (payinfo == null)
                return Content("FAIL");

            if (payinfo.Code == "SUCCESS" && payinfo.TradeStatus == "SUCCESS")
            {

                var pointRechargeOrder = pointRechargeOrderService.Get(long.Parse(payinfo.OutTradeNo));
                if (pointRechargeOrder == null)
                    return Content("FAIL");//订单不存在

                if (pointRechargeOrder.Status == RechargeOrdeStatus.Complete)
                    return Content("SUCCESS");//订单已处理               

                if (pointRechargeOrder.Status == RechargeOrdeStatus.NotPay)
                {
                    if (pointRechargeOrder.TotalPrice == float.Parse(payinfo.TotalAmount) / 100)
                    {
                        pointRechargeOrder.Buyway = Buyway.WxPay;
                        pointRechargeOrder.Status = RechargeOrdeStatus.Complete;
                        pointRechargeOrder.TradeNo = payinfo.TradeNo;
                        pointRechargeOrder.TradingAccount = string.Empty;
                        pointRechargeOrderService.Update(pointRechargeOrder);
                        //更新积分
                        PointService.Reward(pointRechargeOrder.UserId, pointRechargeOrder.UserId, 0, 0, pointRechargeOrder.TradePoints, pointRechargeOrder.Description);

                        return Content("SUCCESS");
                    }
                }
            }
            return Content("FAIL");
        }

        /// <summary>
        /// 微信生成付款二维码
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public ActionResult _QrCode(string Url)
        {
            //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //qrCodeEncoder.QRCodeVersion = 0;
            //qrCodeEncoder.QRCodeScale = 4;

            ////将字符串生成二维码图片
            //Bitmap image = qrCodeEncoder.Encode(Url, Encoding.Default);

            ////保存为PNG到内存流
            //MemoryStream ms = new MemoryStream();
            //image.Save(ms, ImageFormat.Png);

            ////输出二维码图片
            //Response.BinaryWrite(ms.GetBuffer());
            //Response.End();
            return View();
            //return File(ms.GetBuffer(), ImageFormat.Png);
        }

        /// <summary>
        ///微信定时判断 是否支付完成
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public JsonResult WxGetOrderStatus(long orderId)
        {
            var order = pointRechargeOrderService.Get(orderId);
            if (order.Status == RechargeOrdeStatus.Complete)
            {
                return Json(new { state = 1 });
            }
            return Json(new { state = 0 });
        }

        #endregion 用户充值&&回调

        #region 用户通知

        /// <summary>
        /// 用户通知页
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        public ActionResult MyNotice()
        {
            if (UserContext.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<SelectListItem> noticeStatus = new List<SelectListItem>();
            noticeStatus.Add(new SelectListItem { Text = "未处理的通知", Value = NoticeStatus.Unhandled.ToString() });
            noticeStatus.Add(new SelectListItem { Text = "全部通知", Value = "" });
            ViewData["statusSelect"] = noticeStatus;
            ViewData["title"] = "我的通知";
            return View();
        }

        /// <summary>
        /// 用户通知列表
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpGet]
        public PartialViewResult _MyNotice(int? pageIndex, NoticeStatus? status)
        {
            var currentUser = UserContext.CurrentUser;
            ViewData["status"] = status;
            PagingDataSet<Notice> noticeList = new PagingDataSet<Notice>(new List<Notice>());
            if (currentUser != null)
            {
                noticeList = noticeService.Gets(currentUser.UserId, status, pageIndex);
                return PartialView(noticeList);
            }
            else
            {
                return PartialView(noticeList);
            }
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _DeleteNotice(List<long> noticeIds)
        {
            if (UserContext.CurrentUser == null)
            {
                return Json(new { state = 1 });
            }
            foreach (var id in noticeIds)
            {
                noticeService.Delete(id);
            }
            return Json(new { state = 1 });
        }

        /// <summary>
        /// 标记为我知道了
        /// </summary>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _KnowNotice(List<long> noticeIds)
        {
            if (UserContext.CurrentUser == null)
            {
                return Json(new { state = 1 });
            }
            foreach (var id in noticeIds)
            {
                noticeService.SetIsHandled(id, NoticeStatus.Readed);
            }
            return Json(new { state = 1 });
        }

        /// <summary>
        /// 接受
        /// </summary>
        /// <param name="noticeId">通知Id</param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _AcceptNotice(long noticeId)
        {
            if (UserContext.CurrentUser == null)
            {
                return Json(new { state = 1 });
            }
            noticeService.SetIsHandled(noticeId, NoticeStatus.Accepted);
            return Json(new { state = 1 });
        }

        /// <summary>
        /// 拒绝
        /// </summary>
        /// <param name="noticeId">通知Id</param>
        /// <returns></returns>
        [UserAuthorize()]
        [HttpPost]
        public JsonResult _RefuseNotice(long noticeId)
        {
            if (UserContext.CurrentUser == null)
            {
                return Json(new { state = 1 });
            }
            noticeService.SetIsHandled(noticeId, NoticeStatus.Refused);
            return Json(new { state = 1 });
        }

        #endregion 用户通知

        #region 用户资料侧边栏

        /// <summary>
        /// 用户资料侧边栏
        /// </summary>
        [HttpGet]
        public PartialViewResult _UserInformation(long userId)
        {
            var userHolder = UserService.GetUser(userId);
            var cmsCount = 0;
            //评论数量
            int commentCount = 0;

            if (userHolder != null)
            {
                cmsCount = UserServiceExtension.GetUserContentItemCount(userHolder.UserId, ContentModelKeys.Instance().Contribution());
                commentCount = UserServiceExtension.GetUserCommentCount(userHolder.UserId, null);
                //用户资料
                var userProfile = UserProfileService.Get(userId);

                ViewData["commentCount"] = commentCount;
                ViewData["userProfile"] = userProfile;
                ViewData["userHolder"] = userHolder;
                ViewData["cmsCount"] = cmsCount;
            }

            return PartialView();
        }

        #endregion 用户资料侧边栏

        #region 辅组方法
        /*
        /// <summary>
        /// 写入文件日志
        /// </summary>
        /// <param name="content">内容</param>
        static void InsertLog(string content)
        {
            try
            {
                string exdir = "";
                string filename = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "paylog" + exdir;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                FileInfo fileinfo = new FileInfo(filepath + "/" + filename);
                if (System.IO.File.Exists(fileinfo.FullName))
                {
                    fileinfo.IsReadOnly = false;
                }
                else
                {
                    System.IO.File.Create(filepath + "/" + filename).Close();
                    fileinfo.IsReadOnly = false;
                }
                using (StreamWriter writer = new StreamWriter(fileinfo.FullName, true, Encoding.UTF8))
                {
                    writer.WriteLine(DateTime.Now.ToString() + "\r\n" + content);
                    writer.WriteLine("");
                    writer.WriteLine("");
                }
            }
            catch
            {
                return;
            }
        }
        */
        #endregion
    }
}