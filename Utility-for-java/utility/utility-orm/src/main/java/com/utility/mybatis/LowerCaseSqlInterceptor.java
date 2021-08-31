package com.utility.mybatis;

import com.utility.util.StringUtil;
import org.apache.ibatis.executor.Executor;
import org.apache.ibatis.mapping.MappedStatement;
import org.apache.ibatis.mapping.SqlSource;
import org.apache.ibatis.plugin.Intercepts;
import org.apache.ibatis.plugin.Signature;
import org.apache.ibatis.session.ResultHandler;
import org.apache.ibatis.session.RowBounds;

/**
 * 将待执行SQL转换为全小写的拦截器
 */
@Intercepts({@Signature(type = Executor.class, method = "query", args = {MappedStatement.class, Object.class, RowBounds.class, ResultHandler.class})
        , @Signature(type = Executor.class, method = "update", args = {MappedStatement.class, Object.class})
})
public class LowerCaseSqlInterceptor extends  AbstractSqlInterceptor {

    @Override
    protected SqlSource getSqlSource(SqlSource sqlSource) {
        SqlSourceWrapper source= new SqlSourceWrapper(sqlSource);
        source.setStringFormat(StringUtil.StringFormat.Lower);
        return source;
    }
}