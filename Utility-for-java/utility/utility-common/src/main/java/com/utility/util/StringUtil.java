package com.utility.util;

//import org.apache.commons.logging.Log;
//import org.apache.commons.logging.LogFactory;
import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.math.BigInteger;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class StringUtil
{
    public  final  static  String Empty="";
    //private static final Log log = LogFactory.getLog(StringUtils.class);
    public   final  static  Class<?> integerClasss=Integer.class.getClass();

    public static String getString(Object[] objs,final  String suffix){
        String str = "";
        for (int i = 0; i < objs.length; i++) {
            str+=objs[i];
            if(i!=objs.length-1){
                str+=suffix;
            }
        }
        return  str;
    }

    /**
     * 字符转换
     * @param  str
     * @param  format
     * */
    public static  String parse(String str, StringFormat format)
    {
        switch (format)
        {
            case Lower:
                return str.toLowerCase();
            case Upper:
                return str.toUpperCase();
            case InitialLetterUpperCaseLower:
            {
                char[] chars=str.toCharArray();
                StringBuilder builder = new StringBuilder(str.length() + 10);
                for (int i = 0; i < str.length(); i++)
                {
                    if(chars[i]=='_'){
                        builder.append(chars[i]);
                    }
                    else if (i == 0)
                        builder.append(Character.toLowerCase(chars[i]));
                    else if (Character.toUpperCase(chars[i])==chars[i])
                    {
                        builder.append("_").append(Character.toLowerCase(chars[i]));
                    }
                    else
                    {
                        builder.append(chars[i]);
                    }
                }
                str = builder.toString();
                return str;
            }
            case InitialLetterLowerCaseUpper:
            {
                char[] chars=str.toCharArray();
                StringBuilder builder = new StringBuilder(str.length());
                for (int i = 0; i < str.length();)
                {
                  /*  if (i==0*//*&&chars[i] == '_'*//*) {
                        builder.append(chars[i]);
                    }*/
                     if (i == 0)
                        builder.append(Character.toUpperCase(chars[i]));
                    else if (chars[i] == '_')
                    {
                        i++;
                        builder.append(Character.toUpperCase(chars[i]));
                    }
                    else
                    {
                        builder.append(chars[i]);
                    }
                    i++;
                }
                str = builder.toString();
                return str;
            }
            default:
                return str;
        }

    }

    public static StringFormat get(StringFormat stringFormat)
    {
        StringFormat format = StringFormat.None;
        switch (stringFormat)
        {
            case Lower:
                format = StringFormat.Upper;
                break;
            case Upper:
                format = StringFormat.Lower;
                break;
            case InitialLetterLowerCaseUpper:
                format = StringFormat.InitialLetterUpperCaseLower;
                break;
            case InitialLetterUpperCaseLower:
                format = StringFormat.InitialLetterLowerCaseUpper;
                break;
            default:
                break;
        }
        return format;
    }

    /**
     * 获取字符串不同之处的位置
     * @param str1
     * @param str2
     * @return
     * */
    public static int getDiffIndex(String str1,String str2)
    {
        if (str2.length() > str1.length())
        {
            return str1.length();
        }
        char[] chars1=str1.toCharArray();
        char[] chars2=str2.toCharArray();
        for (int i = 0; i <str1.length()&&i<str2.length(); i++)
        {
            if (chars1[i]!=chars2[i])
            {
                return i;
            }
        }
        return -1;
    }


    public final  static  Long toLong(String id){
        return  Long.parseLong(id);
    }

    public  final static  Long[] toLong(String[] id){
        Long[] ids=new Long[id.length];
        for (int i = 0; i < ids.length; i++) {
            ids[i]=Long.parseLong(id[i]);
        }
        return  ids;
    }

    public final  static  Long[] toLong(int[] id){
        Long[] ids=new Long[id.length];
        for (int i = 0; i < ids.length; i++) {
            ids[i]=(long)id[i];
        }
        return  ids;
    }

    public final  static  Long[] toLong(Integer[] id){
        Long[] ids=toLongBy(id);
        return  ids;
    }

    public final  static   Long[] toLongBy(Serializable[] id){
        Long[] ids=new Long[id.length];
        for (int i = 0; i < ids.length; i++) {
            ids[i]=(long)id[i];
        }
        return  ids;
    }

    public  final static  Long[] toLong(ArrayList id){
        Long[] ids=new Long[id.size()];
        Class<?> aClass=id.getClass().getGenericSuperclass().getClass();
        int flag=-1;
        if(aClass==integerClasss){
            flag=1;
        }
        for (int i = 0; i < ids.length; i++) {
            if(flag==1){
                ids[i]=(long)id.get(i);
            }else{
                ids[i]=Long.parseLong(id.get(i).toString());
            }

        }
        return  ids;
    }

    public  final static  int[] toInt(HashMap id){
        if(id!=null&&id.containsKey("ids")){
            ArrayList arrayList=(ArrayList)id.get("ids");
            return  toInt(arrayList);
        }
        return  null;
    }

    public  final static  int[] toInt(ArrayList id){
        int[] ids=new int[id.size()];
        Class<?> aClass=id.getClass().getGenericSuperclass().getClass();
        int flag=-1;
        if(aClass==integerClasss){
            flag=1;
        }
        for (int i = 0; i < ids.length; i++) {
            if(flag==1){
                ids[i]=(int)id.get(i);
            }else{
                ids[i]=Integer.parseInt(id.get(i).toString());
            }

        }
        return  ids;
    }

    public  final static String urlDecode(String string) {
        String str=urlEncoder(string, false);
        return  str;
    }

    public final static String urlEncode(String string) {
        String str=urlEncoder(string, true);
        return  str;
    }
    private   static String urlEncoder(String string,boolean encode) {
        try {
            String str=encode? URLEncoder.encode(string,"UTF-8"): URLDecoder.decode(string,"UTF-8");
            return  str;
        } catch (UnsupportedEncodingException ex) {
            System.out.println(encode?"URLEncoder":"URLDecoder"+"   is error");
            ex.printStackTrace();
        }
        return string;
    }

    public final static String urlRedirect(String redirect) {
        try {
            redirect = new String(redirect.getBytes("UTF-8"), "ISO8859_1");
        } catch (UnsupportedEncodingException ex) {
            System.out.println("urlRedirect is error");
            ex.printStackTrace();
        }
        return redirect;
    }

    public static final boolean anyEmpty(String... strs) {
        boolean empty=empty(true, true, strs);
        return  empty;
    }

    public static final boolean someEmpty(String... strs) {
        boolean empty=empty(true, false, strs);
        return  empty;
    }

    public final static boolean isEmpty(String string) {
        return string == null || string.equals("");
    }

    public final static boolean isNotEmpty(String string) {
        return !isEmpty(string);
    }

    public final static boolean anyBlank(String... strs) {
        boolean blank=empty(false, true, strs);
        return  blank;
    }

    public final static boolean someBlank(String... strs) {
        boolean blank=empty(false, false, strs);
        return  blank;
    }

    private final static boolean empty(boolean empty,boolean any,String... strs) {
        if (strs == null || strs.length == 0)
            return true;
        for (String string : strs) {
            if(string==null){
                return  true;
            }
            else {
                String str=empty?string:string.trim();
                if(any){
                    if(!"".equals(string)){
                        return  true;
                    }
                }else {
                    if("".equals(string)){
                        return  true;
                    }
                }
            }
        }
        return false;
    }

    public final static boolean isBlank(String string) {
        return string == null || string.trim().equals("");
    }
    public final static boolean isNotBlank(String string) {
        return !isBlank(string);
    }

    public static long toLong(String value, Long defaultValue) {
        try {
            if (value == null || "".equals(value.trim()))
                return defaultValue;
            value = value.trim();
            if (value.startsWith("N") || value.startsWith("n"))
                return -Long.parseLong(value.substring(1));
            return Long.parseLong(value);
        }  catch (Exception ex) {
            ex.printStackTrace();
        }
        return defaultValue;
    }

    public static int toInt(String value, int defaultValue) {
        try {
            if (value == null || "".equals(value.trim()))
                return defaultValue;
            value = value.trim();
            if (value.startsWith("N") || value.startsWith("n"))
                return -Integer.parseInt(value.substring(1));
            return Integer.parseInt(value);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return defaultValue;
    }

    public static BigInteger toBigInteger(String value, BigInteger defaultValue) {
        try {
            if (value == null || "".equals(value.trim()))
                return defaultValue;
            value = value.trim();
            if (value.startsWith("N") || value.startsWith("n"))
                return new BigInteger(value).negate();
            return new BigInteger(value);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return defaultValue;
    }

    public static boolean match(String string, String regex) {
        Pattern pattern = Pattern.compile(regex, Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(string);
        return matcher.matches();
    }
    public enum StringFormat
    {
        /**
        * 默认字符 aBc aBc
        * */
        None,
        /**
        * 小写 aBc abc
        * */
        Lower,
        /**
        * 大写 aBc ABC
        * */
        Upper,
        /**
        * 首字母大写转小写 其他大写字母转横线加小写字母 aBc a_bc
        * */
        InitialLetterUpperCaseLower,
        /**
        * 首字母小写转 大写 其他横线加小写字母转大写字母  a_bc aBc
        * */
        InitialLetterLowerCaseUpper
    }
}

