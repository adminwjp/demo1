package com.utility.sdk.wxp.api.SourceMaterialManages;

import com.utility.sdk.wxp.api.AbstractEntry;
import com.utility.sdk.wxp.api.accessToken.AccessTokenEntity;
import com.utility.util.StringUtil;
import lombok.Data;

//临时素材
@Data
public class TempSourceMaterialEntry extends AbstractEntry
{

    private String type;//媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）
    private String mediaId;//媒体文件上传后，获取标识
    private String createdAt;//媒体文件上传时间戳
    private String lastDate;//媒体文件最后更新时间
    private String url;//媒体文件地址
    private AccessTokenEntity token;//AccessToken
    private String accessToken;//不知道 外键 则使用access_token


    @Override
    public  void Clear()
    {
        super.ClearData();
        this.type = StringUtil.Empty;
        this.mediaId = StringUtil.Empty;
        this.createdAt = StringUtil.Empty;
        this.lastDate = StringUtil.Empty;
        this.url = StringUtil.Empty;
        this.token = null;
        this.accessToken = StringUtil.Empty;
    }

}
