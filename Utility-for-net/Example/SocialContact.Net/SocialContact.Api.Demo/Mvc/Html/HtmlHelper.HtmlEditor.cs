//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tunynet.Common
{
    /// <summary>
    /// 扩展对Html编辑器的HtmlHelper输出方法
    /// </summary>
    public static class HtmlHelperHtmlEditorExtensions
    {

        /// <summary>
        /// 输出Html编辑器
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HtmlString HtmlEditor(this HtmlHelper htmlHelper, Action<UEditorSettings> method)
        {
            var settings = new UEditorSettings();
            method(settings);

            TagBuilder builder = new TagBuilder("span");
            Dictionary<string, object> htmlAttrs = new Dictionary<string, object>();
            if (settings.HtmlAttributes != null && settings.HtmlAttributes.Any())
            {
                htmlAttrs.AddRange(settings.HtmlAttributes, false);
            }
            var data = new Dictionary<string, object>();
            data.Add("tenantTypeId", settings.TenantTypeId);
            //data.Add("associateId", settings.AssociateId);

            if (string.IsNullOrEmpty(settings.TenantTypeId))
            {
                htmlAttrs.Add("tenant", 0);
            }
            else
            {
                htmlAttrs.Add("tenant", 1);
            }
            htmlAttrs.Add("data", JsonConvert.SerializeObject(data));

            htmlAttrs.Add("plugin", "ueditor");

            //添加扩展
            var extensions = settings.UEditorExtensionsCollection;
            if (extensions != null && extensions.Any())
            {
                htmlAttrs.Add("data-extensions", string.Join(",", extensions.Select(n => n.Type.ToString().ToLower())));
                extensions.ForEach(n =>
                {
                    if (n.HtmlAttributes != null && n.HtmlAttributes.Any())
                    {
                        htmlAttrs.AddRange(n.HtmlAttributes, false);
                    }
                });
            }

            htmlAttrs.Add("id", settings.Id);
            //builder.InnerHtml = htmlHelper.TextArea(settings.Name, settings.Body ?? string.Empty, htmlAttrs).ToString();
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// 输出Html编辑器 (已过时)
        /// </summary>
        /// <param name="htmlHelper">被扩展的htmlHelper实例</param>
        /// <param name="name">编辑器name属性</param>
        /// <param name="tenantTypeId">租户类型</param>
        /// <param name="associateId">关联项Id</param>
        /// <param name="value">编辑器内容</param>
        /// <param name="htmlAttributes">Html变量</param>
        /// <param name="types"></param>
        /// <param name="extensions">扩展功能 使用","分割</param>
        /// <returns>MvcForm</returns>
        public static HtmlString HtmlEditor(this HtmlHelper htmlHelper, string name, string tenantTypeId, long associateId = 0, string value = null, Dictionary<string, object> htmlAttributes = null, string types = "", string extensions = "insertface,uploadfile,uploadimage")
        {
            TagBuilder builder = new TagBuilder("span");
            Dictionary<string, object> htmlAttrs = new Dictionary<string, object>();
            if (htmlAttributes != null)
                htmlAttrs = new Dictionary<string, object>(htmlAttributes);
            var data = new Dictionary<string, object>();
            data.Add("tenantTypeId", tenantTypeId);
            data.Add("associateId", associateId);

            //todo:libsh,需要后期处理
            //data.Add("ownerId", UserContext.CurrentUser.UserId);

            if (string.IsNullOrEmpty(tenantTypeId))
            {
                htmlAttrs.Add("tenant", 0);
            }
            else
            {
                htmlAttrs.Add("tenant", 1);
            }
            htmlAttrs.Add("types", types);
            htmlAttrs.Add("data", JsonConvert.SerializeObject(data));
            htmlAttrs.Add("plugin", "ueditor");
            htmlAttrs.Add("data-extensions", extensions);
           // builder.InnerHtml = htmlHelper.TextArea(name, value ?? string.Empty, htmlAttrs).ToString();
            return new HtmlString(builder.ToString());
        }

        ///// <summary>
        ///// 利用ViewModel输出Html编辑器
        ///// </summary>
        ///// <typeparam name="TModel"></typeparam>
        ///// <typeparam name="TProperty"></typeparam>
        ///// <param name="htmlHelper">被扩展的htmlHelper实例</param>
        ///// <param name="expression">获取ViewModel中的对应的属性</param>
        ///// <param name="options">编辑器设置选项</param>
        ///// <param name="types">自定义添加- "map"、"insertcode"、"map,insertcode"</param>
        ///// <returns>HtmlString</returns>
        //public static HtmlString HtmlEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string tenantTypeId, long associateId = 0, Dictionary<string, object> htmlAttributes = null, string types = "")
        //{
        //    TagBuilder builder = new TagBuilder("span");
        //    Dictionary<string, object> htmlAttrs = new Dictionary<string, object>();
        //    if (htmlAttributes != null)
        //        htmlAttrs = new Dictionary<string, object>(htmlAttributes);
        //    var data = new Dictionary<string, object>();
        //    data.Add("tenantTypeId", tenantTypeId);
        //    data.Add("associateId", associateId);

        //    //todo:libsh,需要后期处理
        //    //data.Add("ownerId", UserContext.CurrentUser.UserId);

        //    if (string.IsNullOrEmpty(tenantTypeId))
        //    {
        //        htmlAttrs.Add("tenant", 0);
        //    }
        //    else
        //    {
        //        htmlAttrs.Add("tenant", 1);
        //    }
        //    htmlAttrs.Add("types", types);
        //    htmlAttrs.Add("data", JsonConvert.SerializeObject(data));
        //    htmlAttrs.Add("plugin", "ueditor");
        //    builder.InnerHtml = htmlHelper.TextAreaFor(expression, htmlAttrs).ToString();
        //    return new HtmlString(builder.ToString());
        //}
    }

    /// <summary>
    /// Html编辑器扩展
    /// </summary>
    public enum UEditorExtensionType
    {
        /// <summary>
        /// 表情
        /// </summary>
        InsertFace = 0,

        /// <summary>
        /// 上传附件
        /// </summary>
        UploadFile = 1,

        /// <summary>
        /// 上传图片
        /// </summary>
        UploadImage = 2
    }

    /// <summary>
    /// 百度编辑器参数设置
    /// </summary>
    public class UEditorSettings
    {
        private string id = "Body";

        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name = "Body";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string tenantTypeId = "";

        /// <summary>
        /// 租户
        /// </summary>
        public string TenantTypeId
        {
            get { return tenantTypeId; }
            set { tenantTypeId = value; }
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

        private string body = "";

        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private Dictionary<string, object> htmlAttributes = null;

        /// <summary>
        /// html参数对象
        /// </summary>
        public Dictionary<string, object> HtmlAttributes
        {
            get { return htmlAttributes; }
            set { htmlAttributes = value; }
        }

        private List<UEditorExtension> uEditorExtensionsCollection = new List<UEditorExtension>();

        /// <summary>
        /// 扩展
        /// </summary>
        public List<UEditorExtension> UEditorExtensionsCollection
        {
            get { return uEditorExtensionsCollection; }
        }

        //private HtmlEditorExtensions[] extensions = new HtmlEditorExtensions[] {
        //    HtmlEditorExtensions.Insertface,
        //    HtmlEditorExtensions.Uploadfile,
        //    HtmlEditorExtensions.Uploadimage };

        ///// <summary>
        ///// 名称
        ///// </summary>
        //public string Extensions
        //{
        //    get
        //    {
        //        return string.Join(",", extensions.Select(n => n.ToString().ToLower()));
        //    }
        //    set
        //    {
        //        var array = new HtmlEditorExtensions[] { };
        //        var nameEnumDictionary = new Dictionary<string, Enum>();
        //        foreach (Enum item in Enum.GetValues(typeof(HtmlEditorExtensions)))
        //        {
        //            nameEnumDictionary.Add(item.ToString().ToLower(), item);
        //        }
        //        var index = 0;
        //        foreach (var item in value.Split(','))
        //        {
        //            array[index]=(HtmlEditorExtensions)(nameEnumDictionary[item]);
        //            index++;
        //        }
        //        extensions = array;
        //    }
        //}

    }


    /// <summary>
    /// 百度编辑器扩展属性 基类
    /// </summary>
    public abstract class UEditorExtension
    {
        /// <summary>
        /// 扩展类型
        /// </summary>
        public abstract UEditorExtensionType Type { get; }

        /// <summary>
        /// html属性
        /// </summary>
        public abstract Dictionary<string, object> HtmlAttributes { get; }

    }


    /// <summary>
    /// 百度编辑器扩展 表情
    /// </summary>
    public class UEditorExtensionFace : UEditorExtension
    {

        /// <summary>
        /// 扩展类型
        /// </summary>
        public override UEditorExtensionType Type
        {
            get
            {
                return UEditorExtensionType.InsertFace;
            }
        }

        /// <summary>
        /// 表情地址
        /// </summary>
        public string FacePath { get; set; }

        /// <summary>
        /// html属性
        /// </summary>
        public override Dictionary<string, object> HtmlAttributes
        {
            get
            {
                var htmlAttributes = new Dictionary<string, object>();
                htmlAttributes.Add("data-face-path", FacePath);
                return htmlAttributes;
            }
        }

    }

    /// <summary>
    /// 百度编辑器扩展 上传
    /// </summary>
    public class UEditorExtensionUploadFile : UEditorExtension
    {
        /// <summary>
        /// 扩展类型
        /// </summary>
        public override UEditorExtensionType Type
        {
            get
            {
                return UEditorExtensionType.UploadFile;
            }
        }

        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// html属性
        /// </summary>
        public override Dictionary<string, object> HtmlAttributes
        {
            get
            {
                var htmlAttributes = new Dictionary<string, object>();
                htmlAttributes.Add("data-uploader-file-uploadurl", UploadUrl);
                return htmlAttributes;
            }
        }


    }

    /// <summary>
    /// 百度编辑器扩展  上传图片
    /// </summary>
    public class UEditorExtensionUploadImage : UEditorExtension
    {

        /// <summary>
        /// 扩展类型
        /// </summary>
        public override UEditorExtensionType Type
        {
            get
            {
                return UEditorExtensionType.UploadImage;
            }
        }

        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl { get; set; }


        /// <summary>
        /// html属性
        /// </summary>
        public override Dictionary<string, object> HtmlAttributes
        {
            get
            {
                var htmlAttributes = new Dictionary<string, object>();
                htmlAttributes.Add("data-uploader-image-uploadurl", UploadUrl);
                return htmlAttributes;
            }
        }
    }

}