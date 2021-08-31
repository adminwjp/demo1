package com.utility.sdk.wxp.api.authorizations;

import lombok.Data;

@Data
public class RefreshAccessTokenInfo {

    String GrantType;//获取access_token填写client_credential
    String Appid;//第三方用户唯一凭证
    String RefreshToken;//填写通过access_token获取到的refresh_token参数
}
