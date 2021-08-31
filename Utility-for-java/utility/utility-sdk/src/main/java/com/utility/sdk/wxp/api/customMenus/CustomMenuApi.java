package com.utility.sdk.wxp.api.customMenus;

import com.utility.http.HttpFactory;
import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;


/// <summary>
/// 自定义菜单
/// <![CDATA[
/// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141013
/// ]]>
/// </summary>
 public class CustomMenuApi extends BaseApi
{
        /// <summary>
        /// 自定义菜单创建接口
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String CreateCustomMenu(String param, String access_token) {
           return HttpUtils.doPost(CustomMenuApi.Domain+"cgi-bin/menu/create?access_token="+access_token, param, HttpFactory.ApplicationJson);
        }

        /// <summary>自定义菜单查询接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String Get(String access_token) {
            return  HttpUtils.doGet(CustomMenuApi.Domain+"cgi-bin/menu/get?access_token="+access_token);
        }

        /// <summary>自定义菜单删除接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String Delete(String access_token) {
           return HttpUtils.doGet(CustomMenuApi.Domain+"cgi-bin/menu/delete?access_token="+access_token);
        }

        //region 个性化菜单接口
        //////=============================================================================
        //查询个性化菜单
        //使用普通自定义菜单查询接口可以获取默认菜单和全部个性化菜单信息，请见自定义菜单查询接口的说明。


        //删除所有菜单
        //使用普通自定义菜单删除接口可以删除所有自定义菜单（包括默认菜单和全部个性化菜单），请见自定义菜单删除接口的说明。
        //////=============================================================================


        /// <summary>
        /// 创建个性化菜单
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/menu/addconditional?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String CreateConditionalMenu(String param, String access_token){
            return  HttpUtils.doPost(CustomMenuApi.Domain+"cgi-bin/menu/addconditional?access_token="+access_token, param,HttpFactory.ApplicationJson);
        }

        /// <summary>自定义菜单删除接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="menuid">menuid为菜单id，可以通过自定义菜单查询接口获取。</param>
        /// <returns></returns>
        public static String Delconditional(String menuid, String access_token) {
            return  HttpUtils.doPost(CustomMenuApi.Domain
                    +"cgi-bin/menu/delete?access_token="+access_token,
                    "{\"menuid\":\"" + menuid + "\"}",HttpFactory.ApplicationJson);
        }

        /// <summary>测试个性化菜单匹配结果</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/trymatch?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="user_id">user_id可以是粉丝的OpenID，也可以是粉丝的微信号。</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String Trymatch(String user_id, String access_token) {
           return HttpUtils.doPost(CustomMenuApi.Domain+"cgi-bin/menu/trymatch?access_token="+access_token, "{\"user_id\":\"" + user_id + "\"}",HttpFactory.ApplicationJson);
        }

        //endregion 个性化菜单接口

        /// <summary>获取自定义菜单配置接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String getCurrentSelfmenuInfo(String access_token) {
            return  HttpUtils.doGet(CustomMenuApi.Domain+"cgi-bin/get_current_selfmenu_info?access_token="+access_token);
        }
    }
