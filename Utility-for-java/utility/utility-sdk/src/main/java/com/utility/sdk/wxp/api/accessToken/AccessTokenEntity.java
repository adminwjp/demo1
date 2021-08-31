package com.utility.sdk.wxp.api.accessToken;

import com.utility.sdk.wxp.api.AbstractEntry;
import lombok.Data;

import java.util.Date;

/**
 * AccessToken基本信息
 */
@Data
public class AccessTokenEntity extends AbstractEntry {
   /** 实例化对象,相当于 new AccessTokenEntry() */
    public final AccessTokenEntity Empty = new AccessTokenEntity();
    String grantType;//获取access_token填写client_credential
    String appid;//第三方用户唯一凭证
    String secret;//第三方用户唯一凭证密钥，即appsecret
    String account;//获取access_token所用账号
    String password;//获取access_token所用密码
    int status;//access_token账号是否可用,-1不可用，0可用，1正在使用
    String accessToken;//正确获取到 access_token 时有值
    long expiresIn;//正确获取到 access_token 时有值,值为当前时间戳+expires_in*1000
    Date updateDate;//更新时间

    public AccessTokenEntity() {
        this.Clear();
    }

    /// <summary>access_token是否有效</summary>
    public boolean IsAvailable() {

        if (expiresIn < System.currentTimeMillis()) {
            return false;
        }
        return true;
    }

    /**
     * 清除方法,即初始化值
     */
    public void Clear() {
        super.ClearData();
        this.grantType = "";
        this.appid = "";
        this.secret = "";
        this.account = "";
        this.password = "";
        this.status = 0;
        this.accessToken = "";
        this.expiresIn = 0;
        this.updateDate = null;
    }

}
