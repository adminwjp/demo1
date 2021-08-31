package util

import (
	"fmt"
	"log"
	"time"

	"github.com/go-redis/redis"
)

type RedisUtl struct {
	Addr     string
	Password string
	DB       int
	// 作用域 容易 消失 指针 为 nil
	client *redis.Client
}

func RedisUtlInstnce(addr string, password string, DB int) *RedisUtl {
	return &RedisUtl{Addr: addr, Password: password, DB: DB}
}

func NewRedisUtl() *RedisUtl {
	//return &RedisUtl{Addr:"127.0.0.1:6379",Password:"",DB:0}
	return RedisUtlInstnce("127.0.0.1:6379", "", 0)
}

func (redisUtl RedisUtl) GetClient() *redis.Client {
	redisDb := redis.NewClient(&redis.Options{
		Addr:     redisUtl.Addr,
		Password: redisUtl.Password,
		DB:       redisUtl.DB,
		//PoolSize: 5, //连接池 默认情况下,连接池大小是10
	})
	pingResult, err := redisDb.Ping().Result()
	if err != nil {
		fmt.Println(pingResult, err)
		return nil
	} else {
		fmt.Println("connection redis success")
	}
	//defer  redisDb.Close() //延迟 关闭 操作失败
	return redisDb
}

func (redisUtl RedisUtl) Connection() bool {
	if redisUtl.client != nil {
		pingResult, err := redisUtl.client.Ping().Result()
		if err != nil {
			fmt.Println(pingResult, err)
			redisUtl.Close()
			redisUtl.client = redisUtl.GetClient()
			if redisUtl.client != nil {
				fmt.Println("reconnection redis success")
				return true
			}
			return false

		} else {
			return true
		}
	}
	redisDb := redisUtl.GetClient()
	if redisDb == nil {
		return false
	} else {
		redisUtl.client = redisDb
		return true
	}
}

func (redisUtl RedisUtl) ReConnection() bool {
	if redisUtl.client != nil {
		pingResult, err := redisUtl.client.Ping().Result()
		if err != nil {
			fmt.Println(pingResult, err)
		} else {
			fmt.Println("reconnection redis success")
		}
		redisUtl.Close()
	}
	redisUtl.client = redisUtl.GetClient()
	return redisUtl.client != nil
}

func (redisUtl RedisUtl) Close() bool {
	if redisUtl.client != nil {
		err := redisUtl.client.Close()
		if err != nil {
			return false
		}
		return true
	}
	return false
}

func (redisUtl RedisUtl) Keys(redisDb *redis.Client, pattern string) []string {
	stringSliceCmd := redisDb.Keys(pattern)
	if stringSliceCmd.Err() != nil {
		log.Fatal(stringSliceCmd.Err())
		return nil
	}
	return stringSliceCmd.Val()
}

/**
   String 操作
　　Set(key, value)：给数据库中名称为key的string赋予值valueget(key)：返回数据库中名称为key的string的value
　　GetSet(key, value)：给名称为key的string赋予上一次的value
　　MGet(key1, key2,…, key N)：返回库中多个string的value
　　SetNX(key, value)：添加string，名称为key，值为value
　　SetXX(key, time, value)：向库中添加string，设定过期时间time
　　MSet(key N, value N)：批量设置多个string的值
　　MSetNX(key N, value N)：如果所有名称为key i的string都不存在
　　Incr(key)：名称为key的string增1操作
　　Incrby(key, integer)：名称为key的string增加integer
　　Decr(key)：名称为key的string减1操作
　　Decrby(key, integer)：名称为key的string减少integer
　　Append(key, value)：名称为key的string的值附加valuesubstr(key, start, end)：返回名称为key的string的value的子串
*/

func (redisUtl RedisUtl) Set(redisDb *redis.Client, key string, value interface{}, expiration time.Duration) bool {
	statusCmd := redisDb.Set(key, value, expiration)
	if statusCmd.Err() != nil {
		return false
	}
	return true
}

