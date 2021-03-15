using EzrealClient.FluentConfigure.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EzrealClient.FluentConfigure.Builders
{
    public class ParameterAttributesDescriptorBuilder
    {
        public ParameterAttributesDescriptorBuilder(ParameterFluentMetadata metadata)
        {
            Metadata = metadata;
        }

        internal virtual ParameterFluentMetadata Metadata { get; }


        public virtual ParameterAttributesDescriptorBuilder AliasAs(string name)
        {
            Metadata.AliasAs(name);
            return this;
        }

        public ParameterAttributesDescriptorBuilder AddApiParameterAttribute(IApiParameterAttribute apiParameterAttribute)
        {
            ((List<IApiParameterAttribute>)Metadata.ApiParameterAttributes).Add(apiParameterAttribute);
            return this;
        }

        public ParameterAttributesDescriptorBuilder AddValidationAttribute(ValidationAttribute validationAttribute)
        {
            ((List<ValidationAttribute>)Metadata.ValidationAttributes).Add(validationAttribute);
            return this;
        }
    }
}
