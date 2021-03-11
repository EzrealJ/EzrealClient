using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
    /// <summary>
    /// 流式Api元数据
    /// </summary>
    public interface IFluentMetadata
    {
        /// <summary>
        /// 获取Api关联的缓存器特性
        /// </summary>
        IApiCacheAttribute? CacheAttribute { get; }

        /// <summary>
        /// 获取Api关联的执行器特性
        /// </summary>
        IEnumerable<IApiActionAttribute> ApiActionAttributes { get; }

        /// <summary>
        /// 获取Api关联的过滤器特性
        /// </summary>
        IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; }

        /// <summary>
        /// 获取Api关联的返回特性
        /// </summary>
        IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; }
        /// <summary>
        /// 获取自定义数据存储的字典
        /// </summary>
        Dictionary<object, object> Properties { get; }
    }
}
