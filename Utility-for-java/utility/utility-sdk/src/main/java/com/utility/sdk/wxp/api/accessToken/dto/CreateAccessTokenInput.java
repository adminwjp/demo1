package com.utility.sdk.wxp.api.accessToken.dto;

import lombok.Data;

/**
*  access_token 请求参数
* */
  @Data
 public class CreateAccessTokenInput {
    /**
     * 获取access_token填写client_credential
     */
    String grantType;

    /**
     * 第三方用户唯一凭证
     */
    String appid;

    /**
     * 第三方用户唯一凭证密钥，即appsecret
     */
    String secret;
}
