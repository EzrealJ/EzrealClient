using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
    /// <summary>
    /// Method级别的FluentMetadata
    /// </summary>
    public class MethodFluentMetadata : IFluentMetadata
    {
        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute> ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; protected set; }

        public virtual ConcurrentDictionary<object, object> Properties { get; protected set; }

        /// <summary>
        /// 获取所在接口类型
        /// 这个值不一定是声明方法的接口类型
        /// </summary>
        public virtual Type InterfaceType { get; protected set; }

        /// <summary>
        /// 获取Api名称
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// 获取关联的方法信息
        /// </summary>
        public virtual MethodInfo Member { get; protected set; }

        public virtual IEnumerable<ParameterFluentMetadata> Parameters { get; protected set; }
    }
}
