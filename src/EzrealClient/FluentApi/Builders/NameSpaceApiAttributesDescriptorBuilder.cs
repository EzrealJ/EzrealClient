using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EzrealClient.FluentApi.Builders
{
    public class NameSpaceApiAttributesDescriptorBuilder
    {
        public NameSpaceApiAttributesDescriptorBuilder(NameSpaceFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        protected virtual NameSpaceFluentMetadata Metadata { get; }



        protected virtual InterfaceFluentMetadata InterfaceMetadata<TInterface>() => InterfaceMetadata(typeof(TInterface));
        protected virtual InterfaceFluentMetadata InterfaceMetadata(Type interfaceType)
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

            InterfaceFluentMetadata? metadata = Metadata.Interfaces.FirstOrDefault(a => a.InterfaceType == interfaceType);
            if (metadata == null)
            {
                metadata = new InterfaceFluentMetadata(interfaceType, Metadata);
                ((List<InterfaceFluentMetadata>)Metadata.Interfaces).Add(metadata);
            }
            return metadata;
        }


        public virtual InterfaceApiAttributesDescriptorBuilder Interface(Type interfaceType)
        {
            var matadata = InterfaceMetadata(interfaceType);
            return new InterfaceApiAttributesDescriptorBuilder(matadata);
        }
        public virtual InterfaceApiAttributesDescriptorBuilder<TInterface> Interface<TInterface>()
        {
            var matadata = InterfaceMetadata(typeof(TInterface));
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

        public NameSpaceApiAttributesDescriptorBuilder TryAddPropertie(object key, object? value)
        {
            Metadata.TryAddPropertie(key, value);
            return this;
        }
    }
}