package com.utility.template;

import java.util.HashMap;

class HuiAdminTemplate {

 public HashMap<String,String> huiAdmin() {
  HashMap<String,String> map = new HashMap<>();
  map.put("index.html",index());
  return map;
 }

  /** 首页 模板 死的不变*/
  private   String index(){
   return  "<!DOCTYPE HTML>\n" +
           "<html>\n" +
           "<head>\n" +
           "\t<meta charset=\"utf-8\">\n" +
           "\t<meta name=\"renderer\" content=\"webkit|ie-comp|ie-stand\">\n" +
           "\t<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">\n" +
           "\t<meta name=\"viewport\" content=\"width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no\" />\n" +
           "\t<meta http-equiv=\"Cache-Control\" content=\"no-siteapp\" />\n" +
           "\t<link rel=\"Bookmark\" href=\"/favicon.ico\">\n" +
           "\t<link rel=\"Shortcut Icon\" href=\"/favicon.ico\" />\n" +
           "\t<!--[if lt IE 9]>\n" +
           "\t<script type=\"text/javascript\" src=\"lib/html5shiv.js\"></script>\n" +
           "\t<script type=\"text/javascript\" src=\"lib/respond.min.js\"></script>\n" +
           "\t<![endif]-->\n" +
           "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"static/h-ui/css/H-ui.min.css\" />\n" +
           "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"static/h-ui.admin/css/H-ui.admin.css\" />\n" +
           "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"lib/Hui-iconfont/1.0.8/iconfont.css\" />\n" +
           "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"static/h-ui.admin/skin/green/skin.css\" id=\"skin\" />\n" +
           "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"static/h-ui.admin/css/style.css\" />\n" +
           "\t<!--[if IE 6]>\n" +
           "\t<script type=\"text/javascript\" src=\"lib/DD_belatedPNG_0.0.8a-min.js\" ></script>\n" +
           "\t<script>DD_belatedPNG.fix('*');</script>\n" +
           "\t<![endif]-->\n" +
           "\t<title>H-ui.admin v3.1</title>\n" +
           "\t<meta name=\"keywords\" content=\"H-ui.admin v3.1,H-ui网站后台模版,后台模版下载,后台管理系统模版,HTML后台模版下载\">\n" +
           "\t<meta name=\"description\" content=\"H-ui.admin v3.1，是一款由国人开发的轻量级扁平化网站后台模板，完全免费开源的网站后台管理系统模版，适合中小型CMS后台系统。\">\n" +
           "</head>\n" +
           "<body>\n" +
           "\t<header class=\"navbar-wrapper\">\n" +
           "\t\t<div class=\"navbar navbar-fixed-top\">\n" +
           "\t\t\t<div class=\"container-fluid cl\">\n" +
           "\t\t\t\t<a class=\"logo navbar-logo f-l mr-10 hidden-xs\" href=\"/aboutHui.shtml\">H-ui.admin</a> <a class=\"logo navbar-logo-m f-l mr-10 visible-xs\" href=\"/aboutHui.shtml\">H-ui</a>\n" +
           "\t\t\t\t<span class=\"logo navbar-slogan f-l mr-10 hidden-xs\">v3.1</span>\n" +
           "\t\t\t\t<a aria-hidden=\"false\" class=\"nav-toggle Hui-iconfont visible-xs\" href=\"javascript:;\">&#xe667;</a>\n" +
           "\t\t\t\t<nav class=\"nav navbar-nav\">\n" +
           "\t\t\t\t\t<ul class=\"cl\">\n" +
           "\t\t\t\t\t\t<li class=\"dropDown dropDown_hover\">\n" +
           "\t\t\t\t\t\t\t<a href=\"javascript:;\" class=\"dropDown_A\"><i class=\"Hui-iconfont\">&#xe600;</i> 新增 <i class=\"Hui-iconfont\">&#xe6d5;</i></a>\n" +
           "\t\t\t\t\t\t\t<ul class=\"dropDown-menu menu radius box-shadow\">\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"javascript:;\" onclick=\"article_add('添加资讯','article-add.html')\"><i class=\"Hui-iconfont\">&#xe616;</i> 资讯</a></li>\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"javascript:;\" onclick=\"picture_add('添加资讯','picture-add.html')\"><i class=\"Hui-iconfont\">&#xe613;</i> 图片</a></li>\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"javascript:;\" onclick=\"product_add('添加资讯','product-add.html')\"><i class=\"Hui-iconfont\">&#xe620;</i> 产品</a></li>\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"javascript:;\" onclick=\"member_add('添加用户','member-add.html','','510')\"><i class=\"Hui-iconfont\">&#xe60d;</i> 用户</a></li>\n" +
           "\t\t\t\t\t\t\t</ul>\n" +
           "\t\t\t\t\t\t</li>\n" +
           "\t\t\t\t\t</ul>\n" +
           "\t\t\t\t</nav>\n" +
           "\t\t\t\t<nav id=\"Hui-userbar\" class=\"nav navbar-nav navbar-userbar hidden-xs\">\n" +
           "\t\t\t\t\t<ul class=\"cl\">\n" +
           "\t\t\t\t\t\t<li>超级管理员</li>\n" +
           "\t\t\t\t\t\t<li class=\"dropDown dropDown_hover\">\n" +
           "\t\t\t\t\t\t\t<a href=\"#\" class=\"dropDown_A\">admin <i class=\"Hui-iconfont\">&#xe6d5;</i></a>\n" +
           "\t\t\t\t\t\t\t<ul class=\"dropDown-menu menu radius box-shadow\">\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"javascript:;\" onClick=\"myselfinfo()\">个人信息</a></li>\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"#\">切换账户</a></li>\n" +
           "\t\t\t\t\t\t\t\t<li><a href=\"#\">退出</a></li>\n" +
           "\t\t\t\t\t\t\t</ul>\n" +
           "\t\t\t\t\t\t</li>\n" +
           "\t\t\t\t\t\t<li id=\"Hui-msg\"> <a href=\"#\" title=\"消息\"><span class=\"badge badge-danger\">1</span><i class=\"Hui-iconfont\" style=\"font-size:18px\">&#xe68a;</i></a> </li>\n" +
           "\t\t\t\t\t\t<li id=\"Hui-skin\" class=\"dropDown right dropDown_hover\">\n" +
           "\t\t\t\t\t\t\t<a href=\"javascript:;\" class=\"dropDown_A\" title=\"换肤\"><i class=\"Hui-iconfont\" style=\"font-size:18px\">&#xe62a;</i></a>\n" +
           "\t\t\t\t\t\t\t<ul class=\"dropDown-menu menu radius box-shadow\">\n" +
           "\t\t\t\t\t\t\t</ul>\n" +
           "\t\t\t\t\t\t</li>\n" +
           "\t\t\t\t\t</ul>\n" +
           "\t\t\t\t</nav>\n" +
           "\t\t\t</div>\n" +
           "\t\t</div>\n" +
           "\t</header>\n" +
           "\t<aside class=\"Hui-aside\">\n" +
           "\n" +
           "\t</aside>\n" +
           "\t<div class=\"dislpayArrow hidden-xs\"><a class=\"pngfix\" href=\"javascript:void(0);\" onClick=\"displaynavbar(this)\"></a></div>\n" +
           "\t<section class=\"Hui-article-box\">\n" +
           "\t\t<div id=\"Hui-tabNav\" class=\"Hui-tabNav hidden-xs\">\n" +
           "\t\t\t<div class=\"Hui-tabNav-wp\">\n" +
           "\t\t\t\t<ul id=\"min_title_list\" class=\"acrossTab cl\">\n" +
           "\t\t\t\t\t<li class=\"active\">\n" +
           "\t\t\t\t\t\t<span title=\"我的桌面\" data-href=\"welcome.html\">我的桌面</span>\n" +
           "\t\t\t\t\t\t<em></em>\n" +
           "\t\t\t\t\t</li>\n" +
           "\t\t\t\t</ul>\n" +
           "\t\t\t</div>\n" +
           "\t\t\t<div class=\"Hui-tabNav-more btn-group\"><a id=\"js-tabNav-prev\" class=\"btn radius btn-default size-S\" href=\"javascript:;\"><i class=\"Hui-iconfont\">&#xe6d4;</i></a><a id=\"js-tabNav-next\" class=\"btn radius btn-default size-S\" href=\"javascript:;\"><i class=\"Hui-iconfont\">&#xe6d7;</i></a></div>\n" +
           "\t\t</div>\n" +
           "\t\t<div id=\"iframe_box\" class=\"Hui-article\">\n" +
           "\t\t\t<div class=\"show_iframe\">\n" +
           "\t\t\t\t<div style=\"display:none\" class=\"loading\"></div>\n" +
           "\t\t\t\t<iframe scrolling=\"yes\" frameborder=\"0\" src=\"welcome.html\"></iframe>\n" +
           "\t\t\t</div>\n" +
           "\t\t</div>\n" +
           "\t</section>\n" +
           "\n" +
           "\t<div class=\"contextMenu\" id=\"Huiadminmenu\">\n" +
           "\t\t<ul>\n" +
           "\t\t\t<li id=\"closethis\">关闭当前 </li>\n" +
           "\t\t\t<li id=\"closeall\">关闭全部 </li>\n" +
           "\t\t</ul>\n" +
           "\t</div>\n" +
           "\t<!--_footer 作为公共模版分离出去-->\n" +
           "\t<script type=\"text/javascript\" src=\"lib/jquery/1.9.1/jquery.min.js\"></script>\n" +
           "\t<script type=\"text/javascript\" src=\"lib/layer/2.4/layer.js\"></script>\n" +
           "\t<script type=\"text/javascript\" src=\"static/h-ui/js/H-ui.min.js\"></script>\n" +
           "\t<!--/此乃百度统计代码，请自行删除-->\n" +
           "\t<script type=\"text/javascript\" src=\"static/h-ui.admin/js/H-ui.admin.js\"></script> <!--/_footer 作为公共模版分离出去-->\n" +
           "\t<!--请在下方写此页面业务相关的脚本-->\n" +
           "\t<script type=\"text/javascript\" src=\"lib/jquery.contextmenu/jquery.contextmenu.r2.js\"></script>\n" +
           "\t<script src=\"js/index.js\"></script>\n" +
           "\t<script type=\"text/javascript\">\n" +
           "\t\tvar obj = { menu: [], skin: [] };\n" +
           "\t\tfunction updateNav() {\n" +
           "\t\t\tvar navStr = \"\";\n" +
           "\t\t\tfor (let i in obj.menu) {\n" +
           "\t\t\t\tnavStr += \"<li class=\\\"navbar-levelone \" +\n" +
           "\t\t\t\t\t(i == 0 ? \"current\" : \"\") +\n" +
           "\t\t\t\t\t\"\\\"><a href=\\\"javascript:;\\\">\" + getName(obj.menu[i], getLanguage()) + \"</a></li>\";\n" +
           "            }\n" +
           "            $(\"nav.nav:eq(0) > ul.cl >li\").after(navStr);\n" +
           "            //事件不知道在哪 重新 定义\n" +
           "            var clickNavbar = $(\"li.navbar-levelone\");\n" +
           "\t\t\tclickNavbar.click(function () {\n" +
           "                $(\"li.navbar-levelone.current\").removeClass(\"current\");\n" +
           "                var navMenus = $(\"li.navbar-levelone\");\n" +
           "                for (var i in navMenus) {\n" +
           "                    if (navMenus[i] == this) break;\n" +
           "                }\n" +
           "                clickNavbar.addClass(\"current\");\n" +
           "                for (let j = 0; j < navMenus.length; j++) {\n" +
           "                    $(\"aside.Hui-aside > div:eq(\"+j+\")\").css({\"display\":(i==j?\"block\": \"none\")});\n" +
           "                }\n" +
           "            });\n" +
           "          \n" +
           "\t\t}\n" +
           "\t\tfunction updateSkin() {\n" +
           "\t\t\tvar skinStr = \"\";\n" +
           "\t\t\tfor (let i in obj.skin) {\n" +
           "\t\t\t\tlet name = getName(obj.skin[i], getLanguage());\n" +
           "\t\t\t\tskinStr += \"<li><a href=\\\"javascript:;\\\" data-val=\\\"\" +\n" +
           "\t\t\t\t\t(obj.skin[i].enable ? \"default\" : obj.skin[i].color)\n" +
           "                    + \"\\\" title=\\\"\" + name + \"\\\">\" + name + \"</a></li>\";\n" +
           "                if (obj.skin[i].enable) {\n" +
           "\t\t\t\t\t$.cookie(\"Huiskin\", obj.skin[i].color);\n" +
           "                }\n" +
           "\t\t\t}\n" +
           "            $(\"li#Hui-skin > ul:eq(0)\").html(skinStr);\n" +
           "\t\t\t/*换肤*/\n" +
           "\t\t\t$(\"#Hui-skin .dropDown-menu a\").click(function(){\n" +
           "\t\t\t\tvar v = $(this).attr(\"data-val\");\n" +
           "\t\t\t\t$.cookie(\"Huiskin\", v);\n" +
           "\t\t\t\tvar hrefStr=$(\"#skin\").attr(\"href\");\n" +
           "\t\t\t\tvar hrefRes=hrefStr.substring(0,hrefStr.lastIndexOf('skin/'))+'skin/'+v+'/skin.css';\n" +
           "\t\t\t\t$(window.frames.document).contents().find(\"#skin\").attr(\"href\",hrefRes);\n" +
           "\t\t\t});\n" +
           "\t\t}\n" +
           "        function updateMenu() {\n" +
           "            var menuStr = \"\";\n" +
           "            for (let i in obj.menu) {\n" +
           "                menuStr += \"<div class=\\\"menu_dropdown bk_2\\\" \" + (i > 0 ? \"style=\\\"display:none\\\"\" : \"\") + \">\";\n" +
           "                for (let j in obj.menu[i].menus) {\n" +
           "                    menuStr += recursionMenu(obj.menu[i].menus[j]);\n" +
           "                }\n" +
           "                menuStr += \"</div>\";\n" +
           "            }\n" +
           "            //console.log(menuStr);\n" +
           "            $(\"aside.Hui-aside\").html(menuStr);\n" +
           "            $(\".nav-toggle\").click(function () {\n" +
           "                $(\".Hui-aside\").slideToggle();\n" +
           "            });\n" +
           "            $(\".Hui-aside\").on(\"click\", \".menu_dropdown dd li a\", function () {\n" +
           "                if ($(window).width() < 768) {\n" +
           "                    $(\".Hui-aside\").slideToggle();\n" +
           "                }\n" +
           "            });\n" +
           "            /*左侧菜单*/\n" +
           "            $(\".Hui-aside\").Huifold({\n" +
           "                titCell: '.menu_dropdown dl dt',\n" +
           "                mainCell: '.menu_dropdown dl dd',\n" +
           "            });\n" +
           "        }\n" +
           "\t\tfunction recursionMenu(menu) {\n" +
           "\t\t\tif (menu) {\n" +
           "\t\t\t\tlet str = \"\";\n" +
           "\t\t\t\tstr += \"<dl \" + (menu.id_name ? \"id=\\\"\" + menu.id_name + \"\\\"\" : \"\") + \">\";\n" +
           "\t\t\t\tlet name1 = getName(menu, getLanguage());\n" +
           "\t\t\t\tstr += \"<dt><i class=\\\"Hui-iconfont\\\">\" + menu.icon + \"</i> \" + name1 + \"<i class=\\\"Hui-iconfont menu_dropdown-arrow\\\">&#xe6d5;</i></dt>\";\n" +
           "\t\t\t\tstr += \"<dd><ul>\";\n" +
           "\t\t\t\tfor (let i in menu.children) {\n" +
           "\t\t\t\t\tlet name = getName(menu.children[i], getLanguage());\n" +
           "\t\t\t\t\tstr += \"<li><a data-href=\\\"\" + menu.children[i].href\n" +
           "\t\t\t\t\t\t+ \"\\\" data-title=\\\"\" + name + \"\\\" href=\\\"javascript:;\\\">\" + name + \"</a></li>\";\n" +
           "\t\t\t\t\tlet temp = recursionMenu(menu.children[i].children);\n" +
           "\t\t\t\t\tif (temp == undefined) {\n" +
           "\t\t\t\t\t\tcontinue;\n" +
           "\t\t\t\t\t}\n" +
           "\t\t\t\t\telse {\n" +
           "\t\t\t\t\t\tstr += temp;\n" +
           "\t\t\t\t\t}\n" +
           "                }\n" +
           "                str += \"</ul></dd></dl>\";\n" +
           "\t\t\t\treturn str;\n" +
           "\t\t\t}\n" +
           "\t\t\treturn undefined;\n" +
           "\t\t}\n" +
           "        $(function () {\n" +
           "            $.getJSON(urls.category.menus, response => {\n" +
           "\t\t\t\tif (response.success) {\n" +
           "\t\t\t\t\tobj.menu = response.data;\n" +
           "\t\t\t\t\tupdateNav();\n" +
           "\t\t\t\t\tupdateMenu();\n" +
           "\t\t\t\t}\n" +
           "            });\n" +
           "            $.getJSON(urls.skin.get, response => {\n" +
           "\t\t\t\tif (response.success) {\n" +
           "\t\t\t\t\tobj.skin = response.data;\n" +
           "\t\t\t\t\tupdateSkin();\n" +
           "\t\t\t\t}\n" +
           "\t\t\t});\n" +
           "\t\t\t/*$(\"#min_title_list li\").contextMenu('Huiadminmenu', {\n" +
           "\t\t\t\tbindings: {\n" +
           "\t\t\t\t\t'closethis': function(t) {\n" +
           "\t\t\t\t\t\tconsole.log(t);\n" +
           "\t\t\t\t\t\tif(t.find(\"i\")){\n" +
           "\t\t\t\t\t\t\tt.find(\"i\").trigger(\"click\");\n" +
           "\t\t\t\t\t\t}\n" +
           "\t\t\t\t\t},\n" +
           "\t\t\t\t\t'closeall': function(t) {\n" +
           "\t\t\t\t\t\talert('Trigger was '+t.id+'\\nAction was Email');\n" +
           "\t\t\t\t\t},\n" +
           "\t\t\t\t}\n" +
           "\t\t\t});*/\n" +
           "\n" +
           "\n" +
           "\t\t\t$(\"body\").Huitab({\n" +
           "\t\t\t\ttabBar: \".navbar-wrapper .navbar-levelone\",\n" +
           "\t\t\t\ttabCon: \".Hui-aside .menu_dropdown\",\n" +
           "\t\t\t\tclassName: \"current\",\n" +
           "\t\t\t\tindex: 0,\n" +
           "\t\t\t});\n" +
           "\t\t});\n" +
           "\t\t/*个人信息*/\n" +
           "\t\tfunction myselfinfo() {\n" +
           "\t\t\tlayer.open({\n" +
           "\t\t\t\ttype: 1,\n" +
           "\t\t\t\tarea: ['300px', '200px'],\n" +
           "\t\t\t\tfix: false, //不固定\n" +
           "\t\t\t\tmaxmin: true,\n" +
           "\t\t\t\tshade: 0.4,\n" +
           "\t\t\t\ttitle: '查看信息',\n" +
           "\t\t\t\tcontent: '<div>管理员信息</div>'\n" +
           "\t\t\t});\n" +
           "\t\t}\n" +
           "\n" +
           "\t\t/*资讯-添加*/\n" +
           "\t\tfunction article_add(title, url) {\n" +
           "\t\t\tvar index = layer.open({\n" +
           "\t\t\t\ttype: 2,\n" +
           "\t\t\t\ttitle: title,\n" +
           "\t\t\t\tcontent: url\n" +
           "\t\t\t});\n" +
           "\t\t\tlayer.full(index);\n" +
           "\t\t}\n" +
           "\t\t/*图片-添加*/\n" +
           "\t\tfunction picture_add(title, url) {\n" +
           "\t\t\tvar index = layer.open({\n" +
           "\t\t\t\ttype: 2,\n" +
           "\t\t\t\ttitle: title,\n" +
           "\t\t\t\tcontent: url\n" +
           "\t\t\t});\n" +
           "\t\t\tlayer.full(index);\n" +
           "\t\t}\n" +
           "\t\t/*产品-添加*/\n" +
           "\t\tfunction product_add(title, url) {\n" +
           "\t\t\tvar index = layer.open({\n" +
           "\t\t\t\ttype: 2,\n" +
           "\t\t\t\ttitle: title,\n" +
           "\t\t\t\tcontent: url\n" +
           "\t\t\t});\n" +
           "\t\t\tlayer.full(index);\n" +
           "\t\t}\n" +
           "\t\t/*用户-添加*/\n" +
           "\t\tfunction member_add(title, url, w, h) {\n" +
           "\t\t\tlayer_show(title, url, w, h);\n" +
           "\t\t}\n" +
           "\n" +
           "\n" +
           "\t</script>\n" +
           "\n" +
           "\t\n" +
           "\n" +
           "</body>\n" +
           "</html>";
  }
}
