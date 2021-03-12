using Microsoft.Extensions.DependencyInjection.Extensions;
using EzrealClient;
using EzrealClient.Implementations;
using System;
using EzrealClient.FluentApi.Builders;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// IEzrealClientBuilder扩展
    /// </summary>
    public static class EzrealClientBuilderExtensions
    {
        /// <summary>
        /// 添加EzrealClient全局默认配置
        /// </summary>
        /// <remarks>
        /// <para>• 尝试使用DefaultHttpApiActivator，运行时使用Emit动态创建THttpApi的代理类和代理类实例</para>
        /// <para>• 尝试使用DefaultApiActionDescriptorProvider，缺省参数特性声明时为参数应用PathQueryAttribute</para>
        /// <para>• 尝试使用DefaultResponseCacheProvider，在内存中缓存响应结果</para>
        /// <para>• 尝试使用DefaultApiActionInvokerProvider</para>
        /// </remarks> 
        /// <param name="services"></param>
        /// <returns></returns>
        public static IEzrealClientBuilder AddEzrealClient(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddMemoryCache();

            services.TryAddSingleton(typeof(IHttpApiActivator<>), typeof(DefaultHttpApiActivator<>));
            services.TryAddSingleton<IApiActionDescriptorProvider, DefaultApiActionDescriptorProvider>();
            services.TryAddSingleton<IApiActionInvokerProvider, DefaultApiActionInvokerProvider>();
            services.TryAddSingleton<IResponseCacheProvider, DefaultResponseCacheProvider>();

            return new EzrealClientBuilder(services);
        }

        /// <summary>
        /// 当非GET或HEAD请求的缺省参数特性声明时
        /// 为复杂参数类型的参数应用JsonContentAttribute
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IEzrealClientBuilder UseJsonFirstApiActionDescriptor(this IEzrealClientBuilder builder)
        {
            builder.Services.AddSingleton<IApiActionDescriptorProvider, JsonFirstApiActionDescriptorProvider>();
            return builder;
        }


        public static IEzrealClientBuilder UseFluentConfigure(this IEzrealClientBuilder builder,Action<FluentApiAttributesDescriptorBuilder> builderAction)
        {
            builder.Services.AddSingleton(builderAction);
            return builder;
        }
        /// <summary>
        /// EzrealClient全局配置的Builder
        /// </summary>
        private class EzrealClientBuilder : IEzrealClientBuilder
        {
            /// <summary>
            /// 获取服务集合
            /// </summary>
            public IServiceCollection Services { get; }

            public EzrealClientBuilder(IServiceCollection services)
            {
                this.Services = services;
            }
        }
    }
}
