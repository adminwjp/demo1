package com.utility.sdk.wxp.api.internetChecks;

import com.utility.sdk.wxp.api.AbstractEntry;
import com.utility.sdk.wxp.api.accessToken.AccessTokenEntity;
import com.utility.util.StringUtil;
import lombok.Data;

import java.util.Date;

/**
 *    网络检测
 *      <![CDATA[
 *      https://mp.weixin.qq.com/wiki?t=resource/res_main&id=21541575776DtsuT
 *      ]]>
 * */
@Data
public class InternetCheckEntity extends AbstractEntry {
    public final static String DNS = "dns";//DNS
    public final static String PING = "ping";//PING

    private String ip;//dns.ip 解析出来的ip  ping.ip ping的ip，执行命令为ping ip –c 1-w 1 -q
    private String realOperator;//dns.real_operator ip对应的运营商
    private String fromOperator;//ping.from_operator  ping的源头的运营商，由请求中的check_operator控制
    private String packageLoss;//ping.package_loss ping的丢包率，0%表示无丢包，100%表示全部丢包。因为目前仅发送一个ping包，因此取值仅有0%或者100%两种可能。
    private String time;//ping.time ping的耗时，取ping结果的avg耗时。
    private String type;//类型 dns  ping
    private Date lastdate;//最后一次获取ip的时间
    private AccessTokenEntity token;//AccessToken
    private String accessToken;//不知道 外键 则使用access_token


    @Override
    public void Clear() {
        super.ClearData();
        this.ip = StringUtil.Empty;
        this.realOperator = StringUtil.Empty;
        this.fromOperator = StringUtil.Empty;
        this.packageLoss = StringUtil.Empty;
        this.time = StringUtil.Empty;
        this.type = StringUtil.Empty;
        this.lastdate = null;
        this.token = null;
        this.accessToken = StringUtil.Empty;
    }

}
