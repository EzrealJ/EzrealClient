using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using EzrealClient.FluentApi.Builders.Metadata;

namespace EzrealClient.FluentApi.Builders
{
    /// <summary>
    /// API描述流式构建器
    /// </summary>
    public class ApiDescriptorFluentBuilder
    {
        /// <summary>
        /// 构建API描述流式构建器
        /// </summary>
        public ApiDescriptorFluentBuilder()
        {
            MetadataCollection = new InterfaceApiActionDescriptorMetadataCollection();
        }

        /// <summary>
        /// 接口描述构建器集
        /// </summary>
        protected InterfaceApiActionDescriptorMetadataCollection MetadataCollection { get; set; }
        /// <summary>
        /// 获取泛型接口的API描述流式构建器
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public InterfaceApiDescriptorFluentBuilder<TInterface> Interface<TInterface>()
        {
            Type interfaceType = typeof(TInterface);
            if (!interfaceType.IsInterface)
            {
                var message = Resx.required_PublicInterface.Format(interfaceType);
                throw new NotSupportedException(message);
            }
            return new InterfaceApiDescriptorFluentBuilder<TInterface>(MetadataCollection.GetOrAdd(interfaceType, new Metadata.InterfaceApiActionDescriptorMetadata(interfaceType)));
        }
        /// <summary>
        /// 获取API描述流式构建器
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public InterfaceApiDescriptorFluentBuilder Interface(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                var message = Resx.required_PublicInterface.Format(interfaceType);
                throw new NotSupportedException(message);
            }
            return new InterfaceApiDescriptorFluentBuilder(MetadataCollection.GetOrAdd(interfaceType, new Metadata.InterfaceApiActionDescriptorMetadata(interfaceType)));
        }
        /// <summary>
        /// 执行给定接口类型API描述配置。 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public ApiDescriptorFluentBuilder Interface<TInterface>(Action<InterfaceApiDescriptorFluentBuilder<TInterface>> buildAction) {
            buildAction(Interface<TInterface>());
            return this;
        }
    }
}
