package com.template.model;

import com.template.model.enums.DataFlag;
import lombok.Data;

import javax.persistence.Entity;
import javax.persistence.Id;
import java.io.Serializable;

/**
 * 反射有毒 少了异常 多了可以 json转换
 * */
@Entity(name = "t_column")
@Data
public class Column implements Serializable {
    public Column(){

    }

    public Column(String column,String comment){
       this.setString(column, comment);
    }

    public  void  setString(String column,String comment){
        this.setValueType(column, comment, DataFlag.String);
        this.length=50;
    }

    public  void  setBool(String column,String comment){
       this.setValueType(column, comment, DataFlag.Bool);
    }

    public  void  setInt(String column,String comment){
        this.setValueType(column, comment, DataFlag.Int);
    }

    public  void  setLong(String column,String comment){
        this.setValueType(column, comment, DataFlag.Long);
    }

    public  void  setValueType(String column,String comment,DataFlag flag){
        this.column=column;
        this.comment=comment;
        this.flag= flag;
    }
    @Id
    @javax.persistence.Column(length = 36)
    private  String id;//id

    @javax.persistence.Column(length = 20)
    private String database ;//列所在数据库

    @javax.persistence.Column(length = 20)
    private String table ;//列 所在表

    @javax.persistence.Column(name = "reference_table",length = 20)
    private String referenceTable ;//列 所在外键表

    @javax.persistence.Column(name = "reference_column",length = 20)
    private String referenceColumn ;//列 所在外键列

    @javax.persistence.Column(name = "is_pk")
    private boolean isPk ;//列是否主键

    @javax.persistence.Column(name = "is_identity")
    private boolean isIdentity ;//列是否自增

    @javax.persistence.Column(name = "is_fk")
    private boolean isFk ;//列是否外键

    @javax.persistence.Column(length = 20)
    private String column ;//列名

    @javax.persistence.Column(name = "data_type",length = 20)
    private String dataType ;//列 sql 数据类型

    @javax.persistence.Column(name = "is_null")
    private boolean isNull  = true;//列是否为null

    private int length  = 255;//列长度

    @javax.persistence.Column(name = "comment",length = 100)
    private String comment ;//列注释

    @javax.persistence.Column(name = "title",length = 50)
    private  String title;//标题

    private DataFlag flag;//数据类型



}
