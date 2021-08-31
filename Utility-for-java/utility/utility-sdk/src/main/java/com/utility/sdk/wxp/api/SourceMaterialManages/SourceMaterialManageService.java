//using System;
//using System.Data;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.IO;
//using Sdk.Wxp.Api;
//using Utility.Response;

//namespace Utility.Sdk.Wxp.Api.SourceMaterialManages
//{
//    /// <summary>
//    /// 素材管理 服务
//    /// <![CDATA[
//    /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738726
//    /// ]]>
//    /// </summary>
//    public class SourceMaterialManageService
//    {
//        /// <summary>
//        /// 新增临时素材
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type=TYPE
//        /// ]]>
//        /// </summary>
//        /// <param name="access_token">调用接口凭证</param>
//        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>
//        /// <param name="fileInfo"></param>
//        /// <returns></returns>
//        public static dynamic Upload(string access_token, string type, FileInfo fileInfo) {
//            if (string.IsNullOrEmpty(access_token)|| string.IsNullOrEmpty(type)|| fileInfo==null)
//            {
//                return ResponseApi.Create(Utility.Enums.Language.Chinese, Utility.Enums.Code.ParamNotNull);
//            }
//            else
//            {   /**
//                1、临时素材media_id是可复用的。

//                2、媒体文件在微信后台保存时间为3天，即3天后media_id失效。

//                3、上传临时素材的格式、大小限制与公众平台官网一致。

//                图片（image）: 2M，支持PNG\JPEG\JPG\GIF格式

//                语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式

//                视频（video）：10MB，支持MP4格式

//                缩略图（thumb）：64KB，支持JPG格式
//                 */
//                string name = fileInfo.Name;
//                long length = fileInfo.Length;
//                switch (type.ToLower())
//                {
//                    case "image"://图片
//                        {
//                            if (name.IndexOf(".") < 0)
//                            {
//                                return new { msg = "2M，支持PNG\\JPEG\\JPG\\GIF格式", code = (int)Code.操作失败 };
//                            }
//                            else
//                            {
//                                switch (name.Split('.')[1].ToUpper())
//                                {
//                                    case "JPEG":
//                                    case "JPG":
//                                    case "GIF":
//                                        break;
//                                    default: return new { msg = "2M，支持PNG\\JPEG\\JPG\\GIF格式", code = (int)Code.操作失败 };
//                                }
//                            }
//                            if (length > 1024 * 2)
//                            {
//                                return new { msg = "2M，支持PNG\\JPEG\\JPG\\GIF格式", code = (int)Code.操作失败 };
//                            }
//                        }
//                        break;
//                    case "voice"://语音
//                        {
//                            if (name.IndexOf(".") < 0)
//                            {
//                                return new { msg = "2M，播放长度不超过60s，支持AMR\\MP3格式", code = (int)Code.操作失败 };
//                            }
//                            else
//                            {
//                                switch (name.Split('.')[1].ToUpper())
//                                {
//                                    case "AMR":
//                                    case "MP3":
//                                        break;
//                                    default: return new { msg = "2M，播放长度不超过60s，支持AMR\\MP3格式", code = (int)Code.操作失败 };
//                                }
//                            }
//                            if (length > 1024 * 2)
//                            {
//                                return new { msg = "2M，播放长度不超过60s，支持AMR\\MP3格式", code = (int)Code.操作失败 };
//                            }
//                        }
//                        break;
//                    case "video"://视频
//                        {
//                            if (name.IndexOf(".") < 0)
//                            {
//                                return new { msg = "10MB，支持MP4格式", code = (int)Code.操作失败 };
//                            }
//                            else
//                            {
//                                switch (name.Split('.')[1].ToUpper())
//                                {
//                                    case "MP4":
//                                        break;
//                                    default: return new { msg = "10MB，支持MP4格式", code = (int)Code.操作失败 };
//                                }
//                            }
//                            if (length > 1024 * 10)
//                            {
//                                return new { msg = "10MB，支持MP4格式", code = (int)Code.操作失败 };
//                            }
//                        }
//                        break;
//                    case "thumb"://缩略图
//                        {
//                            if (name.IndexOf(".") < 0)
//                            {
//                                return new { msg = "64KB，支持JPG格式", code = (int)Code.操作失败 };
//                            }
//                            else
//                            {
//                                switch (name.Split('.')[1].ToUpper())
//                                {
//                                    case "JPG":
//                                        break;
//                                    default: return new { msg = "64KB，支持JPG格式", code = (int)Code.操作失败 };
//                                }
//                            }
//                            if (length > 64)
//                            {
//                                return new { msg = "64KB，支持JPG格式", code = (int)Code.操作失败 };
//                            }
//                        }
//                        break;
//                    default: return new { msg="媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）", code = (int)Code.操作失败 };
//                }
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(SourceMaterialManageApi.Upload(access_token, type,fileInfo));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        string type1 = apiResult.Get<string>("type"),//媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）
//                            media_id = apiResult.Get<string>("media_id"), //媒体文件上传后，获取标识
//                            created_at = apiResult.Get<string>("created_at");//媒体文件上传时间戳
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string id = media_id.Sha1(), sql = string.Empty;
//                        if (((int)serverFactory.QueryAgreeage("select count(1) from source_material_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) == 0)
//                        {
//                            sql = "insert into source_material_info(id,type,media_id,created_at,create_date) values(@id,@type,@media_id,@created_at,@create_date);";
//                        }
//                        else
//                        {
//                            sql = "update   source_material_info set type=@type,media_id=@media_id,created_at=@created_at,last_date=@create_date where id=@id;";
//                        }
//                        serverFactory.Operate(sql, new SqlParameter[] {
//                            new SqlParameter("@id", id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@type", type1) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@media_id", media_id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@created_at", Convert.ToInt64(created_at)) { SqlDbType = SqlDbType.BigInt },
//                            new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功, type=type1, media_id , created_at };
//                    }
//                    else
//                    {
//                        return new { msg= apiResult.Errmsg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        #region 获取临时素材

