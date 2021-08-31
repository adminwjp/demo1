package com.utility.util;

import com.utility.interfaces.ICasecade;

import java.util.ArrayList;
import java.util.List;

public final class CasecadeUtils {
    public final static <C extends ICasecade >   List<C> tree(List<C> datas)  {
        List<C> list=new ArrayList<>();
        if(datas!=null&&datas.size()>0){
            for (C casecade:datas)
            {
                if(existsCursion(list,casecade.getId())){
                    continue;//已经遍历过则 取消
                }
                if(casecade.getParentId()==null){
                    list.add(casecade);
                }else{
                    findCursion(casecade,datas,list);
                }
            }
        }
        return  list;
    }
    private final static <C extends ICasecade ,F> void findCursion(C it,List<C> datas,List<C> list) {
        if(it.getParentId()!=null){
         /*   if(existsCursion(list, it.getId())){
                return;//已经遍历过则 取消
            }*/
            C parent=find(datas, it.getParentId());//查找父节点
            if(parent==null){
                return;
            }
            //不存在父 节点 则添加 (有可能此父节点是子节点)
            if(parent.getParentId()==null&&!exists(list, parent.getId())){
                list.add(parent);//是父节点则添加
            }else{
                findCursion(parent, datas,list);
            }
            if(parent.getChildren()==null){
                parent.setChildren(new ArrayList<>());//子节点为空 则 创建 对象
            }
            parent.getChildren().add(it);//添加子节点
        }
    }
    public  static <C extends ICasecade ,F> boolean exists(List<C> data,F id) {
        for (C it : data) {
            if (it.getId() == id) {
                return true;
            }
        }
        return false;
    }
    public  static <C extends ICasecade ,F> boolean existsCursion(List<C> data,F id) {
        for (C it : data) {
            if (it.getId() == id) {
                return true;
            }
            if(it.getChildren()!=null&&it .getChildren().size()>0){
                return  existsCursion((List<C>)it .getChildren(), id);
            }
        }
        return false;
    }
    private final static <C extends ICasecade ,F>  C find(List<C> datas,F parentId) {
        for (C it : datas) {
            if (it.getId() == parentId) {
                return it;//查到立即返回
            }
           /* if (it.getChildren() != null && it.getChildren().size() > 0) {
                C data=find((List<C>)it.getChildren(), parentId);//子集里查询
                //不能直接返回 否则 还没 查完就结束了
                if(data!=null){
                    return  data;
                }
            }*/
        }
        return null;
    }
}
