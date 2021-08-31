package com.utility.cache;

import net.sf.ehcache.Cache;
import net.sf.ehcache.CacheException;
import net.sf.ehcache.CacheManager;
import net.sf.ehcache.Element;
import net.sf.ehcache.config.Configuration;
import net.sf.ehcache.config.ConfigurationFactory;
import javax.annotation.PostConstruct;
import java.io.IOException;
import java.io.InputStream;
import java.io.Serializable;

/**
 * ehcache cache provider
 */
public class EhcacheCacheProvider implements ICacheProvider {
    EhcacheCacheProvider(){

    }
   enum  EhcacheCache{
       INSTANCE;

       public  static  EhcacheCache getInstance(){
           return EhcacheCache.INSTANCE;
       }

       private CacheManager cacheManager;
       private String cacheName;
       private Cache cache;

       public InputStream getInputStream() {
           return inputStream;
       }

       public void setInputStream(InputStream inputStream) {
           this.inputStream = inputStream;
       }

       private  InputStream inputStream;
       //private Resource configLocation;//ehcache配置文件

       public void put(String key, Serializable cacheObject) {
           cache.put(new Element(key, cacheObject));
       }

       public Serializable get(String key) {
           Element element = cache.get(key);
           return element != null ? element.getValue() : null;
       }

       public void remove(String key) {
           cache.remove(key);
       }

       public void clear() {
           cache.removeAll();
       }

       public void postCacheManager() throws IOException, CacheException {
           InputStream is = getInputStream();//(this.configLocation != null ? this.configLocation.getInputStream() : null);
           try {
               // A bit convoluted for EhCache 1.x/2.0 compatibility.
               // To be much simpler once we require EhCache 2.1+
               Configuration configuration = (is != null ? ConfigurationFactory.parseConfiguration(is) :
                       ConfigurationFactory.parseConfiguration());
               this.cacheManager = cacheManager != null ? cacheManager : new CacheManager(configuration);
               // For strict backwards compatibility: use simplest possible constructors...
           }
           finally {
               if (is != null) {
                   is.close();
               }
           }
           if(cacheManager == null ) {
               throw new CacheException("cache manager初始化失败");
           }
           cache = cacheManager.getCache(cacheName);
           if(cache == null ) {
               throw new CacheException("cache manager初始化失败");
           }
       }
   }
    @Override
    public void put(String key, Serializable cacheObject) {
        EhcacheCache.getInstance().put(key, cacheObject);
    }

    @Override
    public Serializable get(String key) {
        Serializable res= EhcacheCache.getInstance().get(key);
        return  res;
    }

    @Override
    public void remove(String key) {
        EhcacheCache.getInstance().remove(key);
    }

    @Override
    public void clear() {
        EhcacheCache.getInstance().clear();
    }

    public String getCacheName() {
        return EhcacheCache.getInstance().cacheName;
    }

    public void setCacheName(String cacheName) {
        EhcacheCache.getInstance().cacheName = cacheName;
    }

   /* public Resource getConfigLocation() {
        return EhcacheCache.getInstance().configLocation;
    }

    public void setConfigLocation(Resource configLocation) {
        EhcacheCache.getInstance().configLocation = configLocation;
    }*/

    @PostConstruct
    public void postCacheManager() throws IOException, CacheException {
        EhcacheCache.getInstance().postCacheManager();
    }
}
