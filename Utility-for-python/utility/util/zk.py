import json
# pip3 install kazoo

from kazoo.client import KazooClient

import time

# https://www.cnblogs.com/yyds/p/6901864.html
import logging

import threading

# Parallel
# https://www.parallelpython.com/downloads.php
# python setup.py install
import math, sys
import pp

# Python 的模块就是天然的单例模式，因为模块在第一次导入时，会生成 .pyc 文件，
# 当第二次导入时，就会直接加载 .pyc 文件，而不会再次执行模块代码
class ZkUtil(object):

    node="/info/"
    interface_domain=""

    def __init__(self):
       self.host = ""

    def __init__(self,host):
       self.host = ""

    def start(self):
       self.zk = KazooClient(hosts=self.host)
       self.zk.start()
    
    def __roll_task(self):
        """ 定时轮询任务  """
        while(True):
            path=self.node+self.interface_domain
            self.get_interface_node(path)
            time.sleep(3600)
        else:
           print("定时轮询任务结束")
    
    def create_zk(self):
        """ 初始化zookeeper实例  """
        try:
            if self.zk != None:
                self.zk.close()
            self.zk = KazooClient(hosts=self.host,timeout=10.0)
            self.event_bind_tm()
            return True
        except:
            logging.error("初始化zookeeper实例 Unexpected error:",sys.exc_info())
            return False
    
    def event_bind_tm(self):
        """ 事件绑定  """
        try:
          path=""
          stat=self.zk.exists(path)
          self.zk.get_children(path,self.watch_node,False)
          # return True
          pass
        except:
          logging.error("zookeeper 事件绑定 Unexpected error:",sys.exc_info())
          #再次抛出异常
          #raise
          return False
        else:
          # else 语句没有发生任何异常的时候执行
          # pass
          return True
        finally:
          # finally 语句无论异常是否发生都会执行
          pass

   
    def server_node_update(self, byte_data):
        """ 更新服务节点  """
        # byte[]  b"data"
        try:
          path=""
          stat=self.zk.exists(path)
          if stat:
              self.zk.set(path,byte_data,-1)
              return True
          else:
             logging.warn("zookeeper 检测服务节点失败",path)      
             return False
        except:
          logging.error("zookeeper 更新服务节点 Unexpected error:",sys.exc_info())
          return False

    def get_interface_node(self, path):
        """ 获取接口节点数据  """
        try:
            #set s={1} or s=set() but s={} is map
            s=set()
            ls=self.zk.get_children(path,None,False)
            for x in ls:
                p=path+"/"+x
                t=self.zk.get(p,None)
                s=str(t[0],"utf-8")
                #关于eval()的说法,官方demo解释为：将字符串str当成有效的表达式来求值并返回计算结果。 
                #实际上这是有局限的，例如处理多维字典就不行
                #d=eval(s)
                d=json.loads(s)
                if d!=None and "ip" in d and "port" in d :
                    url="http://"+d["ip"]+":"+d["port"]
                    s.add(url)

            if(len(s)==0):
                NodeInterface.update(list(s))

        except:
            pass
        pass

    def watch_node(selft, event):
        """ 节点监听事件  """
        path=event.path
        if path:
            time.sleep(3)
            selft.get_interface_node(path)
            logging.info("zookeeper TM节点变更结束",path)
        selft.event_bind_tm()


 # 单列 https://www.cnblogs.com/huchong/p/8244279.html
instance=ZkUtil()


class NodeInterface:
    
    __lock = threading.Lock()
    __ls=[] # 接口地址
    __count=0 # 地址计数器
    ppservers = () #Parallel 元组

    @staticmethod
    def update(l):
        """ 更新数据列表 """
        __lock.acquire() # 加锁，保证同一时刻只有一个线程可以修改数据
        __ls.clear() #清空列表
        # https://blog.csdn.net/wangshuang1631/article/details/53196137
        # Creates jobserver with ncpus workers
        job_server = pp.Server(len(__ls), ppservers=ppservers)
        # Submit a job of calulating sum_primes(100) for execution. 
        # sum_primes - the function
        # (100,) - tuple with arguments for sum_primes
        # (isprime,) - tuple with functions on which function sum_primes depends
        # ("math",) - tuple with module names which must be imported before sum_primes execution
        # Execution starts as soon as one of the workers will become available
        job1 = job_server.submit((l))
        # https://www.parallelpython.com/examples.php
        # Retrieves the result calculated by job1
        # The value of job1() is the same as sum_primes(100)
        # If the job has not been finished yet, execution will wait here until result is available
        result = job1()
        __lock.release() # 修改完成就可以解锁

    @staticmethod
    def get_interface():
        """ 获取元素 """
        __lock.acquire() # 加锁，保证同一时刻只有一个线程可以修改数据
        #__count++ # not support
        __count=__count+1
        l=len(__ls)
        if(l==0):
            return None
        else:
            # python not support ? :
            __count=0 if __count>=l else __count
            return __ls[__count]
        __lock.release() # 修改完成就可以解锁

class ChannelUtil:
    is_online=True #上下线状态
    @staticmethod
    def server_nodes_update():
        """ 更新服务节点 """
        #获取服务节点json
        node=FNode()
        node.path="/info/test"
        node.name="test"
        node.describe="test"
        node.times=time.strftime("%Y-%m-%d %H:%M:%S", time.localtime())
        j=json.dumps(node)
        # metadata=eval(j)
        if(instance.server_node_update(bytes(j,"utf-8"))):
            logging.info("服务节点注册成功")
        else :
            logging.warn("服务节点注册失败")

class FNode:
    path=""
    name=""
    describe=""
    times=""