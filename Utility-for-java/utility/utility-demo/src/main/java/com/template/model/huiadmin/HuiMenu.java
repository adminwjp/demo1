package com.template.model.huiadmin;

import lombok.Data;

import java.io.Serializable;
import java.util.Set;
@Data
public class HuiMenu  implements Serializable {
    private  long id;//id
    private HuiIcon icon ;//图标
    private String href ;//链接
    private String name;//名称
    private  HuiMenu parent;//父分类
    private Set<HuiMenu> children;//子分类

    private  long iconId;
    private  long parentId;

}
