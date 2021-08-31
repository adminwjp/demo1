package com.utility.http;

import com.utility.http.model.HttpModel;
import com.utility.http.model.HttpResultModel;
import com.utility.util.HttpUtils;
import com.utility.util.ReaderUtil;
import com.utility.util.StringUtil;

import javax.net.ssl.*;
import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.security.KeyManagementException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;
import java.util.Map;

public class HttpFactory {
    public  static  final  String ApplicationJson="application/json";
    public  static  final  String ApplicationForm="application/form";
    public  static  final  String ApplicationFormData="application/form-data;boundary=----123456789";
    public  static  final  String ApplicationFormDataSuffix="boundary=----123456789";

    public static     class EmptyX509TrustManager implements X509TrustManager {
        public  static  final  X509TrustManager Empty=new HttpFactory.EmptyX509TrustManager();
        @Override
        public void checkClientTrusted(X509Certificate[] x509Certificates, String s) throws CertificateException {

        }

        @Override
        public void checkServerTrusted(X509Certificate[] x509Certificates, String s) throws CertificateException {

        }

        @Override
        public X509Certificate[] getAcceptedIssuers() {
            return new X509Certificate[0];
        }
    }
    public   static HttpURLConnection connection(HttpModel httpModel) throws MalformedURLException, IOException, NoSuchProviderException, NoSuchAlgorithmException, KeyManagementException {
        // 创建SSLContext对象，并使用我们指定的信任管理器初始化
        TrustManager[] tm = { HttpFactory.EmptyX509TrustManager.Empty };
        SSLContext sslContext = SSLContext.getInstance("SSL", "SunJSSE");
        sslContext.init(null, tm, new java.security.SecureRandom());
        // 从上述SSLContext对象中得到SSLSocketFactory对象
        SSLSocketFactory ssf = sslContext.getSocketFactory();
        URL url = new URL(httpModel.getUrl());// 创建远程url连接对象
        HttpsURLConnection connection = (HttpsURLConnection) url.openConnection(); // 通过远程url连接对象打开一个连接，强转成httpURLConnection类
        connection.setSSLSocketFactory(ssf);
        if(StringUtil.isNotEmpty(httpModel.getReferer())){
            connection.addRequestProperty("Referer",httpModel.getReferer());
        }
        if(StringUtil.isNotEmpty(httpModel.getAccept())){
            connection.addRequestProperty("Accept",httpModel.getAccept());
        }
        if(StringUtil.isNotEmpty(httpModel.getUserAgent())){
            connection.addRequestProperty("User-Agent",httpModel.getUserAgent());
        }
        if("post".equals(httpModel.getMethod().toLowerCase())){
            if(StringUtil.isNotEmpty(httpModel.getContentType())){
                connection.addRequestProperty("Content-Type",httpModel.getContentType());
            }
            if(httpModel.getParam()!=null&&httpModel.getParam().length>0)
            {
                connection.setDoOutput(true);  // 默认值为：false，当向远程服务器传送数据/写数据时，需要设置为true
            }
            connection.setDoInput(true); // 默认值为：true，当前向远程服务读取数据时，设置为true，该参数可有可无
         }
        if(StringUtil.isNotEmpty(httpModel.getCookie())){
            connection.addRequestProperty("cookie",httpModel.getCookie());
        }
        if(httpModel.getHeaders()!=null){
            for (String it :httpModel.getHeaders().keySet()) {
                connection.addRequestProperty(it,httpModel.getHeaders().get(it));
            }
        }
        connection.setRequestMethod(httpModel.getMethod()); // 设置连接方式：get
        connection.setConnectTimeout(httpModel.getConnectTimeout());// 设置连接主机服务器的超时时间：15000毫秒
        connection.setReadTimeout(httpModel.getReadTimeout());// 设置读取远程返回的数据时间：60000毫秒


        return  connection;
    }

    public static HttpResultModel doHttp(HttpModel httpModel) {
        HttpURLConnection connection = null;
        InputStream is = null;
        OutputStream os = null;
        BufferedReader br = null;
        String result = null;// 返回结果字符串
        HttpResultModel httpResultModel=new HttpResultModel();
        try {
            connection=connection(httpModel);// 通过connection连接，获取输入流
            connection.connect(); // 发送请求
            if("post".equals(httpModel.getMethod().toLowerCase())) {
                if (httpModel.getParam() != null && httpModel.getParam().length > 0) {
                    os = connection.getOutputStream();// 通过连接对象获取一个输出流
                    os.write(httpModel.getParam());  // 通过输出流对象将参数写出去/传输出去,它是通过字节数组写出的
                }
            }
            if (connection.getResponseCode() == 200) {
                is = connection.getInputStream();
                switch (httpModel.getFlag()) {
                    case Byte:
                        httpResultModel.setByteResult(ReaderUtil.read(is));
                        break;
                    case String: {
                        // 封装输入流is，并指定字符集
                        br = new BufferedReader(new InputStreamReader(is, httpModel.getCharset()));
                        result = ReaderUtil.read(br); // 存放数据
                        httpResultModel.setStringResult(result);
                    }
                    break;
                    case Stream:
                        httpResultModel.setStreamResult(is);
                        break;
                }
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (KeyManagementException e) {
            e.printStackTrace();
        } catch (NoSuchProviderException e) {
            e.printStackTrace();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        } finally {
            switch (httpModel.getFlag()){
                case Stream:
                    close(connection,br,os,null); // 关闭资源
                    break;
                case String:
                case Byte:
                    close(connection,br,os,is); // 关闭资源
                    break;
            }

        }



        return  httpResultModel;
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
    public   static  void  close(HttpURLConnection connection, BufferedReader br, OutputStream os, InputStream is ){
        // 关闭资源
        if (null != br) {
            try {
                br.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        if (null != os) {
            try {
                os.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        if (null != is) {
            try {
                is.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        // 断开与远程地址url的连接
        connection.disconnect();
    }


}
