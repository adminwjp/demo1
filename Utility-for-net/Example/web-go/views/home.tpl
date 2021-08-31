@{
    ISettingsManager<SiteSettings> siteSettingsManager = DIContainer.Resolve<ISettingsManager<SiteSettings>>();
    var siteSettings = siteSettingsManager.Get();
    var user = UserContext.CurrentUser;
    var isCMSDetail = TempData.Get<bool>("isCMSDetail", false);
    var keyword = TempData.Get<string>("keyword", null);
    var description = TempData.TryGetValue<string>("description", null);
}
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge, chrome=1">
    <title>{{.title}}</title>
    <meta name="keywords" content="{{.keyword}}">
    <meta name="description" content="{{.description}}">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style type="text/css">
        input:-webkit-autofill, select:-webkit-autofill, textarea:-webkit-autofill {
            -webkit-box-shadow: 0 0 0px 1000px #ffffff inset !important;
        }
    </style>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="@Tunynet.Utilities.WebUtility.ResolveUrl("~/js/html5shiv.min.js")"></script>
       <script type="text/javascript" src="@Tunynet.Utilities.WebUtility.ResolveUrl("~/js/respond.js")"></script>
    <![endif]-->

    <link rel="icon" type="image/x-icon" href="@Tunynet.Utilities.WebUtility.ResolveUrl("~/favicon.ico")" />
    <link rel="shortcut icon" type="image/x-icon" href="@Tunynet.Utilities.WebUtility.ResolveUrl("~/favicon.ico")" />
</head>
<body data-spy="scroll" data-target="#myScrollspy" style="word-wrap:break-word">
    <div class="tn-doc">
        @Html.Action("_Header", "Portal")

        <div class="tn-content">
            <div class="container">
                @Html.AntiForgeryToken()
                @RenderBody()
            </div>
        </div>
        <div class="tn-footer">
            <div class="container text-center">

                @Html.Action("_Footer", "Portal")
                <input type="hidden" id="randomSignalr" />
            </div>
        </div>
        <!-- 返回顶部  -->
        {{.is_cms_detail}}
    </div>

    @if (Tunynet.Common.Utility.IsMiniProfilerEnabled())
    {
        @StackExchange.Profiling.MiniProfiler.RenderIncludes(position: StackExchange.Profiling.RenderPosition.Left, showTrivial: false, showTimeWithChildren: false, startHidden: false);
    }

    @if (user != null)
    {

    }
    else
    {
        <script>
            require(['jquery', 'signalr.hubs'], function ($) {
                $(function () {
                    try {
                        $.connection.hub.stop();
                    } catch (e) { }
                });
            })
        </script>
    }

</body>
</html>
