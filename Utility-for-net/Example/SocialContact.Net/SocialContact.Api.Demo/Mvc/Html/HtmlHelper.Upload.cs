//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Tunynet.Common
{
    /// <summary>
    /// 封装Uploadify插件
    /// </summary>
    public static class HtmlHelperUploadExtensions
    {

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="htmlHelper">被扩展对象</param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HtmlString FileUploader(this HtmlHelper htmlHelper, Action<UploadSettings> method)
        {
            var settings = new UploadSettings();
            method(settings);

            TagBuilder builder = new TagBuilder("div");
            builder.MergeAttribute("class", "webuploader-pick");

            builder.MergeAttribute("href", "javascript:;");
            builder.MergeAttribute("name", settings.Name);
            builder.MergeAttribute("id", "uploader-" + settings.Name);
            builder.MergeAttribute("data-plugin", "fileuploader");
            builder.MergeAttribute("data-uploadurl", settings.UploadUrl);
            builder.MergeAttribute("data-extensions", settings.Extensions);
            builder.MergeAttribute("data-show-progress", settings.IsShowProgress.ToString().ToLower());
            builder.MergeAttribute("data-maxCount", settings.MaxCount.ToString());
            //builder.MergeAttribute("data-associateid", settings.AssociateId.ToString());
            builder.MergeAttribute("data-multiple", settings.Multiple.ToString().ToLower());
            builder.MergeAttribute("data-innercontent", settings.InnerContent);
            builder.MergeAttribute("data-uploading-only", settings.UploadingOnly.ToString().ToLower());

            if (!string.IsNullOrEmpty(settings.ProgressId))
            {
                builder.MergeAttribute("data-progress-id", settings.ProgressId);
            }

            if (settings.IsPreview)
            {
                builder.MergeAttribute("data-preview", settings.IsPreview.ToString().ToLower());
            }

            //builder.InnerHtml = innerContent;
            if (settings.Callbacks != null && settings.Callbacks.Any())
            {
                foreach (var callback in settings.Callbacks)
                {
                    builder.MergeAttribute("data-" + callback.Key.ToLower(), callback.Value);
                }
            }
           // builder.InnerHtml = "<i class=\"fa fa-spinner \"></i>";
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="htmlHelper">被扩展对象</param>
        /// <param name="name">组件名称</param>
        /// <param name="tenantTypeId">租户类型标识</param>
        /// <param name="ownerId">拥有者标识</param>
        /// <param name="innerContent"></param>
        /// <param name="uploadUrl">文件上传路径</param>
        /// <param name="associateId">关联的主内容</param>
        /// <param name="extensions">允许的文件类型</param>
        /// <param name="callbacks">回调函数 包含("onupload,onerror,onstart")</param>
        /// <param name="btnSelector">指定的上传按钮</param>
        /// <param name="showProgress">显示进度条</param>
        /// <param name="maxCount">最大数量</param>
        /// <param name="multiple">是否允许多个</param>
        /// <param name="key">图片大小的Key</param>
        /// <returns></returns>
        public static HtmlString FileUploader(this HtmlHelper htmlHelper, string name, string tenantTypeId, long ownerId, string innerContent = "", string uploadUrl = "", long associateId = 0, string extensions = "", object callbacks = null, string btnSelector = "", bool showProgress = true, int maxCount = 0, bool multiple = true, string key = "", bool isPreview = false, bool uploadingOnly = true, string progressId = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ExceptionFacade("参数不能为空");
            }

            if (string.IsNullOrWhiteSpace(uploadUrl))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    uploadUrl = "Uploads/Common?key="+ key ;
                }
                else
                {
                    uploadUrl ="Uploads/Common";
                }
            }

            TagBuilder builder = new TagBuilder("div");
            builder.MergeAttribute("class", "webuploader-pick");

            builder.MergeAttribute("href", "javascript:;");
            builder.MergeAttribute("name", name);
            builder.MergeAttribute("id", "uploader-" + name);
            builder.MergeAttribute("data-plugin", "fileuploader");
            builder.MergeAttribute("data-uploadurl", uploadUrl);
            builder.MergeAttribute("data-tenanttypeid", tenantTypeId);
            builder.MergeAttribute("data-ownerid", ownerId.ToString());
            builder.MergeAttribute("data-extensions", extensions);
            builder.MergeAttribute("data-selector", btnSelector);
            builder.MergeAttribute("data-show-progress", showProgress.ToString().ToLower());
            builder.MergeAttribute("data-maxCount", maxCount.ToString());
            builder.MergeAttribute("data-associateid", associateId.ToString());
            builder.MergeAttribute("data-multiple", multiple.ToString().ToLower());
            builder.MergeAttribute("data-innercontent", innerContent);
            builder.MergeAttribute("data-uploading-only", uploadingOnly.ToString().ToLower());

            if (!string.IsNullOrEmpty(progressId))
            {
                builder.MergeAttribute("data-progress-id", progressId);
            }

            if (isPreview)
            {
                builder.MergeAttribute("data-preview", isPreview.ToString().ToLower());
            }

            //builder.InnerHtml = innerContent;
            if (callbacks != null)
            {
                IDictionary<string,object> callbackDict = HtmlHelper.AnonymousObjectToHtmlAttributes(callbacks);

                foreach (var callback in callbackDict)
                    builder.MergeAttribute("data-" + callback.Key.ToLower(), callback.Value.ToString());
            }

            return new HtmlString(builder.ToString());
        }

        ///// <summary>
        ///// 封面图设置
        ///// </summary>
        ///// <param name="htmlHelper"></param>
        ///// <param name="name"></param>
        ///// <param name="tenantTypeId"></param>
        ///// <param name="ownerId"></param>
        ///// <param name="imageId"></param>
        ///// <returns></returns>
        //public static HtmlString CoverImageUploder(this HtmlHelper htmlHelper, string name, string tenantTypeId, long ownerId, long imageId = 0)
        //{
        //    string innerHtml = "";
        //    TagBuilder trigger = new TagBuilder("a");

        //    trigger.MergeAttribute("href", "javascript:;");
        //    trigger.MergeAttribute("data-name", name);
        //    trigger.MergeAttribute("class", "jn-training-cover");
        //    trigger.MergeAttribute("data-plugin", "CoverImageUploder");
        //    trigger.MergeAttribute("data-uploadurl", CachedUrlHelper.Action("UploadPicture", "Portal", "", new RouteValueDictionary { { "TenantTypeId", tenantTypeId }, { "ownerId", ownerId }, { "timestamp", "System.DateTime.Now.Millisecond" } }));

        //    trigger.InnerHtml = "<i class=\"fa fa-picture-o\"></i>";

        //    TagBuilder img = new TagBuilder("img");
        //    img.MergeAttribute("id", "spanimg");

        //    if (imageId > 0)
        //    {
        //        Attachment iamge = DIContainer.Resolve<AttachmentService>().Get(imageId);
        //        if (iamge != null)
        //        {
        //            img.MergeAttribute("src", iamge.GetDirectlyUrl());
        //            img.MergeAttribute("style", "width:100px;height:100px;");
        //            trigger.MergeAttribute("style", "display:none;width:100px;height:100px;");
        //        }
        //    }
        //    else
        //    {
        //        img.MergeAttribute("style", "display:none;width:100px;height:100px;");
        //    }

        //    TagBuilder input = new TagBuilder("input");
        //    input.MergeAttribute("type", "hidden");
        //    input.MergeAttribute("name", name);
        //    input.MergeAttribute("value", imageId.ToString());

        //    innerHtml = trigger.ToString() + img.ToString() + input.ToString();

        //    return new HtmlString(innerHtml);
        //}

        ///// <summary>
        ///// 整理允许上传的文件类型
        ///// </summary>
        ///// <param name="allowedFileExtensions">从配置文件提取的文件类型字符串</param>
        ///// <returns> 整理好的允许上传的文件类型</returns>
        //private static string FileTypeExts(string allowedFileExtensions)
        //{
        //    StringBuilder str = new StringBuilder();
        //    //默认取配置项里的设置
        //    string[] exts = allowedFileExtensions.Split(',');

        //    foreach (var ext in exts)
        //    {
        //        str.Append("*." + ext + ";");
        //    }
        //    return str.ToString().TrimEnd(';');
        //}
    }

    /// <summary>
    /// 上传参数设置
    /// </summary>
    public class UploadSettings
    {

        private string name = "";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                }
                else
                {
                    throw new Exception("参数Name不能为空");
                }
            }
        }

        private string tenantTypeId = "";

        /// <summary>
        /// 租户
        /// </summary>
        public string TenantTypeId
        {
            get { return tenantTypeId; }
            set
            {
                if (!string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
                {
                    tenantTypeId = value;
                }
                else
                {
                    throw new Exception("参数TenantTypeId不能为空");
                }
            }
        }

        //private long associateId = 0L;

        ///// <summary>
        ///// 关联内容Id
        ///// </summary>
        //public long AssociateId
        //{
        //    get { return associateId; }
        //    set { associateId = value; }
        //}

        private string innerContent = "";

        /// <summary>
        /// 关联内容Id
        /// </summary>
        public string InnerContent
        {
            get { return innerContent; }
            set { innerContent = value; }
        }

        private AttachmentPosition position = AttachmentPosition.NotSet;

        /// <summary>
        /// 关联内容Id
        /// </summary>
        public AttachmentPosition Position
        {
            get { return position; }
            set { position = value; }
        }

        private string uploadUrl = "";

        /// <summary>
        /// 内容
        /// </summary>
        public string UploadUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(uploadUrl))
                {
                    uploadUrl = "Uploads/Common?key="+ Key+"&position="+ Position + "&tenantTypeId=" + TenantTypeId ;
                }
                return uploadUrl;
            }
            set { uploadUrl = value; }
        }

        private string extensions = "*";

        /// <summary>
        /// 内容
        /// </summary>
        public string Extensions
        {
            get { return extensions; }
            set { extensions = value; }
        }

        private Dictionary<string, string> callbacks = new Dictionary<string, string>();

        /// <summary>
        /// 回调函数
        /// </summary>
        public Dictionary<string, string> Callbacks
        {
            get { return callbacks; }
        }

        private bool isShowProgress = true;

        /// <summary>
        /// 是否显示进度条
        /// </summary>
        public bool IsShowProgress
        {
            get { return isShowProgress; }
            set { isShowProgress = value; }
        }

        private int maxCount = 0;

        /// <summary>
        /// 最大可传文件数
        /// </summary>
        public int MaxCount
        {
            get { return maxCount; }
            set { maxCount = value; }
        }

        private bool multiple = true;

        /// <summary>
        /// 是否允许多个
        /// </summary>
        public bool Multiple
        {
            get { return multiple; }
            set { multiple = value; }
        }

        private string key = "";

        /// <summary>
        /// 图片预览时图片的Key
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private bool isPreview = true;

        /// <summary>
        /// 是否可以预览
        /// </summary>
        public bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        private bool uploadingOnly = true;

        /// <summary>
        /// 上传列表中只显示上传中的项目
        /// </summary>
        public bool UploadingOnly
        {
            get { return uploadingOnly; }
            set { uploadingOnly = value; }
        }

        private string progressId = "";

        /// <summary>
        /// 进度条容器Id
        /// </summary>
        public string ProgressId
        {
            get { return progressId; }
            set { progressId = value; }
        }

    }

}