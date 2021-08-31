//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Post;
using Tunynet.Settings;
using Tunynet.UI;

namespace Tunynet.Spacebuilder
{
    /// <summary>
    /// 前台
    /// </summary>

    public class PortalController : Controller
    {
        private IUser user = UserContext.CurrentUser;

        //资讯
        private ContentItemService contentItemService;

        //栏目
        private ContentCategoryService contentCategorieService;

        //浏览计数
        private CountService countService = DIContainer.Resolve<CountService>(new Autofac.NamedParameter("tenantTypeId", TenantTypeIds.Instance().ContentItem()));
        private CountService countService2 = DIContainer.Resolve<CountService>(new Autofac.NamedParameter("tenantTypeId", TenantTypeIds.Instance().Bar()));


        //附件
        private AttachmentService attachmentService = new AttachmentService(TenantTypeIds.Instance().ContentItem());

        //标签
        private TagService tagService = new TagService(TenantTypeIds.Instance().ContentItem());

        //评论
        private CommentService commentService;

        //点赞
        private ThreadService threadService;

        private UserService userService;
        private FollowService followService;
        private UserRankService userRankService;
        private PointService pointService;
        private NoticeService noticeService;
        private NavigationService navigationService;
        private SpecialContentItemService specialContentitemService;
        private SectionService sectionService;

        //站点设置
        private SettingManager<SiteSettings> siteSettings;

        //链接
        private LinkService linkService;

        private Authorizer authorizer;
        private INoticeSender noticeSender;
        private UserProfileService userProfileService;



        //签到
        private UserSignInService userSignInService;

        private UserSignInDetailRepository userSignInDetailRepository;

        //搜索热词
        private SearchWordService searchWordService;

        public PortalController(UserService userService,
                                FollowService followService,
                                ContentItemService contentItemService,
                                ContentCategoryService contentCategorieService,
                                ThreadService threadService,
                                CommentService commentService,
                                UserRankService userRankService,
                                PointService pointService,
                                NoticeService noticeService,
                                NavigationService navigationService,
                                LinkService linkService,
                                Authorizer authorizer,
                                SpecialContentItemService specialContentitemService,
                                SectionService sectionService,
                                INoticeSender noticeSender,
                                UserProfileService userProfileService,
                                UserSignInService userSignInService,
                                UserSignInDetailRepository userSignInDetailRepository,
                                SearchWordService searchWordService
                                )
        {
            this.userService = userService;
            this.followService = followService;
            this.contentItemService = contentItemService;
            this.contentCategorieService = contentCategorieService;
            this.threadService = threadService;
            this.commentService = commentService;
            this.userRankService = userRankService;
            this.pointService = pointService;
            this.noticeService = noticeService;
            this.navigationService = navigationService;
            this.siteSettings = siteSettings;
            this.linkService = linkService;
            this.authorizer = authorizer;
            this.noticeSender = noticeSender;
            this.specialContentitemService = specialContentitemService;
            this.sectionService = sectionService;
            this.userProfileService = userProfileService;
            this.userSignInService = userSignInService;
            this.userSignInDetailRepository = userSignInDetailRepository;
            this.searchWordService = searchWordService;
        }

        #region 头部分布页

        /// <summary>
        /// 头部分布页
        /// </summary>
        /// <returns></returns>
        public ActionResult _Header()
        {
            var navigationList = navigationService.GetAll();
            if (navigationList == null)
                return new EmptyResult();
            var navigations = navigationList.Where(n => n.ParentNavigationId == 0).Where(n => n.IsEnabled);
            if (navigations == null)
                return new EmptyResult();
            var currentExecutionFilePath = "";// Request.CurrentExecutionFilePath;
            if (!string.IsNullOrEmpty(currentExecutionFilePath))
            {
                if (navigationList.Count() > 0)
                {
                    navigationList = navigationList.Where(n => n.GetUrl().Trim().ToLower() == currentExecutionFilePath.Trim().ToLower());
                    if (navigationList.Count() > 0)
                    {
                        ViewData["activeNavigation"] = navigationService.GetCurrentNavigationPathIds(navigationList.First().NavigationId); ;
                    }
                }
            }

            //显示通知数量
            if (user != null)
            {
                ViewData["noticeCount"] = noticeService.Gets(user.UserId, NoticeStatus.Unhandled).TotalRecords;
            }
            ViewData["AuthorizeCore"] = authorizer.AuthorizeCore(UserContext.CurrentUser);

            #region 站点风格

            var siteStyle = SiteStyleType.Default;
            //var userBackground = SiteStyleType.Default;

            var siteSetting = siteSettings.Get();
            if (siteSetting != null)
                siteStyle = siteSetting.SiteStyle;

            if (user != null)
            {
                //用户资料
                var userProfile = UserProfileService.Get(user.UserId);
                if (userProfile != null)
                {
                    ViewData["isUseCustomStyle"] = userProfile.IsUseCustomStyle;
                    ViewData["siteStyle"] = userProfile.IsUseCustomStyle ? userProfile.ThemeAppearance : siteStyle.ToString();
                }
            }

            #endregion 站点风格

            return PartialView(navigations);
        }

