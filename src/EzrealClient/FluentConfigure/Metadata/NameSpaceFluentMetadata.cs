using EzrealClient.Implementations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace EzrealClient.FluentConfigure.Metadata
{
    public class NameSpaceFluentMetadata:IFluentMethodAnnotableMetadata
    {
        public NameSpaceFluentMetadata(string @namespace, AssemblyFluentMetadata assemblyMetadata)
        {
            Name = @namespace;
            AssemblyMetadata = assemblyMetadata;
            ApiActionAttributes = new List<IApiActionAttribute>();
            ApiFilterAttributes = new List<IApiFilterAttribute>();
            ApiReturnAttributes = new List<IApiReturnAttribute>();
            Properties = new Dictionary<object, object>();
            Interfaces = new List<InterfaceFluentMetadata>();
        }

        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute> ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; protected set; }

        public virtual Dictionary<object, object> Properties { get; protected set; }
        /// <summary>
        /// 获取命名空间
        /// </summary>
        public virtual string Name { get; protected set; }


        public AssemblyFluentMetadata AssemblyMetadata { get; set; }

        /// <summary>
        /// 接口
        /// </summary>
        public virtual IEnumerable<InterfaceFluentMetadata> Interfaces { get; protected set; }


        public virtual InterfaceFluentMetadata GetOrAddInterfaceMetadata<TInterface>() => GetOrAddInterfaceMetadata(typeof(TInterface));
        public virtual InterfaceFluentMetadata GetOrAddInterfaceMetadata(Type interfaceType)
        {
            if (interfaceType is null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (!interfaceType.IsInterface)
            {
                var message = Resx.required_InterfaceType;
                throw new NotSupportedException(message);
            }
            if (!interfaceType.IsPublic)
            {
                var message = Resx.required_PublicInterface;
                throw new NotSupportedException(message);
            }

            InterfaceFluentMetadata? metadata = Interfaces.FirstOrDefault(a => a.InterfaceType == interfaceType);
            if (metadata == null)
            {
                metadata = new InterfaceFluentMetadata(interfaceType, this);
                ((List<InterfaceFluentMetadata>)Interfaces).Add(metadata);
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
