package com.template.model;

import lombok.Data;
import org.hibernate.annotations.NotFound;
import org.hibernate.annotations.NotFoundAction;

import javax.persistence.*;
import java.io.Serializable;

/**
 * 表单列关联
 * */
@Data
@Entity(name = "t_column_relation")
public class ColumnRelation implements Serializable {
    @Id
    @javax.persistence.Column(length = 36)
    private  String id;//id

    @OneToOne
    private Column column;//列

    @ManyToOne(fetch= FetchType.LAZY,cascade = CascadeType.ALL)
    @JoinColumn(name="table_list_id",foreignKey =@ForeignKey(name = "fk_table_list_id") )
    @NotFound(action =  NotFoundAction.IGNORE)
    private  TableList tableList;//表格

    private  int order;//排序

    @Transient
    private String columnId;

    @Transient
    private  String tableListId;
}
