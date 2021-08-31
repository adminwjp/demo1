package com.utility.sdk.wxp.api.controller;

import com.utility.sdk.wxp.api.accessToken.AccessTokenService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("api")
public class AccessTokenController {
    /// <summary> 获取AccessToken
    /// <![CDATA[
    ///https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
    /// ]]>
    /// </summary>
    /// <param name="grant_type">获取access_token填写client_credential</param>
    /// <param name="appid">第三方用户唯一凭证</param>
    /// <param name="secret">第三方用户唯一凭证密钥，即appsecret </param>
    /// <returns></returns>
    @GetMapping("token/{grant_type}/{appid}/{secret}")
    public Object GetAccessToken(String grant_type, String appid, String secret) {
        return  null;
       // return AccessTokenService.GetAccessToken(grant_type,appid,secret);
    }
}
