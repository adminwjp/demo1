
IDEA
 -DconsulHost=192.168.1.3  -DconsulPort=8500 -Dport=5000 -Dname=consul-gateway -DredisHost=192.168.1.3 -DredisPort=6379 -DredisPassword=wjp930514. -Dhostname=192.168.1.3 -DdefaultZone=http://192.168.1.9:4001/eureka/,http://192.168.1.9:4001/eureka/,http://192.168.1.9:4002/eureka/

java -jar consul.gateway-1.0-SNAPSHOT.jar  --consulHost=192.168.1.3  --consulPort=8500  -Dport=5000 -Dname=consul-gateway -DredisHost=192.168.1.3 -DredisPort=6379 -DredisPassword=wjp930514. -Dhostname=192.168.1.3 -DdefaultZone=http://192.168.1.3:4001/eureka/,http://192.168.1.3:4002/eureka/ 
等同于
java -jar consul.gateway-1.0-SNAPSHOT.jar  --consulHost=127.0.0.1  --consulPort=8500 --port=5000 --name=consul-gateway --redisHost=192.168.1.3 --redisPort=6379 --redisPassword=wjp930514. -Dhostname=192.168.1.3 --defaultZone=http://192.168.1.9:4001/eureka/,http://192.168.1.9:4001/eureka/,http://192.168.1.9:4002/eureka/ 

docker
docker build -t consul-gateway:v1 .

docker run --name consul-gateway  -p 5000:5000 -d   consul-gateway:v1 




docker run  -e consulHost=192.168.1.9  -e consulPort=8500 -e port=5000 -e name=consul-gateway \
-e redisHost=192.168.1.3 -e redisPort=6379 -e redisPassword=wjp930514. \
--name consul-gateway-5000  -p 5000:5000 -d   consul-gateway:v1 

docker run  -e consulHost=192.168.1.3  -e consulPort=8500 -e port=5001 -e name=consul-gateway \
-e redisHost=192.168.1.3 -e redisPort=6379 -e redisPassword=wjp930514. \
--name consul-gateway-5001  -p 5001:5001 -d   consul-gateway:v1 

docker run  -e consulHost=192.168.1.3  -e consulPort=8500 -e port=5002 -e name=consul-gateway \
-e redisHost=192.168.1.3 -e redisPort=6379 -e redisPassword=wjp930514. \
--name consul-gateway-5002 -p 5002:5002 -d   consul-gateway:v1 

docker-compose up