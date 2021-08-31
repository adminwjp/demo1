  <script>



            /****返回顶部js代码 add by zhoums*****/
            $(function () {
                $("#topid").hide();
            });
            $(window).scroll(function () {
                if ($(window).scrollTop() > 0) {
                    if ($(window).scrollTop() > 100) {
                        $("#topid").show();
                    }
                }
                else {
                    $("#topid").hide();
                }
            })
            $(document).on("click", "#topid", function () {
                $('body,html').animate({ scrollTop: 0 }, 700);
            });


            /***************end add*************/

            require(['jquery', 'tnlayer', 'signalr.hubs'], function ($, tnlayer) {

                var jschars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
                function generateMixed(n) {
                    var res = "";
                    for (var i = 0; i < n; i++) {
                        var id = Math.ceil(Math.random() * 35);
                        res += jschars[id];
                    }
                    return res;
                }

                function htmlEncode(str) {
                    var div = document.createElement("div");
                    div.appendChild(document.createTextNode(str));
                    return div.innerHTML;
                }

                function htmlDecode(str) {
                    var div = document.createElement("div");
                    div.innerHTML = str;
                    return div.innerText;
                }

                $(function () {
                    $("#randomSignalr").val(generateMixed(9));
                    //判断是否有发送通知权限
                    if (window.Notification && Notification.permission !== "granted") {
                        //向用户请求获取通知权限
                        Notification.requestPermission(function (status) {
                            if (Notification.permission != status) {
                                Notification.permission = status;
                            }
                        })
                    };
                    if ($.connection.NoticeHub != null) {
                        var chat = $.connection.NoticeHub;
                        $.connection.hub.logging = false;
                        // 设置查询字符串
                        $.connection.hub.qs = { "randomSignalr": $("#randomSignalr").val() };


                        //停止其他链接
                        chat.client.hubStop = function hubStop(randomSignalr) {
                            if (randomSignalr != $("#randomSignalr").val()) {
                                $.connection.hub.stop();
                            }
                        }
                        //初始化连接
                        chat.client.noticeUser = function sendNotice(id, message, userAvatarUrl, relativeObjectUrl) {

                            if (localStorage["lastNoticeId"] != id) {
                                localStorage["lastNoticeId"] = id;
                                //如果用户允许发送通知 推送通知
                                if (window.Notification && Notification.permission === "granted") {
                                    var n = new window.Notification(message, { icon: userAvatarUrl });
                                    if (relativeObjectUrl.length > 0) {
                                        n.onclick = function () {
                                            window.open(relativeObjectUrl);
                                        }
                                    }
                                }
                                else if (window.Notification && Notification.permission !== "denied") {
                                    Notification.requestPermission(function (status) {
                                        if (Notification.permission !== status) {
                                            Notification.permission = status;
                                        }
                                        if (status === "granted") {
                                            var n = new Notification(message);
                                        }
                                        else {
                                            if (relativeObjectUrl.length > 0) {
                                                layer.alert(htmlDecode(message), function () {
                                                    window.open(relativeObjectUrl);
                                                });
                                            } else {
                                                layer.alert(htmlDecode(message));
                                            }
                                        }
                                    })
                                }
                                else {
                                    if (relativeObjectUrl.length > 0) {
                                        layer.alert(htmlDecode(message), function () {
                                            window.open(relativeObjectUrl);
                                        });
                                    } else {
                                        layer.alert(htmlDecode(message));
                                    }
                                }
                            }
                        }
                        $.connection.hub.start();
                    }
                });


                //断开hub连接
                $("a[href='/account/signout']").click(function () {
                    $.connection.hub.stop();
                })

            })

        </script>