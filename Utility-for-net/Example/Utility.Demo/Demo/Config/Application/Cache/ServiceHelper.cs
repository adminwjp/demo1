using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using Utility.Security;
using Utility.Json;
using Config.Domain.Entities;
using Config.Application.Services;

namespace Config.Application.Cache
{
    /// <summary>
    /// 服务公共类
    /// </summary>
    public class ServiceHelper
    {
        public static IConfiguration Configuration { get; set; }
        public static Utility.Cache.ICacheContent Cache { get; set; }
        public static IDbConnection Connection { get; set; }
        public static readonly object Lock = new object();
        public static Utility.Cache.IRedisCache RedisCache { get; set; }
        public static bool IsRedis { get; set; }
        
        /// <summary>
        /// 初始化所有服务
        /// </summary>
        static ServiceHelper()
        {
            RegisterMainService();
            ServiceNodeStart();

        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <param name="ip">服务ip</param>
        /// <param name="port">服务端口</param>
        public static int AddService(string name, string ip, int port)
        {
            lock(Lock)
            {
                ServiceEntity service = new ServiceEntity() { Id = SecurityHelper.Sha1(name), Name = name, Ip = ip, Port = port, CreateDate = DateTime.Now, Status = ServiceStatus.OnLine };
                try
                {
                    return Connection.Insert(service).Value;
                }
                finally
                {
                    var data = GetServiceModels();
                    data.Add(service);
                    if (IsRedis)
                    {
                        RedisCache.Publish(ConfigConstant.SERVICE, System.Text.Encoding.UTF8.GetBytes(JsonHelper.ToJson(data)));
                    }
                    Cache.Set(ConfigConstant.SERVICE, data, DateTime.Now.AddDays(30));
                }
            }
        }

        public static List<ServiceEntity> GetServiceModels()
        {
            List<ServiceEntity> data = Cache.Get<List<ServiceEntity>>(ConfigConstant.SERVICE);
            return data;
        }
        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static int DeleteService(string name)
        {
            lock (Lock)
            {
                try
                {
                    ServiceEntity service = new ServiceEntity() { Id = SecurityHelper.Sha1(name), Status = ServiceStatus.Delete, LastDate = DateTime.Now };
                    return Connection.Execute(" update Service set status=@Status,LastDate=@LastDate where id=@Id  ", service);
                }
                finally
                {
                    UpdateStatus(name, ServiceStatus.Delete);
                }
            }
        }
        /// <summary>
        /// 上线服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static int OnLineService(string name,string ip,int port)
        {
            lock (Lock)
            {
                try
                {
                    ServiceEntity serviceApiInfo = new ServiceEntity() { Id = SecurityHelper.Sha1(name), Status = ServiceStatus.OnLine, LastDate = DateTime.Now, Ip = ip, Port = port };
                    return Connection.Execute(" update Service set Ip=@Ip,Port=@Port,status=@Status,LastDate=@LastDate where id=@Id  ", serviceApiInfo);
                }
                finally
                {
                    UpdateStatus(name, ServiceStatus.OnLine);
                }
            }
        }
        /// <summary>
        /// 下线服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static int OffLineService(string name)
        {
            lock (Lock)
            {
                try
                {
                    ServiceEntity serviceApiInfo = new ServiceEntity() { Id = SecurityHelper.Sha1(name), Status = ServiceStatus.OffLine, LastDate = DateTime.Now };
                    return Connection.Execute(" update Service set status=@Status,LastDate=@LastDate where id=@Id  ", serviceApiInfo);
                }
                finally
                {
                    UpdateStatus(name, ServiceStatus.OffLine);
                }
            }
        }

        private static void UpdateStatus(string name,ServiceStatus status)
        {
            string id = SecurityHelper.Sha1(name);
            var data = GetServiceModels();
            foreach (var item in data)
            {
                if (item.Id.Equals(id))
                {
                    item.Status = status;
                    if (IsRedis)
                    {
                        RedisCache.Publish(ConfigConstant.SERVICE, System.Text.Encoding.UTF8.GetBytes(JsonHelper.ToJson(data)));
                    }
                     Cache.Set(ConfigConstant.SERVICE, data, DateTime.Now.AddDays(30));
                    break;
                }
            }
        }
      
        /// <summary>
        /// 服务节点启动
        /// </summary>
        private static void ServiceNodeStart()
        {
            var data = Cache.Get<List<ServiceEntity>>("");
            if (data == null|| data.Count==0)
            {
                var temp = Connection.Query<ServiceEntity>($"select * from Service where status!=0").ToList();
                data = temp?? data ?? new List<ServiceEntity>();
                if(IsRedis)
                {
                    
                    RedisCache.Publish(ConfigConstant.SERVICE, System.Text.Encoding.UTF8.GetBytes(JsonHelper.ToJson(data)));
                    RedisCache.Subscribe(ConfigConstant.SERVICE, (it, msg) => {
                        string json = System.Text.Encoding.UTF8.GetString(msg);
                        var result = JsonHelper.ToObject<List<ServiceEntity>>(json);
                        Cache.Set(ConfigConstant.SERVICE, data, DateTime.Now.AddDays(30));
                    });
                }
                Cache.Set(ConfigConstant.SERVICE, data, DateTime.Now.AddDays(30));
            }

        }
        /// <summary>
        /// 注册主服务节点
        /// </summary>
        private static void RegisterMainService()
        {
            string apiName = Configuration["ApiName"].ToString();
            string ip = Configuration["Ip"].ToString();
            int port = Convert.ToInt32(Configuration["Port"].ToString());
            AddService(apiName, ip, port);
        }
    }
}
