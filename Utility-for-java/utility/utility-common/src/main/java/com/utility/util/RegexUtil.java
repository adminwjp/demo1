package com.utility.util;

import java.util.regex.Pattern;

public class RegexUtil {
    public  final static boolean isPhone(String phone){
        if (StringUtil.isBlank(phone)){
            return  false;
        }
       return  Pattern.matches("[13|15|17|18]\\d{9,9}]",phone);
    }
}
