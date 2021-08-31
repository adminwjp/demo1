package com.utility.sdk.wxp.api;

  /**抽象基类 众平台接口域名 api **/
public abstract class BaseApi {
      // const String DOMAIN_PATH = "//Config//domain.xml";

      public static int Language;

      /**
       * 获取错误信息
       *
       * @param code
       * @return
       */
      public static String GetErrorMsg(int code) {
          String msg = GetErrorMsg(code, Language);
          return msg;
      }

      /**
       * 获取错误信息
       *
       * @param code
       * @param language
       * @return
       */

      public static String GetErrorMsg(int code, int language) {
          return "";
      }

      // 公众平台接口域名说明 开发者可以根据自己的服务器部署情况，选择最佳的接入点（延时更低，稳定性更高）。除此之外，可以将其他接入点用作容灾用途，当网络链路发生故障时，可以考虑选择备用接入点来接入。
      /**
       * 通用域名(api.weixin.qq.com)，使用该域名将访问官方指定就近的接入点 *
       */
      public static final String CURRENT = "https://api.weixin.qq.com/";
      /**
       * 上海域名(sh.api.weixin.qq.com)，使用该域名将访问上海的接入点 *
       */
      public static final String SH = "https://sh.api.weixin.qq.com/";
      /**
       * 深圳域名(sz.api.weixin.qq.com)，使用该域名将访问深圳的接入点 *
       */
      public final String SZ = "https://sz.api.weixin.qq.com/";
      /**
       * 香港域名(hk.api.weixin.qq.com)，使用该域名将访问香港的接入点 *
       */
      public static final String HK = "https://hk.api.weixin.qq.com/";
      /**
       * 通用异地容灾域名(api2.weixin.qq.com)，当上述域名不可访问时可改访问此域名 *
       */
      public static final String EXCEPTION = "https://api2.api.weixin.qq.com/";
      // 公众平台接口域名说明 开发者可以根据自己的服务器部署情况，选择最佳的接入点（延时更低，稳定性更高）。除此之外，可以将其他接入点用作容灾用途，当网络链路发生故障时，可以考虑选择备用接入点来接入。
      /**
       * 默认地址*
       */
      public static String Domain = BaseApi.CURRENT;

  }
