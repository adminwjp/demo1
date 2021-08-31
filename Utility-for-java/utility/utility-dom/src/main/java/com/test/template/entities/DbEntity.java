package com.test.template.entities;

import com.alibaba.fastjson.annotation.JSONField;
import com.test.template.entities.base.BaseEntity;
import lombok.Data;
import java.util.List;

@Data
public class DbEntity extends BaseEntity {
    private String database;
    @JSONField(name="program_name")
    private String programName ;
    private List<TableEntity> tables ;
}
