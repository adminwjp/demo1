package com.shop.start;

import com.template.dao.data.DataFactory;
import com.utility.template.TemplateGenerator;
import com.utility.util.FileUtils;
import com.utility.util.ReflectionUtils;
import net.sf.ehcache.config.InvalidConfigurationException;
import org.mybatis.generator.api.MyBatisGenerator;
import org.mybatis.generator.config.xml.ConfigurationParser;
import org.mybatis.generator.exception.XMLParserException;
import org.mybatis.generator.internal.DefaultShellCallback;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.test.context.web.WebAppConfiguration;

import java.io.File;
import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;

/**
 * http服务一般都是在java容器中进行调用的,比如tomcat,war包 jetty的启动
 * */
@Configuration
@WebAppConfiguration// No qualifying bean of type 'javax.servlet.ServletContext' available: expected at least 1 bean which qualifies as autowire candidate. Dependency annotations: {}

@ComponentScan(basePackages = { "com.shop.controllers" })
//@Import({ MvcConfiguration.class })
public class JettyStart {

    public static void main(String[] args) {
        String string = "";
       /* HibernateFactory.config("hibernate-shop.cfg.xml");
        HibernateFactory.getSession();*/
/*        mybatisMapper();*/
       // DataManager.initial();
        DataFactory.initialData();
       /* String dire="E:\\work\\program\\java\\shop\\service\\src\\main\\resources\\template";
        Class[] classes=new Class[]{
               // HuiMenu.class,HuiIcon.class,
                //Column.class, ColumnRelation.class, TableList.class,
                HuiSkin.class};
        List<String> strs=TemplateGenerator.Empty.mapp(classes, TemplateGenerator.TemplateFlag.HibernateXml);
        int i=0;
        for (String str:strs) {
            FileUtils.write(dire+"\\hbm\\"+classes[i].getSimpleName()+".hbm.xml",str,false);
            i++;
            System.out.println(str);
        }
        i=0;
        strs=TemplateGenerator.Empty.mapp(classes, TemplateGenerator.TemplateFlag.MybatisXml);
        for (String str:strs) {
            FileUtils.write(dire+"\\mapper\\"+classes[i].getSimpleName()+"Mapper.xml",str,false);
            i++;
            System.out.println(str);
        }*/
       // JettyServerStart.startController(8080,"classpath:spring.xml",true);//https 无效
      /*  List<String> strs= MybatisMappXmlTemplate.mapp(new Class[]{AuthUser.class});
        for (String str:strs) {
            System.out.println(str);
        }*/
        //scanPackageToHibernateMapping("com.shop.pojo");
        //mapp();
    }
    public  static  void  mybatisMapper(){
        try {
            System.out.println("start generator ...");
            List<String> warnings = new ArrayList<>();
            boolean overwrite = true;
            File configFile = new File(JettyStart.class.getResource("generatorConfig.xml").getPath());
            ConfigurationParser cp = new ConfigurationParser(warnings);
            org.mybatis.generator.config.Configuration config = cp.parseConfiguration(configFile);
            DefaultShellCallback callback = new DefaultShellCallback(overwrite);
            MyBatisGenerator myBatisGenerator = new MyBatisGenerator(config, callback, warnings);
            myBatisGenerator.generate(null);
            System.out.println("end generator!");
        } catch (IOException e) {
            e.printStackTrace();
        } catch (XMLParserException e) {
            e.printStackTrace();
        } catch (InvalidConfigurationException e) {
            e.printStackTrace();
        } catch (SQLException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (org.mybatis.generator.exception.InvalidConfigurationException e) {
            e.printStackTrace();
        }
    }
    public  static  void mapp() {
        String path = "E:\\work\\program\\java\\shop\\service\\src\\main\\resources\\hbm\\shop";
        List<String> paths = FileUtils.getFiles(path);
        for (String str : paths) {
            //System.out.println(str.replace(path,""));
            System.out.println((" <mapping resource=\"hbm\\shop" + str.replace(path, "") + "\" />").replace("\\", "/"));
        }
    }
    public  static  void scanPackageToHibernateMapping(String pack){
        Set<Class<?>> classes =ReflectionUtils.getClasses(pack);
        Class<?>[] classes1=new Class<?>[classes.size()];
        //List<String> strs= TemplateGenerator.Empty.mapp(classes.toArray(classes1));
        List<String> strs= TemplateGenerator.Empty.mapp(classes.toArray(classes1),TemplateGenerator.TemplateFlag.MybatisXml);
        for (String str:strs) {
            System.out.println(str);
        }
    }


}
