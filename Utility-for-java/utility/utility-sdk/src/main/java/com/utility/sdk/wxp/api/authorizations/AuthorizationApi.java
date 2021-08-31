package com.utility.sdk.wxp.api.authorizations;

import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;
import com.utility.util.StringUtil;

/**
 *     微信网页授权
 *      1 第一步：用户同意授权，获取code
 *       2 第二步：通过code换取网页授权access_token
 *        3 第三步：刷新access_token（如果需要）
 *      4 第四步：拉取用户信息(需scope为 snsapi_userinfo)
 *        5 附：检验授权凭证（access_token）是否有效
 */
    public class AuthorizationApi  extends BaseApi {
    public static final String WechatRedirect = "#wechat_redirect";

    /**
     * 用户同意授权，获取code code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
     * @param appid  公众号的唯一标识
     *  @param redirect_uri  授权后重定向的回调链接地址， 请使用 urlEncode 对链接进行处理
     *  @param response_type  返回类型，请填写code
     * @param scope  应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
     * @param state  重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节
     *@param wechat_redirect 无论直接打开还是做页面302重定向时候，必须带此参数
    */
    public static String authorizationByUserAgree(String appid, String redirect_uri, String response_type, String scope, String state, String wechat_redirect) {
        if (StringUtil.isEmpty(appid)) {
            throw new NullPointerException("appid  param is null");
        }
        if (StringUtil.isEmpty(redirect_uri)) {
            throw new NullPointerException("redirect_uri  param is null");
        }
        if (StringUtil.isEmpty(response_type)) {
            throw new NullPointerException("response_type  param is null");
        }
        if (StringUtil.isEmpty(scope)) {
            throw new NullPointerException("scope  param is null");
        }
        if (StringUtil.isEmpty(state)) {
            throw new NullPointerException("state  param is null");
        }
        if (StringUtil.isEmpty(wechat_redirect)) {
            wechat_redirect = WechatRedirect;
        }
        return HttpUtils.doGet("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + redirect_uri + "&response_type=" + response_type + "&scope=" + scope + "&state=" + state + wechat_redirect);
    }


    /**
     * 用户同意授权，获取code code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
     *@param authorizationByUserAgreeInfo  用户同意授权
     */
    public static String authorizationByUserAgree(AuthorizationByUserAgreeInfo authorizationByUserAgreeInfo) {
        return authorizationByUserAgree(authorizationByUserAgreeInfo.getAppid(), authorizationByUserAgreeInfo.getRedirectUri(), authorizationByUserAgreeInfo.getResponseType(),
                authorizationByUserAgreeInfo.getScope(), authorizationByUserAgreeInfo.getState(), authorizationByUserAgreeInfo.getWechatRedirec());
    }


/**
 * 通过code换取网页授权access_token
 * @param appid  第三方用户唯一凭证
 *@param secret 第三方用户唯一凭证密钥，即appsecret
 *@param code code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
 *@param grant_type  获取access_token填写client_credential
 */
    public static String getAccessTokenByAuthorization(String appid, String secret, String code, String grant_type) {
        if (StringUtil.isEmpty(code)) {
            throw new NullPointerException("code param is null");
        }
        return HttpUtils.doGet(AuthorizationApi.Domain + "sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + code + "&grant_type=" + grant_type);
    }



    /**
     *刷新access_token（如果需要）
     *@param appid  公众号的唯一标识
     *@param grant_type 填写为refresh_token
     *@param refresh_token 填写通过access_token获取到的refresh_token参数
     */
    public static String refreshAccessToken(String appid, String grant_type, String refresh_token) {
        if (StringUtil.isEmpty(grant_type)) {
            throw new NullPointerException("grant_typeparam is null");
        }
        if (StringUtil.isEmpty(appid)) {
            throw new NullPointerException("appid param is null");
        }
        if (StringUtil.isEmpty(refresh_token)) {
            throw new NullPointerException("refresh_token param is null");
        }
        return HttpUtils.doGet(AuthorizationApi.Domain + "sns/oauth2/refresh_token?appid=" + appid + "&grant_type=" + grant_type + "&refresh_token=" + refresh_token);
    }



/**
 * 拉取用户信息(需scope为 snsapi_userinfo)
 *@param access_token  网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
 *@param openid 用户的唯一标识
 *@param lang 返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语
 */
    public static String pullUserInfo(String access_token, String openid, String lang) {
        ValidateAccessTokenExpriedParam(access_token, openid);
        if (StringUtil.isEmpty(lang)) {
            throw new NullPointerException("lang  param is null");
        }
        return HttpUtils.doGet(AuthorizationApi.Domain + "sns/userinfo?access_token=" + access_token + "&openid=" + openid + "&lang" + lang);
    }



/**
 *@param access_token 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
 *@param openid 用户的唯一标识
 */
    public static String checkAccessTokenExpried(String access_token, String openid) {
        ValidateAccessTokenExpriedParam(access_token, openid);
        return HttpUtils.doGet(AuthorizationApi.Domain + "sns/auth?access_token=" + access_token + "&openid=" + openid);
    }

    public static void ValidateAccessTokenExpriedParam(String access_token, String openid) {
        if (StringUtil.isEmpty(access_token)) {
            throw new NullPointerException("access_token param is null");
        }
        if (StringUtil.isEmpty(openid)) {
            throw new NullPointerException("openid param is null");
        }
    }


}
