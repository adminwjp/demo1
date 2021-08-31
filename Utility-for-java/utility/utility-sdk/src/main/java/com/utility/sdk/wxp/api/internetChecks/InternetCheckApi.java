package com.utility.sdk.wxp.api.internetChecks;

import com.utility.http.HttpFactory;
import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;
import com.utility.util.StringUtil;

 /// <summary>
    /// 网络检测
    /// <![CDATA[
    /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=21541575776DtsuT
    /// ]]>
    /// </summary>
    public class InternetCheckApi  extends BaseApi {
     /// <summary>网络检测</summary>
     ///<![CDATA[
     ///https://api.weixin.qq.com/cgi-bin/callback/check?access_token=ACCESS_TOKEN
     /// ]]>
     /// <param name="action">执行的检测动作，允许的值：dns（做域名解析）、ping（做ping检测）、all（dns和ping都做）</param>
     /// <param name="check_operator">指定平台从某个运营商进行检测，允许的值：CHINANET（电信出口）、UNICOM（联通出口）、CAP（腾讯自建出口）、DEFAULT（根据ip来选择运营商）</param>
     /// <param name="access_token">access_token</param>
     /// <returns></returns>
     public static String Check(String action, String check_operator, String access_token) throws Exception {
         if (StringUtil.isEmpty(access_token)) {
             throw new NullPointerException("access_token  param is null");
         }
         if (StringUtil.isEmpty(action)) {
             throw new NullPointerException("action  param is null");
         }
         if (StringUtil.isEmpty(check_operator)) {
             throw new NullPointerException("action  param is null");
         }
         //检测值是否符合条件
         switch (action.toLowerCase()) {
             case "dns":
             case "ping":
             case "all":
                 break;
             default:
                 throw new Exception("执行的检测动作，允许的值：dns（做域名解析）、ping（做ping检测）、all（dns和ping都做）");
         }
         switch (check_operator.toUpperCase()) {
             case "CHINANET":
             case "UNICOM":
             case "CAP":
             case "DEFAULT":
                 break;
             default:
                 throw new Exception("执行的检测动作，允许的值：dns（做域名解析）、ping（做ping检测）、all（dns和ping都做）");
         }
         String result = HttpUtils.doPost(InternetCheckApi.Domain + "cgi-bin/callback/check?access_token=" + access_token, "{\"action\":" + action + ",\"check_operator\":" + check_operator + "}", HttpFactory.ApplicationJson);
         return result;
     }


     /// <summary>
     /// 错误信息
     /// </summary>
     /// <param name="errorCode">错误码</param>
     public static String GetMsg(int errorCode) {
         switch (errorCode) {
             case 40202:
                 return "不正确的action";
             case 40203:
                 return "不正确的check_operator";
             case 40201:
                 return "不正确的URL，一般是开发者未设置回调URL。";
             default:
                 return "";
         }
     }

 }
