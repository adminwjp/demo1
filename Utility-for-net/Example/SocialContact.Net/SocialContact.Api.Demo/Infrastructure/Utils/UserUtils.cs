using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;
using Tunynet.Common.Configuration;
using Tunynet.Email;
using Tunynet.Settings;
using Tunynet.Spacebuilder;
using Tunynet.Utilities;
using Utility.IO;
using Utility.Net.Http;
using Utility.Randoms;
using Utility.Regexs;

namespace SocialContact.Infrastructure.Utils
{
    public class UserUtils
    {
        public static UserSettings userSetting;
        public static SiteSettings siteSetting;
        public static void setUser(ViewDataDictionary ViewData)
        {
            ViewData["SiteName"] = siteSetting.SiteName;
            ViewData["RegisterType"] = userSetting.RegisterType;

            var accountTypes = AccountBindingService.GetAccountTypes(true);
            ViewData["accountTypes"] = accountTypes;
        }
        public static string GetUserLoginStatus(UserLoginStatus userLogin)
        {
            switch (userLogin)
            {
                case UserLoginStatus.InvalidCredentials:  return "用户名或密码不正确";
                case UserLoginStatus.NotActivated:   return "用户未激活";
                case UserLoginStatus.Banned: return "该用户已被封禁，无法登录";
                case UserLoginStatus.NoMobile:return "暂未开启手机登录功能";
                case UserLoginStatus.NoEmail: return "暂未开启邮箱登录功能";
                default:  return "未知错误,请稍后再试";
            }
        }
        public static Tuple<IUser, UserCreateStatus> CreateUser(RegisterEditModel model)
        {
            string userName = model.AccountEmail;
            //随机处理用户 名字
            UserUtils.RandomName(ref userName);
            var  user =User.New();
            model.MapTo(user);
            user.UserName = userName;
            user.Status = UserStatus.NoActivated;
            user.IsMobileVerified = false;
            user.UserType = (int)UserType.Member;
            UserCreateStatus status;
            //默认密码
            var iuser = MembershipService.CreateUser(user, model.PassWord, out status);
            return new Tuple<IUser, UserCreateStatus>(iuser,status);
        }

        /// <summary>
        /// 随机用户名字
        /// </summary>
        public static void RandomName(ref string mark)
        {
            mark = mark.Replace("@", "").Replace(".", "") + DateTime.Now.Ticks;
            var marks = mark.ToCharArray();
            Random rm = RandomHelper.Random;
            int k = 0;
            mark = "";
            for (int i = 0; i < marks.Length; i++)
            {
                k = rm.Next(0, 18);
                if (k != i)
                {
                    mark += marks[k];
                }
            }
            mark = mark.Substring(0, 16);
            var user = UserService.GetUser(mark);
            if (user != null)
            {
                RandomName(ref mark);
            }
        }

        public static void PerfectInformation(UserProfileEditModel userProfileEditModel)
        {
            var user = UserService.GetUser(userProfileEditModel.UserId);
            if (user != null && user.Status == UserStatus.IsActivated)
            {
                UserIdToUserNameDictionary.RemoveUserId(user.UserId);
                UserIdToUserNameDictionary.RemoveUserName(user.UserName);
                UserProfile userProfile = UserProfile.New(userProfileEditModel.UserId);
                userProfileEditModel.MapTo(user);
                MembershipService.UpdateUser(user);
                userProfileEditModel.MapTo(userProfile);
                UserProfileService.Create(userProfile);
            }
        }

        /// <summary>
        /// 注册发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public static dynamic SMSSend(string phone,string cookie)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return new { state = 0, msg = "手机号码不能为空" };
            }
            if (!RegexHelper.IsPhone(phone))
            {
                return new { state = 0, msg = "手机号格式错误" };
            }
            var user = UserService.GetUserByMobile(phone);
            if (user != null && user.Status == UserStatus.IsActivated)
            {
                return new { state = 0, msg = "发送失败，您发送的手机号已经是注册用户" };
            }

