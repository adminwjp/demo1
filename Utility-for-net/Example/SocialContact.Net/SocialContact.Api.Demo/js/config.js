var baseurl = undefined;
var configjs = document.getElementById('configjs');
if (configjs) {
    baseurl = configjs.getAttribute('src').replace(/config.js$/, '');
    window.UEDITOR_HOME_URL = baseurl + '../js/lib/ueditor/';
}
require.config({
    baseUrl: baseurl !== undefined ? baseurl : './js',
    paths: {
        //必备
        'jquery': 'jquery',
        'bootstrap': 'bootstrap.min',
        'modernizr': 'modernizr.min',
        'site': 'site',
        //工具包
        'livequery': 'lib/jquery.livequery',
        'main': 'main',
        'lodash': 'lib/lodash',
        'unobtrusive': 'tn.common.unobtrusive',
        'store': 'lib/store',
        //侧边栏
        'sideNav': 'tn.sideNav',
        //弹出层
        'layer': 'lib/layer/layer',
        'tnlayer': 'tn.layer',
        //表单验证
        'validate': 'lib/jquery-validation/dist/jquery.validate',
        'validate.unobtrusive': 'lib/jquery-validation/dist/jquery.validate.unobtrusive',
        //ajaxForm表单异步提交
        'form': 'form',
        'blockUI': 'lib/ajaxForm/jquery.blockUI',
        'jqueryform': 'lib/ajaxForm/jquery.form',
        'placeholder': 'lib/jquery.placeholder.min',
        //百度编辑器
        'ueconfig': 'lib/ueditor/ueditor.config',
        'ueditorall': 'lib/ueditor/ueditor.all',
        'ueditor': 'lib/ueditor/ueditor.init',
        'ZeroClipboard': 'lib/ueditor/third-party/zeroclipboard/ZeroClipboard',
        //标签
        'tokenfield': 'lib/tags/js/bootstrap-tokenfield',
        'tag': 'tn.tag',
        //时间选择器
        'moment': 'lib/daterangepicker/moment',
        'daterangepickerjs': 'lib/daterangepicker/daterangepicker',
        'datetimepicker': 'lib/datetimepicker/js/datetimepicker',
        'datetimepicker.zh-CN': 'lib/datetimepicker/js/locales/datetimepicker.zh-CN',
        'datepicker': 'tn.datepicker',
        'daterangepicker': 'tn.daterangepicker',
        //page分页
        'page': 'tn.page',
        //webuploader上传
        // 'webuploader': 'lib/webuploader/webuploader',
        // 'uploader': 'tn.uploader',
        // plupload上传
        'moxie': './lib/plupload/moxie.min',
        'plupload': './lib/plupload/plupload.min',
        'plupload.zh-CN': './lib/plupload/i18n/zh_CN',
        'uploader': 'tn.uploader',
        'uploader.ui.titleimage': './lib/plupload/ui/titleimage',
        //zTree
        'ztree': 'lib/zTree/js/jquery.ztree.all',
        'ztreeexhide': 'lib/zTree/js/jquery.ztree.exhide',
        'userSelector': 'tn.userSelector',
        //bootstrap-select 下拉
        'select': 'lib/bootstrap-select/js/bootstrap-select',
        'selectdefaults': 'lib/bootstrap-select/js/i18n/defaults-zh_CN',
        //fancyBox相册
        'mousewheel': 'lib/fancyBox/lib/jquery.mousewheel.pack',
        'fancybox': 'lib/fancyBox/source/jquery.fancybox',
        'tnfancyBox': 'lib/fancyBox/lib/tnfancyBox',
        //cxselect联动下拉
        'cxselect': 'jquery.cxselect.min',
        'linkageDropDownList': 'linkageDropDownList',
        //tooltip
        'tooltip': 'lib/tooltip',
        //tooltip 用户卡片
        'jquery.tipsy': 'lib/tipsy/jquery.tipsy',
        'tipsyhovercard': 'lib/tipsy/jquery.tipsy.hovercard',
        'tntipsy': 'lib/tipsy/tntipsy',
        //异步修改url
        'histroy': 'lib/jquery.histroy',
        //懒加载
        'lazyload': 'lib/jquery.lazyload',
        //qqFace表情
        'qqFace': 'lib/qqFace/js/jquery.qqFace',
        'browser': 'lib/qqFace/js/jquery-browser',
        //jqueryui
        'jqueryui': 'lib/jquery-ui',
        //slider-pro 幻灯片
        'sliderpro': 'lib/slider-pro/js/jquery.sliderPro',
        //jPlayer視頻播放
        'jplayer': 'lib/jPlayer/jquery.jplayer',
        //jcrop 图片裁剪 头像
        'jcrop': 'lib/Jcrop/js/Jcrop',
        //onscroll
        'onscroll': 'tn.onscroll',
        //地区选择
        'linkageDropDownList': 'linkageDropDownList',
        //Signalr
        "signalr.core": "lib/signalr/jquery.signalR",
        //这里加两个点是用相对路径
        "signalr.hubs": "../signalr/hubs?",

        //签到      
        'calendar': 'lib/SignIn/calendar',
        //echarts
        'echarts': 'echarts.min',
        //饼图、柱形图
        'tnchart': 'tn.chart',
        //Pdf
        'pdfjscompatibility': 'lib/pdfjs/compatibility',
        'pdfjsl10n': 'lib/pdfjs/l10n',
        'pdfjs': 'lib/pdfjs/build/pdf',
        'pdfjsdebugger': 'lib/pdfjs/debugger',
        'pdf': 'lib/pdfjs/viewer',
        //在线预览
        'flexpaper': 'lib/FlexPaper/js/flowpaper',
        'FlowPaperViewer': 'lib/FlexPaper/js/FlowPaperViewer',
        'flexpaper_handlers': 'lib/FlexPaper/js/flowpaper_handlers',
        'extensions': 'lib/FlexPaper/js/jquery.extensions.min',
        //判断浏览器是否PC端
        'IsPC': 'tn_IsPC',

        //图片放大
        'jqzoom': 'lib/jqzoom/jquery.jqzoom',
        'jqzoompack': 'lib/jqzoom/jqzoom.pack',

        //导航下拉
        'superfish': 'lib/superfish/js/superfish',
        'hoverIntent': 'lib/superfish/js/hoverIntent',

    },
    shim: {
        'jquery': {
            exports: 'jquery'
        },
        'bootstrap': {
            deps: ['jquery']
        },
        'modernizr': {
            deps: ['jquery']
        },

        'unobtrusive': {
            deps: ['jquery']
        },
        'store': {
            deps: ['jquery'],
            exports: 'store'
        },
        //侧边栏
        'sideNav': {
            deps: ['jquery']
        },
        'main': {
            deps: ['jquery', 'sideNav']
        },
        //ajax表单异步提交
        'form': {
            deps: ['jquery', 'jqueryform'],
            exports: 'form'
        },
        'blockUI': {
            deps: ['jquery', 'form']
        },
        'jqueryform': {
            deps: ['jquery']
        },
        //弹出层
        'layer': {
            deps: ['jquery']
        },
        'tnlayer': {
            deps: ['jquery', 'layer'],
            exports: 'layer'

        },
        //时间选择器
        'moment': {
            expotrs: 'moment'
        },
        'daterangepicker': {
            deps: ['moment']
        },
        'datetimepicker.zh-CN': {
          deps: ['datetimepicker'],
        },
        'datepicker': {
            deps: ['jquery', 'moment', 'daterangepicker']
        },
        //page分页
        'page': {
            deps: ['jquery','livequery']
        },
        //webuploader上传
        // 'webuploader': {
        //     expotrs: 'WebUploader'
        // },
        // 'uploader': {
        //     deps: ['jquery', 'webuploader']
        // },
        // plupload上传
        'plupload.zh-CN': {
            deps: ['plupload'],
        },
        //表单验证
        'validate': {
            deps: ['jquery'],
            expotrs: 'validate'
        },
        'validate.unobtrusive': {
            deps: ['jquery', 'livequery', 'validate']
        },
        //zTree
        'ztreeexhide': {
            deps: ['jquery', 'ztree']
        },
        'ztree': {
            deps: ['jquery', 'store']
        },
        'userSelector': {
            deps: ['jquery', 'store', 'ztree']
        },
        //百度编辑器
        'ueconfig': {
            deps: ['jquery']
        },
        'ueditorall': {
            deps: ['jquery', 'ueconfig']
        },
        'ueditor': {
            deps: ['jquery', 'ueconfig', 'ueditorall']
        },
        //bootstrap-select 下拉

        'selectdefaults': {
            deps: ['jquery', 'select']
        },
        //fancybox相册
        'mousewheel': {
            deps: ['jquery']
        },
        'fancybox': {
            deps: ['jquery', 'mousewheel']
        },
        'tnfancyBox': {
            deps: ['jquery', 'fancybox']
        },
        //标签
        'tokenfield': {
            deps: ['jquery', 'bootstrap']
        },
        'tag': {
            deps: ['jquery', 'tokenfield']
        },
        //cxselect联动下拉
        'cxselect': {
            deps: ['jquery']
        },
        'linkageDropDownList': {
            deps: ['jquery', 'cxselect']
        },
        //用户卡片
        'jquery.tipsy': {
            deps: ['jquery']
        },
        'tipsyhovercard': {
            deps: ['jquery', 'jquery.tipsy']
        },
        'tntipsy': {
            deps: ['jquery', 'jquery.tipsy', 'tipsyhovercard']
        },
        //异步修改url
        'history': {
            deps: ['jquery']
        },
        //懒加载
        'lazyload': {
            deps: ['jquery']
        },
        //qqFace表情
        'browser': {
            deps: ['jquery']
        },
        'qqFace': {
            deps: ['jquery', 'browser']
        },
        //slider-pro幻灯片
        'sliderpro': {
            deps: ['jquery']
        },
        //jPlayer視頻播放
        'jplayer': {
            deps: ['jquery']
        },
        //jcrop  头像裁剪头像
        'jcrop': {
            deps: ['jquery']
        },
        //onscroll
        'onscroll': {
            deps: ['jquery']
        },
        //地区
        'linkageDropDownList': {
            deps: ['jquery']
        },
        //signalR
        'signalr.core': {
            deps: ['jquery'],
            exports: "$.connection"
        },
        "signalr.hubs": {
            deps: ["signalr.core"]
        },

        "calendar": {
            deps: ["jquery"]
        },
        //饼图、柱形图
        'tnchart': {
            deps: ['jquery', 'echarts']
        },
        //pdf
        'pdf': {
            deps: ['jquery', 'pdfjscompatibility', 'pdfjsl10n', 'pdfjs', 'pdfjsdebugger']
        },

        //在线预览
        'flexpaper': {
            deps: ['jquery']
        },
        'flexpaper_handlers': {
            deps: ['jquery']
        },
        'extensions': {
            deps: ['jquery']
        },
        'jqzoom': {
            deps: ['jquery']
        },
        'jqzoompack': {
            deps: ['jquery','jqzoom']
        },
        //导航下拉
        'superfish': {
            deps: ['jquery']
        }
    },
    waitSeconds:0
})
