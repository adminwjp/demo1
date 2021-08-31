package com.utility.mybatis;

import com.utility.util.StringUtil;
import org.apache.ibatis.mapping.BoundSql;
import org.apache.ibatis.mapping.SqlSource;
import org.springframework.util.ReflectionUtils;
import java.lang.reflect.Field;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public  class SqlSourceWrapper implements SqlSource {
    private SqlSource origin ;
    private StringUtil.StringFormat stringFormat= StringUtil.StringFormat.None;
    public SqlSourceWrapper(SqlSource origin){
        this.origin = origin;
    }

    @Override
    public BoundSql getBoundSql(Object parameterObject) {
        BoundSql boundSql = origin.getBoundSql(parameterObject);
        String sql = boundSql.getSql();
        Field sqlField = null;
        try {
            sqlField = BoundSql.class.getDeclaredField("sql");
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        }
        ReflectionUtils.makeAccessible(sqlField);
        switch (stringFormat)
        {
            case InitialLetterLowerCaseUpper:
            case InitialLetterUpperCaseLower:
                ReflectionUtils.setField(sqlField, boundSql, toSql(sql));
                break;
            default:
                ReflectionUtils.setField(sqlField, boundSql, StringUtil.parse(sql,stringFormat));
                break;
        }
        return boundSql;
    }
    public  String toSql(String sql){
        StringBuffer sb = new StringBuffer() ;
        Pattern p = Pattern.compile("\\w+") ;
        Matcher m = p.matcher(sql) ;
        while( m.find() ){
            String tmp = m.group() ;
            String v = StringUtil.parse(tmp,stringFormat);
            v = v.replace("\\", "\\\\").replace("$", "\\$");
            //替换掉查找到的字符串
            m.appendReplacement(sb, v) ;
        }
        //别忘了加上最后一点
        m.appendTail(sb) ;
        String string = sb.toString();
        return  string;
    }
    public StringUtil.StringFormat getStringFormat() {
        return stringFormat;
    }

    public void setStringFormat(StringUtil.StringFormat stringFormat) {
        this.stringFormat = stringFormat;
    }
}
