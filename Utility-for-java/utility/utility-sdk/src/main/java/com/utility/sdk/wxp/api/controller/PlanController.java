package com.utility.sdk.wxp.api.controller;

import com.utility.sdk.advert.tencent.api.plan.AdvertPlanApi;
import com.utility.sdk.advert.tencent.api.plan.dto.CreateCampaignInput;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping("tencent/advert/api/v1/plan")
public class PlanController {

    @PostMapping("create")

    public Object Create(String access_token, CreateCampaignInput campaign) throws Exception {
        return AdvertPlanApi.add(access_token, campaign);
    }

    @PostMapping("update")
    public Object Update(String access_token,CreateCampaignInput campaign) throws Exception {
        return AdvertPlanApi.update(access_token, campaign);
    }
    @GetMapping("get")

    public Object Get(String access_token) {
        return null;
    }
}
