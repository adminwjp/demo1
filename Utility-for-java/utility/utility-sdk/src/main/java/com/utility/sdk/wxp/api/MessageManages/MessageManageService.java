//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Utility.Sdk.Wxp.Api.MessageManages
//{
//    using System;
//    using System.Data;
//    using System.Collections.Generic;
//    using System.Collections.Concurrent;
//    using Utility;
//    using Wechat.Sdk;
//    using System.Data.SqlClient;
//    using System.IO;
//    /// <summary>
//    /// 消息管理 服务
//    /// </summary>
//    public class MessageManageService
//    {
//        #region 客服消息 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547

//        /// <summary>
//        /// 添加客服帐号
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/customservice/kfaccount/add?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
//        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
//        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic AddKfAccount(string kf_account, string nickname, string password, string access_token)
//        {
//            if (string.IsNullOrEmpty(kf_account))
//            {
//                return new { msg = $"{nameof(kf_account)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(nickname))
//            {
//                return new { msg = $"{nameof(nickname)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(password))
//            {
//                return new { msg = $"{nameof(password)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.AddKfAccount(kf_account,nickname,password,access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string access_id = AccessTokenService.GetAccessId(access_token);
//                        using (SqlConnection connection = serverFactory.Connection())
//                        {
//                            string id = kf_account.Sha1();
//                            if (((int)serverFactory.QueryAgreeage(connection, "select count(1) from custom_service_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) == 0)
//                            {
//                                serverFactory.Operate(connection,$"insert into custom_service_info(id,kf_account,nickname,password,{(access_id.Equals(string.Empty) ? "access_token" : "access_id")},create_date) values(@id,@kf_account,@nickname,@password,@access_token,@create_date);"
//                                                          , new SqlParameter[] {
//                                    new SqlParameter("@id",id) { SqlDbType = SqlDbType.VarChar },
//                                    new SqlParameter("@kf_account", kf_account) { SqlDbType = SqlDbType.VarChar },
//                                    new SqlParameter("@nickname", nickname) { SqlDbType = SqlDbType.VarChar },
//                                    new SqlParameter("@password", password) { SqlDbType = SqlDbType.VarChar },
//                                    new SqlParameter("@access_token", access_id.Equals(string.Empty) ? access_token : access_id) { SqlDbType = SqlDbType.VarChar },
//                                    new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                                  });
//                            }
//                        };
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 修改客服帐号
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/customservice/kfaccount/update?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
//        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
//        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic UpdateKfAccount(string kf_account, string nickname, string password, string access_token)
//        {
//            if (string.IsNullOrEmpty(kf_account))
//            {
//                return new { msg = $"{nameof(kf_account)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(nickname))
//            {
//                return new { msg = $"{nameof(nickname)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(password))
//            {
//                return new { msg = $"{nameof(password)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.UpdateKfAccount(kf_account, nickname, password, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string access_id = AccessTokenService.GetAccessId(access_token);
//                        using (SqlConnection connection = serverFactory.Connection())
//                        {
//                            string id = kf_account.Sha1();
//                            if (((int)serverFactory.QueryAgreeage(connection, "select count(1) from custom_service_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) > 0)
//                            {
//                                serverFactory.Operate(connection, $"update  custom_service_info set kf_account=@kf_account,nickname=@nickname,password=@password,{(access_id.Equals(string.Empty) ? "access_token" : "access_id")}=@access_token,last_date=@create_date where id=@id;"
//                                   , new SqlParameter[] {
//                                        new SqlParameter("@id", id) { SqlDbType = SqlDbType.VarChar },
//                                        new SqlParameter("@kf_account", kf_account) { SqlDbType = SqlDbType.VarChar },
//                                        new SqlParameter("@nickname", nickname) { SqlDbType = SqlDbType.VarChar },
//                                        new SqlParameter("@password", password) { SqlDbType = SqlDbType.VarChar },
//                                        new SqlParameter("@access_token", access_id.Equals(string.Empty) ? access_token : access_id) { SqlDbType = SqlDbType.VarChar },
//                                        new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                               });
//                            }
//                        };

