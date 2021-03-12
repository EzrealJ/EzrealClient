using EzrealClient.FluentApi.Builders.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzrealClient.FluentApi.Builders
{
    public class ParameterAttributesDescriptorBuilder
    {
        public ParameterAttributesDescriptorBuilder(ParameterFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        public ParameterFluentMetadata Metadata { get; }
    }
}