        #endregion 头部分布页

        #region 尾部分布页

        /// <summary>
        /// 尾部分布页
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _Footer()
        {
            ViewData["siteSettings"] = siteSettings.Get();

            return PartialView();
        }

        #endregion 尾部分布页

        #region 右侧签到分布页

        /// <summary>
        /// 用户签到分布页
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [UserAuthorize(IsAllowAnonymous = true)]
        public PartialViewResult _SignIn()
        {
            if (user == null)
            {
                user = new User();
            }
            var userSignIn = userSignInService.GetByUserId(user.UserId);
            return PartialView(userSignIn ?? new UserSignIn());
        }

        /// <summary>
        /// 用户签到
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SignIn()
        {
            var result = userSignInService.Set(user.UserId);
            if (result)
                return Json(new { state = 1, msg = "签到成功" });
            else
                return Json(new { state = 0, msg = "您今日已签到" });
        }

        /// <summary>
        /// 获取每月签到天数
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="recentMonths">向前几个月</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSignInDetail(int recentMonths = 0, long status = 0)
        {
            switch (status)
            {
                case 1:
                    recentMonths++;
                    break;

                case -1:
                    recentMonths--;
                    break;

                default:
                    break;
            }
            if (user == null)
            {
                user = new User();
            }
            var userSignInDetail = userSignInService.GetUserHistorDetails(user.UserId, recentMonths);
            List<int> days = userSignInDetail.Select(n => n.DateCreated.Day).ToList();
            List<object> signList = new List<object>();
            foreach (var item in days)
            {
                signList.Add(new
                {
                    signDay = item
                });
            }
            var userSignIn = userSignInService.GetByUserId(user.UserId);
            var goldSum = "0";
            if (userSignIn != null)
            {
                goldSum = userSignIn.TradePointSum.ToString();
            }

            return Json(new { signList = signList, recentMonths = recentMonths, goldSum = goldSum });
        }

        #endregion 右侧签到分布页

        #region 热词计数

        [HttpPost]
        public void _EidtSearchWord(string keyword, string searchType)
        {
            SearchWord searchWord = new SearchWord();
            searchWord.Word = keyword;
            searchWord.SearchTypeCode = searchType;
            searchWordService.Set(searchWord);
        }

        #endregion 热词计数

        #region 站点主页

        /// <summary>
        /// 站点主页
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(IsAllowAnonymous = true)]
        public ActionResult Home()
        {
            //友情链接
            var imgLinks = new List<LinkEntity>();
            var wordLinks = new List<LinkEntity>();
            var links = linkService.GetsOfSite().Where(n => n.IsEnabled == true).OrderBy(n => n.DisplayOrder);
            if (links.Any())
            {
                imgLinks.AddRange(links.Where(n => n.ImageAttachmentId > 0));
                wordLinks.AddRange(links.Where(n => n.ImageAttachmentId == 0));
            }
            ViewData["imgLinks"] = imgLinks;
            ViewData["wordLinks"] = wordLinks;
            //推荐贴子
            var specialContentItemIds = specialContentitemService.GetItemIds(TenantTypeIds.Instance().Thread(), 0).Distinct();
            var specialthreads = threadService.Gets(specialContentItemIds).Take(10);
            ViewData["listSpecialThread"] = specialthreads;
            //推荐贴吧
            var specialBarIds = specialContentitemService.GetItemIds(TenantTypeIds.Instance().Bar(), SpecialContentTypeIds.Instance().Special());
            ViewData["specialSections"] = SectionService.GetBarSections(specialBarIds);

            ViewData["siteSettings"] = siteSettings.Get();
            ViewData["contentItems"] = ContentItemService.GetTopContentItems(6, null, true, null, ContentItemSortBy.DatePublished_Desc);
            return View();
        }

