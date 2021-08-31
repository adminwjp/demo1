package com.utility.sdk.advert.tencent.api.plan.dto;

/**
 * 推广计划类型
 * * */
public enum CampaignType {
    /**
     * 搜索广告，仅支持读
     * *
     */
    CAMPAIGN_TYPE_SEARCH,
    /**
     * 普通展示广告，可投放除微信朋友圈外的所有流量
     * *
     */
    CAMPAIGN_TYPE_NORMAL,
    /**
     * 微信公众号广告，仅可投放微信非朋友圈流量（公众号、小程序等）的广告
     * *
     */
    CAMPAIGN_TYPE_WECHAT_OFFICIAL_ACCOUNTS,
    /**
     * 微信朋友圈广告，仅可投放微信朋友圈流量的广告
     * *
     */
    CAMPAIGN_TYPE_WECHAT_MOMENTS
}
