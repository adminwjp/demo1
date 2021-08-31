define(['jquery', 'tnlayer'], function ($, tnlayer) {
    String.prototype.format = function () {
        var args = [].slice.call(arguments);
        return this.replace(/(\{\d+\})/g, function (a) {
            return args[+(a.substr(1, a.length - 2)) || 0];
        });
    };


    $(function myfunction() {
        var ua = navigator.userAgent;
        if (ua.indexOf("compatible") === -1) return;
        var ieIndex = ua.indexOf("MSIE");
        if (ieIndex === -1) return;
        var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
        reIE.test(ua);
        var IEVersion = +RegExp["$1"];
        if (IEVersion >= 9) return;
        
        var fls = flashChecker();
        var s = "";
        if (!fls.f && fls.v < 19)
            tnlayer.layeralert(2, "您还没有安装flash插件, 请点击<a href='/lib/flash/flash_player.exe'>下载</a>安装flash插件，并刷新本页面");
    });

    function flashChecker() {

        var hasFlash = 0;　　　　//是否安装了flash
        var flashVersion = 0;　　//flash版本

        if (document.all) {
            try {
                var swf = new ActiveXObject('ShockwaveFlash.ShockwaveFlash');
                if (swf) {
                    hasFlash = 1;
                    VSwf = swf.GetVariable("$version");
                    flashVersion = parseInt(VSwf.split(" ")[1].split(",")[0]);
                }
            } catch (e) {

            }

        } else {
            if (navigator.plugins && navigator.plugins.length > 0) {
                var swf = navigator.plugins["Shockwave Flash"];
                if (swf) {
                    hasFlash = 1;
                    var words = swf.description.split(" ");
                    for (var i = 0; i < words.length; ++i) {
                        if (isNaN(parseInt(words[i]))) continue;
                        flashVersion = parseInt(words[i]);
                    }
                }
            }
        }
        return { f: hasFlash, v: flashVersion };
    }
})
