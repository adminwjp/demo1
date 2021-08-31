package com.eureka.gateway;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.cloud.gateway.actuate.GatewayLegacyControllerEndpoint;
import org.springframework.cloud.gateway.config.GatewayAutoConfiguration;
import org.springframework.cloud.netflix.eureka.EnableEurekaClient;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.reactive.resource.ResourceWebHandler;

@SpringBootApplication
@EnableEurekaClient //申明这是一个Eureka服务
//@ComponentScan(value = {"com.eureka.*"})
@EnableDiscoveryClient//此注解将自动发现服务并注册服务
@RestController
@Configuration
public class EurekaGatewayStart {
    public static void main(String[] args) {
        //配置文件路由 映射 到 Mapped to ResourceWebHandler
        //GatewayAutoConfiguration - > GatewayProperties
        //应该执行 DispatcherHandler (这一步找不到 配置的路由映射 ) 怎么 执行 ResourceWebHandler
        //启动 时 什么 都 没 加载进去(其实加载进去了需要调试才能知道 要么获取 bean 输出 信息)
        //((java.util.ArrayList)((FilteringWebHandler)((RoutePredicateHandlerMapping)this.handlerMappings.get(4)).webHandler).globalFilters).get(0)
        //cannot find local variable 'mapper'  GatewayProperties bean  创建 成功  路由信息怎么没有
        //配置 错了  不报错 根本 查不出来 了(路由匹配不上 配置错了 复制少东西 )
        //properties yml 配置路由 只能 其中 一个文件配置 有效 properties 优先级高 硬代码路由无影响  否则 404 没错误
        SpringApplication.run(EurekaGatewayStart.class, args);
    }


    @GetMapping("/check")
    public String health() {
        return "eureka gateway check success!";
    }

}
