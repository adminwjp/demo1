package com.utility.sdk.wxp.api.accessToken;

import com.utility.sdk.wxp.api.BaseApi;
import com.utility.sdk.wxp.api.accessToken.dto.CreateAccessTokenInput;
import com.utility.sdk.wxp.api.accessToken.dto.GetAccessTokenDto;
import com.utility.util.HttpUtils;
import com.utility.util.JsonUtil;

/**
 * AccessToken Api
* <![CDATA[
* https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140183
*  ]]>
 **/
public class AccessTokenApi extends BaseApi {

    /**
     * 获取AccessToken
     * <![CDATA[
     * https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
     * ]]>
     * @param tokenInfo access_token 请求参数
     * @return
     */
    public static GetAccessTokenDto GetAccessTokenResult(CreateAccessTokenInput tokenInfo) {
        String result = HttpUtils.doGet(AccessTokenApi.Domain + "cgi-bin/token?grant_type=" + tokenInfo.getGrantType() + "&appid=" + tokenInfo.getAppid() + "&secret=" + tokenInfo.getSecret());
        GetAccessTokenDto data = JsonUtil.toObject(result, GetAccessTokenDto.class);
        return data;
    }

    /**
     * 错误信息
     * @param errorCode 错误码
     * @return
     */
    public static String GetMsg(int errorCode) {
        switch (errorCode) {
            case -1:
                return "系统繁忙，此时请开发者稍候再试";
            case 0:
                return "请求成功";
            case 40001:
                return "AppSecret错误或者AppSecret不属于这个公众号，请开发者确认AppSecret的正确性";
            case 40002:
                return "请确保grant_type字段值为client_credential";
            case 40164:
                return "调用接口的IP地址不在白名单中，请在接口IP白名单中进行设置。（小程序及小游戏调用不要求IP地址在白名单内。）";
            default:
                return "";
        }
    }

}
