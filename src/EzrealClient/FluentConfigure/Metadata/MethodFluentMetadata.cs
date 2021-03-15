using EzrealClient.Implementations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace EzrealClient.FluentConfigure.Metadata
{
    /// <summary>
    /// Method级别的FluentMetadata
    /// </summary>
    public class MethodFluentMetadata : IFluentMethodAnnotableMetadata
    {
        private string? name;

        public MethodFluentMetadata(MethodInfo method, InterfaceFluentMetadata InterfaceMetadata)
        {
            Member = method;
            this.InterfaceMetadata = InterfaceMetadata;
            ApiActionAttributes = new List<IApiActionAttribute>();
            ApiFilterAttributes = new List<IApiFilterAttribute>();
            ApiReturnAttributes = new List<IApiReturnAttribute>();
            Properties = new Dictionary<object, object>();
            Parameters = new List<ParameterFluentMetadata>();
        }

        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute> ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; protected set; }

        public virtual Dictionary<object, object> Properties { get; protected set; }

        /// <summary>
        /// 获取所在接口类型
        /// </summary>
        public virtual Type InterfaceType => InterfaceMetadata.InterfaceType;

        /// <summary>
        /// 获取Api名称
        /// </summary>
        public virtual string Name { get => name ?? Member.Name; protected set => name = value; }

        /// <summary>
        /// 获取关联的方法信息
        /// </summary>
        public virtual MethodInfo Member { get; protected set; }
        public InterfaceFluentMetadata InterfaceMetadata { get; }
        public virtual IEnumerable<ParameterFluentMetadata> Parameters { get; protected set; }

        public virtual ParameterFluentMetadata GetOrAddParameterMetadata(ParameterInfo parameterInfo)
        {
            if (parameterInfo is null)
            {
                throw new ArgumentNullException(nameof(parameterInfo));
            }

            ParameterFluentMetadata? metadata = Parameters.FirstOrDefault(a => a.Member == parameterInfo);
            if (metadata == null)
            {
                metadata = new ParameterFluentMetadata(parameterInfo, this);
                ((List<ParameterFluentMetadata>)Parameters).Add(metadata);
            }
            return metadata;
        }
        public void SetCacheAttribute(IApiCacheAttribute apiCacheAttribute)
        {
            this.CacheAttribute = apiCacheAttribute;
        }

        public bool TryAddApiActionAttribute(IApiActionAttribute apiActionAttribute)
        {
            if (ApiActionAttributes.Contains(apiActionAttribute, MultiplableComparer<IApiActionAttribute>.Instance))
            {
                return false;
            }
            ((List<IApiActionAttribute>)ApiActionAttributes).Add(apiActionAttribute);
            return true;
        }

        public bool TryAddApiFilterAttribute(IApiFilterAttribute apiFilterAttribute)
        {
            if (ApiFilterAttributes.Contains(apiFilterAttribute, MultiplableComparer<IApiFilterAttribute>.Instance))
            {
                return false;
            }
            ((List<IApiFilterAttribute>)ApiActionAttributes).Add(apiFilterAttribute);
            return true;
        }

        public bool TryAddApiReturnAttribute(IApiReturnAttribute apiReturnAttribute)
        {
            if (ApiReturnAttributes.Contains(apiReturnAttribute, MultiplableComparer<IApiReturnAttribute>.Instance))
            {
                return false;
            }
            ((List<IApiReturnAttribute>)ApiActionAttributes).Add(apiReturnAttribute);
            return true;
        }

        public bool TryAddPropertie(object key, object value)
        {
            return Properties.TryAdd(key, value);
        }
    }
}
