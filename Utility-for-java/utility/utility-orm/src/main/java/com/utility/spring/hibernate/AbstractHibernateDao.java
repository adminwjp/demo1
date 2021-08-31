package com.utility.spring.hibernate;

import com.utility.service.dto.RecordDto;
import com.utility.service.dto.ResultDto;
import com.utility.util.PageUtil;
import com.utility.util.StringUtil;
import org.hibernate.Criteria;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.criterion.Criterion;
import org.hibernate.criterion.Projection;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Predicate;
import javax.persistence.criteria.Root;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * HibernateTemplate 继承话 不需要 跟 spring (txManger)组合 否则 报错
 * */
@Transactional  //事务也必须配置，否则启动会报找不到事务的错误
public abstract class AbstractHibernateDao<T> /*extends HibernateTemplate*/  {

    protected  Class<T> entityClass=null;

    /**
     * accoring table name generator sql
     * @<code>SELECT * FROM tableName</code>
     * @param tableName  tableName
     * @return  return SELECT * FROM tableName
     * */
    protected  String getQuerySql(String tableName){
        return  "SELECT * FROM "+tableName;
    }

    /**
     * accoring table name generator sql
     * @<code> FROM tableName</code>
     * @param tableName  tableName
     * @return  return FROM tableName
     * */
    protected  String getSuffixQuerySql(String tableName){
        return  " FROM "+tableName;
    }

    @Autowired  //bean对象装载注解注入
    protected SessionFactory sessionFactory;

    /**
     * insert
     * @param obj entity
     * @return  reutrn entity id
     * */
   /* public int insert(T obj){
        int res=this.insertByInt(obj);//主键 id
        return res;
    }*/
    public void insert(T obj){
       saveBySerializable(obj);
    }

    /**
     * update
     * @param obj entity
     * @return  reutrn entity id
     * */
    public  void  update(T obj){
        this.saveBySerializable(obj);
    }

    /**
     * according id delete  entity
     * @param id  id
     * @param tClass  entity class type
     * */
    public void  delete(Serializable id,Class<T> tClass){
        this.deleteByObject(this.getByT(tClass, id));
    }

    /**
     * according id delete  entity
     * @param id  id
     * */
    public void  delete(Serializable id){
        this.delete( id,entityClass);
    }
    /**
     * according id delete  entity
     * @param ids  id
     * */
    public int  delete(Serializable[] ids)
    {
       return   this.delete( ids,entityClass);
    }

    /**
     * according id delete  entity
     * @param ids  id
     * @param tClass  entity class type
     * */
    public int  delete(Serializable[] ids,Class<T> tClass){
        Session session=this.getSession();
        CriteriaBuilder criteriaBuilder=session.getCriteriaBuilder();
        CriteriaQuery<T> criteriaQuery= criteriaBuilder.createQuery(tClass);
        Root<T> root = criteriaQuery.from(tClass);
        List<Predicate> predicates=new ArrayList<>();
        for (Serializable id:ids) {
            Predicate predicate=criteriaBuilder.equal(root.get("id"), id);
            predicates.add(predicate);
        }
        Predicate[] predicates1=new Predicate[predicates.size()];
        criteriaBuilder.or(predicates.toArray(predicates1));
        int res= getSession().createQuery(criteriaQuery).executeUpdate();
        return  res;
    }

    public ResultDto<T> findList(T obj, int page, int size, Class<T> tClass){
        Session session=this.getSession();
        CriteriaBuilder criteriaBuilder=session.getCriteriaBuilder();
        CriteriaQuery<T> criteriaQuery= criteriaBuilder.createQuery(tClass);
        Root<T> root = criteriaQuery.from(tClass);
        where(obj, criteriaBuilder, root);//查询条件
        List<T> data= session.createQuery(criteriaQuery).setMaxResults(size).setFirstResult((page-1)*size).list();//分页记录
        where(obj, criteriaBuilder, root);

 /*       CriteriaBuilder criteriaBuilderCount=session.getCriteriaBuilder();
        CriteriaQuery<T> criteriaQueryCount= criteriaBuilder.createQuery(tClass);
        Root<T> rootCount = criteriaQuery.from(tClass);
        Expression<Long> longExpression= criteriaBuilderCount.count( whereCount(obj, criteriaBuilderCount, rootCount));
        session.createQuery(criteriaQueryCount).uniqueResult();*/
        Criteria criteria = null;
        try {
            criteria = session.createCriteria(tClass);//新 版本 文档 不清楚
        } catch (Exception e) {
            e.printStackTrace();
        }
        criteria.setProjection(Projections.rowCount());
        where(criteria, obj);//查询条件
        long count=(long)criteria.uniqueResult();
        ResultDto<T> resultModel=new ResultDto<>();
        resultModel.setData(data);
        RecordDto recordModel= PageUtil.getRecordModel(page, size, count);
        resultModel.setResult(recordModel);
        return  resultModel;
    }
    protected  abstract  void  where(T obj,CriteriaBuilder criteriaBuilder,Root<T> root);
    protected  abstract  void  where(Criteria criteria,T obj);

