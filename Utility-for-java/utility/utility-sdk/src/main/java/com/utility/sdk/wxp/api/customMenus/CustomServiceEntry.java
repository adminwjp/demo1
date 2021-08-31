package com.utility.sdk.wxp.api.customMenus;

import com.utility.sdk.wxp.api.AbstractEntry;
import com.utility.sdk.wxp.api.accessToken.AccessTokenEntity;
import com.utility.util.StringUtil;
import lombok.Data;

import java.util.Date;

/**
* 客服消息
* <![CDATA[
* https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547
* ]]>
**/
@Data
public class CustomServiceEntry extends AbstractEntry {
    //
    private String kfAccount;//完整客服账号，格式为：账号前缀@公众号微信号
    private String kfNick;//客服昵称
    private String kfId;//客服工号
    private String nickname;//客服昵称，最长6个汉字或12个英文字符
    private String password;//客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码
    private String media;//该参数仅在设置客服头像时出现，是form-data中媒体文件标识，有filename、filelength、content-type等信息
    private Date lastdate;//最后一次获取ip的时间
    private AccessTokenEntity token;//AccessToken


    public void Clear() {
        super.ClearData();
        this.kfAccount = StringUtil.Empty;
        this.kfNick = StringUtil.Empty;
        this.kfId = StringUtil.Empty;
        this.nickname = StringUtil.Empty;
        this.password = StringUtil.Empty;
        this.media = StringUtil.Empty;
        this.lastdate = null;
        this.token = null;
    }

}
