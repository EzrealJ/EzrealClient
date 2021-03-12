using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EzrealClient.FluentApi.Builders
{
    public class FluentApiAttributesDescriptorBuilder
    {
        public FluentApiAttributesDescriptorBuilder()
        {
            Metadata = new FluentMetadata();
        }

        protected virtual FluentMetadata Metadata { get; }

        public virtual AssemblyFluentMetadata AssemblyMetadata(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            AssemblyFluentMetadata metadata = Metadata.Assemblys.FirstOrDefault(a => a.Assembly == assembly);
            if (metadata == null)
            {
                metadata = new AssemblyFluentMetadata(assembly);
                ((List<AssemblyFluentMetadata>)Metadata.Assemblys).Add(metadata);
            }
            return metadata;
        }

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
            var metadata = AssemblyMetadata(assembly);
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

        public virtual FluentApiAttributesDescriptorBuilder ConfigureAssembly(string assemblyName,Action<AssemblyApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Assembly(assemblyName));
            return this;
        }
        public virtual FluentApiAttributesDescriptorBuilder ConfigureAssembly(Assembly assembly, Action<AssemblyApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Assembly(assembly));
            return this;
        }

        public virtual FluentApiAttributesDescriptorBuilder ConfigureInterface(Type interfaceType,Action<InterfaceApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Interface(interfaceType));
            return this;
        }
        public virtual FluentApiAttributesDescriptorBuilder ConfigureInterface<TInterface>(Action<InterfaceApiAttributesDescriptorBuilder> buildAction)
        {
            if (buildAction is null)
            {
                throw new ArgumentNullException(nameof(buildAction));
            }
            buildAction(Interface<TInterface>());
            return this;
        }


        public FluentApiAttributesDescriptorBuilder SetCacheAttribute(IApiCacheAttribute apiCacheAttribute)
        {
            Metadata.SetCacheAttribute(apiCacheAttribute);
            return this;
        }

        public FluentApiAttributesDescriptorBuilder TryAddApiActionAttribute(IApiActionAttribute apiActionAttribute)
        {
            Metadata.TryAddApiActionAttribute(apiActionAttribute);
            return this;
        }

        public FluentApiAttributesDescriptorBuilder TryAddApiFilterAttribute(IApiFilterAttribute apiFilterAttribute)
        {
            Metadata.TryAddApiFilterAttribute(apiFilterAttribute);
            return this;
        }

        public FluentApiAttributesDescriptorBuilder TryAddApiReturnAttribute(IApiReturnAttribute apiReturnAttribute)
        {
            Metadata.TryAddApiReturnAttribute(apiReturnAttribute);
            return this;
        }

        public FluentApiAttributesDescriptorBuilder TryAddPropertie(object key, object? value)
        {
            Metadata.TryAddPropertie(key,value);
            return this;
        }

    }
}