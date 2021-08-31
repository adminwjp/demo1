//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using SocialContact.Domain.Events;
using SocialContact.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tunynet.Attitude;
using Tunynet.CMS;
using Tunynet.Common;
using Tunynet.Post;

namespace Tunynet.Spacebuilder
{
    /// <summary>
    /// CMS 控制器
    /// </summary>
    [UserAuthorize(IsAllowAnonymous = true)]
    public partial class CMSController : Controller
    {

        //附件
        private AttachmentService attachmentService = new AttachmentService(TenantTypeIds.Instance().ContentItem());

        //标签
        private TagService tagService = new TagService(TenantTypeIds.Instance().ContentItem());

        //用户资料
        private UserProfileService userProfileService;

        //评论
        private CommentService commentService;

        //点赞
        private AttitudeService attitudeService = new AttitudeService(TenantTypeIds.Instance().ContentItem());

        private ThreadService threadService;
        private UserService userService;
        private FollowService followService;
        private PointService pointService;

        private NHibernate.ISession session;
        //收藏
        private FavoriteService favoriteService = new FavoriteService(TenantTypeIds.Instance().ContentItem());

        //推荐
        private SpecialContentItemService specialContentitemService;

        private IKvStore kvStore;

        private User user;
       
        #region 资讯前台显示

        /// <summary>
        /// 热点图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _HotarticleImg()
        {
            //一周热文
            var hotcmsImgList = CmsHelper._HotarticleImg();
            return PartialView(hotcmsImgList.Count > 0 ? hotcmsImgList.Take(6) : hotcmsImgList);
        }

        /// <summary>
        /// 一周热文
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _Hotarticle()
        {
            var contentItems = ContentItemService.GetTopContentItems(6, null, true, DateTime.Now.AddDays(-7), ContentItemSortBy.HitTimes);
            if (!contentItems.Any())
                contentItems = ContentItemService.GetTopContentItems(6, null, true, DateTime.Now.AddMonths(-1), ContentItemSortBy.HitTimes);
            return PartialView(contentItems);
        }

        /// <summary>
        /// 资讯详情
        /// </summary>
        /// <param name="contentItemId">资讯ID</param>
        /// <param name="commentId">评论ID(用于 直接跳转到某一个评论)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CMSDetail(long contentItemId, long commentId = 0)
        {
            //获取资讯
            var contentItem = ContentItemService.Get(contentItemId);
            if (contentItem == null)
                return Redirect(SiteUrls.Instance().Error());

            #region 资讯详情 跳转

            if (contentItem.ContentModel != null)
                if ("CMSDetail" != contentItem.ContentModel.PageDetail)
                    //todo by yangzd
                    return RedirectToAction(contentItem.ContentModel.PageDetail, new { contentItemId = contentItemId, commentId = commentId });

            #endregion 资讯详情 跳转

            //所属栏目
            var category = ContentCategoryService.Get(null,contentItem.ContentCategoryId);
            ViewData["category"] = category;
            //内容底部附件列表
            if (contentItem.ContentModel.ModelKey == ContentModelKeys.Instance().Contribution())
                attachmentService = new AttachmentService(TenantTypeIds.Instance().CMS_Article());
            else
                attachmentService = new AttachmentService(TenantTypeIds.Instance().ContentItem());

            var attachments = attachmentService.GetsByAssociateId(session, contentItem.ContentItemId).Where(n => n.Position == AttachmentPosition.AttachmentList);
            ViewData["attachments"] = attachments;
            //标签
            var tags = tagService.GetTopTagsOfItem(contentItemId, 10);
            ViewData["tags"] = tags;

            //点赞
            var attitude = attitudeService.Get(contentItemId);
            ViewData["attitude"] = attitude;
            CountService.ChangeCount(new ChangeCountEvent() { countType= CountTypes.Instance().HitTimes() ,objectId= contentItemId 
            ,ownerId=0,changeCount=1,isRealTime=true});//更新计数
            //评论数
            ViewData["commentCount"] = CommentService.GetObjectComments(TenantTypeIds.Instance().ContentItem(), contentItemId, 10, 1, SortBy_Comment.DateCreated, null).TotalRecords;
            //获取评论在 第几页和第几个
            if (commentId > 0)
            {
                var commentIdPageIndex = CommentService.GetCommentCount(commentId, contentItemId, TenantTypeIds.Instance().ContentItem()) / 10 + 1;
                ViewData["commentIdPageIndex"] = commentIdPageIndex;
                ViewData["commentId"] = commentId;
            }
            return View(contentItem);
        }

        /// <summary>
        /// 前台资讯首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContentItemHome()
        {
            var specialContentiItem = specialContentitemService.GetTops(1, SpecialContentTypeIds.Instance().SpecialCMS(), TenantTypeIds.Instance().CMS_Article(), true);
            var contentItem = specialContentiItem.Any() ? ContentItemService.Get(specialContentiItem.First().ItemId) : new ContentItem();
            ViewData["specialContentiItem"] = specialContentiItem.Any() ? specialContentiItem.FirstOrDefault() : new SpecialContentItem();
            return View(contentItem);
        }

