package com.test.template.net;

import com.test.template.entities.ColumnEntity;
import com.test.template.entities.DbEntity;
import com.test.template.entities.TableEntity;
import com.utility.util.FileUtils;
import com.utility.util.StringUtils;

import java.util.HashMap;
import java.util.Map;

public    class  AbpTpl{
    private  String programName;


    private  String baseEntityStringCode;
    private String path;//根路劲
    private String entityFileName;//每个实体所在文件夹名称

    private  String baseEntityMappNhStringCode;
    private  String baseNhRepositoryStringCode;

    private  String baseEntityDtoStringCode;
    public  void  initial(DbEntity entity){
        this.programName=entity.getProgramName();
        HashMap<String, String> result=new HashMap<>();
        //生成实体
        generatorBaseEntity();
        result.put(path+"BaseEntity.cs",baseEntityStringCode);
        for (TableEntity tableEntity:entity.getTables()) {
            String code=generatorEntity(tableEntity);
            FileUtils.create(path+entityFileName);
            String key=path+entityFileName+"\\"+tableEntity.getClassName()+".cs";
            result.put(key,code);

            //生成 nhibernate mapp
            String code1=generatorEntityNhMapp(tableEntity);
            FileUtils.create(path+"NHibernate\\"+entityFileName);
            String key1=path+"NHibernate\\"+entityFileName+"\\"+tableEntity.getClassName()+"Map.cs";
            result.put(key1,code1);

            //生成 application
            //生成 application dto
            FileUtils.create(path+"App\\"+entityFileName);
            String code2=generatorCreateDto(tableEntity);
            result.put(path+"App\\"+entityFileName+"\\Create"+tableEntity.getClassName()+"Input.cs",code2);
            String code3=generatorUpdateDto(tableEntity);
            result.put(path+"App\\"+entityFileName+"\\Update"+tableEntity.getClassName()+"Input.cs",code3);
            String code4=generatorAllDto(tableEntity);
            result.put(path+"App\\"+entityFileName+"\\All"+tableEntity.getClassName()+"Dto.cs",code4);
        }

        //生成 nhibernate mapp
        generatorBaseNHMapp();
        result.put(path+"NHibernate\\BaseEntityMap.cs",baseEntityMappNhStringCode);
        //生成 application
        //生成 application dto
        FileUtils.create(path+"App");
        generatorBaseDto();
        result.put(path+"App\\BaseEntityDto.cs",baseEntityDtoStringCode);

        //生成文件
        for (Map.Entry<String,String> entry:result.entrySet()) {
            FileUtils.write(entry.getKey(), entry.getValue(), false);
        }
    }

