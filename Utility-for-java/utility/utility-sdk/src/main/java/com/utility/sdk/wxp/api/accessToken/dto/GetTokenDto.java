package com.utility.sdk.wxp.api.accessToken.dto;

import lombok.Data;

@Data
public class GetTokenDto
{

    /**
     *  获取到的凭证
     * */
    String accessToken ;
    /**
     *  凭证有效时间，单位：秒
     * */
    int expiresIn ;

}
