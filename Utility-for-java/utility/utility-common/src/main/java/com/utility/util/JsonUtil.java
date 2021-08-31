package com.utility.util;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class JsonUtil {
    public  static  String toJson(Object obj)   {
        if(obj==null) return  "";
        try {
            ObjectMapper mapper=new ObjectMapper();//先创建objmapper的对象
            String json = mapper.writeValueAsString(obj);
            return json;
        }
        catch (JsonProcessingException ex){
            ex.printStackTrace();
            return "";
        }
    }

    /**
     * File：将obj对象转换为JSON字符串，并保存到指定的文件中
     * */
    public  static  void toJsonSave(Object obj,String fileName)   {
        if(obj==null) return ;
        try {
            ObjectMapper mapper=new ObjectMapper();//先创建objmapper的对象
            mapper.writeValue(new File(fileName),obj);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public static <T> T toObject(String json,Class<T> tclss) {
        try {
            ObjectMapper mapper=new ObjectMapper();
            T obj=mapper.readValue(json,tclss);//有了对象就可以调用getXx和setXxx方法了
            return  obj;
        }
        catch (IOException ex){
            ex.printStackTrace();
            return  null;
        }
    }

    /**
     * Writer：将obj对象转换为JSON字符串，并将json数据填充到字符输出流中
     * */
    public  static  void toJsonSaveStream(Object obj,String fileName)   {
        if(obj==null) return ;
        try {
            ObjectMapper mapper=new ObjectMapper();//先创建objmapper的对象
            mapper.writeValue(new FileWriter(fileName),obj);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
