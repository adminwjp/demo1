package com.utility.jfinal;

import com.jfinal.plugin.activerecord.generator.MetaBuilder;

import javax.sql.DataSource;

public class JfinalMetaBuilder extends MetaBuilder {
    public JfinalMetaBuilder(DataSource dataSource) {
        super(dataSource);
    }
    /**
     * 获取需要生成的实体模型
     * */
    @Override
    protected boolean isSkipTable(String tableName) {
        return false;
    }
}
