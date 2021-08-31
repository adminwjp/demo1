package com.shop.utils;

import com.utility.util.JdbcUtils;
import org.apache.commons.dbcp.BasicDataSource;

public class SqlFactory {
    //@Autowired
    private    org.apache.commons.dbcp.BasicDataSource dataSource;

    public  static JdbcUtils jdbcUtils;

    private  static  final  Object lock=new Object();

    public BasicDataSource getDataSource() {
        return dataSource;
    }

    public void setDataSource(BasicDataSource dataSource) {
       this.dataSource = dataSource;
         if(jdbcUtils==null&& this.dataSource!=null){
             synchronized (lock){
                if(jdbcUtils==null){
                    jdbcUtils=JdbcUtils.create( this.dataSource);
                }
            }
        }
    }

}
