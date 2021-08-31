package com.utility.start;

import com.jfinal.kit.PathKit;
import com.jfinal.kit.Prop;
import com.jfinal.kit.PropKit;
import com.utility.jfinal.JFinalGenerator;
import org.eclipse.jetty.server.*;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;
import org.eclipse.jetty.webapp.WebAppContext;
import org.springframework.web.context.ContextLoaderListener;
import org.springframework.web.context.support.XmlWebApplicationContext;
import org.springframework.web.servlet.DispatcherServlet;

public class JettyServerStart {
    public static final String MavenWebapp = "src/main/webapp";
    public static final int Port = 8080;

    public static void generatorJfinal() {
        Prop p = PropKit.use("jdbc.properties");//加载配置文件
        String url = PathKit.getWebRootPath();
        JFinalGenerator.generator(p, url + "/src/main/java/com/shop/jfinal/pojo/base", "com.shop.jfinal.pojo.base",
                url + "/src/main/java/com/shop/jfinal/pojo", "com.shop.jfinal.pojo");
    }

    public static void main(String[] args) {
        generatorJfinal();
    }

    public static void startController(String path) {
        startController(Port,path,false);
    }

    public static void startController(int port, String path,boolean https) {
        try {
            Server server = getServer(port,https);
            ServletContextHandler springMvcHandler = new ServletContextHandler();
            springMvcHandler.setContextPath("/");
//  AnnotationConfigWebApplicationContext context = new AnnotationConfigWebApplicationContext();
//            context.register(JettyStart.class);
            XmlWebApplicationContext context = new XmlWebApplicationContext();
            //context.setConfigLocation("classpath:xml/spring-web.xml");
            context.setConfigLocation(path);
            springMvcHandler.addEventListener(new ContextLoaderListener(context));
            springMvcHandler.addServlet(new ServletHolder(new DispatcherServlet(context)), "/*");
            server.setHandler(springMvcHandler);
            server.start();
            server.join();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private static Server getServer(int port,boolean https) {
        // jetty server 默认的最小连接线程是8，最大是200，连接线程最大闲置时间60秒
        Server server = new Server(port);
        ConnectionFactory connectionFactory=null;
        if(https){
             connectionFactory =   new SslConnectionFactory();
        }
        else
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            if(https){
                httpConfig.setSecureScheme("https");
                httpConfig.setSecurePort(port);
            }
            httpConfig.setOutputBufferSize(32768);
            httpConfig.setRequestHeaderSize(8192);
            httpConfig.setResponseHeaderSize(8192);
            httpConfig.setSendServerVersion(true);
            httpConfig.setSendDateHeader(false);
            httpConfig.setHeaderCacheSize(512);
            connectionFactory = new HttpConnectionFactory(httpConfig);
        }
        ServerConnector connector = new ServerConnector(server, connectionFactory);
        //connector.setPort(8080);
        connector.setSoLingerTime(-1);
        // 连接线程最大空闲时间
        connector.setIdleTimeout(30000);
        server.addConnector(connector);
        return server;
    }

    public static void startWeb() {
        startWeb(Port, MavenWebapp,false);
    }

    public static void startWeb(int port, String path,boolean https) {
        try {
            // jetty server 默认的最小连接线程是8，最大是200，连接线程最大闲置时间60秒
            Server server = getServer(port,https);
            ServletContextHandler springMvcHandler = new ServletContextHandler();
            springMvcHandler.setContextPath("/");
            WebAppContext webAppContext = new WebAppContext();
            //URL url = JettyStart.class.getProtectionDomain().getCodeSource().getLocation();
            webAppContext.setContextPath("/");
            webAppContext.setDescriptor(path + "/WEB-INF/web.xml");
            webAppContext.setResourceBase(path);
            // webAppContext.setConfigurations();
            webAppContext.setConfigurationDiscovered(true);
            webAppContext.setParentLoaderPriority(true);
            server.setHandler(webAppContext);
            server.start();
            server.join();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}