        #endregion 站点主页

        #region 评论

        /// <summary>
        /// 评论列表
        /// </summary>
        /// <param name="contentItemId"></param>
        /// <param name="commentId">评论ID</param>
        /// <returns></returns>
        [UserAuthorize(IsAllowAnonymous = true)]
        public ActionResult CommentList(long commentedObjectId, string tenantTypeId = "", long commentId = 0)
        {
            if (tenantTypeId == TenantTypeIds.Instance().ContentItem())
            {
                var contentItem = ContentItemService.Get(commentedObjectId);
                ViewData["subject"] = contentItem == null ? string.Empty : contentItem.Subject;
                ViewData["IsComment"] = contentItem == null ? true : contentItem.IsComment;
                ViewData["totalRecords"] = contentItem.CommentCount();
            }
            else if (tenantTypeId == TenantTypeIds.Instance().Thread())
            {
                var contentItem = threadService.Get(commentedObjectId);
                ViewData["subject"] = contentItem == null ? string.Empty : contentItem.Subject;
                ViewData["totalRecords"] = contentItem.CommentCount;
            }

            ViewData["tenantTypeId"] = tenantTypeId;
            ViewData["commentedObjectId"] = commentedObjectId;
            //获取评论在 第几页和第几个
            if (commentId > 0)
            {
                var commentIdPageIndex = CommentService.GetCommentCount(commentId, commentedObjectId, TenantTypeIds.Instance().ContentItem()) / 10 + 1;
                ViewData["commentIdPageIndex"] = commentIdPageIndex;
                ViewData["commentId"] = commentId;
            }
            return View();
        }

        /// <summary>
        /// 评论列表分布页
        /// </summary>
        /// <param name="contentItemId"></param>
        /// <returns></returns>
        public ActionResult _ListComment(long commentedObjectId, string tenantTypeId, SortBy_Comment sortBy_Comment = SortBy_Comment.DateCreated, int pageSize = 10, int pageIndex = 1, long commentId = 0)
        {
            PagingDataSet<Comment> comments = null;
            if (commentedObjectId > 0)
            {
                comments = CommentService.GetRootComments(tenantTypeId, commentedObjectId, pageSize, pageIndex, sortBy_Comment);
                if (comments.Count == 0 && pageIndex > 1)
                {
                    comments = CommentService.GetRootComments(tenantTypeId, commentedObjectId, pageSize, pageIndex > 1 ? pageIndex - 1 : 1, sortBy_Comment);
                }
            }
            var contentItem = ContentItemService.Get(commentedObjectId);
            ViewData["commentService"] = commentService;
            ViewData["tenantTypeId"] = tenantTypeId;
            ViewData["commentedObjectId"] = commentedObjectId;
            ViewData["contentItem"] = contentItem;

            ViewData["count"] = CountService.Get(CountTypes.Instance().CommentCount(), commentedObjectId);
            ViewData["count2"] = CountService.Get(CountTypes.Instance().CommentCount(), commentedObjectId);

            
            ViewData["commentId"] = commentId;
            return PartialView(comments);
        }

        /// <summary>
        /// 评论列表分布页 盖楼效果
        /// </summary>
        /// <param name="contentItemId"></param>
        /// <param name="commentId">评论ID</param>
        /// <returns></returns>
        public ActionResult _ChildComment(long parentId, string tenantTypeId, long commentedObjectId, SortBy_Comment sortBy_Comment = SortBy_Comment.DateCreatedDesc, int pageIndex = 1)
        {
            PagingDataSet<Comment> comments = null;
            if (parentId > 0)
                comments = CommentService.GetChildren(parentId, pageIndex, 20, sortBy_Comment);
            ViewData["tenantTypeId"] = tenantTypeId;
            ViewData["parentId"] = parentId;
            ViewData["commentedObjectId"] = commentedObjectId;
            return PartialView(comments);
        }

