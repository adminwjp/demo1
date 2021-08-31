package com.test.template.entities;

import com.alibaba.fastjson.annotation.JSONField;
import lombok.Data;

@Data
public class ColumnEntity {
    @JSONField(name="column_name")
    private String columnName ;
    private  String comment;
    @JSONField(name="propert_name")
    private String propertName;
    @JSONField(name="property_type")
    private Class<?> propertyType ;
    @JSONField(name="my_sql_value")
    private String mySqlValue ;
    @JSONField(name="sql_server_value")
    private String sqlServerValue ;
    @JSONField(name="oracle_value")
    private String oracleValue ;
    @JSONField(name="sqlite_value")
    private String sqliteValue ;
    @JSONField(name="postgre_value")
    private String postgreValue ;
    private String remark ;
    @JSONField(name="default_value")
    private String defaultValue ;
    @JSONField(name="check_value")
    private String checkValue ;
    @JSONField(name="table_id")
    private String tableId ;
    @JSONField(name="reference_id")
    private String referenceId ;
    @JSONField(name="table_name")
    private String tableName ;
    @JSONField(name="constraint_name")
    private String constraintName ;
    private  String length;
    private TableEntity table ;
}
