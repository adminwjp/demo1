package com.utility.sdk.advert.tencent.api.plan.dto;

  /**
* 投放速度模式
* */
public enum SpeedMode {
      /**
       * 标准投放，系统会优化您的广告的投放，让您的预算在设定的投放时段内较为平稳地消耗，默认为标准投放
       */
      SPEED_MODE_STANDARD,
      /**
       * 加速投放，广告会以较快的速度获得曝光，选择加速投放可能会导致您的预算较快地耗尽
       */
      SPEED_MODE_FAST

  }
