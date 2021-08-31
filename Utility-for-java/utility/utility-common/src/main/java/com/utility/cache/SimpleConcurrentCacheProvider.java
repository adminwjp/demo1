package com.utility.cache;

//import com.google.common.collect.Maps;
import java.io.Serializable;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

/**
 * simple concurrent cache provider use ConcurrentMap
 */
public class SimpleConcurrentCacheProvider extends AbstractCacheProvider<ConcurrentMap<String, Serializable>> implements ICacheProvider {

    public SimpleConcurrentCacheProvider(ConcurrentMap<String, Serializable> cacheContainer){
        super(cacheContainer);
    }

    static class InnerConcurrentCacheProvider{
      private static AbstractCacheProvider instance = new SimpleConcurrentCacheProvider(new ConcurrentHashMap<>());
  }

    protected   ConcurrentMap<String, Serializable> cacheContainer =new ConcurrentHashMap<>();

    public static AbstractCacheProvider getInstance() {
        return InnerConcurrentCacheProvider.instance;
    }
}
