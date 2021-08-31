package com.utility.util;

import org.modelmapper.ModelMapper;

import java.util.ArrayList;
import java.util.List;

public class MapperUtils {
    public   static  final  ModelMapper modelMapper = new ModelMapper();

    public static   <T,F> F mapper(T sourceObj,Class<T> source,Class<F> target){
        F obj= modelMapper.map(sourceObj, target);
        return  obj;
    }

    public static   <T,F> List<F> mapper(List<T> sourceObjs, Class<T> source, Class<F> target){
        List<F> datas=new ArrayList<>();
        for (T it:sourceObjs) {
            F da=mapper(it,source,target);
            datas.add(da);
        }
        return  datas;
    }
}
