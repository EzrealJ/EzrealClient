using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzrealClient.FluentApi.Builders
{
    public class AssemblyApiAttributesDescriptorBuilder
    {
        public AssemblyApiAttributesDescriptorBuilder(AssemblyFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        public AssemblyFluentMetadata Metadata { get; }


        public virtual NameSpaceFluentMetadata NameSpaceMetadata(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentException($"“{nameof(@namespace)}”不能为 null 或空白。", nameof(@namespace));
            }

            NameSpaceFluentMetadata? metadata = Metadata.NameSpaces.FirstOrDefault(a => a.Name == @namespace);
            if (metadata == null)
            {
                metadata = new NameSpaceFluentMetadata(@namespace,Metadata);
                ((List<NameSpaceFluentMetadata>)Metadata.NameSpaces).Add(metadata);
            }
            return metadata;
        }


        public virtual NameSpaceApiAttributesDescriptorBuilder NameSpace(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentException($"“{nameof(@namespace)}”不能为 null 或空白。", nameof(@namespace));
            }
            var matadata = NameSpaceMetadata(@namespace);
            return new NameSpaceApiAttributesDescriptorBuilder(matadata);
        }
    }
}
