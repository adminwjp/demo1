package com.utility.example.designPattern.adapter;

import com.utility.service.dto.Tuple;
import lombok.Data;

@Data
public class DbAdapter
        //继承 话 不是 真正的适配器了
        // extends  MySqlDbProvider
        implements  IDbAdapter {

  MySqlDbProvider mySqlDbProvider=new MySqlDbProvider();

  @Override
  public Tuple<Integer, Object> operator(String sql, String[] params) {
    return mySqlDbProvider.operator(sql,params);
  }
}
