package com.utility.sdk.wxp.api.carts;

import com.utility.http.HttpFactory;
import com.utility.sdk.wxp.api.BaseApi;
import com.utility.util.HttpUtils;
import java.io.File;

/// <summary>微信卡券接口 Api </summary>
    ///<![CDATA[
    ///https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141229
    /// ]]>
    public class CartApi extends BaseApi {
    //获取卡券详情接口
    private static String _getCard = "https://api.weixin.qq.com/card/get?access_token=";

    //创建卡券接口
    private static String _createCard = "https://api.weixin.qq.com/card/create?access_token=";

    //更新卡券接口
    private static String _updateCard = "https://api.weixin.qq.com/card/update?access_token=";

    //批量获取卡券接口
    private static String _batchget = "https://api.weixin.qq.com/card/batchget?access_token=";

    //激活会员卡
    private static String _memberCard = "https://api.weixin.qq.com/card/membercard/activate?access_token=";

    //会员余额，积分变动，调用该接口进行更新
    private static String updateUser = "https://api.weixin.qq.com/card/membercard/updateuser?access_token=";

    //拉去会员的会员卡信息，通过该接口获取会员某张会员卡的积分，余额信息
    private static String memberCardInfo = "https://api.weixin.qq.com/card/membercard/userinfo/get?access_token=";

    //region 创建卡券 接口调用顺序

    /// <summary>
    /// 步骤一：上传卡券图片素材
    /// <![CDATA[
    ///  https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=ACCESS_TOKEN
    /// ]]>
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <param name="access_token">调用接口凭证</param>
    /// <returns></returns>
    public static String UploadImg(File fileInfo, String access_token) {
        return "";
        //HttpHelper.Upload($"{CartApi.Domain}media/uploadimg?access_token={access_token}", fileInfo);
    }


    public static String Create(String json, String access_token) {
        return HttpUtils.doPost(CartApi.Domain+"card/create?access_token="+access_token, json, HttpFactory.ApplicationJson);
    }
    //endregion 创建卡券 接口调用顺序


    /// <summary>
    /// 创建二维码接口 开发者可调用该接口生成一张卡券二维码供用户扫码后添加卡券到卡包。
    /// <![CDATA[
    /// https://api.weixin.qq.com/card/qrcode/create?access_token=TOKEN
    /// ]]>
    /// </summary>
    /// <param name="json"></param>
    /// <param name="access_token"></param>
    /// <returns></returns>
    public static String CreateQrcode(String json, String access_token) {
        return HttpUtils.doPost(CartApi.Domain+"card/qrcode/create?access_token="+access_token, json,HttpFactory.ApplicationJson);
    }

    /// <summary>
    /// 创建货架接口 开发者需调用该接口创建货架链接，用于卡券投放。创建货架时需填写投放路径的场景字段。
    /// <![CDATA[
    /// https://api.weixin.qq.com/card/landingpage/create?access_token=$TOKEN
    /// ]]>
    /// </summary>
    /// <param name="json"></param>
    /// <param name="access_token"></param>
    /// <returns></returns>
    public static String CreateLandingpage(String json, String access_token)   {
        return HttpUtils.doPost(CartApi.Domain + "card/landingpage/create?access_token="+access_token, json, "application/json");
    }

}