    /**
     * 生成 基类 nhibernate mapp 映射文件
     * */
    protected  void   generatorBaseDto(){
        String basePath=path+"Resources\\App\\BaseEntityDto.tpl";
        String baseCode= FileUtils.readString(basePath);
        baseCode=baseCode.replace("{#programName}", programName);
        this.baseEntityDtoStringCode=baseCode;
    }
    /**
     * 生成 结果 dto 信息
     * */
    protected  String  generatorAllDto(TableEntity tableEntity){
        StringBuilder builder=new StringBuilder(tableEntity.getColumns().size()*50);
        StringBuilder privateBuilder=new StringBuilder(tableEntity.getColumns().size()*50);
        for (ColumnEntity columnEntity : tableEntity.getColumns()) {
            char[] chars=columnEntity.getPropertName().toCharArray();
            chars[0]=  Character.toLowerCase(chars[0]);
            privateBuilder.append("        private "+columnEntity.getMySqlValue()+" "+new String(chars)+";\r\n");
            String string = "\t\t/// <summary>\n" +
                    "\t\t/// "+columnEntity.getComment()+"\n" +
                    "\t\t/// </summary>\n" +
                    //"\t\t[StringLength(UserNameMaxLength)]\n" +
                    "\t\tpublic virtual "+
                    (columnEntity.getMySqlValue())
                    +" "+columnEntity.getPropertName()+" { get => "+new String(chars)+"; set { Set(ref "+new String(chars)+", value, \""
                    +columnEntity.getPropertName()+"\"); } }\r\n";
            builder.append(string);
        }
        String basePath=path+"Resources\\App\\AllDto.tpl";
        String baseCode= FileUtils.readString(basePath);
        privateBuilder.append("\r\n");
        baseCode=baseCode.replace("{#namespace}", programName+"."+entityFileName)
                .replace("{#className}", tableEntity.getClassName())
                .replace("{#stringCode}", privateBuilder.toString()+builder.toString());
        return  baseCode;
    }
    /**
     * 生成 更新 dto 信息
     * */
    protected  String  generatorUpdateDto(TableEntity tableEntity){
        StringBuilder builder=new StringBuilder(50);
        StringBuilder privateBuilder=new StringBuilder(50);
        privateBuilder.append("        private string id;\r\n");
        String string = "\t\t/// <summary>\n" +
                "\t\t/// id\n" +
                "\t\t/// </summary>\n" +
                //"\t\t[StringLength(UserNameMaxLength)]\n" +
                "\t\tpublic virtual string Id { get => id; set { Set(ref id, value, \"Id\"); } }\r\n";
        builder.append(string);
        String basePath=path+"Resources\\App\\UpdateDtoInput.tpl";
        String baseCode= FileUtils.readString(basePath);
        privateBuilder.append("\r\n");
        baseCode=baseCode.replace("{#namespace}", programName+"."+entityFileName)
                .replace("{#className}", tableEntity.getClassName())
                .replace("{#stringCode}", privateBuilder.toString()+builder.toString());
        return  baseCode;
    }
    /**
     * 生成 创建 dto 信息
     * */
    protected  String  generatorCreateDto(TableEntity tableEntity){
        StringBuilder builder=new StringBuilder(tableEntity.getColumns().size()*50);
        StringBuilder privateBuilder=new StringBuilder(tableEntity.getColumns().size()*50);
        for (ColumnEntity columnEntity : tableEntity.getColumns()) {
            String name=columnEntity.getColumnName().toLowerCase();
            //"id"=="id" false
            if(name.equals("id")){
                continue;
            }
            char[] chars=columnEntity.getPropertName().toCharArray();
            chars[0]=  Character.toLowerCase(chars[0]);
            privateBuilder.append("        private "+columnEntity.getMySqlValue()+" "+new String(chars)+";\r\n");
            String string = "\t\t/// <summary>\n" +
                    "\t\t/// "+columnEntity.getComment()+"\n" +
                    "\t\t/// </summary>\n" +
                    //"\t\t[StringLength(UserNameMaxLength)]\n" +
                    "\t\tpublic virtual "+
                    (columnEntity.getMySqlValue())
                    +" "+columnEntity.getPropertName()+" { get => "+new String(chars)+"; set { Set(ref "+new String(chars)+", value, \""+
                    columnEntity.getPropertName()+"\"); } }\r\n";
            builder.append(string);
        }
        String basePath=path+"Resources\\App\\CreateDtoInput.tpl";
        String baseCode= FileUtils.readString(basePath);
        privateBuilder.append("\r\n");
        baseCode=baseCode.replace("{#namespace}", programName+"."+entityFileName)
                .replace("{#className}", tableEntity.getClassName())
                .replace("{#stringCode}", privateBuilder.toString()+builder.toString());
        return  baseCode;
    }
    /**
     * 生成 基类 nhibernate mapp 映射文件
     * */
    protected  void   generatorNHRepository(){
        String basePath=path+"Resources\\Repository\\Nh\\NhibernateRepositoryBase.tpl";
        String baseCode= FileUtils.readString(basePath);
        baseCode=baseCode.replace("{#programName}", programName);
        this.baseEntityMappNhStringCode=baseCode;
    }


    /**
     * 生成  实体 nhibernate mapp 映射文件
     * */
    public    String  generatorEntityNhMapp(TableEntity tableEntity){
        String basePath=path+"Resources\\Mapp\\Nh\\EntityNhibernateMapp.tpl";
        String code= FileUtils.readString(basePath);
        String namespace=getFileName(tableEntity.getClassName());
        code=code.replace("{#namespace}", programName+"."+namespace)
                .replace("{#CalssEntityName}", tableEntity.getClassName())
                .replace("{#classMap}", tableEntity.getClassName()+"Map")
                .replace("{#tableName}", tableEntity.getTable());
        StringBuilder builder=new StringBuilder();
        for (ColumnEntity columnEntity:tableEntity.getColumns()) {
            String name=columnEntity.getColumnName().toLowerCase();
            //"id"=="id" false
            if(name.equals("id")){
                continue;
            }
            String str="            Map(x => x."+columnEntity.getPropertName()+")";
            if(columnEntity.getMySqlValue().equals("string")){
                str+=".Length("+columnEntity.getLength()+");";
            }else if(columnEntity.getMySqlValue().equals("char")){
                str+=".Length("+columnEntity.getLength()+");";
            }else {
                str+=";";
            }
            builder.append(str).append("\r\n");
        }
        code=code.replace("{#SetStringCode}", builder.toString());
        return  code;
    }
    /**
     * 生成 基类 nhibernate mapp 映射文件
     * */
    protected  void   generatorBaseNHMapp(){
        String basePath=path+"Resources\\Mapp\\Nh\\BaseEntityNhibernateMapp.tpl";
        String baseCode= FileUtils.readString(basePath);
        baseCode=baseCode.replace("{#programName}", programName);
        this.baseEntityMappNhStringCode=baseCode;
    }

