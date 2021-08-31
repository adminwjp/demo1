

package com.test;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;
import com.test.template.entities.DbEntity;
import com.test.template.net.AbpTpl;
import com.utility.services.dtos.CSharpResponseApi;
import com.utility.util.HttpUtils;

/**
 *
 * */

public class TestDemo1 {
    /**
     * @param args
     * */
    public  static  void  main(String[] args) {
        String str1="";
        String path=System.getProperty("user.dir");
        String in = TestDemo1.class.getResource("").getPath();
        System.out.println(path);
        System.out.println(String.class.getSimpleName());
        AbpTpl abpTpl=new AbpTpl();
        in="E:\\work\\program\\java\\shop\\service\\src\\main\\java\\com\\test";
        String p=in+"\\template\\net\\abp\\";
        abpTpl.setPath(p);
        str1=HttpUtils.doGet("https://localhost:44386/api/v1/Template");
        CSharpResponseApi<DbEntity> res= toObject(str1,DbEntity.class);
        abpTpl.initial(res.getData());
    }

    public static CSharpResponseApi toObject(String json,Class cla)   {
        CSharpResponseApi responseApi=new CSharpResponseApi();
        JSONObject object=JSONObject.parseObject(json);
        responseApi.setSuccess(object.getBoolean("success"));
        responseApi.setMessage(object.getString("message"));
        responseApi.setCode(object.getIntValue("code"));
     /*   SerializeConfig config = new SerializeConfig();
        config.propertyNamingStrategy = PropertyNamingStrategy.SNAKE_CASE;*/
        Object obj= JSON.parseObject(object.getString("com/template/data"),cla);
        responseApi.setData(obj);
        return  responseApi;
    }





}
