c# eureka  server δ�ҵ��İ� ֻ����java ���� ��Ϊ������ �� c# java��� ʹ��
consul zookeeper ֧�� net java

-node=192.168.1.11  ���� �� ip ��Ȼ oelot ��ϲ���ʧ��

 consul agent -server -bootstrap-expect 1  -ui  -node=192.168.1.11 -data-dir=/tmp/consul  -client=0.0.0.0 -bind=192.168.1.11
