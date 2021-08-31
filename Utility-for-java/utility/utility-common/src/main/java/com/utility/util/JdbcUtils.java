package com.utility.util;

import javax.sql.DataSource;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.sql.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * jdbc 公共 类
 * */
public final class JdbcUtils {

    private  JdbcUtils(){

    }
    private String url;
    private String driverClass;
    private String user;
    private String password;
    private  Connection connection;
    private  DataSource dataSource;//数据库 连接池
    //private ComboPooledDataSource  comboPooledDataSource;//数据库 连接池
    //org.apache.commons.dbcp.BasicDataSource basicDataSource;//数据库 连接池
    /**
     * 类字段 缓存
     * */
    public  static final   HashMap<Class<?>,List<Field>> ClassListHashMap=new HashMap<>();

    private JdbcUtils(String url,String driverClass,String user,String password){
        this.url=url;
        this.driverClass=driverClass;
        this.user=user;
        this.password=password;
    }

    private JdbcUtils(DataSource  dataSource){
        this.dataSource=dataSource;
    }

    /*private JdbcUtils(ComboPooledDataSource  comboPooledDataSource){
        this.comboPooledDataSource=comboPooledDataSource;
    }

    private JdbcUtils(org.apache.commons.dbcp.BasicDataSource basicDataSource){
        this.basicDataSource=basicDataSource;
    }
*/
    public static  JdbcUtils  create(String url,String driverClass,String user,String password){
        return  new JdbcUtils(url,driverClass,user,password);
    }

   /* public static  JdbcUtils  create(ComboPooledDataSource comboPooledDataSource){
        return  new JdbcUtils(comboPooledDataSource);
    }

    public static  JdbcUtils  create(org.apache.commons.dbcp.BasicDataSource basicDataSource){
        return  new JdbcUtils(basicDataSource);
    }*/

    public static  JdbcUtils  create(DataSource dataSource){
        return  new JdbcUtils(dataSource);
    }

    private Connection createConnection(){
        try {
            Class.forName(this.driverClass);
            Connection connection=DriverManager.getConnection(url,user,password);
            return  connection;
        }
        catch (ClassNotFoundException ex){
            ex.printStackTrace();
        }
        catch (SQLException ex){
            ex.printStackTrace();
        }
        return  null;
    }

    private Connection getConnection() throws SQLException {
     /*   if(this.basicDataSource!=null){
            return  this.basicDataSource.getConnection();
        }
        if(this.comboPooledDataSource!=null){
            return  this.comboPooledDataSource.getConnection();//默认 先使用 数据库 连接池
        }*/
       if(dataSource!=null){
           return  dataSource.getConnection();
       }
        if(this.connection==null)
        {
            synchronized (this){
                if(this.connection==null){
                    this.connection=createConnection();
                }
            }
        }
        return  this.connection;
    }

    public int execute(String sql){
        Statement statement=null;
        try {
             statement=getConnection().createStatement();
            int res=statement.executeUpdate(sql);
            return res;
        }catch (SQLException ex){
            ex.printStackTrace();
        }finally {
            closeStatement(statement);
        }
        return  -1;
    }

    public Map<String,Object> findSingle(String sql){
        List<Map<String,Object>> datas=find(sql, true);
        if(datas!=null){
            Map<String,Object> it= datas.get(0);
            return  it;
        }
        return  null;
    }

    public List<Map<String,Object>> findList(String sql){
        List<Map<String,Object>> datas=find(sql, false);
        return  datas;
    }

    public <T> T findSingle(String sql,Class<T> tClass){
        List<T> datas=find(sql, true,tClass);
        if(datas!=null){
            T it= datas.get(0);
            return  it;
        }
        return  null;
    }

    public <T> List<T > findList(String sql,Class<T> tClass){
        List<T > datas=find(sql, false,tClass);
        return  datas;
    }

    private List<Map<String,Object>> find(String sql,boolean single){
        Statement statement=null;
        ResultSet resultSet=null;
        try {
            statement=getConnection().createStatement();
            resultSet =statement.executeQuery(sql);
            List<Map<String,Object>> datas=find(resultSet,single);
            return datas;
        }catch (SQLException ex){
            ex.printStackTrace();
        }finally {
            closeStatement(statement);
            closeResultSet(resultSet);
        }
        return  null;
    }

    private <T> List<T> find(String sql,boolean single,Class<T> tClass){
        Statement statement=null;
        ResultSet resultSet=null;
        try {
            statement=getConnection().createStatement();
            resultSet =statement.executeQuery(sql);
            List<T> datas=find(resultSet,single,tClass);
            return datas;
        }catch (SQLException ex){
            ex.printStackTrace();
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } finally {
            closeStatement(statement);
            closeResultSet(resultSet);
        }
        return  null;
    }

    public int execute(String sql, Object... params){
        PreparedStatement preparedStatement=null;
        try {
            preparedStatement=getPreparedStatement(sql,params);
            int res=preparedStatement.executeUpdate();
            connection.commit();
            return res;
        }catch (SQLException ex){
           try {
               connection.rollback();
           }catch(Exception e) {}
            ex.printStackTrace();
        }finally {

            closeStatement(preparedStatement);
        }
        return  -1;
    }



    public Map<String,Object> findSingle(String sql, Object... params){
      List<Map<String,Object>> datas=find(sql, true,params);
      if(datas!=null)
      {
          Map<String,Object> it=datas.get(0);
          return  it;
      }
      return  null;
    }

