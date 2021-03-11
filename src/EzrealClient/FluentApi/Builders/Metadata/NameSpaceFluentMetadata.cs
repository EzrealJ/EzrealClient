using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
    public class NameSpaceFluentMetadata:IFluentMetadata
    {
        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute>? ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute>? ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute>? ApiReturnAttributes { get; protected set; }

        public virtual ConcurrentDictionary<object, object>? Properties { get; protected set; }
        /// <summary>
        /// 获取命名空间
        /// </summary>
        public virtual string? NameSpace { get; protected set; }

        /// <summary>
        /// 接口
        /// </summary>
        public virtual IEnumerable<InterfaceFluentMetadata>? Interfaces { get; protected set; }

    }
}
