using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
    public class ParameterFluentMetadata
    {
        /// <summary>
        /// 获取参数名称
        /// </summary>
        public string? Name { get; protected set; }

        /// <summary>
        /// 获取关联的参数信息
        /// </summary>
        public ParameterInfo? Member { get; protected set; }

        /// <summary>
        /// 获取参数索引
        /// </summary>
        public int Index { get; protected set; }

        /// <summary>
        /// 获取参数类型
        /// </summary>
        public Type? ParameterType { get; protected set; }

        /// <summary>
        /// 获取关联的参数特性
        /// </summary>
        public IEnumerable<IApiParameterAttribute>? Attributes { get; protected set; }

        /// <summary>
        /// 获取关联的ValidationAttribute特性
        /// </summary>
        public IEnumerable<ValidationAttribute>? ValidationAttributes { get; protected set; }
    }
}
