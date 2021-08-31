package com.utility.util;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

public class ReaderUtil {
    public   static  String read(BufferedReader br) throws IOException {
        StringBuffer sbf = new StringBuffer();// 存放数据
        String temp = null;
        // 循环遍历一行一行读取数据
        while ((temp = br.readLine()) != null) {
            sbf.append(temp);
            sbf.append("\r\n");
        }
        String result = sbf.toString();
        return  result;
    }
    public   static  byte[] read(InputStream is) throws IOException {
        List<Byte> buffers=new ArrayList<Byte>(10000);
       byte[] temp=new byte[255];
       int line=0;
        // 循环遍历一行一行读取数据
        while ((line = is.read(temp,0,255)) >-1) {
            for (int i = 0; i < line; i++) {
                buffers.add(temp[i]);
            }
        }
        byte[] rs=new byte[buffers.size()];
        for (int i = 0; i < buffers.size(); i++) {
            rs[i]=buffers.get(i);
        }
        buffers.clear();
       return  rs;
    }
}