        /// <summary>
        /// 前台资讯首页列表
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListContentItem(int pageSize = 10, int pageIndex = 1)
        {
            var contentItems = ContentItemService.GetContentItems(null, true, null, pageSize, pageIndex, true, ContentItemSortBy.DatePublished_Desc);
            return PartialView(contentItems);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="contentItemId">资讯ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Support(long contentItemId)
        {
            var isSupport = attitudeService.IsSupport(contentItemId, user.UserId);
            var isAttitude = attitudeService.Support(contentItemId, user.UserId);
            if (!isAttitude)
            {
                return Json(new StatusMessageData(StatusMessageType.Hint, "操作失败"));
            }
            if (isSupport == true)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "取消点赞成功"));
            }
            else
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "点赞成功"));
            }
        }

        /// <summary>
        /// 作者最近的文章
        /// </summary>
        /// <param name="userId">作者ID</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _RecentCMS(long userId)
        {
            var contentItems = ContentItemService.GetContentItems(null, true, userId, 6, 1, true, ContentItemSortBy.DatePublished_Desc);
            return PartialView(contentItems);
        }

        /// <summary>
        /// 标签内资讯列表
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TagCMS(long tagid)
        {
            var tag = tagService.Get(tagid);
            if (tag == null)
                return Redirect(SiteUrls.Instance().Error());
            //获取热门标签
            ViewData["hotTags"] = tagService.GetTopTags(10, null, SortBy_Tag.ItemCountDesc);
            return View(tag);
        }

        /// <summary>
        /// 标签内资讯列表分页
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListTagCMS(string tagName, int pageSize = 10, int pageIndex = 1)
        {
            var tagContentItems = tagService.GetItemIds(tagName, pageSize, pageIndex);
            var itemIds = tagContentItems.Select(n => n.ItemId);
            PagingDataSet<ContentItem> contentItems = new PagingDataSet<ContentItem>(ContentItemService.Gets(itemIds))
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = tagContentItems.TotalRecords
            };

            return PartialView(contentItems);
        }

        /// <summary>
        /// 栏目内资讯列表
        /// </summary>
        /// <param name="contentCategoryId">栏目Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CategoryCMS(int contentCategoryId)
        {
            var category = ContentCategoryService.Get(null,contentCategoryId);
            if (category == null)
            {
                return Redirect(SiteUrls.Instance().Error());
            }
            //区分栏目级别
            ContentCategory parentCategory;
            if (category.Depth == 0)
                parentCategory = category;
            else
                parentCategory = ContentCategoryService.Get(null,category.ParentId);

            ViewData["contentCategoryId"] = contentCategoryId;
            //栏目
            ViewData["parentCategory"] = parentCategory;

            return View();
        }

        /// <summary>
        /// 栏目内资讯列表分页
        /// </summary>
        /// <param name="categoryId">栏目ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListCategoryCMS(int categoryId, int pageSize = 10, int pageIndex = 1)
        {
            var contentList = ContentItemService.GetContentItems(categoryId, true, null, pageSize, pageIndex, true);
            ViewData["categoryId"] = categoryId;

            return PartialView(contentList);
        }

        /// <summary>
        /// 获取子栏目信息
        /// </summary>
        /// <param name="contentCategoryId">当前栏目ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetChildCategories(int contentCategoryId)
        {
            var category = ContentCategoryService.Get(null,contentCategoryId);
            if (category == null)
            {
                return Redirect(SiteUrls.Instance().Error());
            }
            return Json(category.Children.Select(t => new
            {
                id = t.CategoryId,
                name = t.CategoryName
            }));
        }

        #endregion 资讯前台显示

        #region 视频新闻

        /// <summary>
        /// 视频资讯详情
        /// </summary>
        /// <param name="contentItemId">资讯ID</param>
        /// <param name="commentId">评论ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CMSVideoDetail(long contentItemId, long commentId = 0)
        {
            ContentItem contentItem = ContentItemService.Get(contentItemId);
            if (contentItem == null)
            {
                return Redirect(SiteUrls.Instance().Error());
            }

            attachmentService = new AttachmentService(TenantTypeIds.Instance().CMS_Video());

            #region 资讯详情 跳转

            if (contentItem.ContentModel != null)
                if ("CMSVideoDetail" != contentItem.ContentModel.PageDetail)
                    return RedirectToAction(contentItem.ContentModel.PageDetail, new { contentItemId = contentItemId });

            #endregion 资讯详情 跳转

            //如果是上传附件则获取附件
            if (!contentItem.AdditionalProperties.ContainsKey("VideoUrl") || string.IsNullOrEmpty(contentItem.AdditionalProperties["VideoUrl"].ToString()))
            {
                if (attachmentService.GetsByAssociateId(session, contentItemId).Where(n => n.MediaType == MediaType.Video).Any())
                    ViewData["attachment"] = attachmentService.GetsByAssociateId(session, contentItemId).Where(n => n.MediaType == MediaType.Video).OrderByDescending(n => n.AttachmentId).First();
            }

            //更新计数
            CountService.ChangeCount(CountTypes.Instance().HitTimes(), contentItemId, contentItem.UserId, 1, true);
            //获取评论在 第几页和第几个
            if (commentId > 0)
            {
                var commentIdPageIndex = CommentService.GetCommentCount(commentId, contentItemId, TenantTypeIds.Instance().ContentItem()) / 10 + 1;
                ViewData["commentIdPageIndex"] = commentIdPageIndex;
                ViewData["commentId"] = commentId;
            }

            return View(contentItem);
        }

        /// <summary>
        /// 视频资讯列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CMSVideo()
        {
            //获取推荐的视频
            var specialVideoCMS = specialContentitemService.GetTops(1, SpecialContentTypeIds.Instance().CMS_Video(), TenantTypeIds.Instance().CMS_Video(), true);
            //如果没有推荐则显示最新的视频新闻
            ViewData["specialVideoCMS"] = specialVideoCMS.Any() ? ContentItemService.Get(specialVideoCMS.First().ItemId) : ContentItemService.GetTopContentItemsofModelKey(1, ContentModelKeys.Instance().Video(), null, ContentItemSortBy.DatePublished_Desc).FirstOrDefault();

            ViewData["specialContentiItem"] = specialVideoCMS.Any() ? specialVideoCMS.FirstOrDefault() : new SpecialContentItem();
            //热门视频
            ViewData["hotVideoCMS"] = ContentItemService.GetTopContentItemsofModelKey(4, ContentModelKeys.Instance().Video(), null, ContentItemSortBy.HitTimes);

            return View();
        }

        /// <summary>
        /// 视频资讯列表分页
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ListCMSVideo(int pageIndex = 1)
        {
            var videoCMSList = ContentItemService.GetContentItemsofModelKey(ContentModelKeys.Instance().Video(), 8, pageIndex, ContentItemSortBy.DatePublished_Desc);
            return PartialView(videoCMSList);
        }

        /// <summary>
        /// 热点视频
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _HotarticleVideo()
        {
            return PartialView(ContentItemService.GetTopContentItemsofModelKey(6, ContentModelKeys.Instance().Video(), null, ContentItemSortBy.HitTimes));
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <param name="contentItemId">资讯ID</param>
        /// <param name="isFavorite">是否收藏</param>
        /// <returns></returns>
        public JsonResult Favorite(long contentItemId, bool isFavorite)
        {
            if (user == null)
                return Json(new { state = "用户未登录", msg = "操作失败", isFavorite });
            if (isFavorite)
            {
                if (favoriteService.Favorite(contentItemId, user.UserId))
                    return Json(new { state = "已收藏", msg = "收藏成功", isFavorite });
            }
            else
            {
                if (favoriteService.CancelFavorite(contentItemId, user.UserId))
                    return Json(new { state = "收藏", msg = "取消收藏成功", isFavorite });
            }
            return Json(new { state = "操作失败", msg = "操作失败", isFavorite });
        }

        #endregion 视频新闻

        #region 组图新闻

        /// <summary>
        /// 组图详情
        /// </summary>
        /// <param name="contentItemId">资讯ID</param>
        /// <returns></returns>
        public ActionResult CMSImgDetail(int contentItemId)
        {
            ContentItem contentItem = ContentItemService.Get(contentItemId);
            if (contentItem == null)
            {
                return Redirect(SiteUrls.Instance().Error());
            }

            #region 资讯详情 跳转

            if (contentItem.ContentModel != null)
                if ("CMSImgDetail" != contentItem.ContentModel.PageDetail)
                    return RedirectToAction(contentItem.ContentModel.PageDetail, new { contentItemId = contentItemId });

            #endregion 资讯详情 跳转

            var attachmentService = new AttachmentService(TenantTypeIds.Instance().CMS_Image());
            var attachments = attachmentService.GetsByAssociateId(session, contentItemId).OrderBy(n => n.DisplayOrder).ToList();

            ViewData["attachmentList"] = attachments;
            //更新计数
            CountService.ChangeCount(CountTypes.Instance().HitTimes(), contentItemId, contentItem.UserId, 1, true);
            return View(contentItem);
        }

        /// <summary>
        /// 组图列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CMSImg()
        {
            //获取推荐的组图
            ViewData["cmsImgList"] = specialContentitemService.GetTops(10, SpecialContentTypeIds.Instance().CMS_Image(), TenantTypeIds.Instance().CMS_Image(), true);
            return View();
        }

        /// <summary>
        /// 组图列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public PartialViewResult _ListCMSImg(int pageIndex = 1)
        {
            var cmsImgList = ContentItemService.GetContentItemsofModelKey(ContentModelKeys.Instance().Image(), 9, pageIndex, ContentItemSortBy.DatePublished_Desc);
            return PartialView(cmsImgList);
        }

        #endregion 组图新闻
    }
}