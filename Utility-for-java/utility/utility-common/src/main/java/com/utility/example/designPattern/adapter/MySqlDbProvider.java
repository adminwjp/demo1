package com.utility.example.designPattern.adapter;

import com.utility.service.dto.Tuple;

public class MySqlDbProvider {
   public Tuple<Integer,Object> operator(String sql, String[] params){
       return  new Tuple<Integer,Object>(1,null);
   }
}