    public  List<Map<String,Object>> findList(String sql, Object... params){
        List<Map<String,Object>> datas=find(sql, false,params);
        return  datas;
    }

    public <T> T findSingle(String sql, Class<T> tClass,Object... params){
        List<T> datas=find(sql, true,tClass,params);
        if(datas!=null)
        {
            T it=datas.get(0);
            return  it;
        }
        return  null;
    }

    public <T> List<T> findList(String sql,Class<T> tClass, Object... params){
        List<T> datas=find(sql, false,tClass,params);
        return  datas;
    }

    private <T> List<T> find(String sql,boolean single, Class<T> tClass,Object... params){
        PreparedStatement preparedStatement=null;
        ResultSet resultSet=null;
        try {
            preparedStatement=getPreparedStatement(sql, params);
            resultSet =preparedStatement.executeQuery();
            List<T> datas=find(resultSet,single,tClass);
            return datas;
        }catch (SQLException ex){
            ex.printStackTrace();
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } finally {
            closeStatement(preparedStatement);
            closeResultSet(resultSet);
        }
        return  null;
    }

    private List<Map<String,Object>> find(String sql,boolean single, Object... params){
        PreparedStatement preparedStatement=null;
        ResultSet resultSet=null;
        try {
            preparedStatement=getPreparedStatement(sql, params);
            resultSet =preparedStatement.executeQuery();
            List<Map<String,Object>> datas=find(resultSet,single);
            return datas;
        }catch (SQLException ex){
            ex.printStackTrace();
        }
        finally {
            closeStatement(preparedStatement);
            closeResultSet(resultSet);
        }
        return  null;
    }

    private  void  cache(Class<?> tClass){
        synchronized (JdbcUtils.ClassListHashMap){
            if(!JdbcUtils.ClassListHashMap.containsKey(tClass)){
                Field [] fields=tClass.getFields();
                List<Field> arrayList=new ArrayList<Field>();
                for (Field field : fields) {
                    arrayList.add(field);
                }
                JdbcUtils.ClassListHashMap.put(tClass,arrayList);
            }
        }
    }

    private <T> List<T> find(ResultSet resultSet,boolean single,Class<T> tClass) throws SQLException, NoSuchMethodException,
            IllegalAccessException, InvocationTargetException, InstantiationException, NoSuchFieldException {
        ResultSetMetaData resultSetMetaData=resultSet.getMetaData();//获取 列名
        int  length= resultSetMetaData.getColumnCount();
        List<T> datas=new ArrayList<>();
        T obj=tClass.getConstructor().newInstance();
        if(resultSet.next()){
            set(resultSetMetaData, resultSet, length, obj,tClass);
        }
        datas.add(obj);
        while (resultSet.next()){
            if(single)
                continue;
            obj=tClass.getConstructor().newInstance();
            set(resultSetMetaData, resultSet, length, obj,tClass);
            datas.add(obj);
        }
        return datas;
    }

    private <T> void  set(ResultSetMetaData resultSetMetaData,ResultSet resultSet,int length,T obj,Class<T> tClass) throws SQLException, NoSuchFieldException, IllegalAccessException {
        for (int i = 1; i <=length ; i++) {
            Field field=tClass.getDeclaredField(resultSetMetaData.getColumnName(i));
            field.setAccessible(true);
            field.set(obj,resultSet.getObject(i));
        }
    }

    private List<Map<String,Object>> find(ResultSet resultSet,boolean single) throws  SQLException{
        ResultSetMetaData resultSetMetaData=resultSet.getMetaData();//获取 列名
        int  length= resultSetMetaData.getColumnCount();
        List<Map<String,Object>> datas=new ArrayList<>();
        Map<String,Object> map=new HashMap<>();
        if(resultSet.next()){
            set(resultSetMetaData, resultSet, length, map);
        }
        datas.add(map);
        while (resultSet.next()){
            if(single)
                continue;
            map=new HashMap<>();
            set(resultSetMetaData, resultSet, length, map);
            datas.add(map);
        }
        return datas;
    }

    private  void  set(ResultSetMetaData resultSetMetaData,ResultSet resultSet,int length,Map<String,Object> map) throws SQLException {
        for (int i = 1; i <=length ; i++) {
            map.put(resultSetMetaData.getColumnName(i),resultSet.getObject(i));
        }
    }

    private PreparedStatement getPreparedStatement(String sql, Object... params) throws  SQLException{
        PreparedStatement preparedStatement=getConnection().prepareStatement(sql);
        if(params!=null&&params.length>0){
            for (int i = 1; i <=params.length ; i++) {
                /*if(params[i] instanceof  Object[]){
                    preparedStatement.setArray(i, (Array) params[i]);
                    continue;
                }*/
                preparedStatement.setObject(i, params[i]);
            }
        }
        return  preparedStatement;
    }

    private <T extends  Statement > void closeStatement(T statement){
        try {
            if(statement!=null&&!statement.isClosed()){
                statement.close();
            }
        }catch (SQLException ex){
            ex.printStackTrace();
        }
    }

    private  void  closeResultSet(ResultSet resultSet){
        try {
            if(resultSet!=null&&!resultSet.isClosed()){
                resultSet.close();
            }
        }catch (SQLException ex){
            ex.printStackTrace();
        }
    }

    private  void  closeConnection(Connection conn){
        try {
            if(conn!=null&&!conn.isClosed()){
                conn.close();
            }
        }catch (SQLException ex){
            ex.printStackTrace();
        }
    }

}
