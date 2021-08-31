package com.utility.util;

import java.util.UUID;

public class UUIDUtil {
    public  static  String randomUUID(){
        UUID uuid = UUID.randomUUID();
        return  uuid.toString();
    }
}
