FROM java:8
WORKDIR /app
#EXPOSE 容器暴露端口
EXPOSE 8082
# 作者
MAINTAINER eangulee <973513569@qq.com>
COPY consul.gateway-1.0-SNAPSHOT.jar consul-gateway.jar
# 运行jar包
RUN bash -c 'touch /consul-gateway.jar'
ENV port=5000
ENV name="consul-getway"
ENV redisHost="192.168.1.3"
ENV redisPort=6379
ENV redisPassword=6379
ENV hostname="192.168.1.9"
# 集群ip 单机话 支持一个iP
ENV consulHost="192.168.1.3"
ENV consulPort=8500

#测试 用的
ENV defaultZone="http://192.168.1.9:4000/eureka/,http://192.168.1.9:4001/eureka/,http://192.168.1.9:4002/eureka/"

ENTRYPOINT ["sh","-c","java -jar consul-gateway.jar -Dport=$port --consulHost=$consulHost --consulPort=$consulPort -Dname=$name -DredisHost=$redisHost -DredisPort=$redisPort -DredisPassword=$redisPassword -Dhostname=$hostname -DdefaultZone=$defaultZone ", "> /log/consul-getway.log"]
#docker build -t consul-getway:v1 .