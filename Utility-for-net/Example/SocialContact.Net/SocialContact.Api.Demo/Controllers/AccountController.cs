//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Domain.Events;
using SocialContact.Infrastructure;
using SocialContact.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Tunynet.Caching;
using Tunynet.Common;
using Tunynet.Common.Configuration;
using Tunynet.Email;
using Tunynet.Settings;
using Tunynet.Utilities;
using Utility.IO;
using Utility.Net.Http;
using Utility.Regexs;

namespace Tunynet.Spacebuilder
{

    public partial class AccountController
    {
        #region 注册&&登录&&找回密码

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (UserContext.CurrentUser != null)
                return Redirect("Home/Portal");//前台首页

            setUser();
            return View(new LoginEditModel() { ReturnUrl = returnUrl });
        }

 

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginEditModel model)
        {
            if (UserContext.CurrentUser != null)
                return Redirect("Home/Portal");//前台首页
            setUser();

            if (!this.IsCaptchaValid(string.Empty))
            {
                model.ErrorMessage = "验证码输入有误";
                return PartialView("_Captcha", model);
            }
           
            return Json(UserHelper.Login(model,authenticationService));
        }

        private bool IsCaptchaValid(string empty)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public new ActionResult SignOut()
        {
            base.SignOut();
            authenticationService.SignOut();
            return RedirectToAction("Login", "Account");
        }

        void setUser()
        {
            UserUtils.setUser(ViewData);
        }
        /// <summary>
        /// 电子邮箱注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmailRegister()
        {
            if (UserContext.CurrentUser != null)
                return Redirect("Home/Portal");//前台首页
            //判断后台设置允许什么注册
            if (userSetting.RegisterType == RegisterType.Mobile)
            {
                //如果只允许手机注册则跳转到手机页面
                return RedirectToAction("PhoneRegister");
            }

            RegisterEditModel registerEditModel = new RegisterEditModel();
          
            return View(registerEditModel);
        }

        /// <summary>
        /// 电子邮箱注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmailRegister(RegisterEditModel model)
        {
            var url = UserEmailUtils.EmailRegister(model, TempData, ViewData);
            if (url == null)
            {
                return View(model);
            }
            else
            {
                return Redirect(url);
            }
        }

        /// <summary>
        /// 通过邮件激活激活帐号页面
        /// </summary>
        /// <returns>激活帐号页面</returns>
        [HttpPost]
        public JsonResult _ActivateByEmail(string accountEmail, long userId)
        {
            return Json(UserEmailUtils.ActivateByEmail(accountEmail,userId));
        }

        /// <summary>
        /// 完善资料跳转
        /// </summary>
        /// <returns>激活帐号页面</returns>
        [HttpPost]
        public JsonResult _Perfecthref(string url)
        {
            var msg = SiteUrls.Instance().Home();

            if (!string.IsNullOrEmpty(url))
            {
                msg = HttpUtility.UrlDecode(url);
            }

            return Json(new { type = 2, msg = msg });
        }

      

        /// <summary>
        /// 发送绑定注册码邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool RegisteredMail(IUser user)
        {
            MailMessage model = EmailBuilder.Instance().EmailVerfyCode(user, "密码找回");
            var result = ValidateCodeService.EmailSend(user, "密码找回验证", model);

            return result;
        }

        /// <summary>
        /// 手机号码注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PhoneRegister()
        {
            if (UserContext.CurrentUser != null)
                return Redirect(SiteUrls.Instance().Home());
            //判断后台设置允许什么注册
            if (userSetting.RegisterType == RegisterType.Email)
            {
                //如果只允许邮箱注册则跳转到邮箱注册页面
                return RedirectToAction("EmailRegister");
            }
            setUser();
            RegisterEditModel registerEditModel = new RegisterEditModel();

            return View(registerEditModel);
        }

        /// <summary>
        /// 手机号码注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PhoneRegister(RegisterEditModel model)
        {
            //判断后台设置允许什么注册
            if (userSetting.RegisterType == RegisterType.Email)
            {
                //如果只允许邮箱注册则跳转到邮箱注册页面
                return RedirectToAction("EmailRegister");
            }
            setUser();

            #region 验证码

            if (!this.IsCaptchaValid(string.Empty))
            {
                TempData["errorMessage"] = "验证码输入有误";
                return View(model);
            }

            //手机注册
            long phoneNum;
            ValidateCodeStatus result = ValidateCodeStatus.Empty;
            if (long.TryParse(model.AccountMobile, out phoneNum))
            {
                result = ValidateCodeService.Check(phoneNum.ToString(), model.VerfyCode);
                if (result != ValidateCodeStatus.Passed)
                {
                    TempData["errorMessage"] = ValidateCodeService.GetCodeError(result);
                    return View(model);
                }
            }
            string userName = model.AccountMobile;
            //随机处理用户 名字
            UserUtils.RandomName(ref userName);
            #endregion 验证码

            var user = Common.User.New();
            model.MapTo(user);
            user.UserName = userName;
            user.Status = UserStatus.IsActivated;
            user.IsMobileVerified = true;
            user.UserType = (int)UserType.Member;
            UserCreateStatus status;
            //默认密码
            var iuser = MembershipService.CreateUser(session, user, model.PassWord, out status);
            if (status == UserCreateStatus.Created)
            {
                //是否为受邀请注册用户
                if (Request.Cookies["invite"] != null)
                {
                    InviteRegisterSuccess(Request.Cookies["invite"], user.UserId);
                }
                authenticationService.SignIn(iuser, false);

                return Redirect(SiteUrls.Instance().PerfectInformation());
            }

            TempData["errorMessage"] = "创建用户失败";
            return View(model);
        }

       

        /// <summary>
        /// 帐号异常页面，帐号未激活或者帐号被封禁之类
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemMessage(string returnUrl = null)
        {
            if (TempData["SystemMessageViewModel"] == null)
            {
                Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                buttonLink.Add("首页", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().Home()));
                TempData["SystemMessageViewModel"] = new SystemMessageViewModel
                {
                    Body = "您访问的页面已经失效,<br/><span id='seconds'>5</span>秒后，自动跳转到",
                    ReturnUrl = returnUrl == null ? SiteUrls.Instance().Home() : returnUrl,
                    Title = "链接失效",
                    StatusMessageType = StatusMessageType.Error,
                    ButtonLink = buttonLink
                };
            }

            SystemMessageViewModel systemMessageViewModel = TempData["SystemMessageViewModel"] as SystemMessageViewModel;
            return View(systemMessageViewModel);
        }

        /// <summary>
        /// 邮箱激活页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ValideMailActive(string token, bool change)
        {
            SystemMessageViewModel systemMessageViewModel = null;
            User user = null;
            ThirdUser thirdUser = TempData.Get<ThirdUser>("thirdCurrentUser", null);

            TempData["thirdCurrentUser"] = thirdUser;

            bool isTimeout = false;
            long userId = Tunynet.Common.Utility.DecryptTokenForValidateEmail(token, out isTimeout);
            if (!isTimeout)
            {
                user = UserService.GetUser(session, userId);
                if (user == null)
                    return Redirect(SiteUrls.Instance().SystemMessage());

                var emailViewModel = TempData.Get<SystemMessageViewModel>("SystemMessageViewModel", null);

                if (emailViewModel != null)
                    systemMessageViewModel = emailViewModel;
                else
                {
                    //是否为受邀请注册用户
                    if (Request.Cookies["invite"] != null)
                    {
                        InviteRegisterSuccess(Request.Cookies["invite"], user.UserId);
                    }

                    Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                    buttonLink.Add("用户资料完善页面", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().PerfectInformation()));
                    systemMessageViewModel = new SystemMessageViewModel() { Title = "帐号激活成功！", Body = $"你以后可以使用{user.AccountEmail}登录。<br/><span id='seconds'>5</span>秒后，自动跳转到", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };
                }

                if (change)
                {
                    if (UserService.GetUserByEmail(session, user.UserGuid) != null)
                    {
                        Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                        buttonLink.Add("首页", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().Home()));
                        systemMessageViewModel = new SystemMessageViewModel
                        {
                            Body = "激活失败,您激活的邮箱已经绑定其他账号,<br/><span id='seconds'>5</span>秒后，自动跳转到",
                            ReturnUrl = SiteUrls.Instance().Home(),
                            Title = "激活失败",
                            StatusMessageType = StatusMessageType.Error,
                            ButtonLink = buttonLink
                        };

                        return Redirect(SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel));
                    }

                    user.AccountEmail = user.UserGuid;
                    user.IsEmailVerified = true;
                    MembershipService.UpdateUser(user);
                }
                else
                {
                    if (UserService.GetUserByEmail(session, user.AccountEmail) != null)
                    {
                        Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                        buttonLink.Add("首页", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().Home()));
                        systemMessageViewModel = new SystemMessageViewModel
                        {
                            Body = "激活失败,您激活的邮箱已经绑定其他账号,<br/><span id='seconds'>5</span>秒后，自动跳转到",
                            ReturnUrl = SiteUrls.Instance().Home(),
                            Title = "激活失败",
                            StatusMessageType = StatusMessageType.Error,
                            ButtonLink = buttonLink
                        };

                        return Redirect(SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel));
                    }

                    MembershipService.ActivateUsers(new List<long> { userId });
                    UserServiceExtension.UserEmailVerified(userId);
                    authenticationService.SignIn(user, false);
                }
            }

            return Redirect(SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel));
        }

        /// <summary>
        /// 完善资料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PerfectInformation()
        {
            var user = UserContext.CurrentUser;
            UserProfileEditModel userProfileEditModel = new UserProfileEditModel();
            user.MapTo(userProfileEditModel);
            userProfileEditModel.UserName = string.Empty;
            var profile = UserProfileService.Get(user.UserId);
            //第三方登录的用户
            ThirdUser thirdUser = TempData.Get<ThirdUser>("thirdCurrentUser", null);

            if (thirdUser != null || profile != null)
            {
                ViewData["thirdUser"] = thirdUser;
                userProfileEditModel.UserName = user.UserName;
            }
            return View(userProfileEditModel);
        }

        /// <summary>
        /// 完善资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PerfectInformation(UserProfileEditModel userProfileEditModel)
        {
            UserUtils.PerfectInformation(userProfileEditModel);
            return Redirect(SiteUrls.Instance().Home());
        }

        /// <summary>
        /// 忘记密码的页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword()
        {
            ResetPasswordEditModel resetPasswordEditModel = new ResetPasswordEditModel();
            ViewData["RegisterType"] = userSetting.RegisterType;

            return View(resetPasswordEditModel);
        }

        /// <summary>
        /// 忘记密码提交
        /// </summary>
        /// <param name="resetPasswordEditModel"></param>
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordEditModel resetPasswordEditModel)
        {
            ViewData["RegisterType"] = userSetting.RegisterType;
            if (!this.IsCaptchaValid(string.Empty))
            {
                TempData["errorMessage"] = "验证码输入有误";
                return View(resetPasswordEditModel);
            }
            var user = UserService.GetUserByEmail(session, resetPasswordEditModel.UserName);

            if (user != null)
            {
                if (userSetting.RegisterType > RegisterType.Mobile)
                {
                    //并且发送验证码
                    var result = RegisteredMail(user);
                    if (result)
                        return RedirectToAction("EmailResetPassword", new { email = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                    else
                    {
                        TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                        return View(resetPasswordEditModel);
                    }
                }
                else
                {
                    TempData["errorMessage"] = "系统暂未开启邮箱找回密码";
                    return View(resetPasswordEditModel);
                }
            }

            if (user == null)
            {
                var mobileRegex = RegexUtility.Mobile();
                if (mobileRegex.IsMatch(resetPasswordEditModel.UserName))
                {
                    user = UserService.GetUserByMobile(session, resetPasswordEditModel.UserName);

                    if (user != null && (userSetting.RegisterType == RegisterType.Mobile || userSetting.RegisterType > RegisterType.Email))
                    {
                        //并且发送验证码
                        var result = validateCodeService.ResetPassWord(resetPasswordEditModel.UserName);
                        if (result)
                            return RedirectToAction("MobileResetPassword", new { mobileNum = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                        else
                        {
                            TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                            return View(resetPasswordEditModel);
                        }
                    }
                }
            }
            if (user == null)
            {
                user = UserService.GetUserByUserName(session, resetPasswordEditModel.UserName);
                if (user != null)
                {
                    switch (userSetting.RegisterType)
                    {
                        case RegisterType.Mobile:
                            if (!string.IsNullOrEmpty(user.AccountMobile) && user.IsMobileVerified)
                            {
                                //并且发送验证码
                                var result = validateCodeService.ResetPassWord(user.AccountMobile);
                                if (result)
                                    return RedirectToAction("MobileResetPassword", new { mobileNum = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                                else
                                {
                                    TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                                    return View(resetPasswordEditModel);
                                }
                            }
                            else
                            {
                                TempData["errorMessage"] = "系统未开启邮箱找回密码";
                                return View(resetPasswordEditModel);
                            }
                        case RegisterType.MobileOrEmail:
                            if (!string.IsNullOrEmpty(user.AccountMobile) && user.IsMobileVerified)
                            {
                                //并且发送验证码
                                var result = validateCodeService.ResetPassWord(user.AccountMobile);
                                if (result)
                                    return RedirectToAction("MobileResetPassword", new { mobileNum = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                                else
                                {
                                    TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                                    return View(resetPasswordEditModel);
                                }
                            }
                            else if (!string.IsNullOrEmpty(user.AccountEmail) && user.IsEmailVerified)
                            {
                                //并且发送验证码
                                var result = RegisteredMail(user);
                                if (result)
                                    return RedirectToAction("EmailResetPassword", new { email = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                                else
                                {
                                    TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                                    return View(resetPasswordEditModel);
                                }
                            }
                            break;

                        case RegisterType.Email:
                            if (!string.IsNullOrEmpty(user.AccountEmail) && user.IsEmailVerified)
                            {
                                //并且发送验证码
                                var result = RegisteredMail(user);
                                if (result)
                                    return RedirectToAction("EmailResetPassword", new { email = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                                else
                                {
                                    TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                                    return View(resetPasswordEditModel);
                                }
                            }
                            else
                            {
                                TempData["errorMessage"] = "系统未开启手机找回密码";
                                return View(resetPasswordEditModel);
                            }
                        case RegisterType.EmailOrMobile:
                            if (!string.IsNullOrEmpty(user.AccountEmail) && user.IsEmailVerified)
                            {
                                //并且发送验证码
                                var result = RegisteredMail(user);
                                if (result)
                                    return RedirectToAction("EmailResetPassword", new { email = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                                else
                                {
                                    TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                                    return View(resetPasswordEditModel);
                                }
                            }
                            else if (!string.IsNullOrEmpty(user.AccountMobile) && user.IsMobileVerified)
                            {
                                //并且发送验证码
                                var result = validateCodeService.ResetPassWord(user.AccountMobile);
                                if (result)
                                    return RedirectToAction("MobileResetPassword", new { mobileNum = EncryptionUtility.Base64_Encode(resetPasswordEditModel.UserName) });
                                else
                                {
                                    TempData["errorMessage"] = "您发送的太过频繁,请稍后再发";
                                    return View(resetPasswordEditModel);
                                }
                            }
                            break;
                    }
                }
            }
            TempData["errorMessage"] = "您帐号输入有误,请重新输入";
            return View(resetPasswordEditModel);
        }

        /// <summary>
        /// 手机号重置密码
        /// </summary>
        /// <param name="mobileNum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MobileResetPassword(string mobileNum)
        {
            var session = GlobalHelper.GetSession();
            mobileNum = EncryptionUtility.Base64_Decode(mobileNum);
            if (string.IsNullOrEmpty(mobileNum)|| !RegexHelper.IsPhone(mobileNum))
            {
                return Redirect(SiteUrls.Instance().SystemMessage());
            }
            ResetPasswordEditModel resetPasswordEditModel = new ResetPasswordEditModel();
            resetPasswordEditModel.UserName = mobileNum;
            var user = UserService.GetUserByMobile(session,mobileNum);
            if (user != null)
            {
                resetPasswordEditModel.AccountNumber = mobileNum;
                resetPasswordEditModel.UserName = user.UserName;
            }
            return View(resetPasswordEditModel);
        }

        /// <summary>
        /// 手机号重置密码
        /// </summary>
        /// <param name="resetPasswordEditModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MobileResetPassword(ResetPasswordEditModel resetPasswordEditModel)
        {
            #region 验证码

            //手机注册
            var result = ValidateCodeService.Check(resetPasswordEditModel.AccountNumber, resetPasswordEditModel.VerfyCode);

            if (result != ValidateCodeStatus.Passed)
            {
                TempData["codeError"] = ValidateCodeService.GetCodeError(result);
                return View(resetPasswordEditModel);
            }

            #endregion 验证码

            var isResult = MembershipService.ResetPassword(resetPasswordEditModel.UserName, resetPasswordEditModel.NewPassWord);

            if (isResult)
            {
                Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                buttonLink.Add("用户登录页面", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().Login(HttpContext)));
                var systemMessageViewModel = new SystemMessageViewModel() { Title = "密码重置成功！", Body = "<span id='seconds'>5</span>秒后，自动跳转到", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };
                return Redirect(SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel));
            }
            return Redirect(SiteUrls.Instance().SystemMessage());
        }

        /// <summary>
        /// 邮件找回密码页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmailResetPassword(string email)
        {
            email = EncryptionUtility.Base64_Decode(email);

            if (string.IsNullOrEmpty(email))
            {
                return Redirect(SiteUrls.Instance().SystemMessage());
            }

            ResetPasswordEditModel resetPasswordEditModel = new ResetPasswordEditModel();
            resetPasswordEditModel.UserName = email;

            var user = UserService.GetUserByEmail(session, email);
            if (user == null)
            {
                user = UserService.GetUserByUserName(session, email);
                if (user != null)
                    resetPasswordEditModel.AccountNumber = user.AccountEmail;
            }
            else
                resetPasswordEditModel.AccountNumber = email;
            return View(resetPasswordEditModel);
        }

        /// <summary>
        /// 邮件找回密码页
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmailResetPassword(ResetPasswordEditModel resetPasswordEditModel)
        {
            var result = ValidateCodeService.Check(resetPasswordEditModel.AccountNumber, resetPasswordEditModel.VerfyCode, false);

            if (result != ValidateCodeStatus.Passed)
            {
                TempData["codeError"] = ValidateCodeService.GetCodeError(result);
                return View(resetPasswordEditModel);
            }

            var isResult = MembershipService.ResetPassword(resetPasswordEditModel.UserName, resetPasswordEditModel.NewPassWord);

            if (isResult)
            {
                Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                buttonLink.Add("用户登录页面", SiteUrls.Instance()._Perfecthref(SiteUrls.Instance().Login(HttpContext)));
                var systemMessageViewModel = new SystemMessageViewModel() { Title = "密码重置成功！", Body = "<span id='seconds'>5</span>秒后，自动跳转到", ButtonLink = buttonLink, StatusMessageType = StatusMessageType.Success };
                return Redirect(SiteUrls.Instance().SystemMessage(TempData, systemMessageViewModel));
            }
            TempData["codeError"] = "重置失败";
            return View(resetPasswordEditModel);
        }

        /// <summary>
        /// 邀请注册页面
        /// </summary>
        /// <param name="invitationCode">邀请码</param>
        /// <returns></returns>
        public ActionResult Invite(string invitationCode)
        {
            if (!string.IsNullOrEmpty(invitationCode))
            {
                //获取邀请码实体
                InvitationCode invitationCodeEntity = inviteFriendService.GetInvitationCodeEntity(invitationCode);
                //邀请码过期或不存在
                if (invitationCodeEntity == null || DateTime.Now > invitationCodeEntity.ExpiredDate)
                {
                    TempData["SystemMessageViewModel"] = new SystemMessageViewModel() { Title = "链接失效", Body = "邀请链接已过期", StatusMessageType = StatusMessageType.Hint };
                    return Redirect(SiteUrls.Instance().SystemMessage());
                }
                else
                {
                    //用户未注册跳转注册
                    if (UserContext.CurrentUser == null)
                    {
                        Response.Cookies.Append("invite", invitationCode,new CookieOptions { Expires = DateTime.Now.AddHours(0.16) });
                        return Redirect(SiteUrls.Instance().Register(false));
                    }
                    else
                    {
                        var currentUser = UserContext.CurrentUser;
                        if (!followService.IsMutualFollowed(currentUser.UserId, invitationCodeEntity.UserId))
                        {
                            FollowService.Follow(currentUser.UserId, invitationCodeEntity.UserId);
                            FollowService.Follow(invitationCodeEntity.UserId, currentUser.UserId);
                        }
                        return Redirect(SiteUrls.Instance().SpaceHome(invitationCodeEntity.UserId));
                    }
                }
            }
            else
            {
                TempData["SystemMessageViewModel"] = new SystemMessageViewModel() { Title = "链接失效", Body = "邀请链接已过期", StatusMessageType = StatusMessageType.Hint };
                return Redirect(SiteUrls.Instance().SystemMessage());
            }
        }

        /// <summary>
        /// 邀请注册成功
        /// </summary>
        /// <param name="invitationCode"></param>
        /// <param name="invitedUserId"></param>
        public void InviteRegisterSuccess(string invitationCode, long invitedUserId)
        {
            var userInviteRegisterEvent= new UserInviteRegisterEvent(invitationCode, invitedUserId);
            GlobalHelper.Mediator.Publish(userInviteRegisterEvent);
        }

        #endregion 注册&&登录&&找回密码
    }

    /// <summary>
    /// AccountController
    /// </summary>

    public partial class AccountController : Controller
    {
        #region Service
        private UserService userService;
        private AccountBindingService accountBindingService;
        private ValidateCodeService validateCodeService;
        private MembershipService membershipService;
        private PointService pointService;
        private IAuthenticationService authenticationService;
        private SiteSettings siteSetting;
        private ICacheService cacheService;
        private UserSettings userSetting;
        private InviteFriendService inviteFriendService;
        private FollowService followService;
        #endregion Service

        private NHibernate.ISession session; 
       

        #region 明文&&密文密码显示

        /// <summary>
        /// 明文密文切换
        /// </summary>
        /// <returns></returns>
        public ActionResult _PassWordPoclaimed(string passWord, string name, string passWordTitle, bool isClear = false)
        {
            ViewData["passWord"] = passWord;
            ViewData["isClear"] = isClear;
            ViewData["PassWordTitle"] = passWordTitle;
            if (string.IsNullOrEmpty(name))
                ViewData["name"] = "PassWord";
            else
                ViewData["name"] = name;
            return PartialView();
        }

        #endregion 明文&&密文密码显示

        #region 验证

        /// <summary>
        /// 注册条款
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _Provision()
        {
            ViewData["SiteName"] = siteSetting.SiteName;
            return PartialView();
        }

        #endregion 验证

        #region 验证码

        /// <summary>
        /// 注册发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SMSSend(string phone)
        {
            var cookie = Request.Cookies["__ValidateMobile"];
            return Json(UserUtils.SMSSend(phone,cookie));
        }

        #endregion 验证码

        #region 第三方登录

        /// <summary>
        /// 第三方登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoginToThird(string accountTypeKey)
        {
            ThirdAccountGetter thirdAccountGetter = ThirdAccountGetterFactory.GetThirdAccountGetter(accountTypeKey);
            AccountType accountType = AccountBindingService.GetAccountType(accountTypeKey);
            if (!accountType.IsEnabled)
            {
                return RedirectToAction("Login");
            }
            if (accountTypeKey == AccountTypeKeys.Instance().WeChat())
            {
                ViewData["accountType"] = accountType;
                return View(thirdAccountGetter);
            }
            return Redirect(thirdAccountGetter.GetAuthorizationUrl());
        }

        /// <summary>
        /// 第三方登录返回页面
        /// </summary>
        /// <param name="accountTypeKey"></param>
        /// <returns></returns>
        public ActionResult ThirdCallBack(string accountTypeKey)
        {
            var tuple = UserUtils.ThirdCallBack(accountTypeKey);
            if (tuple.Item2 != null)
            {
                return RedirectToAction(tuple.Item1, tuple.Item2 );
            }else if (tuple.Item3 != null)
            {
                ViewData["StatusMessageData"] = tuple.Item3;
            }
            //ThirdRegister  LoginToThird Login
            if (tuple.Item1.Equals("ThirdRegister") || tuple.Item1.Equals("LoginToThird") || tuple.Item1.Equals("Login"))
            {
                return RedirectToAction(tuple.Item1);
            }
            return Redirect(tuple.Item1);
        }

        /// <summary>
        /// 第三方帐号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ThirdRegister()
        {
            ThirdUser thirdUser = TempData.Get<ThirdUser>("thirdCurrentUser", null);
            if (thirdUser == null)
            {
                return Redirect(SiteUrls.Instance().Home());
            }
            int expires_in = TempData.Get<int>("expires_in", 0);
            TempData["expires_in"] = expires_in;
            TempData["thirdCurrentUser"] = thirdUser;
            ViewData["siteName"] = siteSetting.SiteName;
            return View();
        }

        /// <summary>
        /// 关联已有帐号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AssociateAccount()
        {
            ThirdUser thirdUser = TempData.Get<ThirdUser>("thirdCurrentUser", null);
            int expires_in = TempData.Get<int>("expires_in", 0);

            if (thirdUser == null)
            {
                return Redirect(SiteUrls.Instance().Home());
            }

            ViewData["registerType"] = userSetting.RegisterType;
            TempData["thirdCurrentUser"] = thirdUser;
            TempData["expires_in"] = expires_in;

            return View(new LoginEditModel());
        }

        /// <summary>
        /// 关联已有帐号并登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssociateAccount(LoginEditModel model)
        {
            if (!this.IsCaptchaValid(string.Empty))
            {
                TempData["errorMessage"] = "验证码输入有误";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Name.Trim()) || string.IsNullOrEmpty(model.PassWord.Trim()))
            {
                TempData["errorMessage"] = "请输入有效的用户名和密码";
                return View(model);
            }
            //验证登录
            var result = MembershipService.ValidateUser(model.Name, model.PassWord,out User user1);
            if (result != UserLoginStatus.Success)
            {
                TempData["errorMessage"] = UserUtils.GetUserLoginStatus(result);
                return View(model);
            }
            //获取用户
            var user = UserService.GetUserByEmail(session, model.Name);
            if (user == null)
            {
                var mobileRegex = RegexUtility.Mobile();
                if (mobileRegex.IsMatch(model.Name))
                {
                    user = UserService.GetUserByMobile(session, model.Name);
                }
                else
                {
                    user = UserService.GetUserByUserName(session, model.Name);
                }
            }

            //获取当前用户是否绑定了当前第三方的帐号
            ThirdUser thirdUser = TempData.Get<ThirdUser>("thirdCurrentUser", null);
            int expires_in = TempData.Get<int>("expires_in", 0);

            if (thirdUser == null)
            {
                return Redirect(SiteUrls.Instance().Home());
            }

            var accountBinding = AccountBindingService.GetAccountBinding(user.UserId, thirdUser.AccountTypeKey);

            //已经绑定过别的了,不可以
            if (accountBinding != null)
            {
                //绑定的帐号不是当前第三方的帐号
                if (accountBinding.Identification != thirdUser.Identification)
                {
                    TempData["errorMessage"] = "当前帐号已经绑定过第三方帐号";
                    return View(model);
                }
            }
            else
            {
                UserUtils.SetUserByAccountBinding(thirdUser, user, expires_in);//直接绑定
            }

            user.LastActivityTime = DateTime.Now;
            user.IpLastActivity = Utilities.WebUtility.GetIP();
            MembershipService.UpdateUser(user);

            authenticationService.SignIn(user, model.RememberPassword);

            return Redirect(SiteUrls.Instance().Home());
        }

        /// <summary>
        /// 关联新邮箱
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AssociateEmail()
        {
            var tuple = UserUtils.AssociateEmail(TempData);
            if (tuple.Item1 != null)
            {
                return Redirect(tuple.Item1);
            }
            return View(new RegisterEditModel());
        }

        /// <summary>
        /// 关联新邮箱
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssociateEmail(RegisterEditModel model)
        {
            string url = UserUtils.AssociatePhone(model, TempData);
            if (url == null)
            {
                return View(model);
            }
            return Redirect(url);
        }

        /// <summary>
        /// 关联新手机号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AssociatePhone()
        {
            var tuple = UserUtils.AssociateEmail(TempData);
            if (tuple.Item1 != null)
            {
                return Redirect(tuple.Item1);
            }
            return View(new RegisterEditModel());
        }

        /// <summary>
        /// 关联新手机号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssociatePhone(RegisterEditModel model)
        {
            string url = UserUtils.AssociatePhone(model,TempData);
            if (url == null)
            {
                return View(model);
            }
            return Redirect(url);
        }

        #endregion 第三方登录
    }
}