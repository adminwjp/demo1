package com.utility.sdk.wxp.api.customMenus;

import com.utility.sdk.wxp.api.ApiResult;
import com.utility.service.dto.ResponseApi;
import com.utility.util.JsonUtil;
import com.utility.util.StringUtil;

import java.util.Map;

/// <summary>
    /// 自定义菜单 服务
    /// </summary>
    public class CustomMenuService {
        /// <summary>
        /// 自定义菜单创建接口
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static ResponseApi CreateCustomMenu(Map<String, Object> param, String access_token) {
            if (param == null) {
                throw  new NullPointerException("param  param is null");
            } else if (StringUtil.isEmpty(access_token)) {
                    throw  new NullPointerException("access_token  param is null");
            } else {
                //验证
                ApiResult apiResult = new ApiResult(
                        CustomMenuApi.CreateCustomMenu(JsonUtil.toJson(param), access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

        /// <summary>自定义菜单查询接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public static ResponseApi Get(String access_token) {
            if (StringUtil.isEmpty(access_token)) {
                throw  new NullPointerException("param  param is null");
            } else {
                ApiResult apiResult = new ApiResult(CustomMenuApi.Get( access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

        /// <summary>自定义菜单删除接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        /// ]]>
        public static ResponseApi Delete(String access_token) {
            if (StringUtil.isEmpty(access_token)) {
                throw  new NullPointerException("param  param is null");
            } else {
                ApiResult apiResult = new ApiResult(CustomMenuApi.Delete( access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

        //#region 个性化菜单接口
        //////=============================================================================
        //查询个性化菜单
        //使用普通自定义菜单查询接口可以获取默认菜单和全部个性化菜单信息，请见自定义菜单查询接口的说明。


        //删除所有菜单
        //使用普通自定义菜单删除接口可以删除所有自定义菜单（包括默认菜单和全部个性化菜单），请见自定义菜单删除接口的说明。
        //////=============================================================================


        /// <summary>
        /// 创建个性化菜单
        /// <![CDATA[
        /// https://api.weixin.qq.com/cgi-bin/menu/addconditional?access_token=ACCESS_TOKEN
        /// ]]>
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <param name="access_token">请求参数 access_token</param>
        /// <returns></returns>
        public static ResponseApi CreateConditionalMenu(Map<String, Object> param, String access_token) {
            if (param == null) {
                throw  new NullPointerException("param  param is null");
            } else if (StringUtil.isEmpty(access_token)) {
                throw  new NullPointerException("access_token  param is null");
            } else {
                //验证
                ApiResult apiResult = new ApiResult(
                        CustomMenuApi.CreateConditionalMenu(JsonUtil.toJson(param), access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

        /// <summary>自定义菜单删除接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="menuid">menuid为菜单id，可以通过自定义菜单查询接口获取。</param>
        /// <param name="access_token">请求参数 access_token</param>
        /// <returns></returns>
        public static ResponseApi Delconditional(String menuid, String access_token) {
            if (StringUtil.isEmpty(menuid))  {
                throw  new NullPointerException("menuid  param is null");
            } else if (StringUtil.isEmpty(access_token)) {
                throw  new NullPointerException("access_token  param is null");
            } else {
                //验证
                ApiResult apiResult = new ApiResult(
                        CustomMenuApi.Delconditional(menuid, access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

        /// <summary>测试个性化菜单匹配结果</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/menu/trymatch?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="user_id">user_id可以是粉丝的OpenID，也可以是粉丝的微信号。</param>
        /// <param name="access_token">请求参数 access_token</param>
        /// <returns></returns>
        public static ResponseApi Trymatch(String user_id, String access_token) {
            if (StringUtil.isEmpty(user_id))  {
                throw  new NullPointerException("user_id  param is null");
            } else if (StringUtil.isEmpty(access_token)) {
                throw  new NullPointerException("access_token  param is null");
            } else {
                //验证
                ApiResult apiResult = new ApiResult(
                        CustomMenuApi.Trymatch(user_id, access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

      //  #endregion 个性化菜单接口

        /// <summary>获取自定义菜单配置接口</summary>
        ///<![CDATA[
        ///https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token=ACCESS_TOKEN
        /// ]]>
        /// <param name="access_token">请求参数 access_token</param>
        /// <returns></returns>
        public static ResponseApi GetCurrentSelfmenuInfo(String access_token) {
             if (StringUtil.isEmpty(access_token)) {
                throw  new NullPointerException("access_token  param is null");
            } else {
                //验证
                ApiResult apiResult = new ApiResult(
                        CustomMenuApi.getCurrentSelfmenuInfo( access_token));
                if (apiResult.Success()) {
                    return ResponseApi.success();
                } else {
                    return ResponseApi.fail().setData(apiResult.getErrmsg());
                }
            }
        }

    }