//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 删除客服帐号
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/customservice/kfaccount/del?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
//        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
//        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic DelKfAccount(string kf_account, string nickname, string password, string access_token)
//        {
//            if (string.IsNullOrEmpty(kf_account))
//            {
//                return new { msg = $"{nameof(kf_account)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(nickname))
//            {
//                return new { msg = $"{nameof(nickname)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(password))
//            {
//                return new { msg = $"{nameof(password)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.DelKfAccount(kf_account, nickname, password, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string access_id = AccessTokenService.GetAccessId(access_token);
//                        serverFactory.Operate($"delete from  custom_service_info  where id=@id;"
//                            , new SqlParameter[] {
//                                new SqlParameter("@id", kf_account.Sha1()) { SqlDbType = SqlDbType.VarChar }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 删除客服帐号
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/customservice/kfaccount/del?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic DelCustomService(string access_token)
//        {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.DelCustomService(access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string access_id = AccessTokenService.GetAccessId(access_token);
//                        serverFactory.Operate($"delete from  custom_service_info  where {(access_id.Equals(string.Empty) ? "access_token" : "access_id")}=@access_token;"
//                            , new SqlParameter[] {
//                                new SqlParameter("@access_token",access_id.Equals(string.Empty)?access_token:access_id) { SqlDbType = SqlDbType.VarChar }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 设置客服帐号的头像
//        /// <![CDATA[
//        /// http://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token=ACCESS_TOKEN&kf_account=KFACCOUNT
//        /// ]]>
//        /// </summary>
//        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
//        /// <param name="headImg">客服人员的头像，头像图片文件必须是jpg格式，推荐使用640*640大小的图片以达到最佳效果</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic UploadKfAccountHeadImg(string kf_account, FileInfo headImg, string access_token)
//        {
//            if (string.IsNullOrEmpty(kf_account))
//            {
//                return new { msg = $"{nameof(kf_account)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (headImg==null)
//            {
//                return new { msg = $"{nameof(headImg)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int) Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.UploadKfAccountHeadImg(kf_account,headImg,access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        string access_id = AccessTokenService.GetAccessId(access_token);
//                        serverFactory.Operate($"update  custom_service_info set headImg=@headImg,{(access_id.Equals(string.Empty) ? "access_token" : "access_id")}=@access_token,last_date=@create_date where id=@id;"
//                            , new SqlParameter[] {
//                                new SqlParameter("@id", kf_account.Sha1()) { SqlDbType = SqlDbType.VarChar },
//                                new SqlParameter("@headImg", headImg) { SqlDbType = SqlDbType.Image },
//                                new SqlParameter("@access_token", access_id.Equals(string.Empty) ? access_token : access_id) { SqlDbType = SqlDbType.VarChar },
//                                new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                        });
//                        return new { msg = Code.操作成功.ToString(), code = (int) Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty? apiResult.Errmsg : msg;
//                        return new { msg, code = (int) Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 获取所有客服账号
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic GetKfList(string access_token)
//        {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.GetKfList(access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        //SqlServerFactory serverFactory = AutofacHelper.Resolve<SqlServerFactory>();
//                        //string access_id = AccessTokenService.GetAccessId(access_token);
//                        //using (SqlConnection connection = serverFactory.Connection())
//                        //{
//                        //    string id = kf_account.Sha1();
//                        //    if (((int)serverFactory.QueryAgreeage(connection, "select count(1) from custom_service_info where id=@id;", new SqlParameter[] { new SqlParameter("@id", id) })) == 0)
//                        //    {
//                        //        serverFactory.Operate(connection, $"insert into custom_service_info(id,kf_account,nickname,password,{(access_id.Equals(string.Empty) ? "access_token" : "access_id")},create_date) values(@id,@kf_account,@nickname,@password,@access_token,@create_date);"
//                        //                                  , new SqlParameter[] {
//                        //            new SqlParameter("@id",id) { SqlDbType = SqlDbType.VarChar },
//                        //            new SqlParameter("@kf_account", kf_account) { SqlDbType = SqlDbType.VarChar },
//                        //            new SqlParameter("@nickname", nickname) { SqlDbType = SqlDbType.VarChar },
//                        //            new SqlParameter("@password", password) { SqlDbType = SqlDbType.VarChar },
//                        //            new SqlParameter("@access_token", access_id.Equals(string.Empty) ? access_token : access_id) { SqlDbType = SqlDbType.VarChar },
//                        //            new SqlParameter("@create_date", DateTime.Now) { SqlDbType = SqlDbType.Date }
//                        //          });
//                        //    }
//                        //};
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功, data = apiResult.Result };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        #region 客服接口-发消息 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547

