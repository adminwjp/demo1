package com.utility.service.dto;

import lombok.Data;

/**
 * 数据 记录 信息
 * */
@Data
public class RecordDto {
    private  int page;//页数
    private  int size;//每页 记录
    private  long records;//总 记录 数
    private  long total;//总页数


}
