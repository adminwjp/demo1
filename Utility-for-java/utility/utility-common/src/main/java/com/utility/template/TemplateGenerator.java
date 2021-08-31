package com.utility.template;

import com.utility.template.model.BaseModel;
import com.utility.template.model.ClassModel;
import com.utility.template.model.FieldModel;
import com.utility.template.model.FkGroupModel;
import com.utility.util.StringUtil;
import com.utility.util.TypeUtil;

import java.lang.reflect.Field;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class TemplateGenerator {
    public  enum TemplateFlag{
        /** hibernate mapp xml 实体映射 */
        HibernateXml,
        /** mybatis mapp xml 实体映射 */
        MybatisXml,
        /** hui-admin 前端代码生成 */
        HuiAdmin
    }
    public  static  final Set<String> mysqlKeywords=new HashSet<String>();
    static {
        mysqlKeywords.add("database");
        mysqlKeywords.add("table");
        mysqlKeywords.add("column");
        mysqlKeywords.add("add");
        mysqlKeywords.add("modify");
        mysqlKeywords.add("delete");
    }
    private  final  HibernateXmlTemplate hibernateXmlTemplate=new HibernateXmlTemplate();
    private  final  MybatisMappXmlTemplate mybatisMappXmlTemplate=new MybatisMappXmlTemplate();

    public  static  final  TemplateGenerator Empty=new TemplateGenerator();

    public     List<String> mapp(Class<?>[] classes,TemplateFlag flag){
        switch (flag) {
            case HibernateXml:
                break;
            case MybatisXml:
                return  mybatisMappXmlTemplate.mappXmls(classes);
        }
        return hibernateXmlTemplate.mappXmls(classes);
    }

    protected   String getColumn(String name){
        String alias=StringUtil.parse(name, StringUtil.StringFormat.InitialLetterUpperCaseLower);
        if(mysqlKeywords.contains(alias.toLowerCase())){
            return "`"+alias+"`";
        }
        return  alias;
    }

    private  void insert(List<String> sortStrs,List<Field> fields,Field field,String mapp){
        int index=fields.indexOf(field);
        if(index==-1||index>=sortStrs.size())
        {
            index=sortStrs.size();
        }
        sortStrs.add(index, mapp);
    }

    protected  String getTable(String name){
        String table="t_"+ StringUtil.parse(name, StringUtil.StringFormat.InitialLetterUpperCaseLower);
        return  table;
    }

    private  class  MybatisMappXmlTemplate {
        public List<String> mappXmls(Class<?>... classes) {
            if (classes != null) {
                List<String> strs = new ArrayList<>();
                for (Class<?> cls : classes) {
                    String string = mappXml(cls);
                    strs.add(string);
                }
                return strs;
            }
            return null;
        }



        public String mappXml(Class<?> cla) {
            ClassModel classModel = new ClassModel();
            classModel.setCla(cla);
          try {
              ModelParse.parse.mapping(cla, classModel);
          }catch (Exception ex){
              ex.printStackTrace();
              return "";
          }
            StringBuilder builder = new StringBuilder();
            String string = builder.toString();
            String name = cla.getSimpleName();
            char[] chars = name.toCharArray();
            chars[0] = Character.toLowerCase(chars[0]);
            name = new String(chars);
            resultMap(classModel, builder, name);//resultMap
            insertSql(classModel, builder, name);//insert
            updateSql(classModel, builder, name);//update
            //delete
            if (classModel.getIdModel()!=null) {
                builder.append("    <sql id=\"deleteSql\">delete from  " + getTable(cla.getSimpleName()) + " where " +
                        getColumn(classModel.getIdModel().getField().getName()) + " </sql>\n\n");
                builder.append("    <delete id=\"" + name + ".delete\" parameterType=\"" + classModel.getIdModel().getField().getType().getTypeName() + "\" >\n" +
                        "        <include refid=\"deleteSql\"  /> =#{" + classModel.getIdModel().getField().getName() + "}\n" +
                        "     </delete>\n\n");
                //delete many
                builder.append("    <delete id=\"" + name + ".deleteMany\" >\n" +
                        "       <include refid=\"deleteSql\"  />  in\n" +
                        "        <foreach collection=\"ids\" index=\"index\" item=\"item\" open=\"(\" separator=\",\" close=\")\">\n" +
                        "             #{item}\n" +
                        "        </foreach>\n" +
                        "     </delete>\n\n");
            }
            //select
            whereSql(classModel, builder, name);//select
            builder.append("    <select id=\""+name+".select\" parameterType=\"" + cla.getName() + "\" resultMap=\"" + name + "ResultMap\">\n");
            builder.append("        select * from  " + getTable(cla.getSimpleName()) + "\n");
            builder.append("        <include refid=\"whereIf\"/>\n    </select>\n\n");
            //count
            builder.append("    <select id=\""+name+".count\" parameterType=\"" + cla.getName() + "\" resultType=\"java.lang.Integer\">\n");
            builder.append("        select count(" +
                    (classModel.getIdModel()!=null?getColumn(classModel.getIdModel().getField().getName()):"*") + ") from  " + getTable(cla.getSimpleName()) + "\n");
            builder.append("        <include refid=\"whereIf\"/>\n    </select>\n\n");
            string += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                    "<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\">\n" +
                    "<mapper namespace=\"" + name + "\">\n" +
                    builder.toString() +
                    "</mapper>";
            return string;
        }

        private void resultMap(ClassModel classModel, StringBuilder builder, String name) {
            List<String> sortStrs = new ArrayList<>();
            //resultMap
            builder.append("    <resultMap id=\"" + name + "ResultMap\" type=\"" + classModel.getCla().getName() + "\">\n");
            if (classModel.getIdModel()!=null) {
                String fieldName = classModel.getIdModel().getField().getName() ;
               String xml="        <result property=\"" + fieldName + "\" column=\"" + getColumn(fieldName) + "\" javaType=\"" +
                        classModel.getIdModel().getField().getType().getTypeName() + "\"></result>";
                sortStrs.add(xml);
            }
            for (FieldModel fieldModel:classModel.fields) {
                String fieldName = fieldModel.getField().getName() ;
                String xml="";
                if(fieldModel.isEnum()){
                    xml ="        <result property=\"" + fieldName + "\" column=\"" + getColumn(fieldName) + "\"  javaType=\""
                            + fieldModel.getField().getType().getTypeName() + "\"\n" +
                            "                typeHandler=\"org.apache.ibatis.type.EnumOrdinalTypeHandler\"></result>";
                }else{
                    xml="        <result property=\"" + fieldName + "\" column=\""
                            + getColumn(fieldName) + "\" javaType=\"" +
                            fieldModel.getField().getType().getTypeName() + "\"></result>";
                }
                insert(sortStrs, classModel.fieldList, fieldModel.getField(), xml);
            }
            for (FkGroupModel fkGroupModel : classModel.fkGroupModels) {
                if (fkGroupModel.getBasic() != null) {
                    String fieldName = fkGroupModel.getBasic().getField().getName() ;
                   String xml="        <result property=\"" + fieldName + "\" column=\"" + getColumn(fieldName) + "\" javaType=\"" +
                           fkGroupModel.getBasic().getField().getType().getTypeName() + "\"></result>";
                    insert(sortStrs, classModel.fieldList, fkGroupModel.getBasic().getField(), xml);
                }
            }
            for (String str : sortStrs) {
                builder.append(str).append("\n");
            }
            builder.append("    </resultMap>\n\n\n");
        }

        private void insertSql(ClassModel classModel, StringBuilder builder, String name) {
            List<String> sortStrs = new ArrayList<>();
            List<String> sortValStrs = new ArrayList<>();
            //insert
            if (classModel.getIdModel()!=null) {
                builder.append("    <insert id=\"" + name + ".add\" parameterType=\"" + classModel.getCla().getName() + "\" useGeneratedKeys=\""+
                        (classModel.getIdModel().isIdentity()?"true":"false")
                        +"\" keyProperty=\"" + getColumn(classModel.getIdModel().getField().getName()) + "\">\n");
                builder.append("        insert into " +
                        getTable(classModel.getCla().getSimpleName()) + "\n");
                builder.append("        <trim suffixOverrides=\",\">\n");
                if(!classModel.getIdModel().isIdentity()){
                    String fieldName = classModel.getIdModel().getField().getName();
                    String xml = toInsertXml(classModel.getIdModel().getField());
                    // builder.append( xml).append("\n");
                    sortStrs.add(xml);
                    String xmlVal=toInsertValueXml(classModel.getIdModel().getField());
                    sortValStrs.add(xmlVal);
                }
            } else {
                builder.append("    <insert id=\"" + name + ".add\" parameterType=\"" + classModel.getCla().getName() + "\" >\n");
                builder.append("        insert into " + getTable(classModel.getCla().getSimpleName()) + "\n");
                builder.append("        <trim suffixOverrides=\",\">\n");
            }
            for (FieldModel fieldModel:classModel.fields) {
                String xml = "";
                String xmlVal="";
                if (fieldModel.isEnum()) {
                    String fieldName = fieldModel.getField().getName();
                    xml = "        <if test=\"" + fieldName + "!=null\">\n" +
                            "            ," + getColumn(fieldName) + "\n" +
                            "        </if>";
                    xmlVal="        <if test=\"" + fieldName + "!=null\">\n" +
                            "            ,#{" + fieldName + ", typeHandler=org.apache.ibatis.type.EnumOrdinalTypeHandler}\n" +
                            "        </if>";
                } else {
                    xml = toInsertXml(fieldModel.getField());
                    xmlVal=toInsertValueXml(fieldModel.getField());
                }
                insert(sortStrs, classModel.fieldList, fieldModel.getField(), xml);
                insert(sortValStrs, classModel.fieldList, fieldModel.getField(), xmlVal);
            }
            for (FkGroupModel fkGroupModel : classModel.fkGroupModels) {
                if (fkGroupModel.getBasic() != null) {
                    String xml = toInsertXml(fkGroupModel.getBasic().getField());
                    String xmlVal=toInsertValueXml(fkGroupModel.getBasic().getField());
                    insert(sortStrs, classModel.fieldList, fkGroupModel.getBasic().getField(), xml);
                    insert(sortValStrs, classModel.fieldList, fkGroupModel.getBasic().getField(), xmlVal);
                }
            }
            builder.append("        (\n");
            for (String str : sortStrs) {
                builder.append(str).append("\n");
            }
            builder.append("        </trim>\n");
            builder.append("        )\n");
            // insert values
            builder.append("        values\n");
            builder.append("        (\n");
            builder.append("        <trim suffixOverrides=\",\">\n");
            for (String str : sortValStrs) {
                builder.append(str).append("\n");
            }
            builder.append("        </trim>\n");
            builder.append("        )\n" +
                    "        </insert>\n\n");
        }

        private void updateSql(ClassModel classModel, StringBuilder builder, String name) {
            List<String> sortStrs = new ArrayList<>();
            // update
            builder.append("    <update id=\"" + name + ".modify\" parameterType=\"" + classModel.getCla().getName() + "\" >\n");
            builder.append("        update  " + getTable(classModel.getCla().getSimpleName()) + "  \n");
            builder.append("        <trim prefix=\"set\" suffixOverrides=\",\">\n");
            for (BaseModel baseModel : classModel.fields) {
                String xml = "";
                if (baseModel.isEnum()) {
                    String fieldName = baseModel.getField().getName();
                    xml = "        <if test=\"" + fieldName + "!=null\">\n" +
                            "            " + getColumn(fieldName) + "=#{" + fieldName +
                            ", typeHandler=org.apache.ibatis.type.EnumOrdinalTypeHandler},\n" +
                            "        </if>";
                } else {
                    xml = toUpdateXml(baseModel.getField());
                }
                // builder.append( xml).append("\n");
                insert(sortStrs, classModel.fieldList, baseModel.getField(), xml);
            }
            for (FkGroupModel fkGroupModel : classModel.fkGroupModels) {
                if (fkGroupModel.getBasic() != null) {
                    String xml = toUpdateXml(fkGroupModel.getBasic().getField());
                    insert(sortStrs, classModel.fieldList, fkGroupModel.getBasic().getField(), xml);
                }
            }
            for (String str : sortStrs) {
                builder.append(str).append("\n");
            }
            builder.append("        </trim>\n");
            if (classModel.getIdModel()!=null) {
                builder.append("         where " +
                        getColumn(classModel.getIdModel().getField().getName())
                        + " =#{" + classModel.getIdModel().getField().getName() + "}\n");
            }
            builder.append("    </update>\n\n");
        }

        private void whereSql(ClassModel classModel, StringBuilder builder, String name) {
            List<String> sortStrs = new ArrayList<>();
            // where
            builder.append("    <sql id=\"whereIf\">\n");
            builder.append("        <where>\n");
            if (classModel.getIdModel() != null) {
                String xml = toWhereXml(classModel.getIdModel().getField());
                // builder.append( xml).append("\n");
                sortStrs.add(xml);
            }
            for (FieldModel fieldModel : classModel.fields) {
                String xml = "";
                if (fieldModel.isEnum()) {
                    String fieldName = fieldModel.getField().getName();
                    xml = "            <if test=\"" + fieldName + "!=null\">\n" +
                            "                or  " + getColumn(fieldName) + "=#{" + fieldName + ", typeHandler=org.apache.ibatis.type.EnumOrdinalTypeHandler}\n" +
                            "            </if>";
                } else {
                    xml = toWhereXml(fieldModel.getField());
                }
                // builder.append( xml).append("\n");
                insert(sortStrs, classModel.fieldList, fieldModel.getField(), xml);
            }
            for (FkGroupModel fkGroupModel : classModel.fkGroupModels) {
                if (fkGroupModel.getBasic() != null) {
                    String xml = toWhereXml(fkGroupModel.getBasic().getField());
                    insert(sortStrs, classModel.fieldList, fkGroupModel.getBasic().getField(), xml);
                }
            }
            for (String str : sortStrs) {
                builder.append(str).append("\n");
            }
            builder.append("        </where>\n");
            builder.append("    </sql>\n\n");
        }

        private String toInsertXml(Field field) {
            String fieldName = field.getName();
            String xml = "";
            String where = "";
            if (TypeUtil.isNumberType(field)) {
                where=fieldName +"!=0";
            } else if (TypeUtil.isNumberNullType(field)) {
                where=fieldName +"!=null";
            } else {
                where=fieldName + "!=null and " + fieldName + "!=''";

            }
            xml = "           <if test=\"" + where + "\">\n" +
                    "               " + getColumn(fieldName) +",\n" +
                    "           </if>";
            return xml;
        }

        private String toInsertValueXml(Field field) {
            String fieldName = field.getName();
            String xml = "";
            String where = "";
            if (TypeUtil.isNumberType(field)) {
                where=fieldName +"!=0";
            } else if (TypeUtil.isNumberNullType(field)) {
                where=fieldName +"!=null";
            } else {
                where=fieldName + "!=null and " + fieldName + "!=''";

            }
            xml = "           <if test=\"" + where + "\">\n" +
                    "               #{" + fieldName + "},\n" +
                    "           </if>";
            return xml;
        }

        private String toUpdateXml(Field field) {
            String fieldName = field.getName();
            String xml = "";
            String where = "";
            if (TypeUtil.isNumberType(field)) {
                where=fieldName +"!=0";
            } else if (TypeUtil.isNumberNullType(field)) {
                where=fieldName +"!=null";
            } else {
                where=fieldName + "!=null and " + fieldName + "!=''";
            }
            xml = "            <if test=\"" + where + "\">\n" +
                    "               "+ getColumn(fieldName) + "=#{" + fieldName + "},\n" +
                    "            </if>";
            return xml;
        }

        private String toWhereXml(Field field) {
            String fieldName = field.getName();
            String xml = "";
            String where = "";
            if (TypeUtil.isNumberType(field)) {
                where=fieldName +"!=0";
            } else if (TypeUtil.isNumberNullType(field)) {
                where=fieldName +"!=null";
            } else {
                where=fieldName + "!=null and " + fieldName + "!=''";

            }
            xml = "            <if test=\"" + where+ "\">\n" +
                    "                or  " + getColumn(fieldName) + "=#{" + fieldName + "}\n" +
                    "            </if>";
            return xml;
        }
    }

    private class HibernateXmlTemplate{
        protected  String mappProperty(Field field, boolean isEnum){
            String mapp = "        <property name=\"" + field.getName() + "\" column=\"" +  getColumn(field.getName()) + "\" type=\"" +
                    field.getType().getTypeName() + "\" >";
            if(isEnum){
                mapp+="\n            <type name=\"org.hibernate.type.EnumType\">\n" +
                        "                <param name=\"enumClass\">"+ field.getType().getTypeName()+"</param>\n" +
                        "            </type>\n";
                mapp+="        </property>";
            }
           else{
                mapp+="</property>";
            }
            return  mapp;
        }

        protected  String mappCollection(Field field, Class<?> cla, Type referenceType, boolean isList){
            String mapp = "        <"+(isList?"list":"set")+" name=\""+field.getName()+"\"    inverse=\"false\" lazy=\"false\" cascade=\"all\">\n" +
                    "            <key column=\""+ModelParse.id.getFkColumn(field,cla)+"\" foreign-key=\""+ModelParse.id.getFkName(field,cla)+"\" />\n" ;
            if(isList){
                mapp+= "             <list-index />";
            }
            mapp+= "            <one-to-many class=\""+referenceType.getTypeName()+"\" not-found=\"ignore\"/>\n" +
                    "        </"+(isList?"list":"set")+">";
            return  mapp;
        }

        protected  String mappId(Field field){
            if(ModelParse.id.isIdentity(field))
            {
                String mapp ="        <id name=\""+field.getName()+"\" column=\""+getColumn(field.getName())+"\" unsaved-value=\"0\">\n" +
                        "            <generator class=\"identity\" />\n" +
                        "        </id>" ;
                return  mapp;
            }
            String mapp ="        <id name=\""+field.getName()+"\" column=\""+getColumn(field.getName())+"\" />\n" ;
            return  mapp;
        }


        public   List<String> mappXmls(Class<?>... classes) {
            if (classes != null) {
                List<String> strs = new ArrayList<>();
                for (Class<?> cls : classes) {
                    String string = mappXml(cls);
                    strs.add(string);
                }
                return strs;
            }
            return null;
        }


        public   String mappXml(Class<?> cla){
         try {
             ClassModel classModel=new ClassModel();
             classModel.setCla(cla);
             ModelParse.mapping(cla,classModel);
             StringBuilder builder=new StringBuilder();
             mapping(classModel,builder);
             String string = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                     "<!DOCTYPE hibernate-mapping PUBLIC\n" +
                     "        \"-//Hibernate/Hibernate Mapping DTD 3.0//EN\"\n" +
                     "        \"http://www.hibernate.org/dtd/hibernate-mapping-3.0.dtd\">\n" +
                     "<hibernate-mapping package=\""+cla.getPackage().getName()+"\">\n" +
                     "    <class name=\""+cla.getSimpleName()+"\" table=\""+getTable(cla.getSimpleName())+"\" >\n" +
                     builder.toString()+
                     "    </class>\n" +
                     "</hibernate-mapping>";
             return  string;
         }
         catch (Exception ex){
             ex.printStackTrace();
             return  "";
         }
        }



        /** 映射顺序 需要排序 */
        public   void mapping(ClassModel classModel,StringBuilder builder){
            List<String> sortStrs=new ArrayList<>();
            if(classModel.getIdModel()!=null){
                String mapp =mappId(classModel.getIdModel().getField());
                //builder.append(mapp).append("\n");
                sortStrs.add(mapp);
            }
            for (FieldModel fieldModel:classModel.fields) {
                String mapp =mappProperty(fieldModel.getField(),fieldModel.isEnum());
                //builder.append(mapp).append("\n");
                insert(sortStrs,classModel.fieldList,fieldModel.getField(),mapp);
            }
            for (FkGroupModel fkGroupModel:classModel.fkGroupModels) {
                if(fkGroupModel.getMany()!=null)
                {
                    boolean isList=fkGroupModel.getMany().getField().getType().isAssignableFrom(List.class);
                    String mapp =mappCollection(fkGroupModel.getMany().getField(),classModel.getCla(),fkGroupModel.getReferenceType(),false);
                   // builder.append(mapp).append("\n");
                    insert(sortStrs,classModel.fieldList,fkGroupModel.getMany().getField(),mapp);
                }
                if(fkGroupModel.getSingle()!=null){
                    String mapp ="        <many-to-one name=\""+fkGroupModel.getSingle().getField().getName()+"\" foreign-key=\""+ModelParse.id.getFkName(fkGroupModel.getSingle().getField(),
                            classModel.getCla())+"\" column=\""+
                            ModelParse.id.getFkColumn(fkGroupModel.getSingle().getField(),classModel.getCla())+"\" cascade=\"all\" fetch=\"join\"  />";
                   // builder.append(mapp).append("\n");
                    insert(sortStrs,classModel.fieldList,fkGroupModel.getSingle().getField(),mapp);
                }
            }

            for (String str:sortStrs) {
                builder.append(str).append("\n");
            }
        }
    }
}
