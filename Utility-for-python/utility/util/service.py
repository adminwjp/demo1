

# https://www.cnblogs.com/angelyan/p/11157176.html
# pip install python-consul
import consul

class ConsulUtil:
    def __init__(self, address, port):
        '''初始化，连接consul服务器'''
        self._consul = consul.Consul(address, port)
 
    def register_service(self,check, name,service_id, address, port, tags=None):
        tags = tags or []
        # 注册服务
        self._consul.agent.service.register(
            name,
            service_id,
            address,
            port,
            tags,
            # 健康检查ip端口，检查时间：5,超时时间：30，注销时间：30s
            check=consul.Check().http(None,check, "5s", "30s", "30s",None))
        #self._consul.Health.checks
 
    def get_service(self, name):
        services = self._consul.agent.services()
        service = services.get(name)
        print(service)
        if not service:
            return None, None
        addr = "{0}:{1}".format(service['Address'], service['Port'])
        print(addr)
        return service, addr
    #pass

# https://python-etcd.readthedocs.io/en/latest/
# https://github.com/coreos/etcd
# python setup.py install

class EtcdUtil:
    pass

# https://www.cnblogs.com/zhangb8042/p/11444756.html
# https://github.com/kubernetes-client/python
# pip install kubernetes

class KubernatesUtil:
    pass
