//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------



using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Tunynet.Common
{
    /// <summary>
    /// 封装对HtmlHelper附件文件类型图标的扩展方法
    /// </summary>
    public static class HtmlHelperFileTypeIconExtensions
    {
        /// <summary>
        /// 输出文件类型图标
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentType">文件contentType类型</param>
        /// <param name="classString">自定义样式，不需要请传空</param>
        /// <param name="iconSize">图标大小</param>
        /// <param name="classStyle">图标颜色</param>
        /// <returns>图标标签</returns>
        public static HtmlString FileTypeIcon(this HtmlHelper htmlHelper, string contentType, string classString, IconSize iconSize = IconSize.Small, ClassStyle classStyle = ClassStyle.Default)
        {
            var sizeClass = string.Empty;
            switch (iconSize)
            {
                case IconSize.Medium:
                    sizeClass = "fa-lg";
                    break;

                case IconSize.Big:
                    sizeClass = "fa-2x";
                    break;
            }

            var extensionType = MimeTypeConfiguration.GetExtension(contentType);
            string fileClassName = "fa-file";
            var colorStyle = string.Empty;
            string title = "其他类型";
            switch (extensionType)
            {
                case "wps":
                case "doc":
                case "docx":
                    fileClassName = $"{fileClassName}-word";
                    colorStyle = "tn-blue-color";
                    title = "word文档";
                    break;

                case "pps":
                case "pptx":
                case "ppt":
                    fileClassName = $"{fileClassName}-powerpoint";
                    colorStyle = "tn-orange-color";
                    title = "ppt文档";
                    break;

                case "xls":
                case "xlsx":
                    fileClassName = $"{fileClassName}-excel";
                    colorStyle = "tn-green-color";
                    title = "excel文档";
                    break;

                case "pdf":
                    fileClassName = $"{fileClassName}-pdf";
                    colorStyle = "tn-red-color";
                    title = "pdf文档";
                    break;
                case "rtf":
                case "txt":
                    fileClassName = $"{fileClassName}-text";
                    colorStyle = "tn-blue-color";
                    title = "txt文档";
                    break;

                case "jpg":
                case "png":
                case "bmp":
                case "gif":
                    fileClassName = $"{fileClassName}-image";
                    colorStyle = "tn-red-color";
                    title = "图片";
                    break;

                case "flv":
                case "rmvb":
                case "mp4":
                case "3gp":
                case "mpeg":
                case "wmv":
                case "mov":
                case "avi":
                case "asf":
                    fileClassName = $"{fileClassName}-video";
                    colorStyle = "tn-orange-color";
                    title = "视频";
                    break;

                case "zip":
                case "rar":
                    fileClassName = $"{fileClassName}-zip";
                    colorStyle = "tn-red-color";
                    title = "压缩包";
                    break;

                case "mp3":
                case "wav":
                case "rm":
                    fileClassName = $"{fileClassName}-audio";
                    colorStyle = "tn-dark-blue-color";
                    title = "音频";
                    break;

                case "xml":
                    fileClassName = $"{fileClassName}-code";
                    colorStyle = "tn-green-color";
                    title = "编码";
                    break;

                default:
                    fileClassName = $"fa-file";
                    title = "其他附件";
                    break;
            }
            switch (classStyle)
            {
                case ClassStyle.Blue:
                    colorStyle = "tn-blue-color";
                    break;

                case ClassStyle.Green:
                    colorStyle = "tn-green-color";
                    break;

                case ClassStyle.Yellow:
                    colorStyle = "tn-yellow-color";
                    break;

                case ClassStyle.Red:
                    colorStyle = "tn-red-color";
                    break;

                case ClassStyle.White:
                    colorStyle = "";
                    break;
            }
            fileClassName = $"{fileClassName}-o";
            //生成标签
            TagBuilder containerBuilder = new TagBuilder("span");
            containerBuilder.MergeAttribute("class", $" fa {fileClassName} {sizeClass} {colorStyle} {classString}");
            containerBuilder.MergeAttribute("title", title);
            return new HtmlString(containerBuilder.ToString());
        }
    }

    /// <summary>
    /// 图标基本颜色分类
    /// </summary>
    public enum ClassStyle
    {
        Default,
        White,
        Blue,
        Green,
        Yellow,
        Red
    }
}