//        /// <summary>
//        /// 获取临时素材
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
//        /// ]]>
//        /// </summary>
//        /// <param name="access_token">调用接口凭证</param>
//        /// <param name="media_id">媒体文件ID</param>
//        /// <returns></returns>
//        public static dynamic Get(string access_token, string media_id)
//        {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(SourceMaterialManageApi.Get(access_token, media_id));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        /**
//                        如果返回的是视频消息素材，则内容如下：
//                        {
//                            "video_url":DOWN_URL
//                        }
//                        */
//                        string url = string.Empty, key = string.Empty ;
//                        foreach (var item in apiResult.Result)
//                        {
//                            key = item.Key;
//                            url = item.Value.ToString();
//                        }
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string id = media_id.Sha1(), sql = string.Empty;
//                        if (((int)serverFactory.QueryAgreeage("select count(1) from source_material_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) == 0)
//                        {
//                            sql = "insert into source_material_info(id,url,type,media_id,create_date) values(@id,@url,@type,@media_id,@create_date);";
//                        }
//                        else
//                        {
//                            sql = "update   source_material_info set url=@url,type=@type,media_id=@media_id,last_date=@create_date where id=@id;";
//                        }
//                        serverFactory.Operate(sql, new SqlParameter[] {
//                            new SqlParameter("@id", id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@type", key.Split('_')[0]) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@media_id", media_id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@url",url) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功, url };
//                    }
//                    else
//                    {
//                        return new { msg = apiResult.Errmsg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 高清语音素材获取接口
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/media/get/jssdk?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
//        /// ]]>
//        /// </summary>
//        /// <param name="access_token">调用接口凭证</param>
//        /// <param name="media_id">媒体文件ID，即uploadVoice接口返回的serverID</param>
//        /// <returns></returns>
//        public static dynamic GetJssdk(string access_token, string media_id)
//        {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(SourceMaterialManageApi.GetJssdk(access_token, media_id));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        /**
//                        如果返回的是视频消息素材，则内容如下：
//                        {
//                            "video_url":DOWN_URL
//                        }
//                        */
//                        string url = string.Empty, key = string.Empty;
//                        foreach (var item in apiResult.Result)
//                        {
//                            key = item.Key;
//                            url = item.Value.ToString();
//                        }
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string id = media_id.Sha1(), sql = string.Empty;
//                        if (((int)serverFactory.QueryAgreeage("select count(1) from source_material_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) == 0)
//                        {
//                            sql = "insert into source_material_info(id,url,type,media_id,create_date) values(@id,@url,@type,@media_id,@create_date);";
//                        }
//                        else
//                        {
//                            sql = "update   source_material_info set url=@url,type=@type,media_id=@media_id,last_date=@create_date where id=@id;";
//                        }
//                        serverFactory.Operate(sql, new SqlParameter[] {
//                            new SqlParameter("@id", id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@type", key.Split('_')[0]) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@media_id", media_id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@url",url) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功, url };
//                    }
//                    else
//                    {
//                        return new { msg = apiResult.Errmsg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        #endregion 获取临时素材

//        #region 新增永久素材

//        /// <summary>
//        /// 新增永久素材
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/material/add_news?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="access_token">调用接口凭证</param>
//        /// <param name="articles"></param>
//        /// <returns></returns>
//        public static dynamic AddNews(string access_token, Dictionary<string,object> articles) {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (articles==null||articles.Count==0)
//            {
//                return new { msg = $"{nameof(articles)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                /**
//                 title	是	标题
//                thumb_media_id	是	图文消息的封面图片素材id（必须是永久mediaID）
//                author	否	作者
//                digest	否	图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空。如果本字段为没有填写，则默认抓取正文前64个字。
//                show_cover_pic	是	是否显示封面，0为false，即不显示，1为true，即显示
//                content	是	图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS,涉及图片url必须来源 "上传图文消息内的图片获取URL"接口获取。外部图片url将被过滤。
//                content_source_url	是	图文消息的原文地址，即点击“阅读原文”后的URL
//                need_open_comment	否	Uint32 是否打开评论，0不打开，1打开
//                only_fans_can_comment	否	Uint32 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
//                 */

//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(SourceMaterialManageApi.AddNews(access_token, articles.ToJson()));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        string media_id = apiResult.Get<string>("media_id");
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string id = media_id.Sha1(), sql = string.Empty;
//                        if (((int)serverFactory.QueryAgreeage("select count(1) from source_material_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) == 0)
//                        {
//                            sql = "insert into source_material_info(id,media_id,create_date) values(@id,@media_id,@create_date);";
//                        }
//                        else
//                        {
//                            sql = "update   source_material_info set media_id=@media_id,last_date=@create_date where id=@id;";
//                        }
//                        serverFactory.Operate(sql, new SqlParameter[] {
//                            new SqlParameter("@id", id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@media_id", media_id) { SqlDbType = SqlDbType.VarChar },
//                            new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功, media_id };
//                    }
//                    else
//                    {
//                        return new { msg = apiResult.Errmsg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        #endregion 新增永久素材
//    }
//}
