package com.utility.sdk.wxp.api.ips;

import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;
import com.utility.util.StringUtil;

/// <summary>
    /// <para > 获取微信服务器IP地址</para>
    /// <![CDATA[
    /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140187
    /// ]]>
    /// </summary>
    public class IpApi extends BaseApi
    {
        /// <summary>获取微信服务器IP地址</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static String Get(String access_token)
        {
            if (StringUtil.isEmpty(access_token))
            {
                throw  new NullPointerException("access_token  param is null");
            }
            String result = HttpUtils.doGet(IpApi.Domain+"cgi-bin/getcallbackip?access_token="+access_token);
            return result;
        }

    }

