package com.utility.interceptor;

import com.utility.service.dto.ResponseApi;
import com.utility.util.JsonUtil;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.web.HttpRequestMethodNotSupportedException;
import org.springframework.web.bind.MissingServletRequestParameterException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.NoHandlerFoundException;
import org.springframework.web.servlet.mvc.support.DefaultHandlerExceptionResolver;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@ControllerAdvice
public class GlobalExceptionHandler extends DefaultHandlerExceptionResolver {
    protected Log log= LogFactory.getLog(this.getClass());//日志
    /** 异常 捕获 */
    protected ResponseApi error(String url,Exception ex){
        log.error(this.getClass().getName()+",请求 地址 "+url+", 操作 异常："+ex.getMessage());
        ex.printStackTrace();
        return  ResponseApi.Error;
    }

    @ExceptionHandler(value = Exception.class)
    public ModelAndView defaultErrorHandler(HttpServletRequest request, HttpServletResponse response, Object handler, Exception ex) throws Exception {
        String url = request.getServletPath();
        error(url,ex);
        if (url.startsWith("/api")) {//api返回异常拦截
            if (ex instanceof HttpRequestMethodNotSupportedException) {
                setResponseParam(response, 405, "请求方式错误！");
                return null;
            }
            if (ex instanceof MissingServletRequestParameterException) {
                setResponseParam(response, 400, "错误请求！");
                return null;
            }
            if (ex instanceof NoHandlerFoundException) {
                //可以进行其他方法处理，LOG或者什么详细记录，我这里直接返回JSON
                setResponseParam(response, 404, "请求路径错误！");
                return null;
            }
            setResponseParam(response, 500, "服务器内部错误！服务暂时不可用！");
            return null;
        }
        setResponseParam(response, 500, "服务器内部错误！服务暂时不可用！");
        return null;
        //return super.doResolveException(request, response, handler, ex);//这里调用父类的异常处理方法，实现其他不需要的异常交给SpringMVC处理
    }

    private void setResponseParam(HttpServletResponse response, int code, String note) throws IOException {
        ResponseApi responseApi= ResponseApi.error().set("code",code).set("note", note);
        String json= JsonUtil.toJson(responseApi);
        response.setContentType("application/json");
        response.setCharacterEncoding("utf-8");
        response.getWriter().print(json);
    }
}