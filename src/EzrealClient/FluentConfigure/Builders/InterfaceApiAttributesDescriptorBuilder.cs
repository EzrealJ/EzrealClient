using EzrealClient.FluentConfigure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EzrealClient.FluentConfigure.Builders
{
    public class InterfaceApiAttributesDescriptorBuilder
    {
        public InterfaceApiAttributesDescriptorBuilder(InterfaceFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        internal virtual InterfaceFluentMetadata Metadata { get; }



        public virtual MethodApiAttributesDescriptorBuilder Method(MethodInfo method)
        {
            var matadata = Metadata.GetOrAddMethodMetadata(method);
            return new MethodApiAttributesDescriptorBuilder(matadata);
        }

        public virtual MethodApiAttributesDescriptorBuilder Method(string methodName)
        {
            var methodInfo = Metadata.InterfaceType.GetMethod(methodName);
            return Method(methodInfo);
        }

        public virtual MethodApiAttributesDescriptorBuilder Method(string methodName,params Type[] types)
        {
            var methodInfo = Metadata.InterfaceType.GetMethod(methodName, types);
            return Method(methodInfo);
        }

        public virtual InterfaceApiAttributesDescriptorBuilder ConfigureMethod(string methodName, Action<MethodApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Method(methodName));
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

        public InterfaceApiAttributesDescriptorBuilder TryAddPropertie(object key, object value)
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