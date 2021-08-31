package com.utility.http.model;

import lombok.Data;

import java.util.HashMap;

@Data
public class HttpModel {
    String url;
    String referer;
    String method="get";
    int connectTimeout=3*1000;//3s
    int readTimeout=3*1000;//3s
    byte[] param;
    String userAgent="Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.6)";
    String accept;
    int timeOut;
    String contentType;
    String cookie;
    HashMap<String,String> headers;
    HttpResultFlag flag= HttpResultFlag.String;
    String charset="utf-8";
}
