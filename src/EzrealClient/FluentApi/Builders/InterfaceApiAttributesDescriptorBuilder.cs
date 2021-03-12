using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace EzrealClient.FluentApi.Builders
{
    public class InterfaceApiAttributesDescriptorBuilder
    {
        public InterfaceApiAttributesDescriptorBuilder(InterfaceFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        protected virtual InterfaceFluentMetadata Metadata { get; }

        protected virtual MethodFluentMetadata MethodMetadata(MethodInfo method)
        {
            if (method is null)
            {
                throw new System.ArgumentNullException(nameof(method));
            }
            MethodFluentMetadata? metadata = Metadata.Methods.FirstOrDefault(a => a.Member == method);
            if (metadata == null)
            {
                metadata = new MethodFluentMetadata(method, Metadata);
                ((List<MethodFluentMetadata>)Metadata.Methods).Add(metadata);
            }
            return metadata;
        }

        public virtual MethodApiAttributesDescriptorBuilder Method(MethodInfo method)
        {
            var matadata = MethodMetadata(method);
            return new MethodApiAttributesDescriptorBuilder(matadata);
        }

        public virtual MethodApiAttributesDescriptorBuilder Method(string methodName)
        {
            return Method(methodName, Array.Empty<Type>());
        }

        public virtual MethodApiAttributesDescriptorBuilder Method(string methodName,params Type[] types)
        {
            var method = Metadata.InterfaceType.GetMethod(methodName, types);
            var matadata = MethodMetadata(method);
            return new MethodApiAttributesDescriptorBuilder(matadata);
        }

        public virtual InterfaceApiAttributesDescriptorBuilder ConfigureMethod(string methodName, Action<MethodApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Method(methodName, Array.Empty<Type>()));
            return this;
        }
        public virtual InterfaceApiAttributesDescriptorBuilder ConfigureMethod(string methodName, Type[] types, Action<MethodApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Method(methodName, types));
            return this;
        }

        public InterfaceApiAttributesDescriptorBuilder SetCacheAttribute(IApiCacheAttribute apiCacheAttribute)
        {
            Metadata.SetCacheAttribute(apiCacheAttribute);
            return this;
        }

        public InterfaceApiAttributesDescriptorBuilder TryAddApiActionAttribute(IApiActionAttribute apiActionAttribute)
        {
            Metadata.TryAddApiActionAttribute(apiActionAttribute);
            return this;
        }

        public InterfaceApiAttributesDescriptorBuilder TryAddApiFilterAttribute(IApiFilterAttribute apiFilterAttribute)
        {
            Metadata.TryAddApiFilterAttribute(apiFilterAttribute);
            return this;
        }

        public InterfaceApiAttributesDescriptorBuilder TryAddApiReturnAttribute(IApiReturnAttribute apiReturnAttribute)
        {
            Metadata.TryAddApiReturnAttribute(apiReturnAttribute);
            return this;
        }

        public InterfaceApiAttributesDescriptorBuilder TryAddPropertie(object key, object? value)
        {
            Metadata.TryAddPropertie(key, value);
            return this;
        }

    }
    public class InterfaceApiAttributesDescriptorBuilder<TInterface> : InterfaceApiAttributesDescriptorBuilder
    {
        public InterfaceApiAttributesDescriptorBuilder(InterfaceFluentMetadata matadata) : base(matadata)
        {
 
        }






    }
}