using EzrealClient.FluentConfigure.Builders;
using EzrealClient.FluentConfigure.Descriptors;
using EzrealClient.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentConfigure
{
    /// <summary>
    /// 支持FluentConfigure方式的ApiActionDescriptor提供者的接口
    /// </summary>
    public class FluentConfigureActionDescriptorProvider : IApiActionDescriptorProvider
    {
        public FluentConfigureActionDescriptorProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected IServiceProvider ServiceProvider { get; }

        public ApiActionDescriptor CreateActionDescriptor(MethodInfo method, Type interfaceType)
        {
            var builderAction = ServiceProvider.GetRequiredService<Action<FluentConfigureAttributesDescriptorBuilder>>();
            var builder = new FluentConfigureAttributesDescriptorBuilder();
            builderAction(builder);
            var metadata = builder.Interface(interfaceType).Method(method).Metadata;
            return new FluentConfigureApiActionDescriptor(metadata);
        }
    }
}
