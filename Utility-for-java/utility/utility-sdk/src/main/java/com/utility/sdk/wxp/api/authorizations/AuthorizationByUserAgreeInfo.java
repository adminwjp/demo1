package com.utility.sdk.wxp.api.authorizations;

import lombok.Data;

/**
 * 用户同意授权，获取code code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，
 * code只能使用一次，5分钟未被使用自动过期。
 * */
@Data
public class AuthorizationByUserAgreeInfo {
    // 公众号的唯一标识
    String appid;
    // 授权后重定向的回调链接地址， 请使用 urlEncode 对链接进行处理
    String redirectUri;
    // 返回类型，请填写code
    String responseType;
    ///应用授权作用域，snsapi_base
    // （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo
    // （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，
    // 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
    String scope;
    // 重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字
    String state;
    // 无论直接打开还是做页面302重定向时候，必须带此参数
    String wechatRedirec = "#wechat_redirect";
}
