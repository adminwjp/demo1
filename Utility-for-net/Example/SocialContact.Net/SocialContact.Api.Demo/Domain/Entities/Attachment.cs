//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using System;
using System.IO;
using System.Linq;

namespace Tunynet.Common
{
    /// <summary>
    /// 附件实体
    /// </summary>
    [Serializable]
    public class Attachment 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Attachment()
        {
            New();
        }

        /// <summary>
        /// 初始化属性默认值
        /// </summary>
        private void New()
        {
            this.UserDisplayName = string.Empty;
            this.DateCreated = DateTime.Now;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="contentType">指定contentType，会优先采用此contentType</param>
        public Attachment(IFormFile postedFile, string contentType = null)
        {
            New();//初始化属性默认值
            this.FileLength = postedFile.Length;

            if (!string.IsNullOrEmpty(contentType))
                this.ContentType = contentType;
            else if (!string.IsNullOrEmpty(postedFile.ContentType))
                this.ContentType = postedFile.ContentType;
            else
                this.ContentType = string.Empty;

            if (!string.IsNullOrEmpty(this.ContentType))
            {
                this.ContentType = this.ContentType.Replace("pjpeg", "jpeg");
                this.MediaType = GetMediaType(this.ContentType);
            }
            else
            {
                this.ContentType = "unknown/unknown";
                this.MediaType = MediaType.Other;
            }

            if (Path.GetExtension(postedFile.FileName) == "")
            {
                switch (this.ContentType)
                {
                    case "image/jpeg":
                        this.FileName = postedFile.FileName + ".jpg";
                        break;

                    case "image/gif":
                        this.FileName = postedFile.FileName + ".gif";
                        break;

                    case "image/png":
                        this.FileName = postedFile.FileName + ".png";
                        break;

                    default:

                        break;
                }
            }
            else
            {
                this.FileName = postedFile.FileName;
            }

            this.FriendlyFileName = this.FileName.Substring(this.FileName.LastIndexOf("\\") + 1);

            //自动生成用于存储的文件名称
            this.FileName = GenerateFileName(Path.GetExtension(this.FriendlyFileName));

            //CheckImageInfo(postedFile.InputStream);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="friendlyFileName"></param>
        public Attachment(Stream stream, string contentType, string friendlyFileName)
        {
            New();//初始化属性默认值
            this.FileLength = stream.Length;
            this.ContentType = contentType;
            this.MediaType = GetMediaType(this.ContentType);
            this.FriendlyFileName = friendlyFileName;
            this.FileName = GenerateFileName(Path.GetExtension(this.FriendlyFileName));
            this.DateCreated = DateTime.Now;
            //CheckImageInfo(stream);
        }

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long AttachmentId { get; set; }

        /// <summary>
        ///附件关联Id（例如：博文Id、贴子Id）
        /// </summary>
        public long AssociateId { get; set; }

        /// <summary>
        ///拥有者Id
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        ///租户类型Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        ///附件上传人UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///附件上传人名称
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        ///实际存储文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///文件显示名称
        /// </summary>
        public string FriendlyFileName { get; set; }

        /// <summary>
        ///附件MIME类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///文件大小
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        ///售价（积分）
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        ///10=转换失败，0=待转化，1=转化中，2=已转换
        /// </summary>
        public ConvertStatus ConvertStatus { get; set; }

        /// <summary>
        ///附件描述
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        ///是否在文章中的附件列表显示
        /// </summary>
        public bool IsShowInAttachmentList { get; set; }

        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 附件类型（<seealso cref="Tunynet.Common.MediaType"/>）
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        ///排序
        /// </summary>
        public long DisplayOrder { get; set; }

        #endregion 需持久化属性

    }
}