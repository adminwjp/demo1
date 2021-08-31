package com.utility.hibernate;

import com.utility.util.StringUtil;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;

/**
 * hibernate session manager
 * */
public class HibernateFactory {
    //session thread security manager
    private static final ThreadLocal<Session> session = new ThreadLocal<Session>();
    //hibernate session factory
    private static SessionFactory sessionFactory;
    //hibernate  configuration
    private static Configuration configuration = new Configuration();

   /* static {
        config("");
    }*/

    private HibernateFactory() {

    }

    /**
     * hibernate  configuration,
     * initial  hibernate session factory
     * @param config  config file path
     * */
    public  static  void config(String config){
        try {
            if(StringUtil.isBlank(config))
            {
                configuration.configure();
            }
            else{
                configuration.configure(config);
            }
            sessionFactory = configuration.buildSessionFactory();
        } catch (Exception ex) {
            System.err.println("error created sessionFactory");
            ex.printStackTrace();
        }
    }

    /**
     * get session
     *  create   if not exists session,
     * if or open session  of session close
     * @return   return session
     * */
    public static Session getSession() {
        Session session = HibernateFactory.session.get();
        if (session == null) {
            session = sessionFactory.openSession();
            HibernateFactory.session.set(session);
        }else {
            if(!session.isOpen()){
                session.close();
                session = sessionFactory.openSession();
                HibernateFactory.session.set(session);
            }
        }
        return session;
    }
}
