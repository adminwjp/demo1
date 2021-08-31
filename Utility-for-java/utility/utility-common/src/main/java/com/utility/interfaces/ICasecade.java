package com.utility.interfaces;

import java.util.List;

public interface ICasecade<T extends  ICasecade,F> {

    public F getId();
    public void setId(F id);

    public T getParent();
    public void setParent(T parent);

    public F getParentId();
    public void setParentId(F parentId);

    public List<T> getChildren();
    public void setChildren(List<T> children);

}
