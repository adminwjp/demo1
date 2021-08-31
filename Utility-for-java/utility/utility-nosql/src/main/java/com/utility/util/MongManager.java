package com.utility.util;

import com.mongodb.MongoClient;
import com.mongodb.MongoCredential;
import com.mongodb.ServerAddress;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCursor;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.model.Filters;
import com.mongodb.client.result.UpdateResult;
import org.bson.Document;
import org.bson.conversions.Bson;
import java.util.ArrayList;
import java.util.List;

public class MongManager {
    private MongoClient _client = null;
    private MongoDatabase _db = null;
    public  MongManager(){

    }
    public  MongManager(final String databaseName,final String hostName){
        connect(databaseName,hostName);
    }
    public  MongManager(final String databaseName,final String hostName,final int port,final String username,final String password){
        connect(databaseName,hostName,port,username,password);
    }
    /**
     * 链接数据库
     *
     * @param databaseName 数据库名称
     * @param hostName     主机名
     */
    public void connect(String databaseName, String hostName) {
        this._client = new MongoClient(hostName);
        this._db = this._client.getDatabase(databaseName);
    }

    /**
     * 链接数据库
     *
     * @param databaseName 数据库名称
     * @param hostName     主机名
     * @param port         端口
     * @param username     账户
     * @param password     密码
     */
    public void connect(String databaseName, String hostName, int port, String username, String password) {
        ServerAddress serverurl = new ServerAddress(hostName, port);
        List<ServerAddress> lists = new ArrayList<ServerAddress>();
        lists.add(serverurl);
        MongoCredential credential = MongoCredential.createCredential(username, databaseName, password.toCharArray());
        List<MongoCredential> listm = new ArrayList<MongoCredential>();
        listm.add(credential);
        this._client = new MongoClient(lists, listm);
        this._db = this._client.getDatabase(databaseName);
    }

    /**
     * 关闭连接
     */
    public void closeClient() {
        if (this._client != null) {
            this._client.close();
        }
    }
    /**
     * 插入一个文档
     *
     * @param tab
     * @param obj    文档
     * @param tClass
     */
    public <T> void insert(String tab, T obj, Class<T> tClass) {

        this._db.getCollection(tab, tClass).insertOne(obj);
    }

    /**
     * 插入一个文档
     *
     * @param tab
     * @param objs   文档
     * @param tClass
     */
    public <T> void insertMany(String tab, List<T> objs, Class<T> tClass) {

        this._db.getCollection(tab, tClass).insertMany(objs);
    }
    public <T> void findOneAndDelete(String tab, Class<T> tClass,Bson where) {

        this._db.getCollection(tab, tClass).findOneAndDelete(where);
    }
    /**
     * 查询所有文档
     *
     * @param tab collectionName
     * @return 所有文档集合
     */
    public List<Document> findAll(String tab) {
        return findAll(tab, Document.class);
    }

    /**
     * 查询所有文档
     *
     * @param tab    collectionName
     * @param tClass
     * @return 所有文档集合
     */
    public <T> List<T> findAll(String tab, Class<T> tClass) {
        return findBy(tab, tClass, null);
    }

    /**
     * 根据条件查询
     *
     * @param tab    collectionName
     * @param filter 查询条件 //注意Bson的几个实现类，BasicDBObject, BsonDocument,
     *               BsonDocumentWrapper, CommandResult, Document, RawBsonDocument
     * @return 返回集合列表
     */
    public <T> List<T> findBy(String tab, Class<T> tClass, final Bson filter) {
        return findByPage(tab,tClass,filter,-1,-1);
    }

    /**
     * 根据条件查询
     *
     * @param tab    collectionName
     * @param filter 查询条件 //注意Bson的几个实现类，BasicDBObject, BsonDocument,
     *               BsonDocumentWrapper, CommandResult, Document, RawBsonDocument
     * @param  page page
     * @param  size size
     * @return 返回集合列表
     */
    public <T> List<T> findByPage(String tab, Class<T> tClass, final Bson filter,  int page, int size) {
        List<T> results = new ArrayList<>();
        FindIterable<T> iterables;
        if (filter == null) {
            iterables = this._db.getCollection(tab, tClass).find();
        } else iterables = this._db.getCollection(tab, tClass).find(filter);
        if(page!=-1&&size!=-1){
            if(page<=0){
                page=1;
            }
            if(size<=0){
                size=10;
            }
            iterables.skip((page-1)*size).limit(size);//分页
        }
        MongoCursor<T> cursor = iterables.iterator();
        while (cursor.hasNext()) {
            results.add(cursor.next());
        }
        return results;
    }

    /**
     * 更新查询到的第一个
     *
     * @param tab    collectionName
     * @param filter 查询条件
     * @param update 更新文档
     * @return 更新结果
     */
    public UpdateResult updateOne(String tab, Bson filter, Bson update) {
        UpdateResult result = this._db.getCollection(tab).updateOne(filter, update);
        return result;
    }

    /**
     * 应该是官方的update策略有变化，3.0以前，使用updateOne，3.0以后使用replaceOne $
     * */
    public <T> T findOneAndUpdate(String tab, Class<T> tClass, Bson filter, Bson update) {
        T result = this._db.getCollection(tab,tClass).findOneAndUpdate(filter, update);
        return result;
    }

    public <T> T findOneAndReplace(String tab, Class<T> tClass, Bson filter, T update) {
        T result = this._db.getCollection(tab,tClass).findOneAndReplace(filter, update);
        return result;
    }

    /**
     * 更新查询到的所有的文档
     *
     * @param tab    collectionName
     * @param filter 查询条件
     * @param update 更新文档
     * @return 更新结果
     */
    public UpdateResult updateMany(String tab, Bson filter, Bson update) {
        UpdateResult result = this._db.getCollection(tab).updateMany(filter, update);
        return result;
    }

    /**
     * 更新一个文档, 结果是replacement是新文档，老文档完全被替换
     *
     * @param tab         collectionName
     * @param filter      查询条件
     * @param replacement 跟新文档
     */
    public void replace(String tab, Bson filter, Document replacement) {
        this._db.getCollection(tab).replaceOne(filter, replacement);
    }

    /**
     * 根据条件删除一个文档
     *
     * @param tab    collectionName
     * @param filter 查询条件
     */
    public void deleteOne(String tab, Bson filter) {
        this._db.getCollection(tab).deleteOne(filter);
    }

    /**
     * 根据条件删除多个文档
     *
     * @param tab    collectionName
     * @param filter 查询条件
     */
    public void deleteMany(String tab, Bson filter) {
        this._db.getCollection(tab).deleteMany(filter);
    }

    public  <T> T get(String tab,Object id,Class<T> tClass){
        return  get(tab,Filters.eq("_id",id),tClass);
    }

    public  <T> T get(String tab,Bson where,Class<T> tClass){
        MongoCursor<T> it=this._db.getCollection(tab,tClass).find(where).iterator();
       if( it.hasNext()){
           return  it.next();
       }
        return  null;
    }

}
