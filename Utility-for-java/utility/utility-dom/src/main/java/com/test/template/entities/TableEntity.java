package com.test.template.entities;

import com.alibaba.fastjson.annotation.JSONField;
import lombok.Data;

import java.util.List;

@Data
public class TableEntity {
    private String table;
    @JSONField(name="class_name")
    private String className ;
    private  String comment;
    private DbEntity db ;
    private String dbId;
    private List<ColumnEntity> columns ;

}
