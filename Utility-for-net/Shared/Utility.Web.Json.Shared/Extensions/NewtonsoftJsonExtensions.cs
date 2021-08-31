// Microsoft.AspNetCore.Mvc.NewtonsoftJson AspNetCore >= 3.0 

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Json;

namespace Utility.Extensions
{
    /// <summary>
    /// MvcBuilder  Newtonsoft 扩展类
    /// </summary>
    public static class NewtonsoftJsonExtensions
    {
        /// <summary>
        /// json字符串大小写原样输出
        /// </summary>
        public static readonly IContractResolver ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();

        /// <summary>
        /// json 转换 默认 ABC a_b_c 忽略循环 时间 格式 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="contractResolver">默认为null</param>
        /// <returns></returns>
        public static IMvcBuilder AddJson(this IMvcBuilder builder, IContractResolver contractResolver=null )
        {
#if !(NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2)
            //全局配置Json序列化处理 方案1
            return builder.AddNewtonsoftJson(options =>
            {
               // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //使用 AbC ab_c
                options.SerializerSettings.ContractResolver = contractResolver??JsonContractResolver.ObjectResolverJson;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }
          )
          .AddXmlSerializerFormatters();
#else
            return builder;
#endif
            //全局配置Json序列化处理  方案2
            //  .AddJsonOptions(options =>
            //  {
            //      options.JsonSerializerOptions.MaxDepth = 10;
            //       options.JsonSerializerOptions.PropertyNamingPolicy = JsonPropertyNamingPolicy.CamelCase;

            //  }
            //)

        }
    }
}
#endif



// AspNet >=45

#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Utility.Extensions
{
    /// <summary>
    /// HttpConfiguration Newtonsoft 扩展类
    /// </summary>
    public static  class NewtonsoftJsonExtensions
    {
        /// <summary>
        /// json字符串大小写原样输出
        /// </summary>
        public static readonly IContractResolver ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
    

        /// <summary>
        /// 默认 返回 xml 
        /// 返回 json 格式 ABC a_b_c
        /// </summary>
        /// <param name="config"></param>
        public static void AddJson(this HttpConfiguration config, IContractResolver contractResolver=null )
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();//使用 json 默认 xml
            var jsonFormatter = new JsonMediaTypeFormatter();
            var settings = jsonFormatter.SerializerSettings;

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            settings.Converters.Add(timeConverter);


            settings.ContractResolver =contractResolver==null? Utility.Json.JsonContractResolver.ObjectResolverJson:contractResolver;
            jsonFormatter.SerializerSettings = settings;
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
        public class JsonContentNegotiator : IContentNegotiator
        {
            private readonly JsonMediaTypeFormatter _jsonFormatter;


            public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
            {
                _jsonFormatter = formatter;
            }


            public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
            {
                var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
                return result;
            }
        }

    }
}
#endif
