package com.zookeeper.gateway;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
@EnableDiscoveryClient//此注解将自动发现服务并注册服务
@RestController
@Configuration
public class ZookeeperGatewayStart {
    public static void main(String[] args) {
        //配置文件路由 映射 到 Mapped to ResourceWebHandler
        //GatewayAutoConfiguration - > GatewayProperties
        //应该执行 DispatcherHandler (这一步找不到 配置的路由映射 ) 怎么 执行 ResourceWebHandler
        //启动 时 什么 都 没 加载进去(其实加载进去了需要调试才能知道 要么获取 bean 输出 信息)
        //((java.util.ArrayList)((FilteringWebHandler)((RoutePredicateHandlerMapping)this.handlerMappings.get(4)).webHandler).globalFilters).get(0)
        //cannot find local variable 'mapper'  GatewayProperties bean  创建 成功  路由信息怎么没有
        //配置 错了  不报错 根本 查不出来 了(路由匹配不上 配置错了 复制少东西 )
        //properties yml 配置路由 只能 其中 一个文件配置 有效 properties 优先级高 硬代码路由无影响  否则 404 没错误
       // zookeeper 8080  其他不能使用这个端口
        SpringApplication.run(ZookeeperGatewayStart.class, args);
    }


    @GetMapping("/check")
    public String health() {
        return "zookeeper check success!";
    }

    @GetMapping("/check1")
    public String health1() {
        return "zookeeper check1 success!";
    }

    @GetMapping("/api/v1/check2")
    public String health2() {
        return "zookeeper /api/v1/check2 success!";
    }

    @GetMapping("/check3")
    public String health3() {
        return "zookeeper check3 success!";
    }

}
