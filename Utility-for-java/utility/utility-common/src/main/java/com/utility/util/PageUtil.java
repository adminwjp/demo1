package com.utility.util;

import com.utility.service.dto.RecordDto;

public class PageUtil {
    public  static RecordDto getRecordModel(int page, int size, long total){
        RecordDto recordModel=new RecordDto();
        recordModel.setPage(page);
        recordModel.setSize(size);
        recordModel.setRecords(total);
        long to=total%size==0?total/size:(total/size+1);
        recordModel.setTotal(to);
        return recordModel;
    }
}
