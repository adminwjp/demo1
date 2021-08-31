package com.utility.sdk.advert.tencent.api.plan.dto;

import lombok.Data;

/**
*  推广计划 (Campaign)
 *  https://developers.e.qq.com/docs/apilist/ads/campaign
* */
@Data
public class CreateCampaignInput {
      /**
       * 广告主帐号 id，有操作权限的帐号 id，不支持代理商 id
       */
      int AccountId;
      /**
       * 推广计划名称，同一帐号下的推广计划名称不允许重复字段长度最小 1 字节，长度最大 120 字节
       */
      int CampaignName;
      /**
       * 推广计划类型
       */
      CampaignType CampaignType;
      /**
       * 推广目标类型
       */
      PromotedObjectType PromotedObjectType;
      /**
       * 日消耗限额，单位为分，
       * 对于微信朋友圈广告（campaign_type = CAMPAIGN_TYPE_WECHAT_MOMENTS）：
       * 日预算要求介于 100,000 – 1,000,000,000 分之间（1,000 元-10,000,000 元，单位为人民币）；
       * 修改后的日预算不能低于该计划今日已消耗金额的 1.5 倍；
       * 每次修改幅度不能低于 5000 分（50 元，单位为人民币）。
       * 对于非微信朋友圈广告（campaign_type ！= CAMPAIGN_TYPE_WECHAT_MOMENTS）：
       * 日预算需介于 5,000 分-400,000,000 分之间（50 元-4,000,000 元，单位为人民币）；
       * 每次修改幅度不能低于该计划今日已消耗金额加上 5,000 分（50 元，单位为人民币）；
       * 每次修改幅度不能低于 5,000 分（50 元，单位为人民币）；
       * 每天每计划最多修改 1,000 次；
       */
      int DailyBudget;

      ConfiguredStatus ConfiguredStatus;
  }
