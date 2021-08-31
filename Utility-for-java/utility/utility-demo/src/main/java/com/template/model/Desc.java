package com.template.model;

import com.template.model.enums.DescFlag;
import lombok.Data;
import org.hibernate.annotations.NotFound;
import org.hibernate.annotations.NotFoundAction;
import javax.persistence.*;
import java.io.Serializable;
import java.util.Set;

/**
 * 描述
 * */
@Entity(name ="t_desc")
/*@Entity
@Table(name ="t_desc")*/
@Data
public class Desc  implements Serializable {
    @Id
    @javax.persistence.Column(length = 36)
    //@GeneratedValue(strategy = GenerationType.IDENTITY)
    private  String id;//id

    private String name;//名称

    @javax.persistence.Column(name = "`desc`",length = 500)
    private String desc;//描述

    @javax.persistence.Column(name = "english_desc",length = 500)
    private String englishDesc;//英文描述

    @Enumerated(EnumType.ORDINAL)
    private DescFlag flag;//分类标识

    @ManyToOne(fetch= FetchType.LAZY,cascade = CascadeType.ALL)
    @JoinColumn(name="parent_id",foreignKey =@ForeignKey(name = "fk_parent_id") )
    @NotFound(action =  NotFoundAction.IGNORE)
    private  Desc parent;//父分类

    @OneToMany(fetch= FetchType.LAZY,cascade = CascadeType.ALL)
    @JoinColumn(name="parent_id",foreignKey =@ForeignKey(name = "fk_parent_id") )
    @NotFound(action =  NotFoundAction.IGNORE)
    private Set<Desc> children;//子分类

    @Transient
    private  String parentId;//父分类 id
}
