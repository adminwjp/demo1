package com.utility.cache;

import java.io.Serializable;
import java.util.Map;

/**
 * simple abstract  cache provider use map
 */
public abstract class AbstractCacheProvider<T extends  Map<String,Serializable>>
          implements ICacheProvider {

    public AbstractCacheProvider(){}

    public  AbstractCacheProvider(T cacheContainer){
        this.cacheContainer=cacheContainer;
    }

    protected   T cacheContainer;

    @Override
    public void put(String key, Serializable cacheObject) {
        cacheContainer.put(key, cacheObject);
    }

    @Override
    public Serializable get(String key) {
        return cacheContainer.get(key);
    }

    @Override
    public void remove(String key) {
        cacheContainer.remove(key);
    }

    @Override
    public void clear() {
        cacheContainer.clear();
    }
}