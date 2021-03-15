using EzrealClient.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentConfigure.Metadata
{
    public class AssemblyFluentMetadata : IFluentMethodAnnotableMetadata
    {
        public AssemblyFluentMetadata(Assembly assembly, FluentMetadata fluentMetadata)
        {
            Assembly = assembly;
            FluentMetadata = fluentMetadata;
            ApiActionAttributes = new List<IApiActionAttribute>();
            ApiFilterAttributes = new List<IApiFilterAttribute>();
            ApiReturnAttributes = new List<IApiReturnAttribute>();
            Properties = new Dictionary<object, object>();
            NameSpaces = new List<NameSpaceFluentMetadata>();
        }

        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute> ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; protected set; }

        public virtual Dictionary<object, object> Properties { get; protected set; }

        public virtual Assembly Assembly { get; protected set; }
        public FluentMetadata FluentMetadata { get; }

        public virtual AssemblyName AssemblyName => Assembly.GetName();


        public virtual NameSpaceFluentMetadata GetOrAddNameSpaceMetadata(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentException($"“{nameof(@namespace)}”不能为 null 或空白。", nameof(@namespace));
            }

            NameSpaceFluentMetadata? metadata = NameSpaces.FirstOrDefault(a => a.Name == @namespace);
            if (metadata == null)
            {
                metadata = new NameSpaceFluentMetadata(@namespace, this);
                ((List<NameSpaceFluentMetadata>)NameSpaces).Add(metadata);
            }
            return metadata;
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public virtual IEnumerable<NameSpaceFluentMetadata> NameSpaces { get; protected set; }

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
