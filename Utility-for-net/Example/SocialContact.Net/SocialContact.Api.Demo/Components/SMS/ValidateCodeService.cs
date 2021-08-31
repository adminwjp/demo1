//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dm.Model.V20151123;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Core;
//using Aliyun.MNS;
//using Aliyun.MNS.Model;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web;
using Tunynet.Caching;
using Tunynet.Settings;
using Tunynet.Utilities;

namespace Tunynet.Common
{
    /// <summary>
    /// 手机验证码业务逻辑
    /// </summary>
    public class ValidateCodeService
    {

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNum">手机号</param>
        /// <param name="expiredMinutes">到期时间限制</param>
        /// <returns></returns>
        public static bool Send(string phoneNum, string templateCode, int expiredMinutes = 3)
        {
            string time;
            //给发送短信加了个时间限制 1分钟之内多发 都会失败
            time=GlobalHelper.Cache.Get<string>(phoneNum + "_Time");
            if (!string.IsNullOrEmpty(time))
            {
                return false;
            }
            //给发送短信加了个每日时间限制 1日之内过量就失败
            int count=GlobalHelper.Cache.Get<int>(phoneNum + "_Day");
            if (count >= Convert.ToInt32(SMSConfig.ShortCreedNumber))
            {
                return false;
            }
            var siteSetting = SettingManager<SiteSettings>.Get();
            string validateCode = new Random().Next(999999).ToString().PadLeft(6, '0');

            String product = "Dysmsapi";//短信API产品名称（短信产品名固定，无需修改）
            String domain = "dysmsapi.aliyuncs.com";//短信API产品域名（接口地址固定，无需修改）

            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", SMSConfig.AccessKey, SMSConfig.AccessSecret);
            //初始化ascClient,暂时不支持多region（请勿修改）
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = phoneNum;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = SMSConfig.SignName;
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = templateCode;
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = "{\"code\":\"" + validateCode + "\",\"product\":\"" + siteSetting.SiteName + "\"}";
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                request.OutId = "yourOutId";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                //验证码储存到缓存中
                GlobalHelper.Cache.Set(phoneNum + "_VerificationCode", EncryptionUtility.MD5(validateCode), DateTime.Now.AddMinutes(expiredMinutes));
                //一分钟间隔 超过一分钟才能进行发送
                GlobalHelper.Cache.Set(phoneNum + "_Time", EncryptionUtility.MD5(validateCode), DateTime.Now.AddMinutes(1));
                count++;
                //每日上限+1
                GlobalHelper.Cache.Set(phoneNum + "_Day", EncryptionUtility.MD5(validateCode), DateTime.Now.AddDays(1));

                return true;
            }
            catch (ServerException)
            {
                return false;
            }
            catch (ClientException)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="user">被发送用户</param>
        /// <param name="subject">标题</param>
        /// <param name="model">发送的内容</param>
        /// <param name="change">是否完善资料中的绑定邮箱</param>
        /// <param name="newEmailAddress">新邮箱地址</param>
        /// <returns></returns>
        public static bool EmailSend(IUser user, string subject, MailMessage model, bool change = false, string newEmailAddress = null)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", SMSConfig.AccessKey, SMSConfig.AccessSecret);
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendMailRequest request = new SingleSendMailRequest();
            try
            {
                request.AccountName = SMSConfig.AccountName;
                request.FromAlias = model.From.DisplayName.Length > 13 ? model.From.DisplayName.Substring(0, 13) : model.From.DisplayName;
                request.AddressType = 1;
                request.TagName = SMSConfig.TagName;
                request.ReplyToAddress = true;
                if (change)
                {
                    if (string.IsNullOrEmpty(newEmailAddress))
                    {
                        request.ToAddress = user.UserGuid;
                    }
                    else
                    {
                        request.ToAddress = newEmailAddress;
                    }
                }
                else
                    request.ToAddress = user.AccountEmail;
                int count=GlobalHelper.Cache.Get<int>(request.ToAddress + "_EmailDay"); //给发送邮件加了个每日时间限制 1日之内过量就失败
                if (count >= Convert.ToInt32(SMSConfig.MailArticleNumber))
                {
                    return false;
                }

                request.Subject = subject + "(" + model.From.DisplayName + ")";
                request.HtmlBody = model.Body;

                SingleSendMailResponse httpResponse = client.GetAcsResponse(request);
                if (httpResponse.HttpResponse.Status == 200)
                {
                    count++;
                    //每日上限+1
                    GlobalHelper.Cache.Set(request.ToAddress + "_EmailDay", count, DateTime.Now.AddDays(1));
                    return true;
                }
                return false;
            }
            catch (ServerException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public static ValidateCodeStatus Check(string userName, string validateCode, bool? isMobile = true)
        {
            string cacheKey = userName + "_VerificationCode";
            if (!isMobile.Value)
            {
                cacheKey = userName + "_EmailVerificationCode";
            }
            string code =GlobalHelper.Cache.Get<string>(cacheKey);//从缓存里取验证码
            if (string.IsNullOrEmpty(code))
                return ValidateCodeStatus.WrongInput;

            if (EncryptionUtility.MD5(validateCode) != code)
                return ValidateCodeStatus.WrongInput;
            return ValidateCodeStatus.Passed;
        }

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="validateCode"></param>
        //public void Create(HttpContextBase httpContext, long phoneNum, string validateCode, int expiredMinutes = 2)
        //{
        //    HttpCookie cookies = new HttpCookie("VerificationCode");
        //    cookies.Value = EncryptionUtility.MD5(validateCode);
        //    cookies.Expires = DateTime.Now.AddMinutes(expiredMinutes);
        //    httpContext.Response.Cookies.Add(cookies);
        //}

        public static string GetCodeError(ValidateCodeStatus status)
        {
            switch (status)
            {
                case ValidateCodeStatus.Empty:
                    return "未匹配到号码";

                case ValidateCodeStatus.Overtime:
                    return "验证超时，请重新获取";

                case ValidateCodeStatus.WrongInput:
                    return "验证码输入错误";

                case ValidateCodeStatus.Failure:
                    return "验证码已失效";

                default:
                    return "未知错误";
            }
        }
    }
}