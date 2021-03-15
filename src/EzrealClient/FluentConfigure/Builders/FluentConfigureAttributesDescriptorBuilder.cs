using EzrealClient.FluentConfigure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EzrealClient.FluentConfigure.Builders
{
    public class FluentConfigureAttributesDescriptorBuilder
    {
        public FluentConfigureAttributesDescriptorBuilder()
        {
            Metadata = new FluentMetadata();
        }

        internal virtual FluentMetadata Metadata { get; }



        public virtual AssemblyApiAttributesDescriptorBuilder Assembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentException($"“{nameof(assemblyName)}”不能为 null 或空白。", nameof(assemblyName));
            }
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            return Assembly(assembly);
        }

        public virtual AssemblyApiAttributesDescriptorBuilder Assembly(Assembly assembly)
        {
            var metadata = Metadata.GetOrAddAssemblyMetadata(assembly);
            return new AssemblyApiAttributesDescriptorBuilder(metadata);
        }

        public virtual NameSpaceApiAttributesDescriptorBuilder NameSpace(string @namespace, Assembly assembly)
        {
            return Assembly(assembly).NameSpace(@namespace);
        }
        public virtual InterfaceApiAttributesDescriptorBuilder Interface(Type interfaceType)
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
            Assembly assembly = interfaceType.Assembly;
            string @namespace = interfaceType.Namespace;
            return Assembly(assembly).NameSpace(@namespace).Interface(interfaceType);
        }
        public virtual InterfaceApiAttributesDescriptorBuilder<TInterface> Interface<TInterface>()
        {
            Type interfaceType = typeof(TInterface);
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
            Assembly assembly = interfaceType.Assembly;
            string @namespace = interfaceType.Namespace;
            return Assembly(assembly).NameSpace(@namespace).Interface<TInterface>();
        }

        public virtual FluentConfigureAttributesDescriptorBuilder ConfigureAssembly(string assemblyName, Action<AssemblyApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Assembly(assemblyName));
            return this;
        }
        public virtual FluentConfigureAttributesDescriptorBuilder ConfigureAssembly(Assembly assembly, Action<AssemblyApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Assembly(assembly));
            return this;
        }

        public virtual FluentConfigureAttributesDescriptorBuilder ConfigureInterface(Type interfaceType, Action<InterfaceApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Interface(interfaceType));
            return this;
        }
        public virtual FluentConfigureAttributesDescriptorBuilder ConfigureInterface<TInterface>(Action<InterfaceApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Interface<TInterface>());
            return this;
        }


        public FluentConfigureAttributesDescriptorBuilder SetCacheAttribute(IApiCacheAttribute apiCacheAttribute)
        {
            Metadata.SetCacheAttribute(apiCacheAttribute);
            return this;
        }

        public FluentConfigureAttributesDescriptorBuilder TryAddApiActionAttribute(IApiActionAttribute apiActionAttribute)
        {
            Metadata.TryAddApiActionAttribute(apiActionAttribute);
            return this;
        }

        public FluentConfigureAttributesDescriptorBuilder TryAddApiFilterAttribute(IApiFilterAttribute apiFilterAttribute)
        {
            Metadata.TryAddApiFilterAttribute(apiFilterAttribute);
            return this;
        }

        public FluentConfigureAttributesDescriptorBuilder TryAddApiReturnAttribute(IApiReturnAttribute apiReturnAttribute)
        {
            Metadata.TryAddApiReturnAttribute(apiReturnAttribute);
            return this;
        }

        public FluentConfigureAttributesDescriptorBuilder TryAddPropertie(object key, object value)
        {
            Metadata.TryAddPropertie(key, value);
            return this;
        }

    }
}