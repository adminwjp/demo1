package com.utility.sdk.wxp.api.SourceMaterialManages;

import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;

import java.io.File;

/// <summary>
    /// 素材管理 api
    /// <![CDATA[
    /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738726
    /// ]]>
    /// </summary>
    public class SourceMaterialManageApi extends BaseApi {
    /// <summary>
    /// 新增临时素材
    /// <![CDATA[
    /// https://api.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type=TYPE
    /// ]]>
    /// </summary>
    /// <param name="access_token">调用接口凭证</param>
    /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>
    /// <param name="fileInfo"></param>
    /// <returns></returns>
    public static String Upload(String access_token, String type, File fileInfo) {
        return  "";
        //return HttpUtils.Upload($"{SourceMaterialManageApi.Domain}media/upload?access_token={access_token}&type={type}", fileInfo);
    }
    //region 获取临时素材

    /// <summary>
    /// 获取临时素材
    /// <![CDATA[
    /// https://api.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
    /// ]]>
    /// </summary>
    /// <param name="access_token">调用接口凭证</param>
    /// <param name="media_id">媒体文件ID</param>
    /// <returns></returns>
    public static String Get(String access_token, String media_id) {
        return HttpUtils.doGet(SourceMaterialManageApi.Domain+"media/get?access_token="+access_token+"&media_id="+media_id);
    }

    /// <summary>
    /// 高清语音素材获取接口
    /// <![CDATA[
    /// https://api.weixin.qq.com/cgi-bin/media/get/jssdk?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
    /// ]]>
    /// </summary>
    /// <param name="access_token">调用接口凭证</param>
    /// <param name="media_id">媒体文件ID，即uploadVoice接口返回的serverID</param>
    /// <returns></returns>
    public static String GetJssdk(String access_token, String media_id) {
        return "";
        // HttpHelper.Get($"{SourceMaterialManageApi.Domain}media/get/jssdk?access_token={access_token}&media_id={media_id}");
    }

    //endregion 获取临时素材


    //region 新增永久素材

    /// <summary>
    /// 新增永久素材
    /// <![CDATA[
    /// https://api.weixin.qq.com/cgi-bin/material/add_news?access_token=ACCESS_TOKEN
    /// ]]>
    /// </summary>
    /// <param name="access_token">调用接口凭证</param>
    /// <param name="json"></param>
    /// <returns></returns>
    public static String AddNews(String access_token, String json) {
        return "";
        //HttpUtils.post($"{SourceMaterialManageApi.Domain}cgi-bin/material/add_news?access_token={access_token}", json);
    }
    //endregion 新增永久素材

}
