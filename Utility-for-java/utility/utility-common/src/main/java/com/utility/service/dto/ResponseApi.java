package com.utility.service.dto;


import com.utility.Code;
import java.util.HashMap;

/**
 * 通用 api 结果
 * maven 指定 jdk
 * 不然 object num=1; int res=(int)num;
 * 编译不通过 其他同理
 * */
public class ResponseApi extends HashMap<String,Object> {

    public  static  final  ResponseApi Success=new ResponseApi().put(Code.Success);
    public  static  final  ResponseApi Fail=new ResponseApi().put(Code.Fail);
    public  static  final  ResponseApi Error=new ResponseApi().put(Code.Error);

    public ResponseApi put(Code code){
        return  set("note",code.getNote()).set("code", code.getCode()).set("status", code.isStatus());
    }
    public  int getCode(){
        if(super.containsKey("code")){
            return  (int)super.get("code");
            //return  Integer.parseInt(String.valueOf(super.get("code") ));
        }
        return -1;
    }
    public  ResponseApi setCode(int code){
        return set("code",code);
    }
    public  ResponseApi setNote(String note){
        return set("note",note);
    }
    public  boolean getSuccess(){
        if(super.containsKey("success")){
            return  (boolean) super.get("success");
        }
        return false;
    }
    public  ResponseApi setSuccess(boolean success){
        return set("success",success);
    }

    public ResponseApi set(String  key,Object val){
        if(!this.containsKey(key)){
            put(key,val);
        }
        return  this;
    }

    public static ResponseApi success(){
        return new ResponseApi().put(Code.Success);
    }

    public ResponseApi setData(Object data){
        if(!this.containsKey("com/template/dao/data")){
            put("com/template/dao/data",data);
        }
        return  this;
    }

    public static ResponseApi fail(){
        return  new ResponseApi().put(Code.Fail);
    }

    public static ResponseApi error(){
        return  new ResponseApi().put(Code.Error);
    }
}
