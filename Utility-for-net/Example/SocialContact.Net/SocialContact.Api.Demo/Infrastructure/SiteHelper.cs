using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;
using Tunynet.Common.Configuration;
using Tunynet.Settings;
using Tunynet.Spacebuilder;

namespace SocialContact.Infrastructure
{
     public class SiteHelper
    {
        /// <summary>
        /// 编辑站点设置 (POST)
        /// </summary>
        /// <param name="siteSettingEditModel">站点设置ViewModel</param>
        /// <returns></returns>
        public static StatusMessageData _EditSiteSetting(AuthorizationService authorizationService , SiteSettingEditModel siteSettingEditModel,User user)
        {
            if (authorizationService.Check(user, PermissionItemKeys.Instance().SiteManage()))
            {
                if (siteSettingEditModel.isEmail == false && siteSettingEditModel.isMobile == false)
                {
                    return new StatusMessageData(StatusMessageType.Hint, "编辑失败");
                }
                else
                {
                    //获取设置
                    var siteSetting = SettingManager<SiteSettings>.Get();
                    var userSetting = SettingManager <UserSettings> .Get();
                    var imageSetting = SettingManager <ImageSettings> .Get();
                    var watermarkSetting = imageSetting.WatermarkSettings;
                    var inviteFriendSetting = SettingManager < InviteFriendSettings >.Get();

                    //处理并将ViewModel中的数据分别存入相应的设置
                    if (siteSettingEditModel.isEmail == false || siteSettingEditModel.isMobile == false)
                    {
                        siteSettingEditModel.RegisterType = siteSettingEditModel.isEmail ? RegisterType.Email : RegisterType.Mobile;
                    }

                    siteSetting = siteSettingEditModel.MapTo(siteSetting);
                    userSetting = siteSettingEditModel.MapTo(userSetting);
                    watermarkSetting = siteSettingEditModel.MapTo(watermarkSetting);
                    inviteFriendSetting = siteSettingEditModel.MapTo(inviteFriendSetting);

                    //保存设置
                    SettingManager<SiteSettings>.Save(siteSetting);
                    SettingManager<UserSettings>.Save(userSetting);
                    SettingManager<ImageSettings>.Save(imageSetting);
                    SettingManager<InviteFriendSettings>.Save(inviteFriendSetting);

                    //更改Formatter中的siteName值,让站点更改的名称生效
                    Formatter.ChangeSiteName(siteSetting.SiteName);

                    return new StatusMessageData(StatusMessageType.Success, "编辑站点设置成功");
                }
            }
            else
            {
                return new StatusMessageData(StatusMessageType.Hint, "无权操作");
            }
        }
    }
}
