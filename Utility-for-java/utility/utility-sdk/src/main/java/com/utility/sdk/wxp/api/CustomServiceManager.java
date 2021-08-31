//using Gibson.Nhibernate;
//using Gibson.Utility;
//using Gibson.Wechat.Model;
//using Gibson.Wechat.SDK;
//using NHibernate;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Gibson.Wechat.BLL
//{
//    /// <summary>
//    /// 客服消息 管理类
//    /// </summary>
//    public class CustomServiceManager
//    {
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
//        public static Result Add(string kf_account, string nickname, string password, string access_token)
//        {
//            var result = ValidataCustomService(kf_account, nickname, password, access_token);
//            if (result != null)
//            {
//                return result;
//            }
//            var obj = NhibernateTemplate.Default.Get<CustomServiceInfo>(kf_account.Sha1());
//            if (obj != null)
//            {
//                return Result.Fail.SetMessage("客服帐号已存在，请重新添加");
//            }
//            else
//            {
//                com.utility.sdk.wxp.api.ApiResult apiResult = MessageManageApi.AddKfAccount(kf_account, nickname, password, access_token);
//                if (apiResult.IsSucceed)
//                {
//                    NhibernateTemplate.Default.Add(new CustomServiceInfo() { id = kf_account.Sha1(), kf_account = kf_account, nickname = nickname, password = password });
//                    return Result.Successed;
//                }
//                else
//                {
//                    if (apiResult.Exception != null)
//                    {
//                        Mong.MongHelper.Insert<dynamic>(new { channel = "MessageManageApi", desc = "添加客服帐号", flag = -1, error = apiResult.Exception.Message, date = DateHelper.GetMillSeconds(DateTime.Now) }, "api");
//                        return Result.Errror.SetErrorMsg(apiResult.Exception.Message);
//                    }
//                    else
//                    {
//                        Mong.MongHelper.Insert<dynamic>(new { channel = "MessageManageApi", desc = "添加客服帐号", flag = 0, msg = apiResult.Json, date = DateHelper.GetMillSeconds(DateTime.Now) }, "api");
//                        return Result.Fail.SetMessage(apiResult.errmsg);
//                    }
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
//        public static Result UpdateKfAccount(string kf_account, string nickname, string password, string access_token)
//        {
//            var result = ValidataCustomService(kf_account, nickname, password, access_token);
//            if (result != null)
//            {
//                return result;
//            }
//            else
//            {
//                var obj = NhibernateTemplate.Default.Get<CustomServiceInfo>(kf_account.Sha1());
//                if (obj == null)
//                {
//                    return Result.Fail.SetMessage("客服帐号不存在");
//                }
//                else
//                {
//                    com.utility.sdk.wxp.api.ApiResult apiResult = MessageManageApi.UpdateKfAccount(kf_account, nickname, password, access_token);
//                    if (apiResult.IsSucceed)
//                    {
//                        using (IStatelessSession session = NihibernateHelper.Session)
//                        {
//                            session.CreateSQLQuery("update custom_service_info set kf_account=?,nickname=?, password=?, where id=?")
//                                .SetString(0, kf_account).SetString(1, nickname).SetString(2, password).SetString(3, kf_account.Sha1())
//                                .ExecuteUpdateAsync();
//                            return Result.Successed;
//                        }
//                    }
//                    else
//                    {
//                        return Result.Fail;
//                    }
//                }
//            }
//        }
//        /// <summary> 验证客户客服帐号信息 </summary>
//        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
//        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
//        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
//        /// <param name="access_token">access_token</param>
//        /// <returns></returns> 
//        private static Result ValidataCustomService(string kf_account, string nickname, string password, string access_token)
//        {
//            if (string.IsNullOrEmpty(access_token))
//            {
//                return Result.Create().SetCode((int)Code.参数不能为空).SetMessage($"access_token{Code.参数不能为空.ToString()}");
//            }
//            else if (string.IsNullOrEmpty(kf_account))
//            {
//                return Result.Create().SetCode((int)Code.参数不能为空).SetMessage($"kf_account{Code.参数不能为空.ToString()}");
//            }
//            else if (string.IsNullOrEmpty(nickname))
//            {
//                return Result.Create().SetCode((int)Code.参数不能为空).SetMessage($"nickname{Code.参数不能为空.ToString()}");
//            }
//            else if (string.IsNullOrEmpty(password))
//            {
//                return Result.Create().SetCode((int)Code.参数不能为空).SetMessage($"password{Code.参数不能为空.ToString()}");
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
