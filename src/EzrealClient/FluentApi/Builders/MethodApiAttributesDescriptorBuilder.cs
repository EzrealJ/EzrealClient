using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi.Builders
{
    public class MethodApiAttributesDescriptorBuilder
    {
        public MethodApiAttributesDescriptorBuilder(MethodFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        protected virtual MethodFluentMetadata Metadata { get; }


        protected virtual ParameterFluentMetadata ParameterMetadata(ParameterInfo parameterInfo)
        {
            if (parameterInfo is null)
            {
                throw new ArgumentNullException(nameof(parameterInfo));
            }

            ParameterFluentMetadata? metadata = Metadata.Parameters.FirstOrDefault(a => a.Member == parameterInfo);
            if (metadata == null)
            {
                metadata = new ParameterFluentMetadata(parameterInfo, Metadata);
                ((List<ParameterFluentMetadata>)Metadata.Parameters).Add(metadata);
            }
            return metadata;
        }
        public virtual ParameterAttributesDescriptorBuilder Method(string parameterName)
        {
            var parameterInfo = Metadata.Member.GetParameters().FirstOrDefault(p => p.Name == parameterName);
            return Method(parameterInfo);
        }
        public virtual ParameterAttributesDescriptorBuilder Method(ParameterInfo parameterInfo)
        {
            return new ParameterAttributesDescriptorBuilder(ParameterMetadata(parameterInfo));
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

        public MethodApiAttributesDescriptorBuilder TryAddPropertie(object key, object? value)
        {
            Metadata.TryAddPropertie(key, value);
            return this;
        }
    }
}
