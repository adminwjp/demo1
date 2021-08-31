package com.utility.cache;

//import com.google.common.collect.Maps;
import java.io.Serializable;
import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

/**
 * simple cache provider use HashMap
 */
public class SimpleCacheProvider extends AbstractCacheProvider<HashMap<String, Serializable>> implements ICacheProvider {
    public SimpleCacheProvider(){
        super(new HashMap<>());
    }

    public SimpleCacheProvider(HashMap<String, Serializable> cacheContainer){
        super(cacheContainer);
    }

    private static AbstractCacheProvider instance = new SimpleCacheProvider(new HashMap());

    public static AbstractCacheProvider getInstance() {
        return instance;
    }
}
