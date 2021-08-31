package com.utility.cache;

import lombok.SneakyThrows;
import org.springframework.data.redis.connection.RedisConnection;
import org.springframework.data.redis.core.RedisCallback;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.serializer.RedisSerializer;

import java.io.Serializable;

/**
 * srping redis cache provider
 */
public class RedisCacheProvider implements ICacheProvider {
    static volatile   RedisCacheProvider  redisCacheProvider;

    public RedisCacheProvider(){

    }

    public RedisCacheProvider(RedisTemplate<String, Serializable> redisTemplate){
        setRedisTemplate(redisTemplate);
    }
    //@Autowired
    private RedisTemplate<String, Serializable> redisTemplate;

    public RedisTemplate<String, Serializable> getRedisTemplate() {
        return redisTemplate;
    }

    public void setRedisTemplate(RedisTemplate<String, Serializable> redisTemplate) {
        this.redisTemplate = redisTemplate;
    }

    public  static   RedisCacheProvider getInstance(RedisTemplate<String, Serializable> redisTemplate) {
        if (redisCacheProvider == null) {
            synchronized (RedisCacheProvider.class) {
                if (redisCacheProvider == null){
                    redisCacheProvider=new RedisCacheProvider();
                    redisCacheProvider.setRedisTemplate(redisTemplate);
                }
            }
        }
        return redisCacheProvider;
    }

    @Override
    public void put(final String key, final Serializable cacheObject) {
        redisTemplate.execute(new RedisCallback<Serializable>() {
            @SneakyThrows
            @Override
            public Serializable doInRedis(RedisConnection connection)  {
                RedisSerializer<Serializable> value = (RedisSerializer<Serializable>) redisTemplate.getValueSerializer();
                connection.set(redisTemplate.getStringSerializer().serialize(key), value.serialize(cacheObject));
                return null;
            }
        });
    }

    @Override
    public Serializable get(final String key) {
        return redisTemplate.execute(new RedisCallback<Serializable>() {
            @SneakyThrows
            @Override
            public Serializable doInRedis(RedisConnection connection) {
                byte[] redisKey = redisTemplate.getStringSerializer().serialize(key);
                if (connection.exists(redisKey)) {
                    byte[] value = connection.get(redisKey);
                    Serializable valueSerial = (Serializable)redisTemplate.getValueSerializer()
                            .deserialize(value);
                    return valueSerial;
                }
                return null;
            }
        });
    }

    @Override
    public void remove(String key) {
        redisTemplate.delete(key);
    }

    @Override
    public void clear() {

    }
}
