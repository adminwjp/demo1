package com.utility.util;

import java.lang.reflect.Field;
import java.math.BigDecimal;
import java.util.Date;

public class TypeUtil {

    /**
     * 普通数据类型
     * */
    public static boolean isDataType(Field field){
        boolean res= field.getType()==int.class|| field.getType()==Integer.class||
                field.getType()==double.class||field.getType()==Double.class||
                field.getType()==float.class||field.getType()==Float.class
                ||field.getType()== BigDecimal.class
                ||field.getType()==String.class
                ||field.getType()==long.class||field.getType()==Long.class
                ||field.getType()==boolean.class||field.getType()==Boolean.class
                ||field.getType()==byte[].class
                ||field.getType()== Date.class;
        return  res;
    }

    public static boolean isIdentityType(Field field){
        boolean res= field.getType()==int.class|| field.getType()==Integer.class
                ||field.getType()==long.class||field.getType()==Long.class;
        return  res;
    }

    public static boolean isNumberType(Field field){
        boolean res= field.getType()==int.class||
                field.getType()==double.class
                ||    field.getType()==float.class
                ||field.getType()==long.class;
        return  res;
    }
    public static boolean isNumberNullType(Field field){
        boolean res= field.getType()==Integer.class||
                field.getType()==Double.class||
                field.getType()==Float.class
                ||field.getType()== BigDecimal.class||
                field.getType()==Long.class
                ||field.getType()==byte[].class;
        return  res;
    }

}
