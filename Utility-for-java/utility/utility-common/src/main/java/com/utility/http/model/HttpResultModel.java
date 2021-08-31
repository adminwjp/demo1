package com.utility.http.model;

import lombok.Data;

import java.io.InputStream;
import java.util.HashMap;
import java.util.stream.Stream;

@Data
public class HttpResultModel {
    HttpResultFlag flag= HttpResultFlag.String;
    String stringResult;
    byte[] byteResult;
    InputStream streamResult;
    String cookie;
    HashMap<String,String> headers;
}
