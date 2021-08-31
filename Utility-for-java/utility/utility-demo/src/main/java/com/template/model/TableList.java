package com.template.model;

import lombok.Data;
import org.hibernate.annotations.NotFound;
import org.hibernate.annotations.NotFoundAction;

import javax.persistence.*;
import java.io.Serializable;
import java.util.Set;

/**
 * 列表
 * */
@Data
@Entity(name = "t_table_list")
public class TableList implements Serializable {
    @Id
    @javax.persistence.Column(length = 36)
    private  String id;//id

    @javax.persistence.Column(name = "title",length = 50)
    private  String title;//表格名称

    private boolean add=true;//是否支持添加表单

    private boolean modify=true;//是否支持修改表单

    private boolean delete=true;//是否支持删除表单

    @javax.persistence.Column(name = "table_modify")
    private boolean tableModify=true;//表格是否支持修改操作

    @javax.persistence.Column(name = "table_delete")
    private boolean tableDelete=true;//表格是否支持删除操作

    @javax.persistence.Column(name = "table_preview")
    private boolean tablePreview=true;//表格是否支持预览操作
    //可能表格还有其他操作 先写死后面再改

    @OneToMany(fetch= FetchType.LAZY,cascade = CascadeType.ALL)
    @JoinColumn(name="table_list_id",foreignKey =@ForeignKey(name = "fk_table_list_id") )
    @NotFound(action =  NotFoundAction.IGNORE)
    private Set<ColumnRelation> columns;//列信息

}
