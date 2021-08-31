/*
package com.utility.sdk.wxp.api.MessageManages;

import com.utility.http.HttpFactory;
import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;

import java.io.File;

/// <summary>
    /// 消息管理
    /// </summary>
    public class MessageManageApi extends BaseApi
    {
       //#region 客服消息 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547

        /// <summary>
        /// 添加 1 修改 2 删除 3 客服帐号
        /// <![CDATA[
        /// https://api.weixin.qq.com/customservice/kfaccount/add?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String OperatorKfAccount(String kf_account, String nickname, String password, String access_token,int flag) {
            String o="add";
            switch (flag){
                case  2:
                    o="update";
                    break;
                case  3:
                    o="del";
                    break;
            }
            return  HttpUtils.doPost(MessageManageApi.Domain+"customservice/kfaccount/"+o+"?access_token="+access_token, "{\"kf_account\":\"" + kf_account + "\",\"nickname\":\"" + nickname + "\",\"password\":\"" + password + "\"}", HttpFactory.ApplicationJson);
        }





        /// <summary>
        /// 设置客服帐号的头像
        /// <![CDATA[
        /// http://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token=ACCESS_TOKEN&kf_account=KFACCOUNT
        /// ]]>
        /// </summary>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="headImg">客服人员的头像，头像图片文件必须是jpg格式，推荐使用640*640大小的图片以达到最佳效果</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String UploadKfAccountHeadImg(String kf_account, File headImg, String access_token) {
            //HttpHelper.Upload($"{MessageManageApi.Domain}customservice/kfaccount/uploadheadimg?access_token={access_token}&kf_account={ kf_account}", headImg);
            return  "";
        }


        /// <summary>
        /// 获取所有客服账号
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String GetKfList(String access_token) {
            return  HttpUtils.doGet(MessageManageApi.Domain+"customservice/getkflist?access_token="+access_token);
        }

       // #region 客服接口-发消息 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547

        /// <summary>
        /// 发送文本消息
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        */
/*****
         * 
         * /// <!--
        /// {\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"HelloWorld\"}}
        /// -->
        /// <para><xml></para>
        /// <para><ToUserName><![CDATA[toUser]]></ToUserName></para>
        /// <para><FromUserName><![CDATA[fromUser]]></FromUserName></para>
        /// <para> <CreateTime>1348831860</CreateTime></para>
        /// <para><MsgType><![CDATA[text]]></MsgType></para>
        /// <para><Content><![CDATA[this is a test]]></Content></para>
        /// <para><MsgId>1234567890123456</MsgId></para>
        /// <para> </xml></para>
         * ******//*

        public static String SendMsg(String touser, String content, String access_token) {
            return HttpUtils.doPost(MessageManageApi.Domain+"message/custom/send?access_token="+access_token, "{\"touser\":\"" + touser + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + content + "\"}}",HttpFactory.ApplicationJson);
        }

        /// <summary>
        /// 发送图片消息 
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendImage(String touser, String media_id, String access_token) {
            return  HttpUtils.doPost(MessageManageApi.Domain+"message/custom/send?access_token="+access_token, "{\"touser\":\"" + touser + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + media_id + "\"}}",HttpFactory.ApplicationJson);
        }

        /// <summary>
        /// 发送语音消息 
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendVoice(String touser, String media_id, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"voice\",\"voice\":{\"media_id\":\"" + media_id + "\"}}");


        /// <summary>
        /// 发送视频消息 
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="thumb_media_id">缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
        /// <param name="description">图文消息/视频消息/音乐消息的描述</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendVideo(String touser, String media_id, String thumb_media_id, String title, String description, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"video\",\"video\":{\"media_id\":\"" + media_id + "\",\"thumb_media_id\":\"" + thumb_media_id + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}}");

        /// <summary>
        /// 发送音乐消息
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
        /// <param name="description">图文消息/视频消息/音乐消息的描述</param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumb_media_id">缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendMusic(String touser, String title, String description, String musicurl, String hqmusicurl, String thumb_media_id, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"music\",\"music\":{\"title\":\"" + title + "\",\"description\":\"" + description + "\",\"musicurl\":\"" + musicurl + "\",\"hqmusicurl\":\"" + hqmusicurl + "\",\"thumb_media_id\":\"" + thumb_media_id + "\"}}");

        /// <summary>
        /// 发送图文消息（点击跳转到外链） 图文消息条数限制在1条以内，注意，如果图文数超过1，则将会返回错误码45008。
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
        /// <param name="description">	图文消息/视频消息/音乐消息的描述</param>
        /// <param name="url">图文消息被点击后跳转的链接</param>
        /// <param name="picurl">图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendNews(String touser, String title, String description, String url, String picurl, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"news\",\"news\":{\"articles\":[{\"title\":\"" + title + "\",\"description\":\"" + description + "\",\"url\":\"" + url + "\",\"picurl\":\"" + picurl + "\"}]}}");

        /// <summary>
        /// 发送图文消息（点击跳转到图文消息页面） 图文消息条数限制在1条以内，注意，如果图文数超过1，则将会返回错误码45008。
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendMpnews(String touser, String media_id, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"mpnews\",\"mpnews\":{\"media_id\":\"" + media_id + "\"}}");

        /// <summary>
        /// 发送卡券
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="card_id">card_id</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendWxcard(String touser, String card_id, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"wxcard\",\"wxcard\":{\"card_id\":\"" + card_id + "\"}}");

        /// <summary>
        /// 发送小程序卡片（要求小程序与公众号已关联）
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="title">图文消息/视频消息/音乐消息/小程序卡片的标题</param>
        /// <param name="appid">小程序的appid，要求小程序的appid需要与公众号有关联关系</param>
        /// <param name="pagepath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumb_media_id">缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendMiniprogrampage(String touser, String title, String appid, String pagepath, String thumb_media_id, string access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"miniprogrampage\",\"miniprogrampage\":{\"title\":\"" + title + "\",\"appid\":\"" + appid + "\",\"pagepath\":\"" + pagepath + "\",\"thumb_media_id\":\"" + thumb_media_id + "\"}}");

        /// <summary>
        /// 发送文本消息 请注意，如果需要以某个客服帐号来发消息（在微信6.0.2及以上版本中显示自定义头像），则需在JSON数据包的后半部分加入customservice参数，例如发送文本消息则改为：
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="kf_account">客服帐号</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendMsgtype(String touser, String content, String kf_account, String access_token) => HttpHelper.PostJson($"{MessageManageApi.Domain}message/custom/send?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + content + "\"},\"customservice\":{\"kf_account\":\"" + kf_account + "\"}}");

        /// <summary>
        /// 客服输入状态
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/custom/typing?access_token=ACCESS_TOKEN
        /// ]]> 
        /// </summary>
        /// <param name="touser">普通用户openid</param>
        /// <param name="command">"Typing"：对用户下发“正在输入"状态 "CancelTyping"：取消对用户的”正在输入"状态</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String CustomTyping(String touser, String command, String access_token)=> HttpHelper.PostJson($"{MessageManageApi.Domain}cgi-bin/message/custom/typing?access_token={access_token}", "{\"touser\":\"" + touser + "\",\"command\":\"" + command + "\"}");

       // #endregion 客服接口-发消息


        //#endregion 客服消息


        //#region 群发接口和原创校验 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1481187827_i0l21


        /// <summary>
        /// 上传图文消息内的图片获取URL【订阅号与服务号认证后均可用】
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String Uploadimg(String access_token)
        {
           throw new NotImplementedException();
        }

        /// <summary>
        /// 上传图文消息素材【订阅号与服务号认证后均可用】
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String Uploadnews(String param, String access_token)
        { 
            Dictionary<string, object> data = JsonHelper.ToObject<Dictionary<string, object>>(param);
            //图文消息，一个图文消息支持1到8条图文
            if (data.ContainsKey("articles"))
            {
                List<Dictionary<string, object>> articles = data["articles"] as List<Dictionary<string, object>>;
                if (articles == null || articles.Count == 0)
                {
                    throw new NotSupportedException("图文消息，一个图文消息支持1到8条图文");
                }
                else
                {
                    Action<Dictionary<string, object>, string> validata = (it, key) =>
                    {
                        if (it.ContainsKey(key) || string.IsNullOrEmpty(it[key].ToString()))
                        {
                            throw new ArgumentException($"param {key} not null");
                        }
                    };
                    foreach (Dictionary<string, object> item in articles)
                    {
                        validata(item, "thumb_media_id");//图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
                        validata(item, "author");//图文消息的作者
                        validata(item, "title");//图文消息的标题
                        validata(item, "content_source_url");//在图文消息页面点击“阅读原文”后的页面，受安全限制，如需跳转Appstore，可以使用itun.es或appsto.re的短链服务，并在短链后增加 #wechat_redirect 后缀。
                        validata(item, "content");//图文消息页面的内容，支持HTML标签。具备微信支付权限的公众号，可以使用a标签，其他公众号不能使用，如需插入小程序卡片，可参考下文。
                        validata(item, "digest");//图文消息的描述，如本字段为空，则默认抓取正文前64个字
                        validata(item, "show_cover_pic");//是否显示封面，1为显示，0为不显示
                        validata(item, "need_open_comment");//Uint32 是否打开评论，0不打开，1打开
                        validata(item, "only_fans_can_comment");//Uint32 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
                    }
                }
            }
            else
            {
                throw new ArgumentException("param articles not null");
            }
            return HttpHelper.PostJson($"{MessageManageApi.Domain}cgi-bin/media/uploadnews?access_token={access_token}", param);
        }

        /// <summary>
        /// 根据标签进行群发【订阅号与服务号认证后均可用】 图文消息（注意图文消息的media_id需要通过上述方法来得到）
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String SendAllMpnews(String media_id, String access_token) {
            return  HttpUtils.doPost(MessageManageApi.Domain+"cgi-bin/message/mass/sendall?access_token={access_token}", "{\"filter\":{\"is_to_all\":false,\"tag_id\":2},\"mpnews\":{\"media_id\":\"" + media_id + "\"},\"msgtype\":\"mpnews\",\"send_ignore_reprint\":0}",HttpFactory.ApplicationJson);
        }

        //#endregion 群发接口和原创校验
    }

*/
