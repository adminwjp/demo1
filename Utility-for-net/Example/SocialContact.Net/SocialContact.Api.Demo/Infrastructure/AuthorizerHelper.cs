using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;

namespace SocialContact.Infrastructure
{
    public enum AuthorizerFlag
    {
        None,

        /// <summary>是否有权限管理资讯 </summary>
        IsCMSCategoryManager,

        /// <summary>是否具有公共内容管理的权限 </summary>
        IsCMSManager,

        /// <summary>是否具有公共内容管理的权限 </summary>
        IsSuperAdministrator,

        /// <summary>是否具有公共内容管理的权限 </summary>
        IsSiteManager,

        /// <summary>是否具有公共内容管理的权限 </summary>
        IsGlobalContentManager,

        /// <summary>是否具有用户管理的权限 </summary>
        IsUserManager
    }
    public class AuthorizerHelper
    {
        static SystemMessageViewModel systemMessage;
        public static void Init()
        {
            if (systemMessage != null)
            {
                return;
            }
            Dictionary<string, string> buttonLink = new Dictionary<string, string>();
            buttonLink.Add("返回首页", SiteUrls.Instance().ControlPanelHome());
            Dictionary<string, string> bodyLink = new Dictionary<string, string>();
            bodyLink.Add("Title", "抱歉您没有权限访问该页面！");
            systemMessage = new SystemMessageViewModel
            {
                Body = "请点击以下按钮返回上一页或返回首页。",
                ReturnUrl = SiteUrls.Instance().Home(),
                Title = "无权查看",
                StatusMessageType = StatusMessageType.Error,
                ButtonLink = buttonLink,
                BodyLink = bodyLink
            };
        }
        /// <summary>
        /// 是否有权限
        /// </summary>
        public static Tuple<bool, SystemMessageViewModel> IsCheck(Authorizer authorizer, AuthorizerFlag flag,
            int? contentCategoryId=null, User user=null, AuthorizationService authorizationService=null)
        {
            var isCheck = false;
            switch (flag)
            {
                case AuthorizerFlag.IsCMSManager:
                    isCheck=authorizer.IsCategoryManager(TenantTypeIds.Instance().ContentItem(), user, contentCategoryId);
                    break;
                case AuthorizerFlag.IsSuperAdministrator:
                    isCheck = authorizer.IsSuperAdministrator(UserContext.CurrentUser);
                    break;
                case AuthorizerFlag.IsSiteManager:
                    isCheck = authorizer.IsSiteManager(UserContext.CurrentUser);
                    break;
                case AuthorizerFlag.IsGlobalContentManager:
                    isCheck = authorizer.IsGlobalContentManager(UserContext.CurrentUser);
                    break;
                case AuthorizerFlag.IsUserManager:
                    isCheck = authorizer.IsUserManager(UserContext.CurrentUser);
                    break;
                case AuthorizerFlag.IsCMSCategoryManager:
                    isCheck = authorizationService.Check(user, PermissionItemKeys.Instance().CMS());
                    break;
                case AuthorizerFlag.None:
                default:
                    break;
            }
            if (!isCheck)
            {
                Init();
                return new Tuple<bool, SystemMessageViewModel>(isCheck, systemMessage);
            }
            return new Tuple<bool, SystemMessageViewModel>(isCheck, null);
        }
    }
}
