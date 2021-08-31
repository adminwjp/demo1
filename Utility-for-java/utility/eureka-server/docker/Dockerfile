#https://www.jianshu.com/p/397929dbc27d
# Docker image for springboot file run
# VERSION 0.0.1
# Author: eangulee
# 指定源于一个基础镜像 
# FROM <image>[:<tag>] [AS <name>]
#java:8是指Docker Hub上官方提供的java镜像，版本号是8也就是jdk1.8，有了这个基础镜像后，Dockerfile可以通过FROM指令直接获取它的状态——也就是在容器中java是已经安装的
FROM java:8
WORKDIR /java
#EXPOSE 容器暴露端口
#EXPOSE 4000
# 作者
MAINTAINER wjp <973513569@qq.com>
#VOLUME指向了一个/tmp的目录，由于Spring Boot使用内置的Tomcat容器，Tomcat默认使用/tmp作为工作目录。效果就是在主机的/var/lib/docker目录下创建了一个临时文件，并连接到容器的/tmp
#VOLUME /tmp 
# 将jar包添加到容器中并更名为app.jar
#ADD : 在执行 <源文件> 为 tar 压缩文件的话，压缩格式为 gzip, bzip2 以及 xz 的情况下，会自动复制并解压到 <目标路径>。
#在不解压的前提下，无法复制 tar 压缩文件。会令镜像构建缓存失效，从而可能会令镜像构建变得比较缓慢。具体是否使用，可以根据是否需要自动解压来决定
#ADD eureka-server.jar app.jar
#COPY : 容器内的指定路径，该路径不用事先建好，路径不存在的话，会自动创建
COPY eureka-server.jar eureka-server.jar
# 运行jar包
RUN bash -c 'touch /eureka-server.jar'

#ENTRYPOINT 应用启动命令 参数设定
#ADD
#ENTRYPOINT ["java","-Djava.security.egd=file:/dev/./urandom","-jar","/app.jar"]
#COPY


ENV port=4000
ENV name="eureka-server"
ENV redisHost="192.168.1.3"
ENV redisPassword=6379
ENV hostname="192.168.1.9"
ENV defaultZone="http://192.168.1.9:4001/eureka/,http://192.168.1.9:4002/eureka/"
ENV register-with-eureka=false
ENV fetch-registry=false
#pass
#ENTRYPOINT ["java", "-jar", "eureka-server.jar", "--spring.profiles.active=test", "--server.port=4000", "> /log/app.log"]
#fail
#ENTRYPOINT ["sh","-c","java","-jar"," $PARAMS eureka-server.jar", "> /log/eureka-server.log"]
ENTRYPOINT ["sh","-c","java -jar eureka-server.jar -Dport=$port -Dname=$name -DredisHost=$redisHost -DredisPort=$redisPort -DredisPassword=$redisPassword -Dhostname=$hostname -DdefaultZone=$defaultZone -Dregister-with-eureka=${register-with-eureka} -Dfetch-registry=${fetch-registry}", "> /log/eureka-server.log"]
#docker build -t eureka-server:v1 .