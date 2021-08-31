package com.utility.sdk.wxp.api.ips;

import com.utility.sdk.wxp.api.AbstractEntry;
import com.utility.sdk.wxp.api.accessToken.AccessTokenEntity;
import com.utility.util.StringUtil;
import lombok.Data;

import java.util.Date;

//获取微信服务器IP地址信息
@Data
public class IpEntity extends AbstractEntry {
    private String ip;//ip地址
    private Date lastdate;//最后一次获取ip的时间
    private AccessTokenEntity token;//AccessToken


    public void Clear() {
        super.ClearData();
        this.ip = StringUtil.Empty;
        this.lastdate = null;
        this.token = null;
    }

}
