using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJie.Identity.AuthorizationServer
{
    /// <summary>
    /// 　Step3.为了要把IdentityServer注册到容器中，需要对其进行配置，而这个配置中要包含三个信息：
　　///（1）哪些API可以使用这个AuthorizationServer
　　///（2）哪些Client可以使用这个AuthorizationServer
　　///（3）哪些User可以被这个AuthrizationServer识别并授权
    /// </summary>
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// （1）哪些API可以使用这个AuthorizationServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("clientservice", "CAS Client Service"),
                new ApiResource("productservice", "CAS Product Service"),
                new ApiResource("agentservice", "CAS Agent Service")
            };
        }
        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// （2）哪些Client可以使用这个AuthorizationServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice" }
                },
                new Client
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice", "productservice" }
                },
                new Client
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
                },
            new Client
            {
                ClientId = "cas.mvc.client.implicit",
                ClientName = "CAS MVC Web App Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { $"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signin-oidc" },//登录成功后重定向到客户端
                PostLogoutRedirectUris = { $"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signout-callback-oidc" },//
                AllowedScopes = new [] {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "agentservice", "clientservice", "productservice"
                },
                AllowAccessTokensViaBrowser = true // can return access_token to this client
            }
            };
        }
        /// <summary>
        /// Define which uses will use this IdentityServer
        /// （3）哪些User可以被这个AuthrizationServer识别并授权
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword"
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "leo@hotmail.com",
                    Password = "leopassword"
                },
                new TestUser
                {
                    SubjectId = "10004",
                    Username = "admin",
                    Password = "123456"
                }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
