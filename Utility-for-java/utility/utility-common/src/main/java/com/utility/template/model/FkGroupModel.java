package com.utility.template.model;

import lombok.Data;

import java.lang.reflect.Type;
@Data
public    class FkGroupModel{
    private Type referenceType;//引用类
    private  FkModel basic;//普通外键
    private  FkModel single;//引用 单通外键
    private  FkModel many;//引用 集合外键



}
