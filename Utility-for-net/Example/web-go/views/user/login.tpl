<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge, chrome=1">
    <title>登录</title>
    <meta name="keywords" content="{{.keyword}}">
    <meta name="description" content="{{.description}}">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" type="text/css"  href="/static/css/font-awesome.css"></link>
    <link rel="stylesheet"  type="text/css"  href="/static/css/bootstrap.css"></link>
    <link  rel="stylesheet"  type="text/css"  href="/static/css/jn-style.css"></link>
    <link rel="stylesheet"  type="text/css"  href="/static/css/tnui.css"></link>
    <link rel="stylesheet"  type="text/css"  href="/static/css/animate.css"></link>
    <link rel="stylesheet"  type="text/css"   href="/static/css/Spacebuilder.css"></link>
    <link rel="stylesheet"  type="text/css"  href="/static/js/lib/lib/tipsy/tipsy.hovercard.css"></link>
    <link rel="stylesheet"  type="text/css"  href="/static/js/lib/tipsy/tipsy.css"></link>

    <link rel="stylesheet"  type="text/css"  href="/static/js/lib/layer/skin/layer.css"></link>
    <link rel="stylesheet"  type="text/css"  href="/static//js/lib/layer/skin/tnui/style.css"></link>

    <script src="/static/js/jquery-1.12.0.min.js"></script>
    <script src="/static/js/require.js"></script>
    <script src="/static/js/config.js" id="configjs"></script>
    <script src="/static/js/frameThemes.js"></script>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="/static/js/html5shiv.min.js""></script>
       <script type="text/javascript" src="/static/js/respond.js""></script>
    <![endif]-->

    <link rel="icon" type="image/x-icon" href="/static/favicon.ico" />
    <link rel="shortcut icon" type="image/x-icon" href="/static/favicon.ico" />
    <style>

        input:-webkit-autofill, select:-webkit-autofill, textarea:-webkit-autofill {
            -webkit-box-shadow: 0 0 0px 1000px #ffffff inset !important;
        }

        .form-horizontal .form-group {
            margin-right: 0px;
            margin-left: 0px;
        }

        .btn-default[disabled]:hover {
            color: #333;
        }
    </style>
