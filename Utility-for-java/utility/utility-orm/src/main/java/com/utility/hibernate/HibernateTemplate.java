package com.utility.hibernate;

import org.hibernate.Session;
import org.hibernate.Transaction;
import org.hibernate.query.NativeQuery;
import java.io.Serializable;

/**
 * hibernate template
 * */
public class HibernateTemplate {
    private Session session;//hibernate  session
    public  static  final  HibernateTemplate Instance=new HibernateTemplate();
    /**
     * no param constractor
     * */
    public HibernateTemplate(){

    }

    /**
     * no param constractor
     * @param session  session
     * */
    public HibernateTemplate(Session session) {
        setSession(session);
    }

    /**
     * set session
     * @param session  session
     * */
    public void setSession(Session session) {
        this.session = session;
    }

    /**
     * get session
     * @return  return  session
     * */
    public Session getSession() {
        return  this.session;
    }
    /**
     * insert
     * @param object entity
     * @return  reutrn entity id
     * */
    public Serializable insert(Object object) {
        Transaction tran = (Transaction) null;
        try {
            tran = this.session.beginTransaction();
            Serializable id = this.session.save(object);
            tran.commit();
            return id;
        } catch (Exception ex) {
            this.rollback(tran, ex);
            return null;
        }
    }

    /**
     * according id select single entity
     * @param id  id
     * @param tClass  entity class type
     * @return  reutrn entity result
     * */
    public <T> T get(Serializable id, Class<T> tClass) {
        Transaction tran = (Transaction) null;
        try {
            tran = this.session.beginTransaction();
            T obj = this.session.get(tClass, id);
            tran.commit();
            return obj;
        } catch (Exception ex) {
            this.rollback(tran, ex);
            return null;
        }
    }

    /**
    * update
    * @param object  entity
    * **/
    public void update(Object object) {
        Transaction tran = (Transaction) null;
        try {
            tran = this.session.beginTransaction();
            this.session.update(object);
            tran.commit();
        } catch (Exception ex) {
            this.rollback(tran, ex);
        }
    }

    /**
     * delete
     * @param object  entity
     * **/
    public void delete(Object object) {
        Transaction tran = (Transaction) null;
        try {
            tran = this.session.beginTransaction();
            this.session.delete(object);
            tran.commit();
        } catch (Exception ex) {
            this.rollback(tran, ex);
        }
    }

    /**
     * delete
     * @param id  entity id
     * **/
    public <T> void delete(Serializable id,Class<T> cla) {
        Transaction tran = (Transaction) null;
        try {
            tran = this.session.beginTransaction();
            T a=session.get(cla,id);
            this.session.delete(a);
            tran.commit();
        } catch (Exception ex) {
            this.rollback(tran, ex);
        }
    }

    /**
     * inset delete update
     * @param sql  sql
     * @param vals  param format
     * @return  reutrn operator result
     * */
    public int execute(String sql, Object[] vals) {
        Transaction tran = (Transaction) null;
        try {
            tran = this.session.beginTransaction();
            NativeQuery query = this.session.createNativeQuery(sql);
            if (vals != null) {
                int i = 1;
                for (Object it : vals) {
                    query = query.setParameter(i, it);
                    i++;
                }
            }
            int res = query.executeUpdate();
            tran.commit();
            return res;
        } catch (Exception ex) {
            this.rollback(tran, ex);
            return -1;
        }
    }

    /**
     * exception catch
     * @param transaction  transaction
     * @param ex  exception
     * */
    private void rollback(Transaction transaction, Exception ex) {
        ex.printStackTrace();
        if (transaction != null) {
            transaction.rollback();
        }
    }

    /**
     * close session
     * */
    public  void close(){
        session.close();
    }
}