    /**
     * 生成表信息
     * */
    public    String  generatorEntity(TableEntity tableEntity){
        filterTable(tableEntity);
        String basePath=path+"Resources\\Entity\\Entity.tpl";
        String code= FileUtils.readString(basePath);
        String namespace=getFileName(tableEntity.getClassName());
        entityFileName=namespace;
        code=code.replace("{#namespace}", programName+"."+namespace);
        String comment="\t/// <summary>\n" +
                "\t/// "+tableEntity.getComment()+"\n" +
                "\t/// </summary>";
        code=code.replace("{#comment}", comment);
        code=code.replace("{#className}", tableEntity.getClassName());
        StringBuilder builder=new StringBuilder(tableEntity.getColumns().size()*50);
        generatorColumn(tableEntity,builder);
        code=code.replace("{#classStringCode}", builder.toString());
        return  code;
    }

    /**
     * 转换表信息
     * */
    protected  void  filterTable(TableEntity tableEntity){
        if(StringUtils.isEmpty(tableEntity.getClassName())){
            String pro=StringUtils.parse(tableEntity.getTable().replace("t_", ""), StringUtils.StringFormat.InitialLetterLowerCaseUpper);
            tableEntity.setClassName(pro);
        }
        if(StringUtils.isEmpty(tableEntity.getComment())){
            tableEntity.setComment(tableEntity.getClassName()+" 实体");
        }
    }

    /**
     * 生成列信息
     * */
    protected  void  generatorColumn(TableEntity tableEntity,StringBuilder builder){
        for (ColumnEntity columnEntity : tableEntity.getColumns()) {
            String name=columnEntity.getColumnName().toLowerCase();
            filter(columnEntity);
            //"id"=="id" false
            if(name.equals("id")){
                continue;
            }

            String string = "\t\t/// <summary>\n" +
                    "\t\t/// "+columnEntity.getComment()+"\n" +
                    "\t\t/// </summary>\n" +
                    //"\t\t[StringLength(UserNameMaxLength)]\n" +
                    "\t\tpublic virtual "+
                    (columnEntity.getMySqlValue())
                    +" "+columnEntity.getPropertName()+" { get; set; }\r\n";
            builder.append(string);
        }
    }

    /**
     * 转换列信息
     * */

    protected  void  filter(ColumnEntity columnEntity){
        if(StringUtils.isEmpty(columnEntity.getPropertName())){
            String pro=StringUtils.parse(columnEntity.getColumnName(), StringUtils.StringFormat.InitialLetterLowerCaseUpper);
            columnEntity.setPropertName(pro);
        }
        if(StringUtils.isEmpty(columnEntity.getComment())){
            columnEntity.setComment(columnEntity.getPropertName());
        }

        //msql
        if(columnEntity.getMySqlValue().equals("varchar")||columnEntity.getMySqlValue().equals("text")||columnEntity.getMySqlValue().equals("longtext")){
            columnEntity.setMySqlValue("string");
            columnEntity.setLength("50");
            if(columnEntity.getMySqlValue().equals("text")||columnEntity.getMySqlValue().equals("longtext")){
                columnEntity.setMySqlValue("string");
                columnEntity.setLength("int.MaxValue");
                return;
            }
            return;
        }
        if(columnEntity.getMySqlValue().equals("int")){
            columnEntity.setMySqlValue("int");
            return;
        }
        if(columnEntity.getMySqlValue().equals("char")){
            columnEntity.setMySqlValue("char");
            columnEntity.setLength("1");
            return;
        }
        if(columnEntity.getMySqlValue().equals("datetime")||columnEntity.getMySqlValue().equals("date")){
            columnEntity.setMySqlValue("DateTime");
            return;
        }
        if(columnEntity.getMySqlValue().equals("decimal")){
            columnEntity.setMySqlValue("decimal?");
            return;
        }
    }

    /**
     * 根据 类名 生成对应文件夹
     * */
    protected  String getFileName(String className){
        if(className.toLowerCase().endsWith("y")){
            return  className.substring(0,className.length()-2)+"ies";
        }
        return className+"s";
    }

    /**
     * 根据模板生成基类
     * */
    private     void generatorBaseEntity(){
        String basePath=path+"Resources\\Entity\\BaseEntity.tpl";
        String baseCode= FileUtils.readString(basePath);
        baseCode=baseCode.replace("{#programName}", programName);
        this.baseEntityStringCode=baseCode;
    }

    public String getProgramName() {
        return programName;
    }

    public void setProgramName(String programName) {
        this.programName = programName;
    }

    public String getBaseEntityStringCode() {
        return baseEntityStringCode;
    }

    public void setBaseEntityStringCode(String baseEntityStringCode) {
        this.baseEntityStringCode = baseEntityStringCode;
    }

    public String getPath() {
        return path;
    }

    public void setPath(String path) {
        this.path = path;
    }

    public String getEntityFileName() {
        return entityFileName;
    }

    public void setEntityFileName(String entityFileName) {
        this.entityFileName = entityFileName;
    }

}
