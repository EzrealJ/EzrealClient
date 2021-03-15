using EzrealClient.FluentConfigure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EzrealClient.FluentConfigure.Builders
{
    public class AssemblyApiAttributesDescriptorBuilder
    {
        public AssemblyApiAttributesDescriptorBuilder(AssemblyFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        internal AssemblyFluentMetadata Metadata { get; }





        public virtual NameSpaceApiAttributesDescriptorBuilder NameSpace(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentException($"“{nameof(@namespace)}”不能为 null 或空白。", nameof(@namespace));
            }
            var matadata = Metadata.GetOrAddNameSpaceMetadata(@namespace);
            return new NameSpaceApiAttributesDescriptorBuilder(matadata);
        }
    }
}
