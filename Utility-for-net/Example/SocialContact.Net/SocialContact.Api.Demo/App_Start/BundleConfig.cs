//using System.Web.Optimization;

//namespace Web
//{
//    public class BundleConfig
//    {
//        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
//        public static void RegisterBundles(BundleCollection bundles)
//        {
//            //基本css
//            bundles.Add(new StyleBundle("~/css/Site").Include(
//                   "~/css/font-awesome.css",
//                   "~/css/bootstrap.css",
//                   "~/css/jn-style.css"));

//            //后台
//            bundles.Add(new StyleBundle("~/ConsoleViews/css/SiteConsole").Include(
//                   "~/ConsoleViews/css/tn-console.css"));

//            //前台
//            bundles.Add(new StyleBundle("~/css/SiteThemes").Include(
//                   "~/css/tnui.css",
//                    "~/css/animate.css",
//                    "~/css/Spacebuilder.css"));

//            #region 插件

//            #region bootstrap-select 下拉

//            bundles.Add(new StyleBundle("~/js/lib/bootstrap-select/css/select").Include(
//                   "~/js/lib/bootstrap-select/css/bootstrap-select.css"));

//            #endregion bootstrap-select 下拉

//            #region qqFace 表情

//            bundles.Add(new StyleBundle("~/js/lib/qqFace/css/qqFace").Include(
//                   "~/js/lib/qqFace/css/reset.css"));

//            #endregion qqFace 表情

//            #region jPlayer 视频播放

//            bundles.Add(new StyleBundle("~/js/lib/jPlayer/css/BundlejPlayer").Include(
//                    "~/js/lib/jPlayer/css/jplayer.blue.monday.css",
//                    "~/js/lib/jPlayer/css/jplayer.jinhu.monday.css"));

//            #endregion jPlayer 视频播放

//            #region cropper 图片裁剪 头像

//            bundles.Add(new StyleBundle("~/js/lib/cropper/css/cropper").Include(
//                   "~/js/lib/cropper/css/cropper.css",
//                    "~/js/lib/cropper/css/main.css"));

//            #endregion cropper 图片裁剪 头像

//            #region jcrop 图片裁剪 头像

//            bundles.Add(new StyleBundle("~/js/lib/Jcrop/css/jcrop").Include(
//                   "~/js/lib/Jcrop/css/Jcrop.css"));

//            #endregion jcrop 图片裁剪 头像

//            #region owl.carousel 幻灯片

//            bundles.Add(new StyleBundle("~/js/lib/owl.carousel/assets/carousel").Include(
//                   "~/js/lib/owl.carousel/assets/owl.carousel.css"));

//            #endregion owl.carousel 幻灯片

//            #region slider-pro 幻灯片

//            bundles.Add(new StyleBundle("~/js/lib/slider-pro/css/slider").Include(
//                   "~/js/lib/slider-pro/css/slider-pro.css",
//                    "~/js/lib/slider-pro/css/examples.css",
//                    "~/js/lib/slider-pro/css/jh-img-style.css"));

//            #endregion slider-pro 幻灯片

//            #region fullcalendar日历

//            bundles.Add(new StyleBundle("~/js/lib/fullcalendar/fullcalendar").Include(
//                   "~/js/lib/fullcalendar/fullcalendar.css",
//                    "~/js/lib/fullcalendar/fullcalendar.print.css"));

//            #endregion fullcalendar日历

//            #region daterangepicker 时间选择器

//            bundles.Add(new StyleBundle("~/js/lib/daterangepicker/daterangepicker").Include(
//                   "~/js/lib/daterangepicker/daterangepicker.css"));

//            bundles.Add(new StyleBundle("~/js/lib/datetimepicker/css/datetimepicker").Include(
//                           "~/js/lib/datetimepicker/css/datetimepicker.min.css"));

//            #endregion daterangepicker 时间选择器

//            #region zTree

//            bundles.Add(new StyleBundle("~/js/lib/zTree/css/zTreeStyle/zTree").Include(
//                   "~/js/lib/zTree/css/zTreeStyle/zTreeStyle.css",
//                   "~/css/jh-ztree.css"));

//            #endregion zTree

//            #region webuploader上传

//            bundles.Add(new StyleBundle("~/js/lib/plupload/upload").Include(
//                    "~/js/lib/plupload/upload.css"));
//            #endregion webuploader上传

//            #region toastr提示窗

//            bundles.Add(new StyleBundle("~/js/lib/toastr/toastr").Include(
//                   "~/js/lib/toastr/toastr.min.css"));

//            #endregion toastr提示窗

//            #region 标签

//            bundles.Add(new StyleBundle("~/js/lib/tags/css/tokenfield").Include(
//                   "~/js/lib/tags/css/bootstrap-tokenfield.css",
//                    "~/js/lib/tags/css/tokenfield-typeahead.css"));

//            #endregion 标签

//            #region 弹出层

//            bundles.Add(new StyleBundle("~/js/lib/layer/skin/layer").Include(
//                   "~/js/lib/layer/skin/layer.css",
//                    "~/js/lib/layer/skin/tnui/style.css"));

//            #endregion 弹出层

//            #region fancyBox相册

//            bundles.Add(new StyleBundle("~/js/lib/fancyBox/source/BundleFancyBox").Include(
//                    "~/js/lib/fancyBox/source/jquery.fancybox.css"));

//            #endregion fancyBox相册

//            #region tooltip 用户卡片

//            bundles.Add(new StyleBundle("~/js/lib/tipsy/tipsy").Include(
//                   "~/js/lib/tipsy/tipsy.hovercard.css",
//                    "~/js/lib/tipsy/tipsy.css"));

//            #endregion tooltip 用户卡片

//            #region 用户签到 +万年历

//            bundles.Add(new StyleBundle("~/js/lib/SignIn/css/BundleCalendar").Include(
//                  "~/js/lib/SignIn/css/calendar.css",
//                  "~/js/lib/SignIn/css/jh-calendar.css",
//                  "~/js/lib/calendar/easyui.css"));

//            #endregion 用户签到 +万年历

//            #region jqzoom 图片放大

//            bundles.Add(new StyleBundle("~/js/lib/jqzoom/css/jqzoom").Include(
//           "~/js/lib/jqzoom/css/jqzoom.css"));

//            #endregion jqzoom 图片放大

//            #endregion 插件
//        }
//    }
//}