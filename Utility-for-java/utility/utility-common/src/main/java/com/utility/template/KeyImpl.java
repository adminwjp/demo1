package com.utility.template;


import com.utility.util.StringUtil;
import com.utility.util.TypeUtil;
import java.lang.reflect.Field;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;

public class KeyImpl  {
    public  static final  KeyImpl Empty=new KeyImpl();
    public   boolean isId(String name){
        return "id"==name;
    }
    public   boolean isIdentity(Field field) {
        if (isId(field.getName())) {
            if (TypeUtil.isIdentityType(field)) {
                return true;
            }
        }
        return false;
    }
    public  String getFkColumn(Field field,Class<?> cla){
        Type type=field.getGenericType();
        if(type instanceof ParameterizedType){
            ParameterizedType parameterizedType=(ParameterizedType)type;
            type=parameterizedType.getActualTypeArguments()[0];
        }
        if(cla==type){
            return  "parent_id";
        }
        String alias= StringUtil.parse(field.getName(), StringUtil.StringFormat.InitialLetterUpperCaseLower);
        return  alias+"_id";
    }

    public  String getFkBasicField(String name){
        return  name+"Id";
    }

    public  String getFkBasicField(Type type, Class<?> cla){
        if(cla==type){
            return  "parentId";
        }else{
            String name=type.getClass().getSimpleName();
            char[] chars=name.toCharArray();
            chars[0]=Character.toLowerCase(chars[0]);
           return  new String(chars)+"Id";
        }
    }

    public  String getFkName(Field field,Class<?> cla){
        return  "fk_"+getFkColumn(field,cla);
    }
}