//        /// <summary>
//        /// 发送文本消息
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="content">文本消息内容</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        /*****
//            * 
//            * /// <!--
//        /// {\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"HelloWorld\"}}
//        /// -->
//        /// <para><xml></para>
//        /// <para><ToUserName><![CDATA[toUser]]></ToUserName></para>
//        /// <para><FromUserName><![CDATA[fromUser]]></FromUserName></para>
//        /// <para> <CreateTime>1348831860</CreateTime></para>
//        /// <para><MsgType><![CDATA[text]]></MsgType></para>
//        /// <para><Content><![CDATA[this is a test]]></Content></para>
//        /// <para><MsgId>1234567890123456</MsgId></para>
//        /// <para> </xml></para>
//            * ******/
//        public static dynamic SendMsg(string touser, string content, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(content))
//            {
//                return new { msg = $"{nameof(content)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendMsg(touser,content,access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功};
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送图片消息 
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendImage(string touser, string media_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendImage(touser,media_id,access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送语音消息 
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendVoice(string touser, string media_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendVoice(touser, media_id, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送视频消息 
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
//        /// <param name="thumb_media_id">缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
//        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
//        /// <param name="description">图文消息/视频消息/音乐消息的描述</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendVideo(string touser, string media_id, string thumb_media_id, string title, string description, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(thumb_media_id))
//            {
//                return new { msg = $"{nameof(thumb_media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(title))
//            {
//                return new { msg = $"{nameof(title)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(description))
//            {
//                return new { msg = $"{nameof(description)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendVideo(touser, media_id,thumb_media_id,title,description, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送音乐消息
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
//        /// <param name="description">图文消息/视频消息/音乐消息的描述</param>
//        /// <param name="musicurl">音乐链接</param>
//        /// <param name="hqmusicurl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
//        /// <param name="thumb_media_id">缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendMusic(string touser, string title, string description, string musicurl, string hqmusicurl, string thumb_media_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(title))
//            {
//                return new { msg = $"{nameof(title)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(description))
//            {
//                return new { msg = $"{nameof(description)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(musicurl))
//            {
//                return new { msg = $"{nameof(musicurl)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(hqmusicurl))
//            {
//                return new { msg = $"{nameof(hqmusicurl)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(thumb_media_id))
//            {
//                return new { msg = $"{nameof(thumb_media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendMusic(touser, title, description , musicurl, hqmusicurl,thumb_media_id, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送图文消息（点击跳转到外链） 图文消息条数限制在1条以内，注意，如果图文数超过1，则将会返回错误码45008。
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
//        /// <param name="description">	图文消息/视频消息/音乐消息的描述</param>
//        /// <param name="url">图文消息被点击后跳转的链接</param>
//        /// <param name="picurl">图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendNews(string touser, string title, string description, string url, string picurl, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(title))
//            {
//                return new { msg = $"{nameof(title)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(description))
//            {
//                return new { msg = $"{nameof(description)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(url))
//            {
//                return new { msg = $"{nameof(url)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(picurl))
//            {
//                return new { msg = $"{nameof(picurl)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendNews(touser, title, description, url, picurl, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送图文消息（点击跳转到图文消息页面） 图文消息条数限制在1条以内，注意，如果图文数超过1，则将会返回错误码45008。
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendMpnews(string touser, string media_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendMpnews(touser, media_id, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送卡券
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="card_id">card_id</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendWxcard(string touser, string card_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(card_id))
//            {
//                return new { msg = $"{nameof(card_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendWxcard(touser, card_id, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送小程序卡片（要求小程序与公众号已关联）
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
//        /// <param name="appid">小程序的appid，要求小程序的appid需要与公众号有关联关系</param>
//        /// <param name="pagepath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
//        /// <param name="thumb_media_id">缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendMiniprogrampage(string touser, string title, string appid, string pagepath, string thumb_media_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(title))
//            {
//                return new { msg = $"{nameof(title)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(appid))
//            {
//                return new { msg = $"{nameof(appid)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(pagepath))
//            {
//                return new { msg = $"{nameof(pagepath)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(thumb_media_id))
//            {
//                return new { msg = $"{nameof(thumb_media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendMiniprogrampage(touser, title,appid,pagepath,thumb_media_id, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 发送文本消息 请注意，如果需要以某个客服帐号来发消息（在微信6.0.2及以上版本中显示自定义头像），则需在JSON数据包的后半部分加入customservice参数，例如发送文本消息则改为：
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="content">文本消息内容</param>
//        /// <param name="kf_account">客服帐号</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendMsgtype(string touser, string content, string kf_account, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(content))
//            {
//                return new { msg = $"{nameof(content)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(kf_account))
//            {
//                return new { msg = $"{nameof(kf_account)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendMsgtype(touser, content, kf_account, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }


//        /// <summary>
//        /// 客服输入状态
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/custom/typing?access_token=ACCESS_TOKEN
//        /// ]]> 
//        /// </summary>
//        /// <param name="touser">普通用户openid</param>
//        /// <param name="command">"Typing"：对用户下发“正在输入"状态 "CancelTyping"：取消对用户的”正在输入"状态</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic CustomTyping(string touser, string command, string access_token)
//        {
//            if (string.IsNullOrEmpty(touser))
//            {
//                return new { msg = $"{nameof(touser)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(command))
//            {
//                return new { msg = $"{nameof(command)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                switch (command.ToLower())
//                {
//                    case "typing":
//                    case "canceltyping":
//                        break;
//                    default: return new { msg = "param command not support ,\"Typing\"：对用户下发“正在输入\"状态 \"CancelTyping\"：取消对用户的”正在输入\"状态", code = (int)Code.不支持的错误 }; ;
//                }
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.CustomTyping(touser, command, access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        #endregion 客服接口-发消息


//        #endregion 客服消息


//        #region 群发接口和原创校验 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1481187827_i0l21


//        /// <summary>
//        /// 上传图文消息内的图片获取URL【订阅号与服务号认证后均可用】
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic Uploadimg(string access_token)
//        {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.Uploadimg(access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        /// <summary>
//        /// 上传图文消息素材【订阅号与服务号认证后均可用】
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="param"></param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic Uploadnews(IDictionary<string, object> param, string access_token)
//        {
//            //图文消息，一个图文消息支持1到8条图文
//            if (param.ContainsKey("articles"))
//            {
//                List<Dictionary<string, object>> articles = param["articles"] as List<Dictionary<string, object>>;
//                if (articles == null || articles.Count == 0)
//                {
//                    return new { msg = "图文消息，一个图文消息支持1到8条图文", code = (int)Code.参数不能为空 };
//                }
//                else
//                {
//                    //Func<Dictionary<string, object>, string,dynamic> validata = (it, key) =>
//                    //{
//                    //    if (!it.ContainsKey(key) || string.IsNullOrEmpty(it[key].ToString()))
//                    //    {
//                    //        return new { msg = $"{key}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                    //    }
//                    //    return null;
//                    //};
//                    //dynamic result = null;
//                    foreach (Dictionary<string, object> item in articles)
//                    {
//                        //图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
//                        if (!item.ContainsKey("thumb_media_id") || string.IsNullOrEmpty(item["thumb_media_id"].ToString()))
//                        {
//                            return new { msg = $"thumb_media_id{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //图文消息的作者
//                        else if (!item.ContainsKey("author") || string.IsNullOrEmpty(item["author"].ToString()))
//                        {
//                            return new { msg = $"author{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //图文消息的标题
//                        else if (!item.ContainsKey("title") || string.IsNullOrEmpty(item["title"].ToString()))
//                        {
//                            return new { msg = $"title{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //在图文消息页面点击“阅读原文”后的页面，受安全限制，如需跳转Appstore，可以使用itun.es或appsto.re的短链服务，并在短链后增加 #wechat_redirect 后缀。
//                        else if (!item.ContainsKey("content_source_url") || string.IsNullOrEmpty(item["content_source_url"].ToString()))
//                        {
//                            return new { msg = $"content_source_url{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //图文消息页面的内容，支持HTML标签。具备微信支付权限的公众号，可以使用a标签，其他公众号不能使用，如需插入小程序卡片，可参考下文。
//                        else if (!item.ContainsKey("content") || string.IsNullOrEmpty(item["content"].ToString()))
//                        {
//                            return new { msg = $"content{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //图文消息的描述，如本字段为空，则默认抓取正文前64个字
//                        else if (!item.ContainsKey("digest") || string.IsNullOrEmpty(item["digest"].ToString()))
//                        { 
//                            return new { msg = $"digest{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //是否显示封面，1为显示，0为不显示
//                        else if (!item.ContainsKey("show_cover_pic") || string.IsNullOrEmpty(item["show_cover_pic"].ToString()))
//                        {
//                            return new { msg = $"show_cover_pic{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        //Uint32 是否打开评论，0不打开，1打开
//                        else if (!item.ContainsKey("need_open_comment") || string.IsNullOrEmpty(item["need_open_comment"].ToString()))
//                        {
//                            return new { msg = $"need_open_comment{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//                        }
//                        ////Uint32 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
//                        //if (result = validata(item, "only_fans_can_comment") != null)
//                        //{
//                        //    return result;
//                        //}
//                        #region =======================
//                        //if (result=validata(item, "thumb_media_id") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////图文消息的作者
//                        //if (result = validata(item, "author") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////图文消息的标题
//                        //if (result = validata(item, "title") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////在图文消息页面点击“阅读原文”后的页面，受安全限制，如需跳转Appstore，可以使用itun.es或appsto.re的短链服务，并在短链后增加 #wechat_redirect 后缀。
//                        //if (result = validata(item, "content_source_url") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////图文消息页面的内容，支持HTML标签。具备微信支付权限的公众号，可以使用a标签，其他公众号不能使用，如需插入小程序卡片，可参考下文。
//                        //if (result = validata(item, "content") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////图文消息的描述，如本字段为空，则默认抓取正文前64个字
//                        //if (result = validata(item, "digest") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////是否显示封面，1为显示，0为不显示
//                        //if (validata(item, "show_cover_pic") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////Uint32 是否打开评论，0不打开，1打开
//                        //if (result = validata(item, "need_open_comment") != null)
//                        //{
//                        //    return result;
//                        //}
//                        ////Uint32 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
//                        //if (result = validata(item, "only_fans_can_comment") != null)
//                        //{
//                        //    return result;
//                        //}
//                        #endregion =======================
//                    }
//                    com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.Uploadnews(param.ToJson(),access_token));
//                    if (apiResult.IsSuccess)
//                    {
//                        if (apiResult.Success)
//                        {
//                            return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                        }
//                        else
//                        {
//                            AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                            string msg = AccessTokenApi.Msg;
//                            msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                            return new { msg, code = (int)Code.操作失败 };
//                        }
//                    }
//                    else
//                    {
//                        return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                    }
//                }
//            }
//            else
//            {
//                return new { msg = $"articles{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//        }

//        /// <summary>
//        /// 根据标签进行群发【订阅号与服务号认证后均可用】 图文消息（注意图文消息的media_id需要通过上述方法来得到）
//        /// <![CDATA[
//        /// https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=ACCESS_TOKEN
//        /// ]]>
//        /// </summary>
//        /// <param name="media_id"></param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns>
//        public static dynamic SendAllMpnews(string media_id, string access_token)
//        {
//            if (string.IsNullOrEmpty(media_id))
//            {
//                return new { msg = $"{nameof(media_id)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else if (string.IsNullOrEmpty(access_token))
//            {
//                return new { msg = $"{nameof(access_token)}{Code.参数不能为空.ToString()}", code = (int)Code.参数不能为空 };
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = new com.utility.sdk.wxp.api.ApiResult(MessageManageApi.SendAllMpnews(media_id,access_token));
//                if (apiResult.IsSuccess)
//                {
//                    if (apiResult.Success)
//                    {
//                        return new { msg = Code.操作成功.ToString(), code = (int)Code.操作成功 };
//                    }
//                    else
//                    {
//                        AccessTokenApi.ErrorCode = apiResult.Errcode.Value;
//                        string msg = AccessTokenApi.Msg;
//                        msg = msg == string.Empty ? apiResult.Errmsg : msg;
//                        return new { msg, code = (int)Code.操作失败 };
//                    }
//                }
//                else
//                {
//                    return new { msg = Code.系统内部错误.ToString(), code = (int)Code.系统内部错误 };
//                }
//            }
//        }

//        #endregion 群发接口和原创校验

//    }
//}
