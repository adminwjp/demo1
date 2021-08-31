package com.utility.service.dto;

import lombok.Data;

import java.util.List;

/**
 * 记录 数据 信息
 * */
@Data
public class ResultDto<T> {
    List<T> data;//数据
    RecordDto result;//分页 记录信息


}
