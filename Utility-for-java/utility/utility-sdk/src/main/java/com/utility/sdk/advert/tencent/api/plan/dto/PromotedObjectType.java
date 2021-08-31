package com.utility.sdk.advert.tencent.api.plan.dto;


/**
 * 推广目标类型
 * * */
public enum PromotedObjectType {
    /**
     * Android 应用，创建广告前需通过 [promoted_objects 模块] 登记腾讯开放平台的应用 id，创建广告时需填写之前登记的应用 id
     */
    PROMOTED_OBJECT_TYPE_APP_ANDROID,
    /**
     * IOS 应用，创建广告前需通过 [promoted_objects 模块] 登记 App Store 的应用 id，创建广告时需填写之前登记的应用 id
     */
    PROMOTED_OBJECT_TYPE_APP_IOS,
    /**
     * 电商推广，创建广告时无需创建和指定推广目标
     */
    PROMOTED_OBJECT_TYPE_ECOMMERCE,
    /**
     * 微信品牌页，创建广告时无需创建和指定推广目标
     */
    PROMOTED_OBJECT_TYPE_LINK_WECHAT,
    /**
     * 应用宝推广，创建广告前需通过 [promoted_objects 模块] 登记腾讯应用宝的应用 id，创建广告时需填写之前登记的应用 id
     * /**
     * PROMOTED_OBJECT_TYPE_APP_ANDROID_MYAPP,
     * /**
     * Android 应用（联盟推广），创建广告前需在投放管理平台（e.qq.com）创建联盟应用，创建广告时填写之前已创建的应用 id
     * /**
     * PROMOTED_OBJECT_TYPE_APP_ANDROID_UNION,
     * /**
     * 本地广告（微信推广），创建广告前需在对应的微信公众号中注册登记门店信息，创建广告时需填写之前登记的门店 id，）门店信息的登记及获取可以通过微信公众平台提供的接口进行操作，具体方式可以参考 [本地门店的创建及获取]
     * /**
     * PROMOTED_OBJECT_TYPE_LOCAL_ADS_WECHAT,
     * /**
     * QQ 浏览器小程序，创建广告前需通过 [promoted_objects 模块] 登记 QQ 浏览器的小程序 id，创建广告时需填写之前登记的小程序 id
     */
    PROMOTED_OBJECT_TYPE_QQ_BROWSER_MINI_PROGRAM,
    /**
     * 网页，创建广告时无需创建和指定推广目标
     */
    PROMOTED_OBJECT_TYPE_LINK,
    /**
     * QQ 消息，创建广告时无需创建和指定推广目标
     */
    PROMOTED_OBJECT_TYPE_QQ_MESSAGE,
    /**
     * 认证空间-视频说说，仅可读
     */
    PROMOTED_OBJECT_TYPE_QZONE_VIDEO_PAGE,
    /**
     * 本地广告，仅可读
     */
    PROMOTED_OBJECT_TYPE_LOCAL_ADS,
    /**
     * 好文广告，仅可读
     */
    PROMOTED_OBJECT_TYPE_ARTICLE,
    /**
     * 腾讯课堂，仅可读
     */
    PROMOTED_OBJECT_TYPE_TENCENT_KE,
    /**
     * 换量应用，仅可读
     */
    PROMOTED_OBJECT_TYPE_EXCHANGE_APP_ANDROID_MYAPP,
    /**
     * QQ 空间日志页，仅可读
     */
    PROMOTED_OBJECT_TYPE_QZONE_PAGE_ARTICLE,
    /**
     * QQ 空间嵌入页，仅可读
     */
    PROMOTED_OBJECT_TYPE_QZONE_PAGE_IFRAMED,
    /**
     * QQ 空间首页，仅可读
     */
    PROMOTED_OBJECT_TYPE_QZONE_PAGE,
    /**
     * PC 应用，仅可读
     */
    PROMOTED_OBJECT_TYPE_APP_PC

}
