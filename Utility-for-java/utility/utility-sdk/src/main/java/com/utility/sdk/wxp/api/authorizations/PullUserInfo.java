package com.utility.sdk.wxp.api.authorizations;

import lombok.Data;

/**
 * 拉取用户信息(需scope为 snsapi_userinfo)
 * */
@Data
public class PullUserInfo extends AccessTokenExpriedInfo {


    String Lang;//返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语
}
