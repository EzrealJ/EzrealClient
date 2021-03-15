﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using EzrealClient.Extensions.OAuths.Exceptions;

namespace EzrealClient.Extensions.OAuths.TokenProviders
{
    /// <summary>
    /// 表示Client模式的token提供者
    /// </summary>
    public class ClientCredentialsTokenProvider : TokenProvider
    {
        /// <summary>
        /// Client模式的token提供者
        /// </summary>
        /// <param name="services"></param> 
        public ClientCredentialsTokenProvider(IServiceProvider services)
            : base(services)
        {
        }

        /// <summary>
        /// 请求获取token
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        protected override System.Threading.Tasks.Task<TokenResult?> RequestTokenAsync(IServiceProvider serviceProvider)
        {
            var options = this.GetOptionsValue<ClientCredentialsOptions>();
            if (options.Endpoint == null)
            {
                throw new TokenEndPointNullException();
            }

            var tokenClient = serviceProvider.GetRequiredService<OAuth2TokenClient>();
            return tokenClient.RequestTokenAsync(options.Endpoint, options.Credentials);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
        protected override System.Threading.Tasks.Task<TokenResult?> RefreshTokenAsync(IServiceProvider serviceProvider, string refresh_token)
        {
            var options = this.GetOptionsValue<ClientCredentialsOptions>();
            if (options.Endpoint == null)
            {
                throw new TokenEndPointNullException();
            }

            if (options.UseRefreshToken == false)
            {
                return this.RequestTokenAsync(serviceProvider);
            }

            var refreshCredentials = new RefreshTokenCredentials
            {
                Client_id = options.Credentials.Client_id,
                Client_secret = options.Credentials.Client_secret,
                Extra = options.Credentials.Extra,
                Refresh_token = refresh_token
            };

            var tokenClient = serviceProvider.GetRequiredService<OAuth2TokenClient>();
            return tokenClient.RefreshTokenAsync(options.Endpoint, refreshCredentials);
        }
    }
}