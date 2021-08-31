// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//#define V4 //identityserver >=4.0 新版本
//#define V3 //identityserver <4.0 老版本

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
{
    //参考样例 
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
           };
#if V4 //ApiSecretValidator 通过不了  ApiResource 内部 怎么 转 的
        public static IEnumerable<ApiScope> Scopes =>
           new List<ApiScope>
           {
                new ApiScope("api1", "My API"){ },
                   new ApiScope("resource1.scope1", "My API"){ },
               //new ApiScope(StandardScopes.OpenId, "My API"){ },//异常 /.well-known/openid-configuration
           };
#endif
//#elif V3 //PlainTextSharedSecretValidator 通过不了
        //No shared secret configured for client. v3
        //No API resource with that name found. aborting v4
        //Client ClientId 必须 跟  ApiResource Name 匹配 上 不然 connect/introspect 通过不了  错误日志 
        //网上 这种写法 参考  https://www.cnblogs.com/VirtualMJ/p/12831498.html 不知道 是版本 几 的 
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                //http://localhost:5000/connect/introspect pass
                new ApiResource("api2", "My API")//名称 不能 相同 否则 connect/token 调用失败
                {
                    ApiSecrets = {new Secret("secret".Sha256())} ,  
                    //Scopes = { new Scope("api1")}//冲突 

                },
                new ApiResource(StandardScopes.OpenId, "My API"){ },     
                new ApiResource(StandardScopes.Profile, "My API"){ },
            };
//#endif
        public static IEnumerable<Client> Clients =>
            new List<Client>
            { 
                // machine to machine client
                new Client
                {
                    ClientId = "client",//ClientId
                    
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()){ Type=SecretTypes.SharedSecret}
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" } 
                     // AllowedScopes = { "api1", StandardScopes.OpenId } //connect/userinfo 必须要有  openid
                },
                //CustomGrant
                 new Client
                {
                    ClientId = "client.custom",//ClientId
                    
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = { "custom"},

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()){ Type=SecretTypes.SharedSecret}
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1", "resource1.scope1" } ,
                    Properties={ [
                        "custom_credential"]= "custom credential",
                         ["api1"]= "resource1.scope1"
                     }
                     // AllowedScopes = { "api1", StandardScopes.OpenId } //connect/userinfo 必须要有  openid
                },
                 // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    //AllowedGrantTypes = GrantTypes.Implicit,//Client not configured to receive access tokens via browser
                    //https://stackoverflow.com/questions/57240285/invalid-state-cookie-an-error-was-encountered-while-handling-the-remote-login
                    //https://www.cnblogs.com/ddrsql/p/7922451.html
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },//An error was encountered while handling the remote login.
                        //RequireClientSecret = false,//必须要 
                        //          AllowAccessTokensViaBrowser = true, //允许通过浏览器传输token
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                         StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.OfflineAccess,
                        "api1"
                    }
                },

                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                   // AllowedGrantTypes = GrantTypes.Implicit,//Invalid grant type for client
                    RequireClientSecret = false,//必须要 

                    AllowAccessTokensViaBrowser = true, //允许通过浏览器传输token

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.OfflineAccess,
                        "api1"
                    }
                },
                 new Client
                  {
                        ClientId="api",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //这里是指定授权的模式，选择密码模式， invalid_grant
                        ClientSecrets = { new Secret("secret".Sha256()) },
                         RefreshTokenUsage=TokenUsage.ReUse,
                         AlwaysIncludeUserClaimsInIdToken = true,
                         AllowOfflineAccess = true,
                         AllowedScopes=new List<string>
                        {
                            "api1",
                            StandardScopes.Profile,
                            StandardScopes.OpenId,
                            StandardScopes.OfflineAccess
                        }

                  }
            };
    }
}