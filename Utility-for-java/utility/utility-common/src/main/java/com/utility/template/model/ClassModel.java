package com.utility.template.model;

import lombok.Data;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;
@Data
public    class  ClassModel{
    private Class<?> cla;
    private IdModel idModel;//主键
    public final List<Field> fieldList=new ArrayList<>(50);
    public final List<FieldModel> fields=new ArrayList<>(50);//普通字段
    public final List<FkGroupModel> fkGroupModels=new ArrayList<>(10);//普通字段


}
