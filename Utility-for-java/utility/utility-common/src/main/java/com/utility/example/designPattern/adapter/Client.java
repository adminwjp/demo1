package com.utility.example.designPattern.adapter;

// 类适配器(使用继承的适配器) 对象适配器(使用委托的适配器)
public class Client {
    IDbAdapter dbAdapter;
    public    void  start(){
        dbAdapter.operator("",null);
    }
}
