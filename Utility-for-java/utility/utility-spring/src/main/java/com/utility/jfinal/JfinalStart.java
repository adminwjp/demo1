package com.utility.jfinal;

import com.jfinal.core.JFinal;

public class JfinalStart {
    public  static  final String WebAppDir="src/main/webapp";
    public static void start(String webAppDir, int port, String context, int scanIntervalSeconds) {

        JFinal.start(webAppDir, port, context, scanIntervalSeconds);
    }

    public static void start(){
        start(WebAppDir, 8082, "/", 5);
    }
}
