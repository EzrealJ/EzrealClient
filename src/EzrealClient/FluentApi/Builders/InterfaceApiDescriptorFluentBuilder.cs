using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzrealClient.FluentApi.Builders
{
    /// <summary>
    /// 接口API描述流式构建器
    /// </summary>
    public class InterfaceApiDescriptorFluentBuilder
    {
        /// <summary>
        /// 构建接口API描述流式构建器
        /// </summary>
        /// <param name="metadata">元数据</param>
        public InterfaceApiDescriptorFluentBuilder(InterfaceApiActionDescriptorMetadata metadata)
        {
            Metadata = metadata;
        }
        /// <summary>
        /// 接口API描述流式构建器所服务的接口
        /// </summary>
        public Type InterfaceType => Metadata.InterfaceType;
        /// <summary>
        /// 
        /// </summary>
        public InterfaceApiActionDescriptorMetadata Metadata { get; }
    }
    /// <summary>
    /// 泛型的接口API描述流式构建器
    /// </summary>
    public class InterfaceApiDescriptorFluentBuilder<TInterface> : InterfaceApiDescriptorFluentBuilder
    {
        /// <summary>
        /// 泛型的接口API描述流式构建器 的 构造函数
        /// </summary>
        /// <param name="metadata"></param>
        public InterfaceApiDescriptorFluentBuilder(InterfaceApiActionDescriptorMetadata metadata) : base(metadata)
        {
        }
    }
}
