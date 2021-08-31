FROM java:8
WORKDIR /app
#EXPOSE 容器暴露端口
EXPOSE 8081
# 作者
MAINTAINER eangulee <973513569@qq.com>
COPY eureka.gateway-1.0-SNAPSHOT.jar app.jar
# 运行jar包
RUN bash -c 'touch /app.jar'
ENTRYPOINT ["java", "-jar", "app.jar", "--spring.profiles.active=test", "--server.port=8081", "> /log/app.log"]