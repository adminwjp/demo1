package com.utility.mybatis;

import org.apache.ibatis.session.SqlSession;
import java.io.Serializable;

public class MybatisTemplate {
    private SqlSession session;
    public MybatisTemplate(){

    }

    public MybatisTemplate(SqlSession session) {
        setSession(session);
    }

    public void setSession(SqlSession session) {
        this.session = session;
    }

    public Serializable add(String sql,Object object) {
        try {
            Serializable id = this.session.insert(sql,object);
            session.commit();
            return id;
        } catch (Exception ex) {
            session.rollback();
            return null;
        } finally {

        }
    }

    public <T> T get(String sql, Serializable id) {
        try {
            T obj = this.session.selectOne(sql, id);
            session.commit();
            return obj;
        } catch (Exception ex) {
            session.rollback();
            return null;
        } finally {

        }
    }

    public void update(String sql,Object object) {
        try {
            this.session.update(sql,object);
            this.session.commit();
        } catch (Exception ex) {
            this.session.rollback();
        } finally {

        }
    }

    public void delete(String sql,Object object) {
        try {
            this.session.delete(sql,object);
            session.commit();
        } catch (Exception ex) {
            session.rollback();
        } finally {

        }
    }
    public  void  close(){
        session.close();
    }
}
