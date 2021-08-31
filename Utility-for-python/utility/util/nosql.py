import pymongo
import redis   # 导入redis 模块
import threading

def Singleton(cls):
    """ 实现单例模式 使用装饰器 """
    _instance = {}
    __thred_lock=threading.Lock()
    def _singleton(*args, **kargs):
        if cls not in _instance:
             with RedisUtil.__thred_lock:
                if cls not in _instance:
                    _instance[cls] = cls(*args, **kargs)
        return _instance[cls]

    return _singleton


@Singleton
class MongUtil(object):
    """ 
    mongodb
    pip3 install pymongo
    https://www.runoob.com/python3/python-mongodb.html
    """
    host="mongodb://127.0.0.1:27017/"

    def __init__(self, host):
        self.host=host

    def init(self):
        self.client = pymongo.MongoClient(self.host)

    def exists_db(self, name):
        dblist = self.client.list_database_names()
        # dblist = self.client.database_names() 
        return name in dblist

    def exists_col(self, db, name):
        dblist = self.client.list_database_names()
        # dblist = self.client.database_names() 
        ex= db in dblist
        if ex :
            collist = dblist[db].list_collection_names()
            # collist = dblist[db].collection_names()
            return name in collist
        else:
            return False

    def insert(self, db, col, data):
        dblist = self.client.list_database_names()
        d= dblist[db]
        c=d[col]
        c.inert_one(data)

    def insert_many(self, db, col, data_list):
        dblist = self.client.list_database_names()
        d= dblist[db]
        c=d[col]
        c.insert_many(data_list)

    def update(self, db, col, query, data):
        dblist = self.client.list_database_names()
        d= dblist[db]
        c=d[col]
        c.update_one(query,data)

    def update_many(self, db, col,query, data):
        dblist = self.client.list_database_names()
        d= dblist[db]
        c=d[col]
        c.update_many(query,data)

    def delete(self, db, col, query):
        dblist = self.client.list_database_names()
        d= dblist[db]
        c=d[col]
        c.delete_one(query)

    def delete_many(self, db, col,query):
        dblist = self.client.list_database_names()
        d= dblist[db]
        c=d[col]
        c.delete_many(query)


class RedisUtil(object):
    """ 
    redis 
    https://www.runoob.com/w3cnote/python-redis-intro.html
    """
    host="127.0.0.1"
    port=6379
    db=0
    password=None
    __thred_lock=threading.Lock()
    @classmethod
    def instance(cls, *args, **kwargs):
        if not hasattr(RedisUtil,"_instance"):
            with RedisUtil.__thred_lock:
                 if not hasattr(RedisUtil,"_instance"):
                     RedisUtil._instance=RedisUtil(*args, **kwargs)
        return RedisUtil._instance



    def conn(self):
        """ 链接服务器  """
        self.pool = redis.ConnectionPool(host=self.host, port=self.port, db=self.db,password=self.password, decode_responses=True)
        # self.redis =self.pool.get_connection() # redis.StrictRedis(host=self.host, port=self.port, db=self.db,password=self.password)
    

    def get_list_last(self, key):
       """  查询并删除redis数据  """
       #r=self.pool.get_connection()
       r =redis.StrictRedis(connection_pool=self.pool)
       try:
          va=r.rpop(key)
          return va
       finally:
         self.pool.release(r)

    def get_set_first(self, key):
       """  查询redis数据  """
       #r=self.pool.get_connection()
       r =redis.StrictRedis(connection_pool=self.pool)
       try:
          va=r.g
          r.xdel(key,va)
          return va
       finally:
         self.pool.release(r)

    def delete(self, key):
       """  根据key删除数据  """
       #r=self.pool.get_connection()
       r =redis.StrictRedis(connection_pool=self.pool)
       try:
          r.delete(key)
          return True
       finally:
         self.pool.release(r)

    def add(self, key, val):
       """  数据回写Redis  """
       #r=self.pool.get_connection()
       r =redis.StrictRedis(connection_pool=self.pool)
       try:
          r.set(key,val)
          return True
       finally:
         self.pool.release(r)

    def set(self, key, val):
       """  数据回写Redis  """
       #r=self.pool.get_connection()
       r =redis.StrictRedis(connection_pool=self.pool)
       try:
          r.sadd(key,val)
          return True
       finally:
         self.pool.release(r)


# pip3 install elasticsearch  -i http://pypi.douban.com/simple --trusted-host pypi.douban.com
from elasticsearch import Elasticsearch

@Singleton
class ElasticsearchUtil:
    def __init__(self):
        self.host="127.0.0.1"
        self.port=9200
        self.username=""
        self.pwd=""
    pass
    def conn(self):
        # 连接ES
        if self.username!="" or self.pwd!="":
            # 若需验证
            self.es = Elasticsearch([self.host], http_auth=(self.username, self.pwd), timeout=3600)
        else:
            self.es = Elasticsearch([{"host":self.host,"port":self.port}], timeout=3600)
   
    def insert(self,index,body):
        self.es.index(index=index,body=body)

    def insert(self,index,id):
        self.es.delete(index=index,id=id)

    def update(self,index,id,body):
        self.es.update(index=index,id=id,body=body)
        