            if (cookie == null)
                return new { state = 0, msg = "发送失败,请稍等一会再发" };

            DateTime cookieValue = Convert.ToDateTime(cookie);
            var smsSendTime = Convert.ToInt32(ConfigurationManager.AppSettings["SMSSendTime"]);
            if (cookieValue > DateTime.Now.AddSeconds(smsSendTime * -1))
            {
                return new { state = 0, msg = "发送失败,请稍等一会再发" };
            }
            var result = ValidateCodeService.Send(phone, SMSConfig.SMSBindingTemplateCode, 30);
            if (result)
            {
                return new { state = 1, msg = "发送成功" };
            }
            else
            {
                return new { state = 0, msg = "发送失败,请稍等一会再发" };
            }
        }

        /// <summary>
        /// 第三方登录返回页面
        /// </summary>
        /// <param name="accountTypeKey"></param>
        /// <returns></returns>
        public static Tuple<string,dynamic, StatusMessageData> ThirdCallBack(string accountTypeKey)
        {
            ThirdAccountGetter thirdAccountGetter = ThirdAccountGetterFactory.GetThirdAccountGetter(accountTypeKey);

            int expires_in = 0;
            string accessToken = "";// thirdAccountGetter.GetAccessToken(Request, out expires_in);
            if (string.IsNullOrEmpty(accessToken))
            {
                return new Tuple<string, dynamic, StatusMessageData>("LoginToThird",
                    new { accountTypeKey = AccountTypeKeys.Instance().AliPay() },null);
            }

            //当前第三方帐号上用户标识
            var thirdCurrentUser = thirdAccountGetter.GetThirdUser(accessToken, thirdAccountGetter.OpenId);
            if (thirdCurrentUser != null)
            {
                //ViewData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Success, "登录成功");
                //ViewData["thirdCurrentUser"] = thirdCurrentUser;
                //TempData["expires_in"] = expires_in;
                //当前登录用户
                var systemCurrentUser = UserContext.CurrentUser;
                //是否已绑定过其他帐号
                long userId = AccountBindingService.GetUserId(accountTypeKey, thirdCurrentUser.Identification);
                User systemUser = UserService.GetUser(userId);

                //登录用户直接绑定帐号
                if (systemCurrentUser != null)
                {
                    if (systemUser != null)
                    {
                        if (systemCurrentUser.UserId != systemUser.UserId)
                        {
                            return new Tuple<string, dynamic, StatusMessageData>("Login",null, 
                                new StatusMessageData(StatusMessageType.Hint, "此帐号已在网站中绑定过，不可再绑定其他网站帐号"));
                        }
                        else
                        {
                            AccountBindingService.UpdateAccessToken(systemUser.UserId, thirdCurrentUser.AccountTypeKey, 
                                thirdCurrentUser.Identification, thirdCurrentUser.AccessToken, expires_in);
                            return new Tuple<string, dynamic, StatusMessageData>(SiteUrls.Instance().Home(), null,
                                new StatusMessageData(StatusMessageType.Success, "更新授权成功"));
                        }
                    }
                    else
                    {
                        AccountBinding account = AccountBinding.New();
                        account.AccountTypeKey = accountTypeKey;
                        account.Identification = thirdCurrentUser.Identification;
                        account.UserId = systemCurrentUser.UserId;
                        account.AccessToken = accessToken;
                        if (expires_in > 0)
                            account.ExpiredDate = DateTime.Now.AddSeconds(expires_in);
                        AccountBindingService.CreateAccountBinding(account);

                        //如果用户资料为空,需要完善信息
                        if (UserProfileService.Get(systemCurrentUser.UserId) == null)
                        {
                            return new Tuple<string, dynamic, StatusMessageData>(SiteUrls.Instance().PerfectInformation(), null,
                                  new StatusMessageData(StatusMessageType.Success, "绑定成功"));
                        }
                        return new Tuple<string, dynamic, StatusMessageData>(SiteUrls.Instance().Home(), null,
                               new StatusMessageData(StatusMessageType.Success, "绑定成功"));
                    }
                }
                else
                {
                    //已经绑定过，直接登录
                    if (systemUser != null)
                    {
                      //  authenticationService.SignIn(systemUser, true);

                        //如果用户资料为空,需要完善信息
                        if (UserProfileService.Get(systemUser.UserId) == null)
                        {
                            return new Tuple<string, dynamic, StatusMessageData>(SiteUrls.Instance().PerfectInformation(), null,null);
                        }
                        return new Tuple<string, dynamic, StatusMessageData>(SiteUrls.Instance().Home(), null, null);
                    }
                    else
                    {
                        return new Tuple<string, dynamic, StatusMessageData>("ThirdRegister", null, null);
                    }
                }
            }
            return new Tuple<string, dynamic, StatusMessageData>("Login", null, null);
        }
        public static bool IsCaptchaValid(string empty)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 关联新手机号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AssociatePhone(RegisterEditModel model, ITempDataDictionary TempData)
        {
            if (!IsCaptchaValid(string.Empty))
            {
                TempData["codeError"]= "验证码输入有误";
                return null;
            }
            var tuple = UserUtils.AssociateEmail(TempData);
            if (tuple.Item1 != null)
            {
                return tuple.Item1;
            }
            ThirdUser thirdUser = tuple.Item2;
            int expires_in = tuple.Item3;
            #region 验证码

            //手机注册
            long phoneNum;
            ValidateCodeStatus result = ValidateCodeStatus.Empty;
            if (long.TryParse(model.AccountMobile, out phoneNum))
            {
                result = ValidateCodeService.Check(phoneNum.ToString(), model.VerfyCode);
                if (result != ValidateCodeStatus.Passed)
                {
                    TempData["codeError"] = ValidateCodeService.GetCodeError(result);
                    return null;
                }
            }

            #endregion 验证码

            var user =User.New();
            model.MapTo(user);
            user.UserName = HtmlUtility.TrimHtml(thirdUser.NickName + model.AccountMobile, 32);
            user.Status = UserStatus.IsActivated;
            user.IsMobileVerified = true;
            user.UserType = (int)UserType.Member;
            UserCreateStatus status;
            //默认密码
            var iuser = MembershipService.CreateUser(user, model.PassWord, out status);
            if (status == UserCreateStatus.Created)
            {
                //authenticationService.SignIn(iuser, false);
                setUserPic(thirdUser, user,iuser,expires_in);
                return SiteUrls.Instance().PerfectInformation();
            }
            TempData["codeError"] = "未知错误,请稍后重试";
            return null;
        }

        public static void SetUserByAccountBinding(ThirdUser thirdUser,  IUser iuser, int expires_in)
        {
            //绑定当前第三方帐号
            //直接绑定
            AccountBinding newAccountBinding = new AccountBinding()
            {
                UserId = iuser.UserId,
                AccountTypeKey = thirdUser.AccountTypeKey,
                Identification = thirdUser.Identification,
                AccessToken = thirdUser.AccessToken,
            };
            if (expires_in > 0)
            {
                newAccountBinding.ExpiredDate = DateTime.Now.AddSeconds(expires_in);
            }
            AccountBindingService.CreateAccountBinding(newAccountBinding);
        }
        static void setUserPic(ThirdUser thirdUser,User user,IUser iuser,int expires_in)
        {
            SetUserByAccountBinding(thirdUser,iuser,expires_in);
            //绑定用户头像
            if (!string.IsNullOrEmpty(thirdUser.UserAvatarUrl))
            {
                var stream = HttpHelper.GetStream(thirdUser.UserAvatarUrl);
                var buffer = StreamHelper.GetBuffer(stream);//close  stream buffer
                var ms = StreamHelper.GetStream(buffer);
                UserServiceExtension.UploadOriginalAvatar(user.UserId, ms);
                ms = StreamHelper.GetStream(buffer);
                UserServiceExtension.ResizeAvatar(user.UserId, ms);
            }
        }
        /// <summary>
        /// 发送绑定注册码邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool RegisteredMail(IUser user)
        {
            MailMessage model = EmailBuilder.Instance().EmailVerfyCode(user, "密码找回");
            var result = ValidateCodeService.EmailSend(user, "密码找回验证", model);

            return result;
        }
        public static Tuple<string, ThirdUser,int> AssociateEmail(ITempDataDictionary TempData)
        {
            ThirdUser thirdUser = TempData.Get<ThirdUser>("thirdCurrentUser", null);
            if (thirdUser == null)
            {
                return new Tuple<string, ThirdUser, int>(SiteUrls.Instance().Home(),null,0);
            }
            int expires_in = TempData.Get<int>("expires_in", 0);
            TempData["expires_in"] = expires_in;
            TempData["thirdCurrentUser"] = thirdUser;
            return new Tuple<string, ThirdUser, int>(null,thirdUser,expires_in);
        }

        /// <summary>
        /// 关联新邮箱
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AssociateEmail(RegisterEditModel model, ITempDataDictionary TempData)
        {
            var tuple = UserUtils.AssociateEmail(TempData);
            if (tuple.Item1 != null)
            {
                return tuple.Item1;
            }
            ThirdUser thirdUser = tuple.Item2;
            int expires_in = tuple.Item3;

            if (!IsCaptchaValid(string.Empty))
            {
                TempData["codeError"] = "验证码输入有误";
                return null;
            }

            #region 创建用户

            //如果是之前未注册完的用户
            User user = UserService.GetUserByEmail(session,model.AccountEmail) as User;
            if (user != null)
            {
                Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                buttonLink.Add("点击重发", SiteUrls.Instance()._ActivateByEmail(model.AccountEmail, user.UserId));
                var systemMessageViewModel = new SystemMessageViewModel() { Title = "马上激活帐号，完成注册吧！", Body = $"邮箱确认邮件已经发送到[{model.AccountEmail}]，点击邮件里的确认链接即可登录[{siteSetting.SiteName}]，如果没有收到，可以", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };

                //发送邮箱邮件并跳转
                var result = ActivateByEmail(user);
                if (result) return SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel);
                else
                {
                    TempData["codeError"] = "发送邮件数量超过日限制,请24小时后再进行发送";
                    return null;
                }
            }
            else
            {
                user = User.New();
                model.MapTo(user);
                user.UserName = HtmlUtility.TrimHtml(thirdUser.NickName + model.AccountEmail.Replace("@", "").Replace(".", ""), 32);
                user.Status = UserStatus.NoActivated;
                user.IsMobileVerified = false;
                user.UserType = (int)UserType.Member;
                UserCreateStatus status;
                //默认密码
                var iuser = MembershipService.CreateUser(user, model.PassWord, out status);
                if (status == UserCreateStatus.Created)
                {
                    Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                    buttonLink.Add("点击重发", SiteUrls.Instance()._ActivateByEmail(model.AccountEmail, user.UserId));
                    var systemMessageViewModel = new SystemMessageViewModel() { Title = "马上激活帐号，完成注册吧！", Body = $"邮箱确认邮件已经发送到[{model.AccountEmail}]，点击邮件里的确认链接即可登录[{siteSetting.SiteName}]，如果没有收到，可以", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };

                    //发送邮箱邮件并跳转
                    var result = ActivateByEmail(user);
                    if (!result)
                    {
                        TempData["codeError"] = "发送邮件数量超过日限制,请24小时后再进行发送";
                        return  null;
                    }
                    setUserPic(thirdUser, user, iuser, expires_in);

                    return SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel);
                }

                TempData["codeError"] = "未知错误,请稍后重试";
                return  null;
            }

            #endregion 创建用户
        }

        /// <summary>
        /// 发送激活邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool ActivateByEmail(IUser user)
        {
            MailMessage model = EmailBuilder.Instance().RegisterValidateEmail(user);
            var result = ValidateCodeService.EmailSend(user, "邮箱验证", model);

            return result;
        }
    }
}