        /// <summary>
        /// 评论控件
        /// </summary>
        /// <param name="contentItemId"></param>
        /// <returns></returns>
        public ActionResult _Comment(long parentId, string tenantTypeId, long commentedObjectId, SortBy_Comment sortBy_Comment = SortBy_Comment.DateCreatedDesc, int pageIndex = 1)
        {
            CommentEditModel commentEditModel = new CommentEditModel();
            ViewData["tenantTypeId"] = tenantTypeId;
            ViewData["parentId"] = parentId;
            ViewData["commentedObjectId"] = commentedObjectId;
            return PartialView(commentEditModel);
        }

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <returns></returns>
        [UserAuthorize(IsAllowAnonymous = true)]
        [HttpPost]
        public ActionResult CreateComment(CommentEditModel commentEditModel)
        {
            #region 资讯是否支持评论

            if (commentEditModel.TenantTypeId == TenantTypeIds.Instance().ContentItem())
            {
                var contentItem = ContentItemService.Get(commentEditModel.CommentedObjectId);
                if (contentItem == null)
                    return Json(new StatusMessageData(StatusMessageType.Hint, "评论失败"));

                if (contentItem.IsComment)
                    return Json(new StatusMessageData(StatusMessageType.Hint, "此文章禁止评论"));
            }

            #endregion 资讯是否支持评论

            if (string.IsNullOrEmpty(commentEditModel.Body))
            {
                return Json(new StatusMessageData(StatusMessageType.Hint, "评论失败"));
            }

            var parentComment = CommentService.Get(commentEditModel.ParentId);
            if (parentComment != null)
            {
                commentEditModel.ParentIds = string.Format("{0}{1},", parentComment.ParentIds, commentEditModel.ParentId);
                commentEditModel.TenantTypeId = parentComment.TenantTypeId;
            }
            else
                commentEditModel.ParentIds = "0,";

            commentEditModel.UserId = user.UserId;
            commentEditModel.DateCreated = DateTime.Now;
            commentEditModel.Author = user.DisplayName;
            commentEditModel.IP = Utilities.WebUtility.GetIP();
            commentEditModel.IsPrivate = commentEditModel.IsPrivate;
            Comment comment = Comment.New();
            commentEditModel.MapTo<CommentEditModel, Comment>(comment);

            //判断 内容的所属
            long ownerId = 0;
            if (commentEditModel.TenantTypeId == TenantTypeIds.Instance().ContentItem())
            {
                var contentItem = ContentItemService.Get(commentEditModel.CommentedObjectId);
                if (contentItem != null)
                    ownerId = contentItem.UserId;
            }
            else if (commentEditModel.TenantTypeId == TenantTypeIds.Instance().Thread())
            {
                var thread = threadService.Get(commentEditModel.CommentedObjectId);
                if (thread != null)
                    ownerId = thread.UserId;
            }

            comment.OwnerId = parentComment == null ? ownerId : parentComment.UserId;

            if (!CommentService.Create(comment))
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "发布评论过于频繁"));
            }


            ////更新最后 回复时间
            //if (commentEditModel.TenantTypeId == TenantTypeIds.Instance().Thread())
            //{
            //    var contentItem = contentItemService.Get(commentEditModel.CommentedObjectId);
            //    if (contentItem != null)
            //    {
            //        contentItem.LastModified = DateTime.Now;
            //        contentItemService.Update(contentItem);
            //    }
            //}

            var statusMessage = new StatusMessageData(StatusMessageType.Success, "评论成功");
            return Json(statusMessage);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId">评论Id</param>
        /// <returns>Json状态</returns>
        [HttpPost]
        public JsonResult _DeleteComment(long commentId)
        {
            var comment = CommentService.Get(commentId);
            if (comment != null)
            {
                if (authorizer.Comment_Delete(comment, UserContext.CurrentUser))
                    if (CommentService.Delete(commentId))
                        return Json(new StatusMessageData(StatusMessageType.Success, "删除成功"));
            }

            return Json(new StatusMessageData(StatusMessageType.Error, "删除错误"));
        }

        #endregion 评论

        #region 全文检索 by fanggm

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize(IsAllowAnonymous = true)]
        public ActionResult Search(string keyword = "", string searchType = "All")
        {
            ViewData["keyword"] = Utilities.WebUtility.HtmlDecode(keyword);
            ViewData["searchType"] = searchType;
            return View();
        }

        /// <summary>
        /// 获得热词
        /// </summary>
        /// <param name="searchType">搜索类型</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _HotWord(string searchType = "All")
        {
            IEnumerable<SearchWord> searchWord = searchWordService.GetTopWord(searchType);
            ViewData["searchWord"] = searchWord;
            return PartialView();
        }

        /// <summary>
        /// 快捷搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public PartialViewResult _SearchQuick(string keyword = "")
        {
            var url = ConfigurationManager.AppSettings["Search"];

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(keyword))
            {
                return PartialView(new AllResultModel());
            }

            keyword = Utilities.WebUtility.HtmlDecode(keyword);

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/SearchAll?keyword={1}", url, keyword));
                myRequest.Method = "GET";

                HttpWebResponse myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string content = reader.ReadToEnd();

                    var results = JsonConvert.DeserializeObject<AllResultModel>(content);

                    ViewData["keyword"] = keyword;

                    return PartialView(results);
                }
            }
            catch (Exception)
            {
                return PartialView(new AllResultModel());
            }

            return PartialView(new AllResultModel());
        }

        /// <summary>
        /// 搜索全部
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize(IsAllowAnonymous = true)]
        public PartialViewResult _SearchAllResult(string keyword = "")
        {
            var url = ConfigurationManager.AppSettings["Search"];

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(keyword))
            {
                return PartialView(new AllResultModel());
            }
            keyword = Utilities.WebUtility.HtmlDecode(keyword);

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/SearchAll?keyword={1}", url, keyword));
                myRequest.Method = "GET";

                HttpWebResponse myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string content = reader.ReadToEnd();

                    var results = JsonConvert.DeserializeObject<AllResultModel>(content);

                    ViewData["keyword"] = keyword;

                    return PartialView(results);
                }
            }
            catch (Exception)
            {
                return PartialView(new AllResultModel());
            }

            return PartialView(new AllResultModel());
        }

        /// <summary>
        /// 搜索结果
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="searchType">搜索类型(cms or thread)</param>
        /// <param name="isDefaultOrder">是否默认排序(默认相关度)</param>
        /// <param name="time">时间筛选</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize(IsAllowAnonymous = true)]
        public PartialViewResult _SearchResult(string keyword, string searchType, bool isDefaultOrder = true, string time = "year", int pageIndex = 1)
        {
            string url = "";
            switch (searchType.ToLower())
            {
                case "cms":
                    url = ConfigurationManager.AppSettings["Search"] + "/Search/CmsSearch";
                    break;

                case "thread":
                    url = ConfigurationManager.AppSettings["Search"] + "/Search/ThreadSearch";
                    break;

                case "ask":
                    url = ConfigurationManager.AppSettings["Search"] + "/AskSearch/QuickAskSearch";
                    break;

                case "doc":
                    url = ConfigurationManager.AppSettings["Search"] + "/DocSearch/DocSearch";
                    break;

                default:
                    break;
            }

            DateTime minDate = DateTime.Now.AddYears(-1);

            switch (time.ToLower())
            {
                case "month":
                    minDate = DateTime.Now.AddMonths(-1);
                    break;

                case "week":
                    minDate = DateTime.Now.AddDays(-7);
                    break;

                case "all":
                    minDate = DateTime.MinValue;
                    break;

                case "year":
                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(keyword))
            {
                return PartialView(new SearchResultModel());
            }
            keyword = Utilities.WebUtility.HtmlDecode(keyword);

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}?keyword={1}&minDate={2}&isDefaultOrder={3}&pageIndex={4}", url, keyword, minDate, isDefaultOrder, pageIndex));
                myRequest.Method = "GET";

                HttpWebResponse myResponse = null;

                myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string content = reader.ReadToEnd();

                    var results = JsonConvert.DeserializeObject<SearchResultModel>(content);

                    ViewData["keyword"] = keyword;
                    ViewData["searchType"] = searchType;
                    ViewData["time"] = time;
                    ViewData["isDefaultOrder"] = isDefaultOrder;

                    var page = results.Page;

                    ViewData["pageIndex"] = pageIndex;
                    ViewData["pageSize"] = 10;
                    ViewData["totalRecords"] = page.TotalRecords;

                    return PartialView(results);
                }
            }
            catch (Exception)
            {
                return PartialView(new SearchResultModel());
            }
            return PartialView(new SearchResultModel());
        }

        #endregion 全文检索 by fanggm
    }
}