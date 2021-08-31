package com.utility.util;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import java.util.ResourceBundle;

public class PropertiesUtil {
    /**
     * 该方式只能读取类路径下的配置文件，有局限但是如果配置文件在类路径下比较方便
     *
     * @param classLoader
     * @param file
     * @return Properties
     * @throws IOException
     */
    public static Properties load(ClassLoader classLoader, String file) throws IOException {
        try {
            Properties properties = new Properties();
            // 使用ClassLoader加载properties配置文件生成对应的输入流
            InputStream in = classLoader.getResourceAsStream(file);
            // 使用properties对象加载输入流
            properties.load(in);
            return properties;
        } catch (IOException ex) {
            ex.printStackTrace();
            throw ex;
        }
    }

    /**
     * 通过 ResourceBundle.getBundle() 静态方法来获取（ResourceBundle是一个抽象类），这种方式来获取properties属性文件不需要加.properties后缀名，只需要文件名即可
     * * 这种方式比使用 Properties 要方便一些
     *
     * @param file
     * @return ResourceBundle
     */
    public static ResourceBundle loadResourceBundle(String file) {
        //config为属性文件名，放在包com.test.config下，如果是放在src下，直接用config即可
        ResourceBundle resource = ResourceBundle.getBundle("file");
        return resource;
    }

    /**
     * 该方式的优点在于可以读取任意路径下的配置文件
     *
     * @param file
     * @return Properties
     * @throws IOException
     */
    public static Properties load(String file) throws IOException {
        try {
            Properties properties = new Properties();
            // 使用InPutStream流读取properties文件
            BufferedReader bufferedReader = new BufferedReader(new FileReader(file));
            properties.load(bufferedReader);
            return properties;
        } catch (IOException ex) {
            ex.printStackTrace();
            throw ex;
        }
    }

}
