package com.utility.util;

import java.security.MessageDigest;

public final class SecurityUtil {
    public static final String encrypt(String str,String algorithm) {
        try {
            String result = "";
            MessageDigest md = MessageDigest.getInstance(algorithm);
            byte[] bytes = md.digest(str.getBytes("utf-8"));
            byte[] arr$ = bytes;
            int len$ = bytes.length;
            for(int i$ = 0; i$ < len$; ++i$) {
                byte b = arr$[i$];
                String hex = Integer.toHexString(b & 255).toUpperCase();
                result = result + (hex.length() == 1 ? "0" : "") + hex;
            }
            return result;
        } catch (Exception var9) {
            throw new RuntimeException(var9);
        }
    }
    public static final String md5(String str) {
       return  encrypt(str,"MD5");
    }

    public static final String sha(String str) {
        return  encrypt(str,"SHA");
    }

    public static final String sha1(String str) {
        return  encrypt(str,"SHA-1");
    }
    public static final String sha224(String str) {
        return  encrypt(str,"SHA-224");
    }
    public static final String sha256(String str) {
        return  encrypt(str,"SHA-256");
    }
    public static final String sha384(String str) {
        return  encrypt(str,"SHA-384");
    }
    public static final String sha512(String str) {
        return  encrypt(str,"SHA-512");
    }
}

