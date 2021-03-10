using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
    /// <summary>
    /// 接口定义的Api描述的元数据
    /// </summary>
    public class InterfaceApiActionDescriptorMetadata : ConcurrentDictionary<MethodInfo, ApiActionDescriptor>
    {
        /// <summary>
        /// 接口定义的Api描述的元数据的构造函数
        /// </summary>
        /// <param name="interfaceType"></param>
        public InterfaceApiActionDescriptorMetadata(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }

        /// <summary>
        /// 接口API描述流式构建器所服务的接口
        /// </summary>
        public Type InterfaceType { get; }
    }
}
