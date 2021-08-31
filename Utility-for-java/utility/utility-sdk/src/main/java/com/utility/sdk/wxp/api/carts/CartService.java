package com.utility.sdk.wxp.api.carts;

import com.utility.http.HttpFactory;
import com.utility.sdk.wxp.api.ApiResult;
import com.utility.service.dto.ResponseApi;
import com.utility.util.HttpUtils;
import com.utility.util.StringUtil;

import java.io.File;

// <summary>
    /// 微信卡券接口 服务
    /// </summary>
    public class CartService
    {
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
        public static ResponseApi UploadImg(File fileInfo, String access_token)
        {
            if (StringUtil.isEmpty(access_token)|| fileInfo == null)
            {
                throw  new NullPointerException("fileInfo or access_token  param is null");
            }
            else
            {
                ApiResult apiResult = new ApiResult(CartApi.UploadImg(fileInfo, access_token));

                    if (apiResult.Success())
                    {
                        String url= apiResult.get("url",String.class);//商户图片url，用于创建卡券接口中填入。特别注意：该链接仅用于微信相关业务，不支持引用。

                       /* Session session = HibernateFactory.getSession();
                        Transaction transaction =session.beginTransaction();
                        String id = SecurityUtil.sha1(url);
                        AccessTokenEntity temp=session.get(AccessTokenEntity.class,token.getId());
                        if(temp==null){
                            session.save(temp);
                        }else{
                            session.update(temp);
                        }
                        transaction.commit();
                        session.close();*/

                        return ResponseApi.fail().setData(url);
                    }
                    else
                    {
                        return ResponseApi.fail().setNote(apiResult.getErrmsg());
                    }
                }
        }


        public static String Create(String json, String access_token) {

            return HttpUtils.doPost(CartApi.Domain + "card/create?access_token=" + access_token, json, HttpFactory.ApplicationJson);
        }
        //endregion 创建卡券 接口调用顺序
    }
