package com.utility.sdk.advert.tencent.api.plan;

import com.utility.sdk.advert.tencent.api.plan.dto.CreateCampaignInput;
import com.utility.util.HttpUtils;
import com.utility.util.JsonUtil;

/**
*  推广计划 (Campaign)
* https://developers.e.qq.com/docs/apilist/ads/campaign
* */
public class AdvertPlanApi {
      public static String add(String access_token, CreateCampaignInput campaign) {
          String url = "https://api.e.qq.com/v1.1/campaigns/add?access_token="+access_token+"&timestamp="+System.currentTimeMillis()+"&nonce="+System.currentTimeMillis();
          return HttpUtils.doPost(url, JsonUtil.toJson(campaign),"application/json");
      }

      public static String update(String access_token, CreateCampaignInput campaign) throws Exception {
          String url = "https://api.e.qq.com/v1.1/campaigns/update?access_token="+access_token+"&timestamp="+System.currentTimeMillis()+"&nonce="+System.currentTimeMillis();
          return HttpUtils.doPost(url, JsonUtil.toJson(campaign),"application/json");
      }
  }
