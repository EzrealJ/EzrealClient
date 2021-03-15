using EzrealClient.Implementations;
using EzrealClient.Implementations.TypeAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;

namespace EzrealClient.FluentConfigure.Metadata
{
    public class ParameterFluentMetadata
    {
        private string? name;

        public ParameterFluentMetadata(ParameterInfo parameterInfo, MethodFluentMetadata metadata)
        {
            Member = parameterInfo;
            MethodMetadata = metadata;
            ApiParameterAttributes = new List<IApiParameterAttribute>();
            ValidationAttributes = new List<ValidationAttribute>();
        }

        /// <summary>
        /// 获取参数名称
        /// </summary>
        public string Name { get => name ?? Member.Name; protected set => name = value; }

        /// <summary>
        /// 获取关联的参数信息
        /// </summary>
        public ParameterInfo Member { get; protected set; }

        /// <summary>
        /// 获取参数索引
        /// </summary>
        public int Index => Member.Position;

        /// <summary>
        /// 获取参数类型
        /// </summary>
        public Type ParameterType => Member.ParameterType;

        /// <summary>
        /// 获取关联的参数特性
        /// </summary>
        public IEnumerable<IApiParameterAttribute> ApiParameterAttributes { get; protected set; }

        /// <summary>
        /// 获取关联的ValidationAttribute特性
        /// </summary>
        public IEnumerable<ValidationAttribute> ValidationAttributes { get; protected set; }

        public MethodFluentMetadata MethodMetadata { get; }



        public virtual void AliasAs(string name)
        {
            this.Name = name;
        }
        
        public bool AddApiParameterAttribute(IApiParameterAttribute apiParameterAttribute)
        {
            ((List<IApiParameterAttribute>)ApiParameterAttributes).Add(apiParameterAttribute);
            return true;
        }

        public bool AddValidationAttribute(ValidationAttribute validationAttribute)
        {
            ((List<ValidationAttribute>)ValidationAttributes).Add(validationAttribute);
            return true;
        }
    }
}
