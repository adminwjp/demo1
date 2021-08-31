package com.utility.spring;

import com.utility.annotation.ApiVersion;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.StringUtils;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.method.HandlerMethod;
import org.springframework.web.servlet.mvc.condition.PatternsRequestCondition;
import org.springframework.web.servlet.mvc.condition.RequestMethodsRequestCondition;
import org.springframework.web.servlet.mvc.method.RequestMappingInfo;
import org.springframework.web.servlet.mvc.method.annotation.RequestMappingHandlerMapping;

import javax.servlet.http.HttpServletRequest;
import java.lang.reflect.Method;
import java.util.*;

public class MultiVersionRequestMappingHandlerMapping extends RequestMappingHandlerMapping {

    private static final Logger logger = LoggerFactory.getLogger(MultiVersionRequestMappingHandlerMapping.class);

    private final static Map<String, HandlerMethod> HANDLER_METHOD_MAP = new HashMap<>();

    /**
     * key pattern,such as：/api/product/detail[GET]@1.1
     */
    private final static String HANDLER_METHOD_KEY_PATTERN = "%s[%s]@%s";

    private List<ApiVersionCodeDiscoverer> apiVersionCodeDiscoverers = new ArrayList<>();

    @Override
    protected void registerHandlerMethod(Object handler, Method method, RequestMappingInfo mapping) {
        ApiVersion apiVersionAnnotation = method.getAnnotation(ApiVersion.class);
        if (apiVersionAnnotation != null) {
            registerMultiVersionApiHandlerMethod(handler, method, mapping, apiVersionAnnotation);
            return;
        }
        super.registerHandlerMethod(handler, method, mapping);
    }

    @Override
    protected HandlerMethod lookupHandlerMethod(String lookupPath, HttpServletRequest request) throws Exception {
        HandlerMethod restApiHandlerMethod = lookupMultiVersionApiHandlerMethod(lookupPath, request);
        if (restApiHandlerMethod != null)
            return restApiHandlerMethod;
        return super.lookupHandlerMethod(lookupPath, request);
    }

    public void registerApiVersionCodeDiscoverer(ApiVersionCodeDiscoverer apiVersionCodeDiscoverer){
        if(!apiVersionCodeDiscoverers.contains(apiVersionCodeDiscoverer)){
            apiVersionCodeDiscoverers.add(apiVersionCodeDiscoverer);
        }
    }

    private void registerMultiVersionApiHandlerMethod(Object handler, Method method, RequestMappingInfo mapping, ApiVersion apiVersionAnnotation) {
        PatternsRequestCondition patternsCondition = mapping.getPatternsCondition();
        RequestMethodsRequestCondition methodsCondition = mapping.getMethodsCondition();
        if (patternsCondition == null
                || methodsCondition == null
                || patternsCondition.getPatterns().size() == 0
                || methodsCondition.getMethods().size() == 0) {
            return;
        }
        Iterator<String> patternIterator = patternsCondition.getPatterns().iterator();
        Iterator<RequestMethod> methodIterator = methodsCondition.getMethods().iterator();
        while (patternIterator.hasNext() && methodIterator.hasNext()) {
            String patternItem = patternIterator.next();
            RequestMethod methodItem = methodIterator.next();
            String key = String.format(HANDLER_METHOD_KEY_PATTERN, patternItem, methodItem.name(), apiVersionAnnotation.value());
            HandlerMethod handlerMethod = super.createHandlerMethod(handler, method);
            if (!HANDLER_METHOD_MAP.containsKey(key)) {
                HANDLER_METHOD_MAP.put(key, handlerMethod);
                if (logger.isDebugEnabled()) {
                    logger.debug("register ApiVersion HandlerMethod of %s %s", key, handlerMethod);
                }
            }
        }
    }

    private HandlerMethod lookupMultiVersionApiHandlerMethod(String lookupPath, HttpServletRequest request) {
        String version = tryResolveApiVersion(request);
        if (StringUtils.hasText(version)) {
            String key = String.format(HANDLER_METHOD_KEY_PATTERN, lookupPath, request.getMethod(), version);
            HandlerMethod handlerMethod = HANDLER_METHOD_MAP.get(key);
            if (handlerMethod != null) {
                if (logger.isDebugEnabled()) {
                    logger.debug("lookup ApiVersion HandlerMethod of %s %s", key, handlerMethod);
                }
                return handlerMethod;
            }
            logger.debug("lookup ApiVersion HandlerMethod of %s failed", key);
        }
        return null;
    }

    private String tryResolveApiVersion(HttpServletRequest request) {
        for (int i = 0; i < apiVersionCodeDiscoverers.size(); i++) {
            ApiVersionCodeDiscoverer apiVersionCodeDiscoverer = apiVersionCodeDiscoverers.get(i);
            String versionCode = apiVersionCodeDiscoverer.getVersionCode(request);
            if(StringUtils.hasText(versionCode))
                return versionCode;
        }
        return null;
    }
}
