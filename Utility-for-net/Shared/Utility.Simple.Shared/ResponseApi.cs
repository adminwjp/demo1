using System;
using System.Collections.Generic;
using Utility.Attributes;
using Utility.Helpers;
using Utility.Json;

namespace Utility
{
    /// <summary>
    ///IResponseApi:common api interface 
    /// </summary>
    public interface IResponseApi
    {
        /// <summary> 
        /// wether operator succcess
        /// </summary>
        bool Success { get; set; }

        /// <summary> 
        /// operator information
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///  code
        /// </summary>

        int Code { get; set; }
    }

    /// <summary>
    ///BaseResponseApi:common api .
    ///<see cref="IResponseApi"/> interface  implement
    /// </summary>
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]//remote webservice(interfacer not support) must need, wcf not need
#endif
    public abstract class BaseResponseApi : IResponseApi
    {
        /// <summary> 
        /// wether operator succcess
        /// </summary>
        public bool Success { get; set; }

        /// <summary> 
        /// operator information
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///  code
        /// </summary>

        public int Code { get; set; }
    }
    /// <summary>
    /// <see cref="IResponseApi"/> Middleware
    /// </summary>
    public interface IResponseMiddleware
    {
        /// <summary>
        /// <see cref="IResponseApi"/> Middleware impltement
        /// </summary>
        /// <param name="response"></param>
        /// <returns>operator succcess</returns>
        bool Exected(IResponseApi response);
    }

    /// <summary>
    ///  <see cref="IResponseApi"/> Middleware implement
    /// </summary>
    public class DefaultResponseMiddleware : IResponseMiddleware
    {
        /// <summary>
        /// <see cref="IResponseApi"/> Middleware impltement
        /// </summary>
        /// <param name="response"></param>
        /// <returns>operator succcess</returns>
        bool IResponseMiddleware.Exected(IResponseApi response)
        {
            if (response.Code.ToString().StartsWith("2"))
                response.Success = true;
            else
            {
                response.Success = false;
            }
            return false;
        }
    }
    /// <summary>
    /// common result  api.
    /// <see cref="BaseResponseApi"/>  and 
    /// <see cref="IResponseApi"/> implement.
    /// </summary>
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]//remote webservice(interfacer not support) must need, wcf not need
#endif
    public class ResponseApi:BaseResponseApi, IResponseApi
    {
        /// <summary>
        /// midellware update  Success result
        /// </summary>
        public readonly static List<IResponseMiddleware> Middlewares = new List<IResponseMiddleware>() {
            new DefaultResponseMiddleware()
        };

        /// <summary>
        /// chinese  suceesss
        /// </summary>
        public static readonly ResponseApi Ok = Create(Language.Chinese);

        /// <summary>
        /// chinese fail
        /// </summary>
        public static readonly ResponseApi Fail = CreateFail(Language.Chinese);

        /// <summary>
        /// chinese system error
        /// </summary>
        public static readonly ResponseApi Error = CreateError(Language.Chinese);

        /// <summary>
        /// english  suceesss
        /// </summary>
        public static readonly ResponseApi OkByEnglish = Create(Language.English);

        /// <summary>
        /// english fail
        /// </summary>
        public static readonly ResponseApi FailByEnglish = CreateFail(Language.English);

        /// <summary>
        /// english system error
        /// </summary>
        public static readonly ResponseApi ErrorByEnglish = CreateError(Language.English);

        /// <summary>
        /// datasource ,unkown type  
        /// </summary>

        public object Data { get; set; }

        /// <summary> return json </summary>
        /// <returns>return json</returns>
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }

        /// <summary>
        /// set  datasource ,unkown type  
        /// </summary>
        /// <param name="data">datasource</param>
        /// <returns>return this </returns>
        public virtual ResponseApi SetData(object data)
        {
            this.Data = data;
            return this;
        }

        /// <summary>
        /// set Message
        /// </summary>
        /// <param name="msg">message</param>
        /// <returns>return this</returns>
        public virtual ResponseApi SetMessage(string msg)
        {
            this.Message = msg;
            return this;
        }

        /// <summary>
        /// create <see cref="ResponseApi"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="code">code</param>
        /// <param name="success">success</param>
        /// <param name="statusCode">code ,if code equal zero invalid,if or use</param>
        /// <returns>return this</returns>
        public static ResponseApi Create(Language language = Language.Chinese, Code code = Utility.Code.Success, bool success = true, int statusCode = 0)
        {
#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            DescAttribute desc = DescHelper.CodeDescCache.ContainsKey(code.ToString()) ? DescHelper.CodeDescCache[code.ToString()] : DescHelper.GetDescAttribute(code);
#else
            DescAttribute desc=null;
#endif
            ResponseApi response = new ResponseApi() { Code = (statusCode == 0 ? (int)code : statusCode), Success = success };
            if (desc == null)
            {
                response.Message = code.ToString();
            }
            else
            {
                switch (language)
                {
                    case Language.Chinese: response.Message = desc.ChineseDesc; break;
                    case Language.English: response.Message = desc.EnglishDesc; break;
                    default: response.Message = code.ToString(); break;
                }
            }
            //midellware update  Success result
            foreach (var item in Middlewares)
            {
                if (item.Exected(response)) break;
            }
            return response;
        }

        /// <summary>
        /// Create Success <see cref=" ResponseApi"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>return this</returns>
        public static ResponseApi CreateSuccess(Language language = Language.English)
        {
            return Create(language, Utility.Code.Success);
        }


        /// <summary>
        /// Create Fail <see cref=" ResponseApi"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>return this</returns>
        public static ResponseApi CreateFail(Language language = Language.English)
        {
            return Create(language, Utility.Code.Fail, false);
        }

        /// <summary>
        /// Create Error <see cref=" ResponseApi"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>return this</returns>
        public static ResponseApi CreateError(Language language = Language.English)
        {
            return Create(language, Utility.Code.Error, false);
        }

    }

    /// <summary> 
    /// 通用返回结果
    /// webservice 统一接口 不然需要指定新名称 别扭  隐藏类型 System.Object 的基类成员 ResponseApi.Data。使用 XmlElementAttribute 或 XmlAttributeAttribute 指定一个新名称。
    /// </summary>
    /// <typeparam name="T"></typeparam>
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]
#endif
    public class ResponseApi<T> :BaseResponseApi,IResponseApi//: ResponseApi
    {
        /// <summary>
        /// midellware update  Success result
        /// </summary>
        public readonly static List<IResponseMiddleware> Middlewares = new List<IResponseMiddleware>() {
            new DefaultResponseMiddleware()
        };

        /// <summary>
        /// chinese  suceesss
        /// </summary>
        public static readonly ResponseApi<T> Ok = Create();

        /// <summary>
        /// chinese fail
        /// </summary>
        public static readonly ResponseApi<T> Fail = CreateFail();

        /// <summary>
        /// chinese system error
        /// </summary>
        public static readonly ResponseApi<T> Error = CreateError();

        /// <summary>
        /// english  suceesss
        /// </summary>
        public static readonly ResponseApi<T> OkByEnglish = Create(Language.English);

        /// <summary>
        /// english fail
        /// </summary>
        public static readonly ResponseApi<T> FailByEnglish = CreateFail(Language.English);

        /// <summary>
        /// english system error
        /// </summary>
        public static readonly ResponseApi<T> ErrorByEnglish = ResponseApi<T>.CreateError(Language.English);

        /// <summary>
        /// datasource ,unkown type  
        /// </summary>

        public T Data { get; set; }

        /// <summary> return json </summary>
        /// <returns>return json</returns>
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }

        /// <summary>
        /// set  datasource ,unkown type  
        /// </summary>
        /// <param name="data">datasource</param>
        /// <returns>return this </returns>
        public virtual ResponseApi<T> SetData(T data)
        {
            this.Data = data;
            return this;
        }

        /// <summary>
        /// set Message
        /// </summary>
        /// <param name="msg">message</param>
        /// <returns>return this</returns>
        public virtual ResponseApi<T> SetMessage(string msg)
        {
            this.Message = msg;
            return this;
        }

        /// <summary>
        /// create <see cref="ResponseApi{T}"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="code">code</param>
        /// <param name="success">success</param>
        /// <param name="statusCode">code ,if code equal zero invalid,if or use</param>
        /// <returns>return this</returns>
        public static ResponseApi<T> Create(Language language = Language.English, Code code = Utility.Code.Success, bool success = true, int statusCode = 0)
        {
#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            DescAttribute desc = DescHelper.CodeDescCache.ContainsKey(code.ToString()) ? DescHelper.CodeDescCache[code.ToString()] : DescHelper.GetDescAttribute(code);
#else
            DescAttribute desc=null;
#endif
            ResponseApi<T> response = new ResponseApi<T>() { Code = (statusCode == 0 ? (int)code : statusCode), Success = success };
            if (desc == null)
            {
                response.Message = code.ToString();
            }
            else
            {
                switch (language)
                {
                    case Language.Chinese: response.Message = desc.ChineseDesc; break;
                    case Language.English: response.Message = desc.EnglishDesc; break;
                    default: response.Message = code.ToString(); break;
                }
            }
            //midellware update  Success result
            foreach (var item in Middlewares)
            {
                if (item.Exected(response)) break;
            }
            return response;
        }

        /// <summary>
        /// Create Success <see cref=" ResponseApi{T}"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>return this</returns>
        public static ResponseApi<T> CreateSuccess(Language language = Language.Chinese)
        {
            return Create(language, Utility.Code.Success);
        }


        /// <summary>
        /// Create Fail <see cref=" ResponseApi"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>return this</returns>
        public static ResponseApi<T> CreateFail(Language language = Language.Chinese)
        {
            return Create(language, Utility.Code.Fail, false);
        }

        /// <summary>
        /// Create Error <see cref=" ResponseApi{T}"/>
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>return this</returns>
        public static ResponseApi<T> CreateError(Language language = Language.Chinese)
        {
            return Create(language, Utility.Code.Error, false);
        }
    }
}
