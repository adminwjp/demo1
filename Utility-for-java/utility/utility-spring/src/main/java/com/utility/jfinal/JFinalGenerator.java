package com.utility.jfinal;

import com.jfinal.kit.Prop;
import com.jfinal.plugin.activerecord.dialect.MysqlDialect;
import com.jfinal.plugin.activerecord.generator.Generator;
import com.jfinal.plugin.c3p0.C3p0Plugin;

import javax.sql.DataSource;

public class JFinalGenerator {
    private static DataSource getDataSource(Prop prop) {
       // Prop prop = PropKit.use("jdbc.properties");//加载配置文件
        C3p0Plugin c3p0Plugin = new C3p0Plugin(prop.get("jdbcUrl"),
                prop.get("user"), prop.get("password")); //创建c3p0连接
        c3p0Plugin.start();
        return c3p0Plugin.getDataSource();
    }
    /**
     *  @param  baseModelOutputDir base model 文件保存路径
     * @param baseModelPackageName   base model 所使用的包名
     * @param modelOutputDir model 文件保存路径 (MappingKit 与 DataDictionary 文件默认保存路径)
     * @param  modelPackageName model 所使用的包名 (MappingKit 默认使用的包名)
     * */
    public  static  void  generator(Prop prop,String baseModelOutputDir,String baseModelPackageName,String modelOutputDir,String modelPackageName){
        DataSource dataSource=getDataSource(prop);
        Generator gernerator = new Generator(dataSource,
                baseModelPackageName, baseModelOutputDir, modelPackageName, modelOutputDir);// 创建生成器
        gernerator.setDialect(new MysqlDialect());  // 设置数据库方言
        //无效
        gernerator.setRemovedTableNamePrefixes("t_");
        JfinalMetaBuilder metaBulider = new JfinalMetaBuilder(dataSource);//添加需要生成的表名
        gernerator.setGenerateRemarks(true);// 配置是否生成备注
        gernerator.setGenerateChainSetter(true);
        //gernerator.addExcludedTable("adv");  // 添加不需要生成的表名
        gernerator.setGenerateDaoInModel(true); // 设置是否在 Model 中生成 dao 对象
        gernerator.setGenerateDataDictionary(false); // 设置是否生成字典文件
        // 设置需要被移除的表名前缀用于生成modelName。例如表名 "osc_user"，移除前缀 "osc_"后生成的model名为 "User"而非 OscUser
         metaBulider.setRemovedTableNamePrefixes("t_");//有效
        gernerator.setMetaBuilder(metaBulider);
        gernerator.generate(); // 生成
    }
}
