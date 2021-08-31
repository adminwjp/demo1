package com.utility.swagger;

import lombok.Data;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.EnableWebMvc;
import springfox.documentation.builders.ApiInfoBuilder;
import springfox.documentation.builders.PathSelectors;
import springfox.documentation.builders.RequestHandlerSelectors;
import springfox.documentation.service.ApiInfo;
import springfox.documentation.service.Contact;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

@EnableWebMvc
@Configuration
@EnableSwagger2
@Data
public class SwaggerConfig {
    private String controllerPackage;//控制器所占包
    private String address;//地址
    private  String author;//作者
    private String version;//版本
    private  String title;//标题
    private String desc;//描述
    @Bean
    public Docket buildDocket() {
        return new Docket(DocumentationType.SWAGGER_2)
                .select()
                .apis(RequestHandlerSelectors.basePackage(controllerPackage)) //要扫描的API(Controller)基础包
                .paths(PathSelectors.any()) // and by paths
                .build().apiInfo(apiInfo());
    }

    private ApiInfo apiInfo() {
        return new ApiInfoBuilder()
                .title(title) // 任意，请稍微规范点
                .description(desc) // 任意，请稍微规范点
                .termsOfServiceUrl(address+"/swagger-ui.html") // 将“url”换成自己的ip:port
                .contact(new Contact(author, "", "")) // 无所谓（这里是作者的别称）
                .version(version)
                .build();
    }


}
