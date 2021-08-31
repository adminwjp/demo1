package com.utility.sdk.wxp.api.authorizations;

import com.utility.sdk.wxp.api.accessToken.dto.CreateAccessTokenInput;
import lombok.Data;

/**
 * 通过code换取网页授权access_token
 * */
@Data
public class AuthorizationInfo extends CreateAccessTokenInput {
/**
 * code作为换取access_token的票据，每次用户授权带上的code将不一样，
 * code只能使用一次，5分钟未被使用自动过期。
 * */

    String code;
}
