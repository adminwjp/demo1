package com.utility.sdk.wxp.api.authorizations;

import lombok.Data;

@Data
public class AccessTokenExpriedInfo
{

    String accessToken;//网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同

    String openid ;//用户的唯一标识
}
