using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
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
    }
}
