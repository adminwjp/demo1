package com.utility.cache;

import java.io.Serializable;

public class CacheProvider implements ICacheProvider {
    ICacheProvider cacheProvider;

    public  CacheProvider(){
        this(SimpleConcurrentCacheProvider.getInstance());
    }

    public  CacheProvider(ICacheProvider cacheProvider){
        this.cacheProvider=cacheProvider;
    }

    public ICacheProvider getCacheProvider() {
        return cacheProvider;
    }

    public void setCacheProvider(ICacheProvider cacheProvider) {
        this.cacheProvider = cacheProvider;
    }
    @Override
    public void put(String key, Serializable cacheObject) {
        cacheProvider.put(key,cacheObject);
    }

    @Override
    public Serializable get(String key) {
       return cacheProvider.get(key);
    }

    @Override
    public void remove(String key) {
        cacheProvider.remove(key);
    }

    @Override
    public void clear() {
        cacheProvider.clear();
    }
}
