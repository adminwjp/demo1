package com.utility.spring;

import com.utility.annotation.ApiVersion;
import org.springframework.core.annotation.AnnotationUtils;
import org.springframework.web.servlet.mvc.condition.RequestCondition;
import org.springframework.web.servlet.mvc.method.annotation.RequestMappingHandlerMapping;

import java.lang.reflect.Method;

public class CustomRequestMappingHandlerMapping extends RequestMappingHandlerMapping {
    private double latestVersion =1.0;
    @Override
    protected RequestCondition<ApiRequestCondition> getCustomTypeCondition(Class<?> handlerType) {
        // 判断是否有@ApiVersion注解，构建基于@ApiVersion的RequestCondition
        ApiVersion apiVersion = AnnotationUtils.findAnnotation(handlerType, ApiVersion.class);
        return createCondition(apiVersion);
    }

    @Override
    protected RequestCondition<ApiRequestCondition> getCustomMethodCondition(Method method) {
        // 判断是否有@ApiVersion注解，构建基于@ApiVersion的RequestCondition
        ApiVersion apiVersion = AnnotationUtils.findAnnotation(method, ApiVersion.class);
        return createCondition(apiVersion);
    }

    private RequestCondition<ApiRequestCondition> createCondition(ApiVersion apiVersion) {
        ApiRequestCondition apiRequestCondition=null;
        if(apiVersion == null){
            return  null;
        }
        else{
            apiRequestCondition=new ApiRequestCondition(apiVersion.value());
            // 保存最大版本号
            if(apiRequestCondition.getVersion() > latestVersion)
            {
                ApiRequestCondition.setMaxVersion(apiRequestCondition.getVersion());
            }
        }
        return apiRequestCondition;
    }
}