func (redisUtl RedisUtl) GetSet(redisDb *redis.Client, key string, value interface{}) string {
	stringCmd := redisDb.GetSet(key, value)
	if stringCmd.Err() != nil {
		println(key, stringCmd.Err())
		return ""
	}
	return stringCmd.Val()
}

func (redisUtl RedisUtl) MGet(redisDb *redis.Client, keys ...string) []interface{} {
	sliceCmd := redisDb.MGet(keys...)
	if sliceCmd.Err() != nil {
		return nil
	}
	return sliceCmd.Val()
}

func (redisUtl RedisUtl) SetNX(redisDb *redis.Client, key string, value interface{}, expiration time.Duration) bool {
	boolCmd := redisDb.SetNX(key, value, expiration)
	if boolCmd.Err() != nil {
		return false
	}
	return boolCmd.Val()
}

func (redisUtl RedisUtl) SetXX(redisDb *redis.Client, key string, value interface{}, expiration time.Duration) bool {
	boolCmd := redisDb.SetXX(key, value, expiration)
	if boolCmd.Err() != nil {
		return false
	}
	return boolCmd.Val()
}

func (redisUtl RedisUtl) MSet(redisDb *redis.Client, key string, value interface{}, expiration time.Duration) bool {
	statusCmd := redisDb.MSet(key, value, expiration)
	if statusCmd.Err() != nil {
		return false
	}
	return true
}

func (redisUtl RedisUtl) MSetNX(redisDb *redis.Client, pairs []interface{}) bool {
	boolCmd := redisDb.MSetNX(pairs)
	if boolCmd.Err() != nil {
		return false
	}
	return boolCmd.Val()
}

