package com.utility.spring;

import com.utility.util.StringUtil;
import org.springframework.web.servlet.mvc.condition.RequestCondition;

import javax.servlet.http.HttpServletRequest;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class ApiRequestCondition implements RequestCondition<ApiRequestCondition> {

    private double apiVersion=1.0;
    private static  double maxApiVersion=1.0;
    private  static  final   Pattern apiVersionPattern = Pattern.compile("v(\\d+\\.?\\d*)");
    public ApiRequestCondition(double apiVersion) {
        this.apiVersion = apiVersion;
    }
    public double getApiVersion() {
        return apiVersion;
    }

    @Override
    public ApiRequestCondition combine(ApiRequestCondition other) {
        // 采用最后定义优先原则，则方法上的定义覆盖类上面的定义
        return new ApiRequestCondition(other.getApiVersion());
    }

    @Override
    public int compareTo(ApiRequestCondition other, HttpServletRequest request) {
        //对符合请求版本的版本号进行排序
        if(other.getApiVersion() - this.apiVersion>0){
            return 1;
        }else if(other.getApiVersion() == this.apiVersion){
            return  0;
        }
       return -1;
    }

    @Override
    public ApiRequestCondition getMatchingCondition(HttpServletRequest request) {
        //设置默认版本号，请求版本号错误时使用最新版本号的接口
        Double version=1.0;
        //得到请求版本号
        String apiversion=request.getHeader("apiversion");
        if(StringUtil.isNotEmpty(apiversion)){
            Matcher m = apiVersionPattern.matcher(apiversion);
            if (m.find()) {
                version = Double.valueOf(m.group(1));
            }
        }
        // 超过当前最大版本号或者低于最低的版本号均返回不匹配
        if (version <= maxApiVersion && version >= this.apiVersion) {
            return this;
        }
        return null;
    }
    public double getVersion() {
        return apiVersion;
    }
    public static void setMaxVersion(double maxVersion) {
        ApiRequestCondition.maxApiVersion = maxVersion;
    }
}
