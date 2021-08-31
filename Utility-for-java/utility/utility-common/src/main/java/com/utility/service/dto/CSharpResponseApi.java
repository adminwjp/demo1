package com.utility.service.dto;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;

public class CSharpResponseApi<T> {
    private  boolean success;//操作是否成功
    private String message;//描述信息
    private int code;//状态码
    private T data;//数据

    public boolean getSuccess() {
        return success;
    }

    public void setSuccess(boolean success) {
        this.success = success;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }

    public T getData() {
        return data;
    }

    public void setData(T data) {
        this.data = data;
    }

    public static CSharpResponseApi toObject(String json,Class cla)   {
        CSharpResponseApi responseApi=new CSharpResponseApi();
        JSONObject object=JSONObject.parseObject(json);
        responseApi.setSuccess(object.getBoolean("success"));
        responseApi.setMessage(object.getString("message"));
        responseApi.setCode(object.getIntValue("code"));
     /*   SerializeConfig config = new SerializeConfig();
        config.propertyNamingStrategy = PropertyNamingStrategy.SNAKE_CASE;*/
        Object obj= JSON.parseArray(object.getString("com/template/data"),cla);
        responseApi.setData(obj);
        return  responseApi;
    }



}
