package com.utility.sdk.wxp.api.accessToken;

import com.utility.hibernate.HibernateFactory;
import com.utility.hibernate.HibernateTemplate;
import com.utility.sdk.wxp.api.accessToken.dto.CreateAccessTokenInput;
import com.utility.sdk.wxp.api.accessToken.dto.GetAccessTokenDto;
import com.utility.sdk.wxp.api.accessToken.dto.GetTokenDto;
import com.utility.service.dto.ResponseApi;
import com.utility.util.SecurityUtil;
import com.utility.util.StringUtil;
import org.hibernate.Session;
import org.hibernate.Transaction;
import sun.security.provider.SHA;
import sun.security.util.Password;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class AccessTokenService
{
    /** 账号或认证信息*/
    public static final List<AccessTokenEntity> AccessTokenEntries = new ArrayList<AccessTokenEntity>(10);
    public  static  void  init(){
        AccessTokenEntity token= new AccessTokenEntity();
        AccessTokenEntries.add(token);
        token.setAccount("230827935@qq.com");
        token.setPassword("heyichuanmei168");
        token.setAppid("wx5539507b4b603ce5");
        token.setSecret("client_credential");
        token.setGrantType("2d45a5013620d70d6a7c25215bf8b59e");

        token= new AccessTokenEntity();
        AccessTokenEntries.add(token);
        token.setAccount("973513569@qq.com");
        token.setPassword("wjp950301.");
        token.setAppid("wx174f88f6a12ba98a");
        token.setSecret("client_credential");
        token.setGrantType("ee48b9a9fcfe09a0b49e86ae93c544c2");
    }
    public static Map<String, String> AccessTokenInfos = new ConcurrentHashMap<String, String>();

    /**
     * 获取AccessToken
     * https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
     * @param accessTokenInput
     * */
    public static ResponseApi GetAccessToken(CreateAccessTokenInput accessTokenInput)
    {
        GetAccessTokenDto accessTokenDto = AccessTokenApi.GetAccessTokenResult(accessTokenInput);
        AccessTokenEntity  token= new AccessTokenEntity();
        token.setId(SecurityUtil.sha(accessTokenInput.getAppid()));
        token.setAppid(accessTokenInput.getAppid());
        token.setSecret(accessTokenInput.getSecret());
        token.setGrantType(accessTokenInput.getGrantType());
        try
        {
            if (accessTokenDto.getAccessToken() != null)
            {
                GetTokenDto getTokenDto=new GetTokenDto() ;
                getTokenDto.setAccessToken(accessTokenDto.getAccessToken());
                getTokenDto.setExpiresIn(accessTokenDto.getExpiresIn()-5);
                return ResponseApi.success().setData(getTokenDto);
            }
            else
            {
                String msg = AccessTokenApi.GetMsg(accessTokenDto.getErrcode());
                msg = msg == "" ? accessTokenDto.getErrmsg() : msg;
                return ResponseApi.fail().setNote(msg);
            }
        }
        finally
        {
            addOrUpdate(token);
        }
    }
 static  void  addOrUpdate( AccessTokenEntity  token){
     Session session =HibernateFactory.getSession();
     Transaction transaction =session.beginTransaction();
     AccessTokenEntity temp=session.get(AccessTokenEntity.class,token.getId());
     if(temp==null){
         session.save(temp);
     }else{
         session.update(temp);
     }
     transaction.commit();
     session.close();
 }

    public static String getAccessId(String access_token)
    {

        for (String item  : AccessTokenService.AccessTokenInfos.keySet())
        {
            if (AccessTokenService.AccessTokenInfos.get(item).equals(access_token))
            {
                return item;
            }
        }
        return null;
    }




    public ResponseApi getById(String id)
    {
        if (StringUtil.isEmpty(id))
        {
            throw new NullPointerException("id param is null");
        }
        HibernateTemplate.Instance.setSession(HibernateFactory.getSession());
        AccessTokenEntity tokenEntity= HibernateTemplate.Instance.get(id,AccessTokenEntity.class);
        HibernateTemplate.Instance.getSession().close();
        CreateAccessTokenInput tokenInput=new CreateAccessTokenInput();
        if(tokenEntity!=null){
            tokenInput.setAppid(tokenEntity.appid);

        }
        return  ResponseApi.success().setData(tokenInput);
    }

    public  ResponseApi Delete(String id)
    {
        if (StringUtil.isEmpty(id))
        {
            throw new NullPointerException("id param is null");
        }
        HibernateTemplate.Instance.setSession(HibernateFactory.getSession());
        HibernateTemplate.Instance.delete(id,AccessTokenEntity.class);
        HibernateTemplate.Instance.getSession().close();
        return  ResponseApi.Success;
    }
}
