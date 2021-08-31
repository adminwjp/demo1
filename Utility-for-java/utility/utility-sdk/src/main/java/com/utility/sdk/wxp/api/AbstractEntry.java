package com.utility.sdk.wxp.api;

import lombok.Data;
import javax.persistence.*;
import java.util.Date;

/**
 * 抽象基类
 */
@Data
@MappedSuperclass
public abstract class AbstractEntry
{
     String id;//主键编号
     Date createDate;//创建时间

    /**
     * 清除接口
     * */
    public abstract void Clear();
    /**
     * 基类清除接口
     * */
    public void ClearData()
    {
        this.id = "";
        this.createDate = null;
    }
}
