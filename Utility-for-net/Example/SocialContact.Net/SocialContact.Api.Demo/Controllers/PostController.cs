//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Tunynet.Attitude;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Post;
using Tunynet.Settings;
using Utility.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tunynet.Spacebuilder
{
    /// <summary>
    /// 贴吧 控制器
    /// </summary>
    [UserAuthorize(IsAllowAnonymous = true)]
    public class PostController : Controller
    {
        #region 构造函数 service

        private IUser _currentUser = UserContext.CurrentUser;
        private SectionService sectionService;
        private CategoryService categoryService;
        private ThreadService threadService;
        private SpecialContentItemService specialContentitemService;
        private UserService userService;
        private UserProfileService userProfileService;
        private CommentService commentService;
        private ContentItemService contentItemService;
        private AuthorizationService authorizationService;
        private NoticeService noticeService;
        private Authorizer authorizer;
        private AttitudeService attitudeService = new AttitudeService(TenantTypeIds.Instance().Comment());
        private FavoriteService favoriteSectionService = new FavoriteService(TenantTypeIds.Instance().Bar());
        private FavoriteService favoriteThreadService = new FavoriteService(TenantTypeIds.Instance().Thread());
        private CountService threadCountService = new CountService(TenantTypeIds.Instance().Thread());
        private SettingManager<SectionSettings> sectionSettings = DIContainer.Resolve<ISettingsManager<SectionSettings>>();
        private AttachmentService attachmentService = new AttachmentService(TenantTypeIds.Instance().Thread());
        private IKvStore kvStore;
        private INoticeSender noticeSender;
        private SettingManager<SiteSettings> siteSettings = DIContainer.Resolve<ISettingsManager<SiteSettings>>();


        #endregion 构造函数 service

        #region 贴吧页面

        /// <summary>
        /// 贴吧主页
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BarSectionHome()
        {
            var rootCategories = categoryService.GetRootCategoriesOfOwner(TenantTypeIds.Instance().Bar());
            var specialBarIds = specialContentitemService.GetItemIds(TenantTypeIds.Instance().Bar(), SpecialContentTypeIds.Instance().Special());

            if (_currentUser != null)
            {
                var favoriteSectionIds = favoriteSectionService.GetPagingObjectIds(_currentUser.UserId, 1);
                var favoriteSections = sectionService.GetBarSections(favoriteSectionIds);

                ViewData["canEditBar"] = authorizer.IsPostManager(_currentUser);
                ViewData["favoriteSections"] = favoriteSections;
            }
            ViewData["rootCategories"] = rootCategories;
            ViewData["specialSections"] = SectionService.GetBarSections(specialBarIds);

            return View();
        }

        /// <summary>
        /// 贴吧列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BarSection(long? categoryId)
        {
            ViewData["Categories"] = JsonHelper.ToJson(GetCategories());

            if (categoryId.HasValue)
            {
                var category = categoryService.Get(categoryId.Value);
                if (category.ParentId == 0)
                {
                    ViewData["rootCategoryId"] = category.CategoryId;
                    ViewData["childrenCategoryId"] = category.CategoryId;
                }
                else
                {
                    ViewData["rootCategoryId"] = category.ParentId;
                    ViewData["childrenCategoryId"] = category.CategoryId;
                }
            }

            return View();
        }

        /// <summary>
        /// 贴吧列表
        /// </summary>
        /// <param name="categoryId">类别ID</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListBarSection(long? categoryId, int pageIndex = 1)
        {
            var barSectionList = SectionService.Gets(TenantTypeIds.Instance().Bar(), categoryId: categoryId, isEnabled: true, pageSize: 15, pageIndex: pageIndex);

            return PartialView(barSectionList);
        }

        /// <summary>
        /// 创建、编辑贴吧
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize]
        public ActionResult _EditBarSection(long? sectionId)
        {
            //验证权限
            bool authorizeResult = _currentUser.Rank > sectionSettings.Get().MinimumCreateLevel;
            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            ViewData["Categories"] = JsonHelper.ToJson(this.GetCategories());

            SectionEditModel sectionEditModel = new SectionEditModel();

            if (sectionId.HasValue)
            {
                //验证权限
                authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, sectionId.Value) ||
                    sectionService.IsSectionOwner(_currentUser.UserId, sectionId.Value) ||
                    authorizer.IsSuperAdministrator(_currentUser);
                if (!authorizeResult)
                {
                    SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                    TempData["SystemMessageViewModel"] = systemMessageViewModel;
                    return Redirect(SiteUrls.Instance().SystemMessage());
                }

                var section = SectionService.Get(sectionId.Value);
                section.MapTo(sectionEditModel);
                sectionEditModel.EnabledThreadCategory = section.ThreadCategorySettings != ThreadCategoryStatus.Disabled;
                sectionEditModel.IsOnlyOwnerAndManager = section.SectionPostSetting == SectionPostSetting.OwnerAndManagers;

                ViewData["rootCategoryId"] = section.Category?.ParentId == 0 ? section.Category?.CategoryId : section.Category?.ParentId;

                ViewData["childrenCategoryId"] = section.Category?.CategoryId;

                ViewData["managers"] = section.SectionManagers.Select(n => n.UserId);
            }

            return PartialView(sectionEditModel);
        }

        /// <summary>
        /// 创建、编辑贴吧
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult _EditBarSection(SectionEditModel sectionEditModel)
        {
            if (sectionEditModel.SectionId == 0)
            {
                //创建
                //验证权限
                bool authorizeResult = authorizer.IsPostManager(_currentUser);
                if (!authorizeResult)
                {
                    SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                    TempData["SystemMessageViewModel"] = systemMessageViewModel;
                    return Redirect(SiteUrls.Instance().SystemMessage());
                }

                var section = Section.New();

                section.TenantTypeId = TenantTypeIds.Instance().Bar();
                section.Name = sectionEditModel.Name;
                section.Description = sectionEditModel.Description;
                section.UserId = _currentUser.UserId;
                section.IsEnabled = true;
                section.FeaturedImageAttachmentId = sectionEditModel.FeaturedImageAttachmentId;
                section.ThreadCategorySettings = sectionEditModel.EnabledThreadCategory ? ThreadCategoryStatus.ForceEnabled : ThreadCategoryStatus.Disabled;
                section.SectionPostSetting = sectionEditModel.IsOnlyOwnerAndManager ? SectionPostSetting.OwnerAndManagers : SectionPostSetting.Default;

                //吧管理员
                List<long> manageIds = new List<long>();
                if (sectionEditModel.SectionManagers != null && sectionEditModel.SectionManagers.Any())
                {
                    manageIds = sectionEditModel.SectionManagers.Select(n => long.Parse(n)).ToList();
                }

                //创建贴吧
                sectionService.Create(section, manageIds);

                //添加贴吧分类
                categoryService.AddCategoriesToItem(new List<long> { sectionEditModel.CategoryId }, section.SectionId);

                //吧主管理员关注贴吧
                favoriteSectionService.Favorite(section.SectionId, _currentUser.UserId);
                foreach (var item in manageIds)
                {
                    favoriteSectionService.Favorite(section.SectionId, item);
                }
                //添加贴子分类
                if (sectionEditModel.EnabledThreadCategory)
                {
                    if (!string.IsNullOrEmpty(sectionEditModel.ThreadCategoryNames))
                    {
                        var categoryNames = sectionEditModel.ThreadCategoryNames.Split(';');

                        for (int i = 0; i < categoryNames.Count(); i++)
                        {
                            var newcategory = Category.New();
                            newcategory.TenantTypeId = TenantTypeIds.Instance().Thread();
                            newcategory.OwnerId = section.SectionId;
                            newcategory.CategoryName = categoryNames[i];
                            categoryService.Create(newcategory);
                        }
                    }
                }

                return Json(new { state = 1, sectionId = section.SectionId, msg = "创建成功" }, "text/html");
            }
            else
            {
                //编辑
                //权限验证
                bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, sectionEditModel.SectionId) ||
                    sectionService.IsSectionOwner(_currentUser.UserId, sectionEditModel.SectionId);

                if (!authorizeResult)
                {
                    SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                    TempData["SystemMessageViewModel"] = systemMessageViewModel;
                    return Redirect(SiteUrls.Instance().SystemMessage());
                }

                //获取贴吧
                var section = sectionService.Get(sectionEditModel.SectionId);
                section.Name = sectionEditModel.Name;
                section.Description = sectionEditModel.Description;
                section.FeaturedImageAttachmentId = sectionEditModel.FeaturedImageAttachmentId;
                section.SectionPostSetting = sectionEditModel.IsOnlyOwnerAndManager ? SectionPostSetting.OwnerAndManagers : SectionPostSetting.Default;

                if (sectionEditModel.EnabledThreadCategory)
                {
                    section.ThreadCategorySettings = ThreadCategoryStatus.NotForceEnabled;
                }
                else
                {
                    section.ThreadCategorySettings = ThreadCategoryStatus.Disabled;
                }
                //吧管理员
                List<long> manageIds = new List<long>();
                if (sectionEditModel.SectionManagers != null && sectionEditModel.SectionManagers.Any())
                {
                    manageIds = sectionEditModel.SectionManagers.Select(n => long.Parse(n)).ToList();
                }
                else
                {
                    //吧管理员编辑贴吧时 贴吧管理员不变
                    manageIds = section.SectionManagers.Select(n => n.UserId).ToList();
                }
                //管理员关注贴吧
                foreach (var item in manageIds)
                {
                    favoriteSectionService.Favorite(section.SectionId, item);
                }
                //更新
                sectionService.Update(section, manageIds);
                //删除旧贴吧类别
                if (section.Category != null)
                {
                    categoryService.DeleteItemInCategory(section.Category.CategoryId, section.SectionId);
                }
                //添加新贴吧类别
                categoryService.AddCategoriesToItem(new List<long> { sectionEditModel.CategoryId }, section.SectionId);

                //贴子类别
                if (!string.IsNullOrEmpty(sectionEditModel.ThreadCategoryNames) && !string.IsNullOrEmpty(sectionEditModel.ThreadCategoryIds))
                {
                    var threadCategoryNamesList = sectionEditModel.ThreadCategoryNames.Split(';');
                    var threadCategoryIdsList = sectionEditModel.ThreadCategoryIds.Split(';');

                    for (int i = 0; i < threadCategoryIdsList.Count(); i++)
                    {
                        var threadCategoryId = long.Parse(threadCategoryIdsList[i]);
                        if (threadCategoryId == 0)
                        {
                            //添加新贴子分类
                            Category newcategory = Category.New();
                            newcategory.CategoryName = threadCategoryNamesList[i];
                            newcategory.OwnerId = section.SectionId;
                            newcategory.TenantTypeId = TenantTypeIds.Instance().Thread();

                            categoryService.Create(newcategory);
                        }
                        else
                        {
                            //更新前贴吧贴子分类
                            var oldThreadCategory = categoryService.Get(threadCategoryId);

                            //与更新前不一致则更新
                            if (oldThreadCategory.CategoryName != threadCategoryNamesList[i])
                            {
                                oldThreadCategory.CategoryName = threadCategoryNamesList[i];
                                categoryService.Update(oldThreadCategory);
                            }
                        }
                    }
                }

                return Json(new { state = 1, sectionId = 0, msg = "编辑成功" }, "text/html");
            }
        }

        /// <summary>
        /// 贴吧详情
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="tageName">标签名称</param>
        /// <param name="sortByBarThread">贴子排序</param>
        /// <param name="sortByBarDateThread">贴子时间排序依据</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BarSectionDetail(long sectionId, long? threadCategoryId)
        {
            var section = SectionService.Get(sectionId);

            var specialBarIds = specialContentitemService.GetItemIds(TenantTypeIds.Instance().Bar(), SpecialContentTypeIds.Instance().Special());
            var specialSections = SectionService.GetBarSections(specialBarIds);

            ViewData["specialSections"] = specialSections;
            ViewData["threadCategoryId"] = threadCategoryId;

            #region 活动贴和投票贴

            ViewData["isEventThread"] = IsEnableAppThread("Event");
            ViewData["isVoteThread"] = IsEnableAppThread("Vote");

            #endregion

            ViewData["enabledThread"] = EnabledThread(sectionId);

            return View(section);
        }

        /// <summary>
        /// 贴吧管理
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize]
        public ActionResult BarSectionManage(long sectionId, int pageSize = 20, int pageIndex = 1)
        {
            var section = sectionService.Get(sectionId);

            //权限验证
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, section.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, section.SectionId);

            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            return View(section);
        }

        /// <summary>
        /// 关注贴吧
        /// </summary>
        /// <param name="sectionId">贴吧id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FavoriteSection(long sectionId)
        {
            bool result = false;
            if (_currentUser == null)
            {
                return Json(new { state = result, msg = "请先登录" });
            }
            else
            {
                var section = SectionService.Get(sectionId);

                if (section != null)
                {
                    result = favoriteSectionService.Favorite(sectionId, _currentUser.UserId);

                    if (!result)
                    {
                        return Json(new { state = result, msg = "关注失败" });
                    }

                    return Json(new { state = result, msg = "关注成功" });
                }

                return Json(new { state = result, msg = "关注失败" });
            }
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CancelFavoriteSection(long sectionId)
        {
            var section = sectionService.Get(sectionId);

            bool isSectionManager = section.SectionManagers.Contains(_currentUser);
            bool result = false;

            if (section != null)
            {
                if (!isSectionManager && _currentUser.UserId != section.UserId)
                {
                    result = favoriteSectionService.CancelFavorite(sectionId, _currentUser.UserId);
                    if (result)
                    {
                        return Json(new { state = result, msg = "取消关注成功" });
                    }
                    else
                    {
                        return Json(new { state = result, msg = "取消关注失败" });
                    }
                }
                else
                {
                    return Json(new { state = result, msg = "吧主或管理员不能取消关注" });
                }
            }

            return Json(new { state = result, msg = "贴吧不存在" });
        }

        #endregion 贴吧页面

        #region 贴子页面

        /// <summary>
        /// 贴吧详情贴子列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="sortBy_BarThread"></param>
        /// <param name="sortBy_BarDateThread"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListThread(long sectionId, string keyword, long? categoryId, int pageSize = 10, int pageIndex = 1, int sortBy_BarThread = 1, int sortBy_BarDateThread = 0, int isSpecial = 0)
        {
            //查询条件
            ViewData["categoryId"] = categoryId;
            ViewData["keyword"] = keyword;
            ViewData["sortBy_BarThread"] = sortBy_BarThread;
            ViewData["sortBy_BarDateThread"] = sortBy_BarDateThread;
            ViewData["isSpecial"] = isSpecial;
            ViewData["section"] = SectionService.Get(sectionId);

            if (string.IsNullOrEmpty(keyword))
            {
                if (isSpecial == 1)
                {
                    var specialIds = specialContentitemService.GetItemIds(TenantTypeIds.Instance().Thread(), SpecialContentTypeIds.Instance().OfficialThread());

                    var specialthreads = threadService.Gets(specialIds).Where(n => n.SectionId == sectionId);
                    var specialthreadslist = specialthreads.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    var threads = new PagingDataSet<Thread>(specialthreadslist)
                    {
                        TotalRecords = specialthreads.LongCount(),
                        PageSize = pageSize,
                        PageIndex = pageIndex
                    };

                    return PartialView(threads);
                }
                else
                {
                    var threads = ThreadService.Gets(sectionId, null, categoryId, true, (SortBy_BarThread)sortBy_BarThread, pageSize, pageIndex, (SortBy_BarDateThread)sortBy_BarDateThread);

                    return PartialView(threads);
                }
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var url = ConfigurationManager.AppSettings["Search"];

                try
                {
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/Search/ThreadSearchInSection?sectionId={1}&categoryId={2}&keyword={3}&pageIndex={4}&pageSize={5}", url, sectionId, categoryId, keyword, pageIndex, pageSize));
                    myRequest.Method = "GET";

                    HttpWebResponse myResponse = null;

                    myResponse = (HttpWebResponse)myRequest.GetResponse();
                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        string content = reader.ReadToEnd();

                        var result = JsonConvert.DeserializeObject<SearchResultIdModel>(content);

                        var threads = new PagingDataSet<Thread>(threadService.Gets(result.Data))
                        {
                            TotalRecords = result.Page.TotalRecords,
                            PageSize = result.Page.PageSize,
                            PageIndex = result.Page.PageIndex
                        };

                        sw.Stop();
                        TimeSpan timespan = sw.Elapsed;

                        string seconds = string.Format("{0:F3}", timespan.TotalSeconds);

                        ViewData["times"] = string.Format("约有{0}个搜索结果(用时{1}秒)", threads.TotalRecords, seconds);

                        return PartialView(threads);
                    }
                }
                catch (Exception)
                {
                    return PartialView(new PagingDataSet<Thread>(new List<Thread>()));
                }

                return PartialView(new PagingDataSet<Thread>(new List<Thread>()));
            }
        }

        /// <summary>
        /// 最新贴子列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListNewThread(int pageIndex = 1, int pageSize = 10)
        {
            var threads = ThreadService.Gets(null, sortBy: SortBy_BarThread.DateCreated_Desc, pageSize: pageSize, pageIndex: pageIndex);

            return PartialView(threads);
        }

        /// <summary>
        /// 推荐贴子
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListSpecialThread(int pageIndex = 1, int pageSize = 10)
        {
            var specialContentItemIds = specialContentitemService.GetItemIds(TenantTypeIds.Instance().Thread(), 0).Distinct();

            var specialthreads = threadService.Gets(specialContentItemIds).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var threads = new PagingDataSet<Thread>(specialthreads)
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = specialContentItemIds.Count()
            };

            return PartialView(threads);
        }

        /// <summary>
        /// 创建 编辑贴子
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize]
        public ActionResult EditThread(long sectionId, long? threadId)
        {
            var section = SectionService.Get(sectionId);
            if (section == null)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NotFound();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            ViewData["Section"] = section;
            var threadEditModel = new ThreadEditModel();
            if (!threadId.HasValue)
            {
                if (section.ThreadCategorySettings != ThreadCategoryStatus.Disabled && section.ThreadCategories != null)
                {
                    ViewData["threadcategories"] = section.ThreadCategories;
                }
                threadEditModel.AssociateId = IdGenerator.Next();

                return View(threadEditModel);
            }
            else
            {
                //编辑
                var thread = threadService.Get(threadId.Value);
                thread.MapTo(threadEditModel);
                threadEditModel.SectionId = thread.BarSection.SectionId;
                threadEditModel.Body = threadService.GetBody(threadId.Value);
                threadEditModel.AssociateId = threadId.Value;
                if (section.ThreadCategorySettings != ThreadCategoryStatus.Disabled && section.ThreadCategories != null)
                {
                    threadEditModel.CategoryId = thread.ThreadCategory != null ? thread.ThreadCategory.CategoryId : 0;
                    ViewData["threadcategories"] = section.ThreadCategories;
                }
                var attachments = attachmentService.GetsByAssociateId(threadId.Value);
                ViewData["attachmentList"] = attachments;

                if (attachments != null && attachments.Count() > 0)
                {
                    ViewData["videoCoverImage"] = attachments.Where(n => n.Position == AttachmentPosition.Cover).FirstOrDefault();
                    ViewData["videoAttachment"] = attachments.Where(n => n.Position == AttachmentPosition.Body).Where(n => n.MediaType == MediaType.Video).FirstOrDefault();
                }

                return View(threadEditModel);
            }
        }

        /// <summary>
        /// 创建 编辑贴子
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult EditThread(ThreadEditModel threadEditModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { state = 0 }, "text/html");
            }

            if (threadEditModel.ThreadId == 0)
            {
                //创建
                var section = SectionService.Get(threadEditModel.SectionId);

                var thread = Thread.New();
                thread.SectionId = threadEditModel.SectionId;
                thread.Author = _currentUser.DisplayName;
                thread.Subject = threadEditModel.Subject;
                thread.TenantTypeId = TenantTypeIds.Instance().Thread();
                thread.UserId = _currentUser.UserId;
                thread.Body = threadEditModel.Body;
                thread.IsAllowMobileEdit = false;
                thread.AttachmentIdsFinal = threadEditModel.AttachmentIdsFinal;

                if (!ThreadService.Create(thread, authorizer.IsPostManager(UserContext.CurrentUser)))
                {
                    return Json(new { state = 0 });
                }

                if (threadEditModel.CategoryId != 0)
                {
                    categoryService.AddCategoriesToItem(new List<long> { threadEditModel.CategoryId }, thread.ThreadId);
                }

                return Json(new { state = 1, threadId = thread.ThreadId }, "text/html");
            }
            else
            {
                //编辑权限
                var thread = threadService.Get(threadEditModel.ThreadId);
                bool authorizeResult = thread.UserId == _currentUser.UserId ||
                    authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.SectionId) ||
                    SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId);

                if (!authorizeResult)
                {
                    SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                    TempData["SystemMessageViewModel"] = systemMessageViewModel;
                    return Redirect(SiteUrls.Instance().SystemMessage());
                }

                thread.Subject = threadEditModel.Subject;
                thread.Body = threadEditModel.Body;
                thread.IsAllowMobileEdit = false;
                thread.AttachmentIdsFinal = threadEditModel.AttachmentIdsFinal;

                //更新
                threadService.Update(thread, _currentUser.UserId, authorizer.IsPostManager(UserContext.CurrentUser));

                if (thread.ThreadCategory != null)
                {
                    categoryService.DeleteItemInCategory(thread.ThreadCategory.CategoryId, thread.ThreadId);
                    categoryService.AddCategoriesToItem(new List<long> { threadEditModel.CategoryId }, thread.ThreadId);
                }

                return Json(new { state = 1, threadId = thread.ThreadId }, "text/html");
            }
        }

        /// <summary>
        /// 贴子管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize]
        public ActionResult _ThreadManage(long sectionId)
        {
            var section = SectionService.Get(sectionId);

            //权限验证
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, section.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, section.SectionId);

            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            return PartialView(section);
        }

        /// <summary>
        /// 贴吧管理贴子列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [UserAuthorize]
        public PartialViewResult _ListThreadManage(long sectionId, long? categoryId, int pageIndex = 1, int pageSize = 10, int sortBy_BarDateThread = 0)
        {
            DateTime? startdate = null;
            switch (sortBy_BarDateThread)
            {
                case 1:
                    startdate = DateTime.Now.AddDays(-3);
                    break;

                case 2:
                    startdate = DateTime.Now.AddDays(-7);
                    break;

                case 3:
                    startdate = DateTime.Now.AddMonths(-1);
                    break;

                default:
                    break;
            }

            var threads = threadService.GetsForAdmin(sectionId: sectionId, categoryId: categoryId, startDate: startdate, pageSize: pageSize, pageIndex: pageIndex);

            //查询条件
            ViewData["sectionId"] = sectionId;
            ViewData["categoryId"] = categoryId;
            ViewData["sortBy_BarDateThread"] = sortBy_BarDateThread;
            ViewData["pageIndex"] = pageIndex;

            return PartialView(threads);
        }

        /// <summary>
        /// 贴子详情
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="commentId">回贴评论ID(用于 直接跳转到某一个评论)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ThreadDetail(long threadId, long commentId = 0)
        {
            //todo @yangzd 获取视频文件和视频封面图
            var attachments = attachmentService.GetsByAssociateId(threadId);
            if (attachments != null && attachments.Count() > 0)
            {
                ViewData["featuredImageAttachment"] = attachments.Where(n => n.Position == AttachmentPosition.Cover).FirstOrDefault();
                ViewData["videoAttachment"] = attachments.Where(n => n.Position == AttachmentPosition.Body).Where(n => n.MediaType == MediaType.Video).FirstOrDefault();
            }

            var thread = threadService.Get(threadId);
            var section = SectionService.Get(thread.SectionId);
            ViewData["sectionName"] = section.Name;
            if (thread.ThreadCategory != null)
            {
                ViewData["threadCategoryName"] = thread.ThreadCategory.CategoryName;
            }
            if (thread == null)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NotFound();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            var authorThreadList = ThreadService.GetUserThreads(TenantTypeIds.Instance().Thread(), thread.UserId, false, 6);
            //更新计数
            ThreadCountService.ChangeCount(CountTypes.Instance().HitTimes(), thread.ThreadId, thread.SectionId);

            var author = UserService.GetUser(thread.UserId);
            var userProfile = UserProfileService.Get(thread.UserId);

            int userThreadCount = 0;
            int userCommentCount = 0;
            userThreadCount = UserService.GetUserThreadCount(author.UserId, TenantTypeIds.Instance().Thread());
            userCommentCount = UserService.GetUserCommentCount(author.UserId, TenantTypeIds.Instance().Thread(), false);
            ViewData["userCommentCount"] = userCommentCount;
            ViewData["author"] = author;
            ViewData["userProfile"] = userProfile;
            ViewData["userThreadCount"] = userThreadCount;
            ViewData["rank"] = author.Rank;
            ViewData["authorThreadList"] = authorThreadList;
            //获取评论在 第几页和第几个
            if (commentId > 0)
            {
                var commentIdPageIndex = commentService.GetCommentCount(commentId, threadId, TenantTypeIds.Instance().Thread()) / 10 + 1;
                ViewData["commentIdPageIndex"] = commentIdPageIndex;
                ViewData["commentId"] = commentId;
            }

            #region 活动贴和投票贴

            ViewData["isEventThread"] = IsEnableAppThread("Event");
            ViewData["isVoteThread"] = IsEnableAppThread("Vote");

            #endregion

            ViewData["enabledThread"] = EnabledThread(thread.SectionId);

            return View(thread);
        }

        /// <summary>
        /// 删除贴子
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult DeleteThread(long threadId)
        {
            var thread = threadService.Get(threadId);
            long userId = _currentUser.UserId;
            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.BarSection.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId) ||
                _currentUser.UserId == thread.UserId;

            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            threadService.Delete(threadId);

            return Json(new { state = 1 });
        }

        /// <summary>
        /// 批量删除贴子
        /// </summary>
        /// <param name="threadIds"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult DeleteThreads(string threadIds)
        {
            var ids = threadIds.Split(';');
            var thread = threadService.Get(long.Parse(ids[0]));
            //权限验证
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.SectionId);
            if (!authorizeResult)
            {
                return Json(new { state = 0, msg = "无权操作" });
            }

            for (int i = 0; i < ids.Length; i++)
            {
                long id;
                if (long.TryParse(ids[i], out id))
                {
                    threadService.Delete(id);
                }
            }
            return Json(new { state = 1, msg = "删除成功" });
        }

        /// <summary>
        /// 精华/取消精华
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult Essential(long threadId, bool isApproved)
        {
            var thread = threadService.Get(threadId);

            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.BarSection.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId);

            if (!authorizeResult)
            {
                return Json(new { state = 0, msg = "无权操作" });
            }

            if (isApproved)
            {
                specialContentitemService.Create(threadId, TenantTypeIds.Instance().Thread(), SpecialContentTypeIds.Instance().Essential(), _currentUser.UserId, thread.Subject);
            }
            else
            {
                specialContentitemService.UnStick(threadId, TenantTypeIds.Instance().Thread(), SpecialContentTypeIds.Instance().Essential());
            }

            return Json(new { state = 1 });
        }

        /// <summary>
        /// 批量精华/取消精华
        /// </summary>
        /// <param name="threadIds"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult UpdateEssential(string threadIds, bool isApproved)
        {
            var ids = threadIds.Split(';');
            var thread = threadService.Get(long.Parse(ids[0]));
            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.BarSection.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId);

            if (!authorizeResult)
            {
                return Json(new { state = 0, msg = "无权操作" });
            }

            if (isApproved)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    long id;
                    if (long.TryParse(ids[i], out id))
                    {
                        var essentialthread = threadService.Get(id);

                        specialContentitemService.Create(id, TenantTypeIds.Instance().Thread(), SpecialContentTypeIds.Instance().Essential(), _currentUser.UserId, essentialthread.Subject);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    long id;

                    if (long.TryParse(ids[i], out id))
                    {
                        var essentialthread = threadService.Get(id);
                        specialContentitemService.UnStick(id, TenantTypeIds.Instance().Thread(), SpecialContentTypeIds.Instance().Essential());
                    }
                }
            }

            return Json(new { state = 1 });
        }

        /// <summary>
        /// 置顶/取消置顶
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult Sticky(long threadId, bool isApproved)
        {
            var thread = threadService.Get(threadId);
            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.BarSection.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId);

            if (!authorizeResult)
            {
                return Json(new { state = 0, msg = "无权操作" });
            }

            if (threadService.SetSticky(threadId, isApproved))
                return Json(new { state = 1 });

            return Json(new { state = 0 });
        }

        /// <summary>
        /// 批量取消置顶/置顶
        /// </summary>
        /// <param name="threadIds"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult UpdateStick(string threadIds, bool isApproved)
        {
            var ids = threadIds.Split(';');
            var thread = threadService.Get(long.Parse(ids[0]));
            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.SectionId);

            if (!authorizeResult)
            {
                return Json(new { state = 0, msg = "无权操作" });
            }

            for (int i = 0; i < ids.Length; i++)
            {
                long id;
                if (long.TryParse(ids[i], out id))
                {
                    var stickythread = threadService.Get(id);
                    stickythread.IsSticky = isApproved;

                    threadService.Update(thread, _currentUser.UserId, authorizer.IsPostManager(UserContext.CurrentUser));
                }
            }

            return Json(new { state = 1 });
        }

        /// <summary>
        ///批量贴子通过/不通过审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AuditStatusThreads(string threadIds, bool isApproved)
        {
            var ids = threadIds.Split(';');
            var thread = threadService.Get(long.Parse(ids[0]));
            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.BarSection.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId);
            if (!authorizeResult)
            {
                return Json(new { state = 0, msg = "无权操作" });
            }

            foreach (var item in ids)
            {
                long id = 0;
                if (long.TryParse(item, out id))
                    threadService.UpdateAuditStatus(id, isApproved);
            }

            return Json(new { state = 1, message = "操作成功" });
        }

        /// <summary>
        /// 收藏/取消收藏贴子
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="isApproved"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FavoriteThread(long threadId, bool isApproved = true)
        {
            if (_currentUser == null)
            {
                return Json(new { state = 0, msg = "请先登录" });
            }

            if (isApproved)
            {
                var result = favoriteThreadService.Favorite(threadId, _currentUser.UserId);
                if (result)
                {
                    return Json(new { state = 1, msg = "收藏成功" });
                }

                return Json(new { state = 0, msg = "收藏失败" });
            }
            else
            {
                var result = favoriteThreadService.CancelFavorite(threadId, _currentUser.UserId);
                if (result)
                {
                    return Json(new { state = 1, msg = "取消收藏成功" });
                }

                return Json(new { state = 0, msg = "取消收藏失败" });
            }
        }

        /// <summary>
        /// 删除贴子分类
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteThreadCategorys(long sectionId, string categoryIds)
        {
            //权限
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, sectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, sectionId);
            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            string[] ids = categoryIds.Split(';');
            foreach (var id in ids)
            {
                categoryService.Delete(long.Parse(id));
            }

            return Json(new { state = 1 });
        }

        /// <summary>
        /// 贴吧管理回贴管理
        /// </summary>
        /// <returns></returns>
        public ActionResult _ManagePostComments(long sectionId)
        {
            var section = SectionService.Get(sectionId);

            //权限验证
            bool authorizeResult = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, section.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, section.SectionId);

            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }
            else
            {
                //审核下拉列表项
                List<SelectListItem> auditStatus = new List<SelectListItem>();
                auditStatus.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
                foreach (AuditStatus item in Enum.GetValues(typeof(AuditStatus)))
                {
                    auditStatus.Add(new SelectListItem() { Text = item.GetDisplayName(), Value = item.ToString() });
                }
                ViewData["auditStatus"] = auditStatus;
                ViewData["sectionId"] = sectionId;
                return PartialView();
            }
        }

        /// <summary>
        /// 贴吧管理回贴管理列表
        /// </summary>
        /// <param name="sectionId">贴吧Id</param>
        /// <param name="auditStatus">审核状态</param>
        /// <param name="sortByDate">最近发布的时间</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        public PartialViewResult _ListPostComments(long sectionId, AuditStatus? auditStatus = null, int sortByDate = 0, int pageSize = 10, int pageIndex = 1)
        {
            DateTime? minDate = null;
            switch (sortByDate)
            {
                case 1:
                    minDate = DateTime.Now.AddDays(-3);
                    break;

                case 2:
                    minDate = DateTime.Now.AddDays(-7);
                    break;

                case 3:
                    minDate = DateTime.Now.AddMonths(-1);
                    break;

                default:
                    break;
            }
            var comments = CommentService.GetSectionComments(sectionId, auditStatus, minDate, DateTime.Now, pageSize, pageIndex);
            ViewData["sectionId"] = sectionId;
            return PartialView(comments);
        }

        /// <summary>
        /// 删除回贴
        /// </summary>
        /// <param name="commentIds">用";"拼接的回贴Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeletePostComments(string commentIds)
        {
            if (!string.IsNullOrEmpty(commentIds))
            {
                int deletedNum = 0;
                foreach (var item in commentIds.Split(';'))
                {
                    if (CommentService.Delete(long.Parse(item)))
                        deletedNum++;
                }

                return Json(new StatusMessageData(StatusMessageType.Success, "成功删除" + deletedNum + "条回贴"));
            }

            return Json(new StatusMessageData(StatusMessageType.Error, "删除失败"));
        }

        /// <summary>
        /// 批量审核回贴
        /// </summary>
        /// <param name="commentIds">用";"拼接的回贴Id</param>
        /// <param name="isApproved">是否通过审核</param>
        /// <returns>Json状态</returns>
        [HttpPost]
        public JsonResult UpdatePostComments(string commentIds, bool isApproved)
        {
            if (!string.IsNullOrEmpty(commentIds))
            {
                int updateNum = 0;
                foreach (var commentId in commentIds.Split(';'))
                {
                    CommentService.UpdateAuditStatus(long.Parse(commentId), isApproved);
                    updateNum++;
                }

                return Json(new StatusMessageData(StatusMessageType.Success, "成功更改" + updateNum + "条回贴的审核状态"));
            }

            return Json(new StatusMessageData(StatusMessageType.Error, "更改失败"));
        }

        #endregion 贴子页面

        #region 评论

        /// <summary>
        /// 获取贴子评论列表
        /// </summary>
        /// <param name="threadId">贴子ID</param>
        /// <param name="userId">只看楼主时</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="sortBy_Comment">排序</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListComment(long threadId, long userId = 0, bool isOnlyAuthor = false, int pageSize = 10, int pageIndex = 1, int sortBy_Comment = 0, string tenantTypeId = "")
        {
            ViewData["commentedObjectId"] = threadId;
            ViewData["isOnlyAuthor"] = isOnlyAuthor;
            ViewData["pageIndex"] = pageIndex;
            PagingDataSet<Comment> rootCommentList = null;

            if (string.IsNullOrEmpty(tenantTypeId))
            {
                tenantTypeId = TenantTypeIds.Instance().Thread();
            }
            if (isOnlyAuthor)
            {
                rootCommentList = CommentService.GetObjectComments(tenantTypeId, threadId, pageSize, pageIndex, (SortBy_Comment)sortBy_Comment, false, userId, 0);
            }
            else
            {
                rootCommentList = CommentService.GetRootComments(tenantTypeId, threadId, pageSize, pageIndex, (SortBy_Comment)sortBy_Comment);
            }

            //ViewData["commentCount"] = thread.CommentCount;
            //ViewData["disapprovedCount"] = thread.CommentCount - rootCommentList.TotalRecords;

            ViewData["isPostManager"] = authorizer.IsPostManager(_currentUser);
            ViewData["tenantTypeId"] = tenantTypeId;
            ViewData["userId"] = userId;
            return PartialView(rootCommentList);
        }

        /// <summary>
        /// 子评论列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="commentedObjectId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _ChildrenComment(long parentId, long commentedObjectId, int pageIndex = 1)
        {
            var comment = CommentService.Get(parentId);

            if (comment == null)
            {
                return PartialView(new PagingDataSet<Comment>(null));
            }

            var comments = CommentService.GetChildren(parentId, pageIndex, 20, SortBy_Comment.DateCreated);

            ViewData["parentId"] = parentId;
            ViewData["commentedObjectId"] = commentedObjectId;

            ViewData["isPostManager"] = authorizer.IsPostManager(_currentUser);

            return PartialView(comments);
        }

        /// <summary>
        /// 评论控件
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="commentedObjectId"></param>
        /// <param name="tenantTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _Comment(long parentId, long commentedObjectId, bool isParent = true)
        {
            ViewData["parentId"] = parentId;
            ViewData["commentedObjectId"] = commentedObjectId;
            ViewData["isParent"] = isParent;

            return PartialView();
        }

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="commentEditModel"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public JsonResult EditComment(CommentEditModel commentEditModel)
        {
            long ownerId = 0;
            var parentComment = CommentService.Get(commentEditModel.ParentId);
            if (parentComment != null)
            {
                commentEditModel.ParentIds = string.Format("{0}{1},", parentComment.ParentIds, commentEditModel.ParentId);
                commentEditModel.TenantTypeId = parentComment.TenantTypeId;
                ownerId = parentComment.UserId;
            }
            else
            {
                commentEditModel.ParentIds = "0,";
                var thread = threadService.Get(commentEditModel.CommentedObjectId);
                if (thread != null)
                    ownerId = thread.UserId;
            }

            Comment comment = Comment.New();
            comment.Author = _currentUser.DisplayName;
            comment.Body = commentEditModel.Body;
            comment.ParentIds = commentEditModel.ParentIds;
            comment.ParentId = commentEditModel.ParentId;
            comment.TenantTypeId = commentEditModel.TenantTypeId;
            comment.UserId = _currentUser.UserId;
            comment.CommentedObjectId = commentEditModel.CommentedObjectId;
            comment.OwnerId = ownerId;
            comment.AttachmentIdsFinal = commentEditModel.AttachmentIdsFinal;

            if (!CommentService.Create(comment))
            {
                return Json(new { state = 0 });
            }


            return Json(new { state = 1 });
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpPost]
        [UserAuthorize]
        public ActionResult DeleteComment(long commentId)
        {
            var comment = CommentService.Get(commentId);
            var result = CommentService.Delete(commentId);
            var thread = threadService.Get(comment.CommentedObjectId);
            //权限
            bool authorizeResult = comment.UserId == _currentUser.UserId ||
                authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, thread.BarSection.SectionId) ||
                SectionService.IsSectionOwner(_currentUser.UserId, thread.BarSection.SectionId);

            if (!authorizeResult)
            {
                SystemMessageViewModel systemMessageViewModel = SystemMessageViewModel.NoCompetence();
                TempData["SystemMessageViewModel"] = systemMessageViewModel;
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            return Json(new { state = result });
        }

        #endregion 评论

        #region 验证

        /// <summary>
        /// 获取分类级联
        /// </summary>
        /// <returns></returns>
        private List<object> GetCategories()
        {
            var rootCategories = categoryService.GetRootCategoriesOfOwner(TenantTypeIds.Instance().Bar()); ;
            List<object> results = new List<object>();
            results.Add(new
            {
                v = 0,
                n = "所有分类"
            });
            foreach (var item in rootCategories)
            {
                List<object> childrenResults = new List<object>();

                childrenResults.Add(new
                {
                    v = item.CategoryId,
                    n = "所有分类"
                });

                foreach (var childItem in item.Children)
                {
                    childrenResults.Add(new
                    {
                        v = childItem.CategoryId,
                        n = childItem.CategoryName
                    });
                }

                results.Add(new
                {
                    v = item.CategoryId,
                    n = item.CategoryName,
                    s = childrenResults
                });
            }
            return results;
        }

        /// <summary>
        /// 是否可以发帖
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        private bool EnabledThread(long sectionId)
        {
            bool enabledThread = false;
            if (_currentUser == null)
                return enabledThread;

            var section = SectionService.Get(sectionId);
            switch (section.SectionPostSetting)
            {
                case SectionPostSetting.OwnerAndManagers:
                    enabledThread = authorizer.IsCategoryManager(TenantTypeIds.Instance().Section(), _currentUser, sectionId) ||
                    SectionService.IsSectionOwner(_currentUser.UserId, sectionId);
                    break;
                default:
                    enabledThread = true;
                    break;
            }

            return enabledThread;
        }

        /// <summary>
        /// 是否启用投票贴或活动贴
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        private bool IsEnableAppThread(string appKey)
        {
            int eventRankLimit = 0;
            bool evenIsEnableAppThread = false;
            var evenConfig = ApplicationConfig.GetConfig(appKey);
            if (evenConfig != null)
            {
                Type t = evenConfig.ApplicationType;
                eventRankLimit = (int)t.GetProperty("RankLimit").GetValue(evenConfig, null);
                evenIsEnableAppThread = (bool)t.GetProperty("IsEnableAppThread").GetValue(evenConfig, null);
            }

            return evenIsEnableAppThread && _currentUser != null && (authorizer.IsPostManager(_currentUser) || _currentUser.Rank >= eventRankLimit);
        }

        #endregion 验证

        /// <summary>
        /// 编辑贴子中的视频
        /// </summary>
        /// <param name="videoAttachmentId"></param>
        /// <param name="videoCoverAttachmentId"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _EditThreadVideo(long videoAttachmentId = 0, long videoCoverAttachmentId = 0)
        {
            if (videoAttachmentId > 0)
            {
                var videoAttachment = attachmentService.Get(videoAttachmentId);
                ViewData["videoAttachment"] = videoAttachment;
            }
            if (videoCoverAttachmentId > 0)
            {
                var videoCoverImage = attachmentService.Get(videoCoverAttachmentId);
                ViewData["videoCoverImage"] = videoCoverImage;
            }

            return PartialView();
        }

    }
}