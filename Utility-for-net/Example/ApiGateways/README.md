c# eureka  server 未找到改包 只能用java 语言 作为服务器 端 c# java组合 使用
consul zookeeper 支持 net java

-node=192.168.1.11  必须 是 ip 不然 oelot 组合查找失败

 consul agent -server -bootstrap-expect 1  -ui  -node=192.168.1.11 -data-dir=/tmp/consul  -client=0.0.0.0 -bind=192.168.1.11
