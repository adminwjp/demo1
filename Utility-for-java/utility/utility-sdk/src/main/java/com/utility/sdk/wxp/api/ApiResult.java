package com.utility.sdk.wxp.api;

import com.utility.util.JsonUtil;
import com.utility.util.StringUtil;

import java.util.Map;

/**
 * 通用微信公众号返回结果
 */
public class ApiResult
{

    /// <summary>
    /// 通用微信公众号
    /// </summary>
    /// <param name="json"></param>
    public ApiResult(String json)
    {
        try
        {
            this.json = json;
            if (StringUtil.isEmpty(json))
            {
                throw new NullPointerException("json");
            }
            else
            {
                this.result = JsonUtil.toObject(json,Map.class);
                this.isSuccess = true;
            }
        }
        catch (Exception e)
        {
            this.isSuccess=false;
            this.exception = e;
        }
    }

     Map<String, Object> result;//转换的结果
     String json ;//原始数据
    boolean isSuccess;//操作是否成功
     Exception exception;//异常信息
    /**
     * 获取信息
     * */
    public <T> T get(String key,Class<T> cla)  {
        return this.result.containsKey(key)? JsonUtil.toObject(result.get(key).toString(),cla):null;
    }
     /**
      * 状态码
      * */
    public int getErrcode(){
       if (!isSuccess){
           return  -1;
       }
        Integer code= get("errcode",Integer.class);
        if (code!=null){
            return  code.intValue();
        }
        return  0;
    }
     /**
      * 描述信息
      * */
    public String getErrmsg (){
        return  get("errmsg",String.class);
    }
  /**
   * 请求是否成功
   * */

    public boolean Success() {

        int errorCode = getErrcode();
        return  errorCode == 0;
    }
}
