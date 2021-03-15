using EzrealClient.Implementations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace EzrealClient.FluentConfigure.Metadata
{
   public class InterfaceFluentMetadata:IFluentMethodAnnotableMetadata
    {
        public InterfaceFluentMetadata(Type interfaceType, NameSpaceFluentMetadata metadata)
        {
            InterfaceType = interfaceType;
            NameSpaceMetadata = metadata;
            ApiActionAttributes = new List<IApiActionAttribute>();
            ApiFilterAttributes = new List<IApiFilterAttribute>();
            ApiReturnAttributes = new List<IApiReturnAttribute>();
            Properties = new Dictionary<object, object>();
            Methods = new List<MethodFluentMetadata>();
        }

        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute> ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; protected set; }

        public virtual Dictionary<object, object> Properties { get; protected set; }


        public virtual Type InterfaceType { get; protected set; }

 
        public virtual TypeInfo TypeInfo => InterfaceType.GetTypeInfo();
 
        public virtual IEnumerable<MethodFluentMetadata> Methods { get; protected set; }


        public virtual MethodFluentMetadata GetOrAddMethodMetadata(MethodInfo method)
        {
            if (method is null)
            {
                throw new System.ArgumentNullException(nameof(method));
            }
            MethodFluentMetadata? metadata = Methods.FirstOrDefault(a => a.Member == method);
            if (metadata == null)
            {
                metadata = new MethodFluentMetadata(method, this);
                ((List<MethodFluentMetadata>)Methods).Add(metadata);
            }
            return metadata;
        }

        public NameSpaceFluentMetadata NameSpaceMetadata { get; }


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
