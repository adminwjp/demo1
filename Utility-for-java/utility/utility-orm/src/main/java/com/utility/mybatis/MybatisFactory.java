package com.utility.mybatis;

import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;
import java.io.IOException;
import java.io.InputStream;

//https://mybatis.org/mybatis-3/zh/java-api.html#sqlSessions
public class MybatisFactory {
  static SqlSessionFactory sqlSessionFactory;
  private static final ThreadLocal<SqlSession> _session = new ThreadLocal<SqlSession>();
  public  static  final  String DefaultConfig="mybaits.xml";
  static  {
    //  config(DefaultConfig);
  }

    public  static  void config(String config){
        InputStream inputStream=null;
        try {
            //读取配置文件
            inputStream = Resources.getResourceAsStream(config);
            sqlSessionFactory=new SqlSessionFactoryBuilder().build(inputStream);
        }catch (IOException ex){
            ex.printStackTrace();
        }finally {
            if(inputStream!=null){
                try {
                    inputStream.close();
                }catch (IOException ex){
                    ex.printStackTrace();
                }
            }
        }
    }
    public static SqlSession openSession() {
      return  getSession(true);
    }

    public static SqlSession getSession(final  boolean  open) {
        SqlSession session = _session.get();
        if (open||session == null) {
            session = sqlSessionFactory.openSession();
            _session.set(session);
        }
        return session;
    }
}
