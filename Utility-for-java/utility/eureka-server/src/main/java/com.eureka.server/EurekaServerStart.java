package  com.eureka.server;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;



@SpringBootApplication
// Eureka （服务器） 注册中心
@EnableEurekaServer //申明这是一个Eureka服务
@EnableDiscoveryClient//此注解将自动发现服务并注册服务
@RestController
//https://github.com/spring-cloud  参考 这 应该 有 问题 参考别人文章 都是 坑 混合起来
//gateway spring-cloud 2.3.6
// https://github.com/spring-cloud/spring-cloud-gateway/blob/v2.2.6.RELEASE/spring-cloud-gateway-sample/pom.xml
// https://spring.io/projects/spring-cloud-netflix#overview eureka
// 该来该去 容易 出错 最好 一步步来 先注释包
@Configuration
public class EurekaServerStart
{
    public static void main(String[] args)
    {
        //eureka server c# 不支持 (没找到改包 支持 eureka client)
        SpringApplication.run(EurekaServerStart.class, args);
    }
    //健康检查，给 eureka 服务发现组件调用的
    @GetMapping("/check")
    public String health() {
        return "eureka check success!";
    }

}
