using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace EzrealClient.FluentApi.Builders
{
    public class FluentApiAttributesDescriptorBuilder
    {
        public FluentApiAttributesDescriptorBuilder()
        {
            Metadata = new List<AssemblyFluentMetadata>();
        }

        public IEnumerable<AssemblyFluentMetadata> Metadata { get; }



        public virtual AssemblyApiAttributesDescriptorBuilder Assembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentException($"“{nameof(assemblyName)}”不能为 null 或空白。", nameof(assemblyName));
            }

            AssemblyFluentMetadata metadata = Metadata.FirstOrDefault(assembly => assembly.AssemblyName.Name == assemblyName);
            if (metadata == null)
            {
                var assembly = System.Reflection.Assembly.Load(assemblyName);
                metadata = new AssemblyFluentMetadata(assembly);
                ((List<AssemblyFluentMetadata>)Metadata).Add(metadata);
            }
            return new AssemblyApiAttributesDescriptorBuilder(metadata);
        }

        public virtual AssemblyApiAttributesDescriptorBuilder NameSpace(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentException($"“{nameof(@namespace)}”不能为 null 或空白。", nameof(@namespace));
            }

            AssemblyFluentMetadata metadata = Metadata.FirstOrDefault(assembly => assembly.AssemblyName.Name == @namespace);
            if (metadata == null)
            {
                var assembly = System.Reflection.Assembly.Load(@namespace);
                metadata = new AssemblyFluentMetadata(assembly);
                ((List<AssemblyFluentMetadata>)Metadata).Add(metadata);
            }
            return new AssemblyApiAttributesDescriptorBuilder(metadata);
        }
    }
}
