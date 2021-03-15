using EzrealClient.FluentConfigure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EzrealClient.FluentConfigure.Builders
{
    public class NameSpaceApiAttributesDescriptorBuilder
    {
        public NameSpaceApiAttributesDescriptorBuilder(NameSpaceFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        internal virtual NameSpaceFluentMetadata Metadata { get; }






        public virtual InterfaceApiAttributesDescriptorBuilder Interface(Type interfaceType)
        {
            var matadata = Metadata.GetOrAddInterfaceMetadata(interfaceType);
            return new InterfaceApiAttributesDescriptorBuilder(matadata);
        }
        public virtual InterfaceApiAttributesDescriptorBuilder<TInterface> Interface<TInterface>()
        {
            var matadata = Metadata.GetOrAddInterfaceMetadata<TInterface>();
            return new InterfaceApiAttributesDescriptorBuilder<TInterface>(matadata);
        }


        public virtual NameSpaceApiAttributesDescriptorBuilder ConfigureInterface(Type interfaceType, Action<InterfaceApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Interface(interfaceType));
            return this;
        }
        public virtual NameSpaceApiAttributesDescriptorBuilder ConfigureInterface<TInterface>(Action<InterfaceApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Interface<TInterface>());
            return this;
        }

        public NameSpaceApiAttributesDescriptorBuilder SetCacheAttribute(IApiCacheAttribute apiCacheAttribute)
        {
            Metadata.SetCacheAttribute(apiCacheAttribute);
            return this;
        }

        public NameSpaceApiAttributesDescriptorBuilder TryAddApiActionAttribute(IApiActionAttribute apiActionAttribute)
        {
            Metadata.TryAddApiActionAttribute(apiActionAttribute);
            return this;
        }

        public NameSpaceApiAttributesDescriptorBuilder TryAddApiFilterAttribute(IApiFilterAttribute apiFilterAttribute)
        {
            Metadata.TryAddApiFilterAttribute(apiFilterAttribute);
            return this;
        }

        public NameSpaceApiAttributesDescriptorBuilder TryAddApiReturnAttribute(IApiReturnAttribute apiReturnAttribute)
        {
            Metadata.TryAddApiReturnAttribute(apiReturnAttribute);
            return this;
        }

        public NameSpaceApiAttributesDescriptorBuilder TryAddPropertie(object key, object value)
        {
            Metadata.TryAddPropertie(key, value);
            return this;
        }
    }
}