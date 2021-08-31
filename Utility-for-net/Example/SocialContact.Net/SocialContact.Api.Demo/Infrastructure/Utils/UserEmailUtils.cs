using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;
using Tunynet.Common.Configuration;
using Tunynet.Settings;
using Tunynet.Spacebuilder;

namespace SocialContact.Infrastructure.Utils
{
    public class UserEmailUtils
    {
        static UserSettings userSetting;
        static SiteSettings siteSetting;
        /// <summary>
        /// 电子邮箱注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string   EmailRegister(RegisterEditModel model, ITempDataDictionary TempData, ViewDataDictionary ViewData)
        {
             
            //判断后台设置允许什么注册
            if (userSetting.RegisterType == RegisterType.Mobile)
            {
                //如果只允许手机注册则跳转到手机页面
                return ("PhoneRegister");
            }

            UserUtils.setUser(ViewData);

            if (!UserUtils.IsCaptchaValid(string.Empty))
            {
                TempData["codeError"] = "验证码输入有误";
                return null;
            }

            #region 创建用户
            var session = GlobalHelper.GetSession();
            using var tx = session.BeginTransaction();
            //如果是之前未注册完的用户
            User user = UserService.GetUserByEmail(session, model.AccountEmail, UserStatus.NoActivated) as User;
            if (user != null)
            {
                Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                buttonLink.Add("点击重发", SiteUrls.Instance()._ActivateByEmail(model.AccountEmail, user.UserId));
                var systemMessageViewModel = new SystemMessageViewModel() { Title = "马上激活帐号，完成注册吧！", Body = $"邮箱确认邮件已经发送到[{model.AccountEmail}]，点击邮件里的确认链接即可登录[{siteSetting.SiteName}]，如果没有收到，可以", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };

                //发送邮箱邮件并跳转
                var result = UserUtils.ActivateByEmail(user);
                if (result)
                    return SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel);
                else
                {
                    TempData["codeError"] = "发送邮件数量超过日限制,请24小时后再进行发送";
                    return null;
                }
            }
            else
            {
                var tuple = UserUtils.CreateUser(model);
                if (tuple.Item2 == UserCreateStatus.Created)
                {
                    Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                    buttonLink.Add("点击重发", SiteUrls.Instance()._ActivateByEmail(model.AccountEmail, user.UserId));
                    var systemMessageViewModel = new SystemMessageViewModel() { Title = "马上激活帐号，完成注册吧！", Body = $"邮箱确认邮件已经发送到[{model.AccountEmail}]，点击邮件里的确认链接即可登录[{siteSetting.SiteName}]，如果没有收到，可以", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };
                    //发送邮箱邮件并跳转
                    var result = UserUtils.ActivateByEmail(tuple.Item1);
                    if (result) return SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel);
                    else
                    {
                        TempData["codeError"] = "发送邮件数量超过日限制,请24小时后再进行发送";
                        return null;
                    }
                }
                else
                {
                    TempData["codeError"] = "未知错误,请稍后重试";
                    return null;
                }
            }

            #endregion 创建用户
        }


        /// <summary>
        /// 通过邮件激活激活帐号页面
        /// </summary>
        /// <returns>激活帐号页面</returns>
        public static dynamic ActivateByEmail(string accountEmail, long userId)
        {
            var session = GlobalHelper.GetSession();
            using var tx = session.BeginTransaction();
            var user = UserService.GetUser(session, userId);
            if (user == null)
            {
                return new { type = 0, msg = "用户不存在" };
            }

            //发送邮箱邮件
            var result = UserUtils.ActivateByEmail(user);
            if (result)
                return new { type = 1, msg = "发送成功" };
            else
            {
                return new { type = 0, msg = "发送邮件数量超过日限制,请24小时后再进行发送" };
            }
        }
    }
}
