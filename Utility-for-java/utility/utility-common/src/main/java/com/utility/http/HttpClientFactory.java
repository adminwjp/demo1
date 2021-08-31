package com.utility.http;

import com.utility.http.model.HttpModel;
import com.utility.http.model.HttpResultModel;
import com.utility.util.ReaderUtil;
import com.utility.util.StringUtil;
import org.apache.http.HttpEntity;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.config.RequestConfig;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.entity.ByteArrayEntity;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class HttpClientFactory {
    // 配置请求参数实例
    public  final   static RequestConfig requestConfig= RequestConfig.custom()
            .setConnectTimeout(3000)// 设置连接主机服务超时时间
            .setConnectionRequestTimeout(3000)// 设置连接请求超时时间
            .setSocketTimeout(6000)// 设置读取数据连接超时时间
            .build();

    public   static HttpResultModel doHttp(HttpModel httpModel){
        CloseableHttpClient httpClient = HttpClients.createDefault();// 通过址默认配置创建一个httpClient实例
        HttpUriRequest request=null;
        if("get".equals((httpModel.getMethod().toLowerCase()))){
            request = new HttpGet(httpModel.getUrl());// 创建httpGet远程连接实例
        } else   if("post".equals((httpModel.getMethod().toLowerCase()))){
            /*   MultipartEntityBuilder builder = MultipartEntityBuilder.create();
        builder.addTextBody("json", jsonParam.toString(), ContentType.MULTIPART_FORM_DATA);
        HttpEntity multipart = builder.build();
        httpPost.setEntity(multipart);*/
            HttpPost httpPost = new HttpPost(httpModel.getUrl());
            request=httpPost;
            httpPost.setConfig(requestConfig);// 为httpPost实例设置配置
            //StringEntity ByteArrayEntity BasicNameValuePair UrlEncodedFormEntity
            if (httpModel.getParam() != null && httpModel.getParam().length > 0){

                httpPost.setEntity(new ByteArrayEntity(httpModel.getParam()));
            }

        }
        if(StringUtil.isNotEmpty(httpModel.getReferer())){
            request.addHeader("Referer",httpModel.getReferer());
        }
        if(StringUtil.isNotEmpty(httpModel.getAccept())){
            request.addHeader("Accept",httpModel.getAccept());
        }
        if(StringUtil.isNotEmpty(httpModel.getUserAgent())){
            request.addHeader("User-Agent",httpModel.getUserAgent());
        }
        if("post".equals(httpModel.getMethod().toLowerCase())){
            if(StringUtil.isNotEmpty(httpModel.getContentType())){
                request.addHeader("Content-Type",httpModel.getContentType());
            }
        }
        if(StringUtil.isNotEmpty(httpModel.getCookie())){
            request.addHeader("cookie",httpModel.getCookie());
        }
        // 设置请求头信息，鉴权
        if(httpModel.getHeaders()!=null){
            for(String it:httpModel.getHeaders().keySet()){
                request.setHeader(it, httpModel.getHeaders().get(it));
            }
        }
        CloseableHttpResponse response = null;
        HttpResultModel httpResultModel=new HttpResultModel();
        try {
            response = httpClient.execute(request);// 执行get请求得到返回对象
            HttpEntity entity = response.getEntity();// 通过返回对象获取返回数
            switch (httpModel.getFlag()) {
                case Byte:
                    httpResultModel.setByteResult(EntityUtils.toByteArray(entity));
                    break;
                case String: {
                    String result = EntityUtils.toString(entity);// 通过EntityUtils中的toString方法将结果转换为字符串
                    httpResultModel.setStringResult(result);
                }
                break;
                case Stream:
                    httpResultModel.setStreamResult(entity.getContent());
                    break;
            }

        } catch (ClientProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }finally {
            HttpClientFactory.close(httpClient,response);
        }
        return httpResultModel;
    }
    public static String doGet(String httpUrl) {
        HttpModel httpModel=new HttpModel();
        httpModel.setUrl(httpUrl);
        HttpResultModel httpResultModel=doHttp(httpModel);
        return  httpResultModel.getStringResult();
    }

    public static String doPost(String httpUrl, String param, String contenType){
        HttpModel httpModel=new HttpModel();
        httpModel.setUrl(httpUrl);
        httpModel.setMethod("post");
        httpModel.setContentType(contenType);
        if(StringUtil.isNotEmpty(param)){
            httpModel.setParam(param.getBytes());
        }
        HttpResultModel httpResultModel=doHttp(httpModel);
        return  httpResultModel.getStringResult();
    }
    public   static  void close(CloseableHttpClient httpClient, CloseableHttpResponse response) {
        // 关闭资源
        if (null != response) {
            try {
                response.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        if (null != httpClient) {
            try {
                httpClient.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