    public  T get(Serializable id){
        return  getByT(entityClass, id);
    }
    public  T getByT(Serializable id){
        return  getByT(entityClass, id);
    }
    public  T getByT(Class<T> tClass,Serializable id){
        return  getSession().get(tClass, id);
    }

    public Serializable saveBySerializable(Object obj){
        return  getSession().save(obj);
    }

    public void updateByObject(Object obj){
          getSession().update(obj);
    }

    public void deleteByObject(Object obj){
        getSession().delete(obj);
    }

    protected  <T> List<T> findByWhere(List<Criterion> ors, List<Criterion> ands, Class<T> tClass){
        Criteria criteria=getSession().createCriteria(tClass);
        Criterion criterion=null;
        Criterion or=null;
        Criterion and=null;
        if(ors!=null&&ors.size()>0){
            or =(Criterion) Restrictions.or((Criterion[])ors.toArray());
        }
        if(ands!=null&&ands.size()>0){
            and=(Criterion) Restrictions.and((Criterion[])ands.toArray());
        }
        if(or!=null&&and!=null){
            criterion=(Criterion) Restrictions.or(or,and);
        }
        else {
            criterion=or!=null?or : and;
        }
        if(criterion!=null){
            criteria.add(criterion);
        }
        List<T> datas=(List<T>)criteria.list();
        return  datas;
    }

    protected  <T> List<T> findByWhere(CriteriaQuery<T> criteriaQuery, Class<T> tClass){
        List<T> datas=getSession().createQuery(criteriaQuery).getResultList();
        return  datas;
    }

    public  int delete(String table,Long[] id) {
        String ids = StringUtil.getString(id, ",");
        int res = getSession().createNativeQuery("delete " + getSuffixQuerySql(table) + " where id in (" + ids + ")").executeUpdate();
        return res;
    }

    protected <T> List<T> findList(String table,Class<T> tClass){
        List<T> datas=getSession().createQuery(getSuffixQuerySql(table),tClass).getResultList();
        return  datas;
    }

    protected <T> void createQuery(CriteriaBuilder criteriaBuilder, List<Predicate> ors, List<Predicate> ands, Class<T> tClass){
        Predicate predicate=null;
        Predicate or=null;
        Predicate and=null;
        if(ors!=null&&ors.size()>0){
            criteriaBuilder.or((Predicate[])ors.toArray());
        }
        if(ands!=null&&ands.size()>0){
            and=criteriaBuilder.or((Predicate[])ands.toArray());
        }
        if(or!=null&&and!=null){
            predicate=criteriaBuilder.or(or,and);
        }
        else {
            predicate=or!=null?or : and;
        }
        if(predicate!=null){
            criteriaBuilder.and(predicate);
        }
    }

    //获取和当前线程绑定的Seesion

    protected Session getSession()
    {
        return sessionFactory.getCurrentSession();
    }

    public   long  insertByLong(Object obj){
        return (long)saveBySerializable(obj);
    }
    public   int  insertByInt(Object obj){
        return (int)saveBySerializable(obj);
    }

    /**
     * according id delete eneity
     * @param table  table name
     * @param id  id
     * @return  return result
     * */
    public   int   delete(String table,Long id){
       int res=getSession().createNativeQuery("delete  from "+table +" where id =?").setParameter(1, id).executeUpdate();
       return  res;
    }

    /**
     * according conditional or pager select
     * @param page  page number
     * @param size  records of page
     * @param projections  conditional
     * @param tClass  entity class type
     * @return  return list result
     * */
    public <T> List<T> getListBypage(int page,int size,List<Projection> projections, Class<T> tClass){
        /*return  get(id,tClass);*/
      /*  getSession().getCriteriaBuilder().createQuery(tClass).*/
  /*      getSession().createEntityGraph(tClass)   */
        Criteria criteria=getSession().createCriteria(tClass);//最新 文档 没 找到 算 了 用 之前的老版本 方法
        if(projections!=null&&projections.size()>0){
            for (Projection it:projections ) {
                criteria.setProjection(it);
            }
        }
        return criteria
                .setFirstResult((page-1)*size).setMaxResults(size).list();
    }

    /**
     * according conditional or pager select
     * @param page  page number
     * @param size  records of page
     * @param tClass  entity class type
     * @return  return list result
     * */
    public <T> List<T> getList(int page,int size,Class<T> tClass){
        Criteria criteria=getSession().createCriteria(tClass);//最新 文档 没 找到 算 了 用 之前的老版本 方法
         return criteria
                .setFirstResult((page-1)*size).setMaxResults(size).list();
    }

    /**
     * according conditional or pager select
     * @param page  page number
     * @param size  records of page
     * @return  return list result
     * */
    public List<T> getList(int page,int size){
        return getList(page,size,entityClass);
    }
}