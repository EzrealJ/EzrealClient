using EzrealClient.FluentConfigure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EzrealClient.FluentConfigure.Builders
{
    public class MethodApiAttributesDescriptorBuilder
    {
        public MethodApiAttributesDescriptorBuilder(MethodFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        internal virtual MethodFluentMetadata Metadata { get; }



        public virtual ParameterAttributesDescriptorBuilder Parameter(string parameterName)
        {
            var parameterInfo = Metadata.Member.GetParameters().FirstOrDefault(p => p.Name == parameterName);
            return Parameter(parameterInfo);
        }
        public virtual ParameterAttributesDescriptorBuilder Parameter(ParameterInfo parameterInfo)
        {
            return new ParameterAttributesDescriptorBuilder(Metadata.GetOrAddParameterMetadata(parameterInfo));
        }


        public virtual MethodApiAttributesDescriptorBuilder ConfigureParameter(string parameterName, Action<ParameterAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Parameter(parameterName));
            return this;
        }
        public MethodApiAttributesDescriptorBuilder SetCacheAttribute(IApiCacheAttribute apiCacheAttribute)
        {
            Metadata.SetCacheAttribute(apiCacheAttribute);
            return this;
        }

        public MethodApiAttributesDescriptorBuilder TryAddApiActionAttribute(IApiActionAttribute apiActionAttribute)
        {
            Metadata.TryAddApiActionAttribute(apiActionAttribute);
            return this;
        }

        public MethodApiAttributesDescriptorBuilder TryAddApiFilterAttribute(IApiFilterAttribute apiFilterAttribute)
        {
            Metadata.TryAddApiFilterAttribute(apiFilterAttribute);
            return this;
        }

        public MethodApiAttributesDescriptorBuilder TryAddApiReturnAttribute(IApiReturnAttribute apiReturnAttribute)
        {
            Metadata.TryAddApiReturnAttribute(apiReturnAttribute);
            return this;
        }

        public MethodApiAttributesDescriptorBuilder TryAddPropertie(object key, object value)
        {
            Metadata.TryAddPropertie(key, value);
            return this;
        }
    }
}
