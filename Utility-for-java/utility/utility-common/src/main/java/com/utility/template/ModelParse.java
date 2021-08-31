package com.utility.template;

import com.utility.template.model.*;
import com.utility.util.TypeUtil;

import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.util.List;
import java.util.Set;

public class ModelParse {
    public    interface Parse{
      void   mapping(Class<?> cla,ClassModel classModel) throws Exception;
      void mapping(Class<?> cla,ClassModel classModel,int index) throws Exception;
    }
    static class DefaultParse implements  Parse{
        public void   mapping(Class<?> cla,ClassModel classModel) throws Exception {
            ModelParse.mapping(cla,classModel);
        }
        public void mapping(Class<?> cla,ClassModel classModel,int index) throws Exception {
            ModelParse.mapping(cla,classModel,index);
        }
    }
    static  Parse empty=new DefaultParse();
    public  static  Parse parse=empty;
    public  static KeyImpl id = KeyImpl.Empty;
    public   static   FkGroupModel fkGroupModel(List<FkGroupModel> fkGroupModels, Type type){
        for (FkGroupModel fkGroupModel:fkGroupModels ) {
            if(type.equals((fkGroupModel.getReferenceType()))){
                return  fkGroupModel;
            }
        }
        FkGroupModel fkGroupModel=new FkGroupModel();
        fkGroupModels.add(fkGroupModel);
        return  fkGroupModel;
    }
    public final static void mapping(Class<?> cla,ClassModel classModel) throws  Exception{
        mapping(cla,classModel,-1);
    }

    public  final static void mapping(Class<?> cla,ClassModel classModel,int index) throws  Exception{
        //这一步问题 cla.getSuperclass()!=Object.class
        if(cla.getSuperclass()!=null&&
                cla.getSuperclass()!=Object.class){
            mapping(cla.getSuperclass(),classModel,0);
        }
        //@Data public class b{}
        //private int a;//get fail 什么情况 必须在同一个 jar 包中才能获取到
        //class loader 怎么改变  类都加载成功了 就 获取不到字段
        Field[] fields=cla.getDeclaredFields();
        if(fields==null||fields.length==0){
            fields=cla.getFields();
        }
        if(fields==null||fields.length==0){

        }
        for (Field f:fields) {
            //属性排序
            if(index==-1){
                classModel.fieldList.add(f);
            }
            else{
                classModel.fieldList.add(index,f);
                index++;
            }
            if(Modifier.isStatic(f.getModifiers())){
                System.out.println("静态常量 skip...");
                continue;
            }
            //System.out.println(f.getName());
            if(id.isId(f.getName())&& TypeUtil.isDataType(f)){
                IdModel idModel=new IdModel();
                idModel.setField(f);
                classModel.setIdModel(idModel);
                if(id.isIdentity(f)){
                    idModel.setIdentity(true);
                }
                continue;
            }
           // System.out.println(f.getType());
            boolean common=TypeUtil.isDataType(f);
            if(common||f.getType().isEnum()) {
                FieldModel fieldModel=new FieldModel();
                fieldModel.setField(f);
                if(!common){
                    fieldModel.setEnum(true);
                }
                classModel.fields.add(fieldModel);
            }else if(f.getType().isAssignableFrom(Set.class)||f.getType().isAssignableFrom(List.class)){
                Type type=f.getGenericType();
                if(type instanceof ParameterizedType){
                    ParameterizedType parameterizedType=(ParameterizedType)type;
                    type=parameterizedType.getActualTypeArguments()[0];
                }
                FkGroupModel fkGroupModel=fkGroupModel(classModel.fkGroupModels,type);
               // System.out.println(f.getGenericType());
                if(fkGroupModel.getMany()!=null){
                    throw  new Exception(f.getGenericType()+" 已存在(集合)!");
                }
                if(fkGroupModel.getReferenceType()==null){
                    fkGroupModel.setReferenceType(type);
                }
                FkModel fkModel=new FkModel();
                fkModel.setField(f);
                fkGroupModel.setMany(fkModel);

            }else {
                FkGroupModel fkGroupModel=fkGroupModel(classModel.fkGroupModels,f.getType());
                if(fkGroupModel.getSingle()!=null){
                    throw  new Exception(f.getType()+" 已存在(引用)!");
                }
                if(fkGroupModel.getReferenceType()==null){
                    fkGroupModel.setReferenceType(f.getType());
                }
                FkModel fkModel=new FkModel();
                fkModel.setField(f);
                fkGroupModel.setSingle(fkModel);
            }
        }
        if(classModel.fkGroupModels.size()>0){
            for (FkGroupModel  fkGroupModel:classModel.fkGroupModels) {
                String  name="";
                if(fkGroupModel.getReferenceType()!=cla){
                    if(fkGroupModel.getSingle()!=null){
                        name=id.getFkBasicField(fkGroupModel.getSingle().getField().getName());
                    }
                }else
                {
                    name=id.getFkBasicField(fkGroupModel.getReferenceType(),cla);
                }
                BaseModel temp=null;
                for (BaseModel baseModel:classModel.fields) {
                    if(baseModel.getField().getName().equals(name)){
                        temp=baseModel;
                        break;
                    }
                }
                if(temp!=null)
                {
                    FkModel fkModel=new FkModel();
                    fkGroupModel.setBasic(fkModel);
                    fkModel.setField(temp.getField());
                    classModel.fields.remove(temp);
                }
            }
        }
    }


}
