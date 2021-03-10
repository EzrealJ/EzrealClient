using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
    /// <summary>
    /// 接口定义的Api描述的元数据集合
    /// </summary>
   public class InterfaceApiActionDescriptorMetadataCollection: ConcurrentDictionary<Type, InterfaceApiActionDescriptorMetadata>
    {
    }
}
