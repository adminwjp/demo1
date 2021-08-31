package com.utility.template.model;

import lombok.Data;

import java.lang.reflect.Field;

@Data
public   abstract   class BaseModel{
    private Field field;//字段
    private  boolean isEnum;//枚举
}
