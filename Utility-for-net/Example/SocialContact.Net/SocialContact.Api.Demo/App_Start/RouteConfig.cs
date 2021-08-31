//using System.Web.Mvc;
//using System.Web.Routing;

//namespace Web
//{
//    public class RouteConfig
//    {
//        public static void RegisterRoutes(RouteCollection routes)
//        {
//            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//            //统一配置url 2018.7.17 todo by yangzd

//            #region 站点主页

//            //站点主页
//            routes.MapRoute(
//                name: "Home",
//                url: "",
//                defaults: new { controller = "Portal", action = "Home" }
//            );

//            #endregion 站点主页

//            #region 找回密码

//            //邮箱找回密码
//            routes.MapRoute(
//               name: "EmailResetPassword",
//               url: "Account/EmailResetPassword/{email}",
//               defaults: new { controller = "Account", action = "EmailResetPassword" }
//           );

//            //手机号找回密码
//            routes.MapRoute(
//             name: "MobileResetPassword",
//             url: "Account/MobileResetPassword/{mobileNum}",
//             defaults: new { controller = "Account", action = "MobileResetPassword" }
//            );

//            #endregion  找回密码

//            #region UserSpace

//            //用户设置
//            routes.MapRoute(
//                name: "UserSpace_Setting",
//                url: "setting",
//                defaults: new { controller = "UserSpace", action = "UserSetting" }
//                );

//            // 她/他的主页
//            routes.MapRoute(
//                name: "UserSpace_SpaceHome",
//                url: "u/{spaceKey}",
//                defaults: new { controller = "UserSpace", action = "SpaceHomepage" }
//            );

//            // 我的主页
//            routes.MapRoute(
//                name: "UserSpace_MyHome",
//                url: "u/{spaceKey}/MyHome",
//                defaults: new { controller = "UserSpace", action = "MyHomepage" }
//            );

//            #endregion UserSpace

//            #region 资讯

//            //资讯列表
//            routes.MapRoute(
//                name: "CMS_List",
//                url: "CMS",
//                defaults: new { controller = "CMS", action = "ContentItemHome" }
//            );
//            //资讯列表 组图
//            routes.MapRoute(
//                name: "CMSImg_List",
//                url: "CMS/Img",
//                defaults: new { controller = "CMS", action = "CMSImg" }
//            );
//            //资讯列表 视频
//            routes.MapRoute(
//                name: "CMSVideo_List",
//                url: "CMS/Video",
//                defaults: new { controller = "CMS", action = "CMSVideo" }
//            );
//            //资讯列表
//            routes.MapRoute(
//                name: "CategoryCMS_Detail",
//                url: "CMS/s-{contentcategoryId}",
//                defaults: new { controller = "CMS", action = "CategoryCMS" }
//            );
//            //资讯图片详情页
//            routes.MapRoute(
//                name: "CMSImgDetail_Detail",
//                url: "CMS/i-{contentItemId}/{commentId}",
//                defaults: new { controller = "CMS", action = "CMSImgDetail", commentId = UrlParameter.Optional }
//            );
//            //资讯视频详情页
//            routes.MapRoute(
//                name: "CMSVideoDetail_Detail",
//                url: "CMS/v-{contentItemId}/{commentId}",
//                defaults: new { controller = "CMS", action = "CMSVideoDetail", commentId = UrlParameter.Optional }
//            );
//            //资讯文章详情页
//            routes.MapRoute(
//                name: "CMSDetail_Detail",
//                url: "CMS/c-{contentItemId}/{commentId}",
//                defaults: new { controller = "CMS", action = "CMSDetail", commentId = UrlParameter.Optional }
//            );

//            #endregion 资讯

//            #region 贴子Url

//            //贴吧主页
//            routes.MapRoute(
//                name: "Barsection_Home",
//                url: "Post",
//                defaults: new { controller = "Post", action = "BarsectionHome" }
//            );

//            //贴吧列表-Category
//            routes.MapRoute(
//                name: "Barsection_List_Category",
//                url: "Post/list/i-{categoryid}",
//                defaults: new { controller = "Post", action = "Barsection" }
//            );

//            //贴吧列表
//            routes.MapRoute(
//                name: "Barsection_List",
//                url: "Post/list",
//                defaults: new { controller = "Post", action = "Barsection" }
//            );

//            //贴子详情
//            routes.MapRoute(
//            name: "ThreadDetail_Details",
//            url: "Post/t-{threadId}/{commentId}",
//            defaults: new { controller = "Post", action = "ThreadDetail", commentId = UrlParameter.Optional }
//         );

//            //贴吧详情
//            routes.MapRoute(
//            name: "SectionDetail_Details",
//            url: "Post/s-{sectionId}",
//            defaults: new { controller = "Post", action = "BarSectionDetail" }
//         );

//            //贴吧管理
//            routes.MapRoute(
//            name: "SectionManage",
//            url: "Post/Manage/s-{sectionId}",
//            defaults: new { controller = "Post", action = "BarSectionManage" }
//         );

//            //创建编辑贴子
//            routes.MapRoute(
//            name: "EditThread",
//            url: "Post/EditThread/s-{sectionId}",
//            defaults: new { controller = "Post", action = "EditThread" }
//         );


//            #endregion 贴子Url

//            #region 评论

//            //评论列表页
//            routes.MapRoute(
//                name: "Comment_List",
//                url: "CommentList/c-{commentedObjectId}/t-{tenantTypeId}/{commentId}",
//                defaults: new { controller = "Portal", action = "CommentList", commentId = UrlParameter.Optional }
//            );

//            #endregion 评论

//            #region 全文检索

//            routes.MapRoute(
//            name: "Search",
//            url: "Portal/Search/k-{keyword}/t-{searchType}",
//            defaults: new { controller = "Portal", action = "Search" }
//         );

//            #endregion 全文检索

//            routes.MapRoute(
//           name: "Default",
//           url: "{controller}/{action}",
//           defaults: new { controller = "Message", action = "Home" }
//        );

//            ////注册全局特性路由
//            ////routes.MapMvcAttributeRoutes();
//            //routes.MapHttpHandler<CaptchaHttpHandler>("Captcha", "Handlers/CaptchaImage/p.ashx");
//        }
//    }
//}