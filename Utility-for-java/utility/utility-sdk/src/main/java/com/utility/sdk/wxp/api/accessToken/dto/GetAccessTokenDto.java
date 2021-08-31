package com.utility.sdk.wxp.api.accessToken.dto;

import lombok.Data;

@Data
public class GetAccessTokenDto extends  GetTokenDto
{
        
    public   GetAccessTokenDto(int errcode, String access_token,int expires_in)
    {
        this.errcode = errcode;
        this.accessToken = access_token;
        this.expiresIn = expires_in;
    }

    public  GetAccessTokenDto(String errmsg, int errcode)
    {
        this.errmsg = errmsg;
        this.errcode = errcode;
    }

     int errcode ;
     String errmsg ;
}