func (redisUtl RedisUtl) Incr(redisDb *redis.Client, key string) int64 {
	intCmd := redisDb.Incr(key)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) IncrBy(redisDb *redis.Client, key string, value int64) int64 {
	intCmd := redisDb.IncrBy(key, value)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) Decr(redisDb *redis.Client, key string) int64 {
	intCmd := redisDb.Decr(key)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) DecrBy(redisDb *redis.Client, key string, value int64) int64 {
	intCmd := redisDb.DecrBy(key, value)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) Append(redisDb *redis.Client, key string, value string) int64 {
	intCmd := redisDb.Append(key, value)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

//-------------------------------------- 方法名 不能相同 重载
func (redisUtl RedisUtl) SetString(key string, value interface{}, expiration time.Duration) bool {
	return redisUtl.Set(redisUtl.client, key, value, expiration)
}

func (redisUtl RedisUtl) GetSetString(key string, value interface{}) string {
	return redisUtl.GetSet(redisUtl.client, key, value)
}

func (redisUtl RedisUtl) MGetString(keys ...string) []interface{} {
	return redisUtl.MGet(redisUtl.client, keys...)
}

func (redisUtl RedisUtl) SetNXString(key string, value interface{}, expiration time.Duration) bool {
	return redisUtl.SetNX(redisUtl.client, key, value, expiration)
}

func (redisUtl RedisUtl) SetXXString(key string, value interface{}, expiration time.Duration) bool {
	return redisUtl.SetXX(redisUtl.client, key, value, expiration)
}

func (redisUtl RedisUtl) MSetString(key string, value interface{}, expiration time.Duration) bool {
	return redisUtl.MSet(redisUtl.client, key, value, expiration)
}

func (redisUtl RedisUtl) MSetNXString(pairs []interface{}) bool {
	return redisUtl.MSetNX(redisUtl.client, pairs)
}

func (redisUtl RedisUtl) IncrString(key string) int64 {
	return redisUtl.Incr(redisUtl.client, key)
}

func (redisUtl RedisUtl) IncrByString(key string, value int64) int64 {
	return redisUtl.IncrBy(redisUtl.client, key, value)
}

func (redisUtl RedisUtl) DecrString(key string) int64 {
	return redisUtl.Decr(redisUtl.client, key)
}

func (redisUtl RedisUtl) DecrByString(key string, value int64) int64 {
	return redisUtl.DecrBy(redisUtl.client, key, value)
}

func (redisUtl RedisUtl) AppendString(key string, value string) int64 {
	return redisUtl.Append(redisUtl.client, key, value)
}

/**
   List 操作
　　RPush(key, value)：在名称为key的list尾添加一个值为value的元素
　　LPush(key, value)：在名称为key的list头添加一个值为value的 元素
　　LLen(key)：返回名称为key的list的长度
　　LRange(key, start, end)：返回名称为key的list中start至end之间的元素
　　LTrim(key, start, end)：截取名称为key的list
　　LIndex(key, index)：返回名称为key的list中index位置的元素
　　LSet(key, index, value)：给名称为key的list中index位置的元素赋值
　　LRem(key, count, value)：删除count个key的list中值为value的元素
　　LPop(key)：返回并删除名称为key的list中的首元素
　　RPop(key)：返回并删除名称为key的list中的尾元素
　　BLPop(key1, key2,… key N, timeout)：lpop命令的block版本。
　　BRPop(key1, key2,… key N, timeout)：rpop的block版本。
　　RPopLPush(srckey, dstkey)：返回并删除名称为srckey的list的尾元素，并将该元素添加到名称为dstkey的list的头部
*/

func (redisUtl RedisUtl) RPush(redisDb *redis.Client, key string, values ...interface{}) int64 {
	intCmd := redisDb.RPush(key, values...)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) LPush(redisDb *redis.Client, key string, values ...interface{}) int64 {
	intCmd := redisDb.LPush(key, values...)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) LLen(redisDb *redis.Client, key string) int64 {
	intCmd := redisDb.LLen(key)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) LRange(redisDb *redis.Client, key string, start int64, end int64) []string {
	stringSliceCmd := redisDb.LRange(key, start, end)
	if stringSliceCmd.Err() != nil {
		return nil
	}
	return stringSliceCmd.Val()
}

func (redisUtl RedisUtl) LTrim(redisDb *redis.Client, key string, start int64, end int64) bool {
	statusCmd := redisDb.LTrim(key, start, end)
	if statusCmd.Err() != nil {
		return false
	}
	return true
}

func (redisUtl RedisUtl) LIndex(redisDb *redis.Client, key string, index int64) string {
	stringCmd := redisDb.LIndex(key, index)
	if stringCmd.Err() != nil {
		println(key, stringCmd.Err())
		return ""
	}
	return stringCmd.Val()
}

func (redisUtl RedisUtl) LSet(redisDb *redis.Client, key string, index int64, value interface{}) bool {
	statusCmd := redisDb.LSet(key, index, value)
	if statusCmd.Err() != nil {
		return false
	}
	return true
}

func (redisUtl RedisUtl) LRem(redisDb *redis.Client, key string, index int64, value interface{}) bool {
	intCmd := redisDb.LRem(key, index, value)
	if intCmd.Err() != nil {
		return false
	}
	return intCmd.Val() > 0
}

func (redisUtl RedisUtl) LPop(redisDb *redis.Client, key string) string {
	stringCmd := redisDb.LPop(key)
	if stringCmd.Err() != nil {
		print(key, stringCmd.Err(), "\n")
		return ""
	}
	return stringCmd.Val()
}

func (redisUtl RedisUtl) RPop(redisDb *redis.Client, key string) string {
	stringCmd := redisDb.RPop(key)
	if stringCmd.Err() != nil {
		print(key, stringCmd.Err())
		return ""
	}
	return stringCmd.Val()
}

func (redisUtl RedisUtl) BLPop(redisDb *redis.Client, timeout time.Duration, keys ...string) []string {
	stringSliceCmd := redisDb.BLPop(timeout, keys...)
	if stringSliceCmd.Err() != nil {
		return nil
	}
	return stringSliceCmd.Val()
}

func (redisUtl RedisUtl) BRPop(redisDb *redis.Client, timeout time.Duration, keys ...string) []string {
	stringSliceCmd := redisDb.BRPop(timeout, keys...)
	if stringSliceCmd.Err() != nil {
		return nil
	}
	return stringSliceCmd.Val()
}

func (redisUtl RedisUtl) RPopLPush(redisDb *redis.Client, source string, destination string) bool {
	stringCmd := redisDb.RPopLPush(source, destination)
	if stringCmd.Err() != nil {
		return false
	}
	return true
}

//-------------------------------------- 方法名 不能相同 重载

func (redisUtl RedisUtl) RPushList(key string, values ...interface{}) int64 {
	return redisUtl.RPush(redisUtl.client, key, values...)
}

func (redisUtl RedisUtl) LPushList(key string, values ...interface{}) int64 {
	return redisUtl.RPush(redisUtl.client, key, values...)
}

func (redisUtl RedisUtl) LLenList(key string) int64 {
	return redisUtl.LLen(redisUtl.client, key)
}

func (redisUtl RedisUtl) LRangeList(key string, start int64, end int64) []string {
	return redisUtl.LRange(redisUtl.client, key, start, end)
}

func (redisUtl RedisUtl) LTrimList(key string, start int64, end int64) bool {
	return redisUtl.LTrim(redisUtl.client, key, start, end)
}

func (redisUtl RedisUtl) LIndexList(key string, index int64) string {
	return redisUtl.LIndex(redisUtl.client, key, index)
}

func (redisUtl RedisUtl) LSetList(key string, index int64, value interface{}) bool {
	return redisUtl.LSet(redisUtl.client, key, index, value)
}

func (redisUtl RedisUtl) LRemList(key string, index int64, value interface{}) bool {
	return redisUtl.LRem(redisUtl.client, key, index, value)
}

func (redisUtl RedisUtl) LPopList(key string) string {
	return redisUtl.LPop(redisUtl.client, key)
}

func (redisUtl RedisUtl) RPopList(key string) string {
	return redisUtl.RPop(redisUtl.client, key)
}

func (redisUtl RedisUtl) BLPopList(timeout time.Duration, keys ...string) []string {
	return redisUtl.BLPop(redisUtl.client, timeout, keys...)
}

func (redisUtl RedisUtl) BRPopList(timeout time.Duration, keys ...string) []string {
	return redisUtl.BRPop(redisUtl.client, timeout, keys...)
}

func (redisUtl RedisUtl) RPopLPushList(source string, destination string) bool {
	return redisUtl.RPopLPush(redisUtl.client, source, destination)
}

/**
   Hash 操作
　　HSet(key, field, value)：向名称为key的hash中添加元素field
　　HGet(key, field)：返回名称为key的hash中field对应的value
　　HMget(key, (fields))：返回名称为key的hash中field i对应的value
　　HMset(key, (fields))：向名称为key的hash中添加元素field
　　HIncrby(key, field, integer)：将名称为key的hash中field的value增加integer
　　HExists(key, field)：名称为key的hash中是否存在键为field的域
　　HDel(key, field)：删除名称为key的hash中键为field的域
　　HLen(key)：返回名称为key的hash中元素个数
　　HKeys(key)：返回名称为key的hash中所有键
　　HVals(key)：返回名称为key的hash中所有键对应的value
　　HGetall(key)：返回名称为key的hash中所有的键（field）及其对应的value
*/

func (redisUtl RedisUtl) HSet(redisDb *redis.Client, key string, field string, value interface{}) bool {
	boolCmd := redisDb.HSet(key, field, value)
	if boolCmd.Err() != nil {
		return false
	}
	return boolCmd.Val()
}

func (redisUtl RedisUtl) HGet(redisDb *redis.Client, key string, field string) string {
	stringCmd := redisDb.HGet(key, field)
	if stringCmd.Err() != nil {
		return ""
	}
	return stringCmd.Val()
}

func (redisUtl RedisUtl) HMGet(redisDb *redis.Client, key string, fields ...string) []interface{} {
	stringCmd := redisDb.HMGet(key, fields...)
	if stringCmd.Err() != nil {
		return nil
	}
	return stringCmd.Val()
}

func (redisUtl RedisUtl) HMSet(redisDb *redis.Client, key string, fields map[string]interface{}) bool {
	statusCmd := redisDb.HMSet(key, fields)
	if statusCmd.Err() != nil {
		return false
	}
	return true
}

func (redisUtl RedisUtl) HIncrBy(redisDb *redis.Client, key string, field string, incr int64) int64 {
	intCmd := redisDb.HIncrBy(key, field, incr)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) HExists(redisDb *redis.Client, key string, field string) bool {
	boolCmd := redisDb.HExists(key, field)
	if boolCmd.Err() != nil {
		return false
	}
	return boolCmd.Val()
}

func (redisUtl RedisUtl) HDel(redisDb *redis.Client, key string, fields ...string) bool {
	intCmd := redisDb.HDel(key, fields...)
	if intCmd.Err() != nil {
		return false
	}
	return intCmd.Val() > 0
}

func (redisUtl RedisUtl) HLen(redisDb *redis.Client, key string) int64 {
	intCmd := redisDb.HLen(key)
	if intCmd.Err() != nil {
		return -1
	}
	return intCmd.Val()
}

func (redisUtl RedisUtl) HKeys(redisDb *redis.Client, key string) []string {
	stringSliceCmd := redisDb.HKeys(key)
	if stringSliceCmd.Err() != nil {
		return nil
	}
	return stringSliceCmd.Val()
}

func (redisUtl RedisUtl) HVals(redisDb *redis.Client, key string) []string {
	stringSliceCmd := redisDb.HVals(key)
	if stringSliceCmd.Err() != nil {
		return nil
	}
	return stringSliceCmd.Val()
}

func (redisUtl RedisUtl) HGetAll(redisDb *redis.Client, key string) map[string]string {
	stringStringMapCmd := redisDb.HGetAll(key)
	if stringStringMapCmd.Err() != nil {
		return nil
	}
	return stringStringMapCmd.Val()
}

//-------------------------------------- 方法名 不能相同 重载

func (redisUtl RedisUtl) HSetHash(key string, field string, value interface{}) bool {
	return redisUtl.HSet(redisUtl.client, key, field, value)
}

func (redisUtl RedisUtl) HGetHash(key string, field string) string {
	return redisUtl.HGet(redisUtl.client, key, field)
}

func (redisUtl RedisUtl) HMGetHash(key string, fields ...string) []interface{} {
	return redisUtl.HMGet(redisUtl.client, key, fields...)
}

func (redisUtl RedisUtl) HMSetHash(key string, fields map[string]interface{}) bool {
	return redisUtl.HMSet(redisUtl.client, key, fields)
}

func (redisUtl RedisUtl) HIncrByHash(key string, field string, incr int64) int64 {
	return redisUtl.HIncrBy(redisUtl.client, key, field, incr)
}

func (redisUtl RedisUtl) HExistsHash(key string, field string) bool {
	return redisUtl.HExists(redisUtl.client, key, field)
}

func (redisUtl RedisUtl) HDelHash(key string, fields ...string) bool {
	return redisUtl.HDel(redisUtl.client, key, fields...)
}

func (redisUtl RedisUtl) HLenHash(key string) int64 {
	return redisUtl.HLen(redisUtl.client, key)
}

func (redisUtl RedisUtl) HKeysHash(key string) []string {
	return redisUtl.HKeys(redisUtl.client, key)
}

func (redisUtl RedisUtl) HValsHash(key string) []string {
	return redisUtl.HVals(redisUtl.client, key)
}

func (redisUtl RedisUtl) HGetAllHash(key string) map[string]string {
	return redisUtl.HGetAll(redisUtl.client, key)
}

func (redisUtl RedisUtl) KeysByPattern(pattern string) []string {
	return redisUtl.Keys(redisUtl.client, pattern)
}