</head>
<body data-spy="scroll" data-target="#myScrollspy" style="word-wrap:break-word">
<header>  @Html.Action("_Header", "Portal")</header>
<main class="container">
    <div class="jh-login-home">
        <div class="login row">
            <h1 class="tn-mb-30 tn-space-xlg-hor">欢迎登录{{.SiteName}}</h1>
            <form id="login_form" action="user/login" class = "tn-space-xlg-hor tn-form-validation form-horizontal ">
                <input type="hidden" name="return_url"/>
                <div class="form-group ">
                    <input type="text" name="name" id="name" class = "form-control"
                           placeholder = "{{.placeholder}}"  style = "padding-left:35.5px" />
                    <span style="left:0px" class="glyphicon  form-control-feedback" aria-hidden="true"><i class="fa fa-user"></i></span>
                    <span class="error_name"></span>
                </div>
                <div class="form-group">
                    <input type="password" name="password" id="password" class = "form-control"
                           placeholder = "请输入密码"  style = "padding-left:35.5px" />
                    <span style="left:0px" class="glyphicon  form-control-feedback" aria-hidden="true"><i class="fa fa-lock"></i></span>
                    <span id="qtts" class="error_password" style="color:red;display:none">{{.error_message}}</span>
                </div>
                <div id="captchaDiv">
                    @Html.Partial("_Captcha")
                </div>
                <div class="form-group">
                    <button type="button" class="btn btn-primary btn-block btn-submit">登录</button>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-5">
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" name="remember_password" />
                                    <small> 下次自动登录</small>
                                </label>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <a href="/user/login?forget" class="btn btn-link a">忘记密码？</a>
                        </div>
                        <div class="col-xs-3 text-right">
                            <a onclick="window.location.href='/user/login?reg'" class="btn btn-link a">注册帐号</a>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                   <!-- {{ if .is_thrid_login}}-->
                    <hr class="tn-mt-0 tn-mb-10" />
                    <ul class="list-inline jh-share-icon">
                        <li>第三方登录</li>
                        <li><button type="button" class="btn btn-link" onclick="window.location=''"><i class="fa fa-qq"></i></button></li>
                        <li><button type="button" class="btn btn-link layerwechat"><i class="fa fa-weixin"></i></button></li>
                        <li><button type="button" class="btn btn-link" onclick="window.location=''"><i class="fa fa-weibo"></i></button></li>
                        <li><button type="button" class="btn btn-link" onclick="window.location=''"><img src="/static/img/alipay-icon.png" width="20" height="20" /></button></li>
                    </ul>
                  <!--  {{end}}-->
                </div>
            </form>
        </div>

        <div class="row register" style="display: none">
            <h1 class="tn-mb-50">欢迎加入{{.SiteName}}</h1>
            <div class="col-xs-8" style="padding-right:100px;border-right:1px solid #ddd;">
                <form id="register_form" action="user/register" class = "tn-form-validation form-horizontal ">
                    <div class="form-group">
                        <label for="account_mobile">手机号</label>
                        <input type="text" name="account_mobile" id="account_mobile" class = "form-control"
                               placeholder = "{{.placeholder}}"  />
                        <span class="error_account_mobile"></span>
                        <span class="help-block tn-mb-0">或使用<a href="" class="a tn-ml-5">邮箱注册</a></span>
                    </div>
                    <div class="form-group " id="passwordLoad">
                        <label for="pwd">{{.passwordTitle}}</label>
                        <input style="padding-right:39px;" class="form-control" data-val="true" data-val-length="字母、数字至少6位,并且不能超过64位" data-val-length-max="64"
                               data-val-length-min="6" data-val-required="请输入密码" type="{{.password_type}}" value="{{.password}}" name="pwd" id="pwd" />
                        <span style="top:15px; " class="glyphicon  form-control-feedback" aria-hidden="true"><i class="fa fa-eye-slash"></i></span>
                        <input style="" class="tn-click-eye" type="button" />

                        <span class="help-block tn-mb-0">字母、数字至少6位</span>
                    </div>
                    <div class="form-group">
                        <label for="VerfyCode">验证码</label>
                        <div class="row">
                            <div class="col-xs-6">
                                <input id="getCode" type="button" class="btn btn-default" value="免费获取短信激活码" />
                            </div>
                            <div class="col-xs-6">
                                <input type="text" name="VerfyCode" id="VerfyCode" class = "form-control" />
                                <span id="errorMsg" style="color:#ff0000;display:none">@errorMessage</span>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="checkbox">
                            <label>
                                <input style="margin-top:6px" id="ispass" type="checkbox"><small> 我已看过并完全同意
                                <a href="javascript:;" class="a tn-ml-5 layerprovision">《用户使用协议》</a> </small>
                            </label>
                        </div>
                    </div>
                    <div class="form-group">
                        <button type="submit" class="btn btn-primary btn-submit tn-btn-wm">注册</button>
                    </div>
                </form>
            </div>
            <div class="col-xs-4" style="padding-left:30px;">
                <p>已有帐号？<a href="/user/login" class="a tn-ml-5">直接登录 >></a> </p>
                <p>使用第三方帐号登录</p>
                <ul class="list-inline jh-share-icon">
                    <li><button type="button" class="btn btn-link" onclick="window.location=''"><i class="fa fa-qq"></i></button></li>
                    <li><button type="button" class="btn btn-link layerwechat"><i class="fa fa-weixin"></i></button></li>
                    <li><button type="button" class="btn btn-link" onclick="window.location=''"><i class="fa fa-weibo"></i></button></li>
                    <li><button type="button" class="btn btn-link" onclick="window.location=''"><img src="/static/img/alipay-icon.png" width="20" height="20" /></button></li>
                </ul>
            </div>

        </div>

        <div class="row forget" style="display: none">
            <h1 class="tn-mb-30 tn-space-xlg-hor">找回帐号密码</h1>
            <form id="ResetPassword" action="user/forget" class = "tn-space-xlg-hor tn-form-validation form-horizontal ">
                <div class="form-group">
                    <label for="username">账号</label>
                    <input type="text" name="username" id="username" class = "form-control"
                           placeholder = "{{.placeholder}}"  />
                    <span id="forget_errorMsg" style="color:red;display:none">@errorMessage</span>
                </div>
                <div class="form-group">
                    <button type="submit" class="btn btn-primary tn-btn-wm">确定</button>
                </div>
            </form>>
        </div>
    </div>

    <!-- 返回顶部  -->
    <div class="jh-position-fixed">
        <ul class="list-unstyled jh-share-style">
            <li id="topid"><a href="#"><i class="fa fa-chevron-up "></i><br>顶部</a></li>
        </ul>
    </div>
</main>
<footer class="tn-footer">
    <div class="container text-center">
        <a href="/footer/"/>
        @Html.Action("_Footer", "Portal")
        <input type="hidden" id="random_signalr" />
    </div>
</footer>
<div class="tn-doc">



    </div>

    {{ if .user}}
        {{ .login_js }}
    {{ else }}
        <script>
            require(['jquery', 'signalr.hubs'], function ($) {
                $(function () {
                    try {
                        $.connection.hub.stop();
                    } catch (e) { }
                });
            })
        </script>
    {{ end }}
<!--微信第三方登录js-->
<!--<script src="http://res.wx.qq.com/connect/zh_CN/htmledition/js/wxLogin.js"></script>-->
<script src="/static/js/tn_IsPC.js"></script>
<script>
    ///支持手机访问页面跳转手机客户端
    (function () {
        var url = "@Utility.GetTouchScreenUrl()";
        if (!IsPC() && url != "") {
            window.location.href = "@touchUrl";

        }
        var loca=location.href;
        if (loca.indexOf("forget")>-1){
            $(".forget").show();
            $(".login").hide();
            document.title="找回密码";
        }else{
            if (loca.indexOf("reg")>-1){
                $(".register").show();
                $(".login").hide();
                document.title="注册账号";
            }
        }

    })();
</script>
</body>
</html>




