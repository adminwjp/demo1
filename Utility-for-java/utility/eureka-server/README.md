# eureka-server
git  remote add docker_gitlab http://192.168.99.100:8000/java/eureka-server.git
git  remote add centos_gitlab http://192.168.1.9:8000/java/eureka-server.git
git  remote add gitblit http://192.168.1.3:8001/java/eureka-server.git

git  push  docker_gitlab local:remote
git  push  docker_gitlab :remote //delete
git  push  docker_gitlab name:refs/tags/name
git  push  docker_gitlab :refs/tags/name delete tag

git  pull  docker_gitlab master
git  push  docker_gitlab dev:dev

git  push  docker_gitlab
远程 标签不存在则 使用以下命令
git  push  docker_gitlab win7_docker_server_4000:refs/tags/win7_docker_server_4000
git  push  docker_gitlab win7_docker_server_4001:refs/tags/win7_docker_server_4001
git  push  docker_gitlab win7_docker_server_4002:refs/tags/win7_docker_server_4002


  
java eureka-server
jenkins gitlab centos8 docker 
pack jar 
publish  centos8 docker 
idea
 -Dport=4000 -Dname=eureka-server -DredisHost=192.168.1.3 -DredisPort=6379 -DredisPassword=wjp930514. -Dhostname=192.168.1.3 -DdefaultZone=http://192.168.1.3:4001/eureka/,http://192.168.1.3:4002/eureka/ -Dregister-with-eureka=true -Dfetch-registry=true

java -jar eureka-server.jar -Dport=4000 -Dname=eureka-server -DredisHost=192.168.1.3 -DredisPort=6379 -DredisPassword=wjp930514. -Dhostname=192.168.1.3 -DdefaultZone=http://192.168.1.3:4001/eureka/,http://192.168.1.3:4002/eureka/ -Dregister-with-eureka=true -Dfetch-registry=true
等同于
java -jar eureka-server.jar --port=4000 --name=eureka-server --redisHost=192.168.1.3 --redisPort=6379 --redisPassword=wjp930514. -Dhostname=192.168.1.3 --defaultZone=http://192.168.1.3:4001/eureka/,http://192.168.1.3:4002/eureka/ --register-with-eureka=true --fetch-registry=true

docker
ENTRYPOINT ["sh","-c","java ${JAVA_OPTS} -jar /eureka-server.jar ${JAVA_OPTS1} ${JAVA_OPTS2} ${JAVA_OPTS3} ${JAVA_OPTS4} ${JAVA_OPTS5} ${JAVA_OPTS6} "]
docker build -t eureka-server:v1 .

docker run --name eureka-server  -p 4000:4000 -d   eureka-server:v1 


docker run -e name=eureka-server  -e port=4000  -e hostname=192.168.1.9  \
-e defaultZone=http://192.168.1.9:4001/eureka/,http://192.168.1.9:4002/eureka/ \
--name eureka-server  -p 4000:4000 -d   eureka-server:v1 

docker run -e name=eureka-server  -e port=4000  -e hostname=192.168.1.9  \
-e defaultZone=http://192.168.1.9:4001/eureka/,http://192.168.1.9:4002/eureka/ \
-e register-with-eureka=true -e fetch-registry=true \
--name eureka-server  -p 4000:4000 -d   eureka-server:v1 

docker run -e name=eureka-server  -e port=4001  -e hostname=192.168.1.9  \
-e defaultZone=http://192.168.1.9:4000/eureka/,http://192.168.1.9:4002/eureka/ \
-e register-with-eureka=true -e fetch-registry=true \
--name eureka-server-4001  -p 4001:4001 -d   eureka-server:v1 

docker run -e name=eureka-server  -e port=4002  -e hostname=192.168.1.9  \
-e defaultZone=http://192.168.1.9:4000/eureka/,http://192.168.1.9:4001/eureka/ \
-e register-with-eureka=true -e fetch-registry=true \
--name eureka-server-4002  -p 4002:4002 -d   eureka-server:v1 

docker-compose up #打开的是啥 一个进程 关闭后也会关闭

kubectl apply -f k8s.yml #部署的啥玩意 动态分配的机器 不好控制 ip
     
                                                               