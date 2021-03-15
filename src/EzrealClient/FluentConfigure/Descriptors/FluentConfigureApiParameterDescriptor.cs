using EzrealClient.Attributes;
using EzrealClient.FluentConfigure.Metadata;
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

namespace EzrealClient.FluentConfigure.Descriptors
{
    public class FluentConfigureApiParameterDescriptor : ApiParameterDescriptor
    {
        /// <summary>
        /// 缺省参数特性时的默认特性
        /// </summary>
        private static readonly IApiParameterAttribute defaultAttribute = new PathQueryAttribute();

        /// <summary>
        /// 获取参数名称
        /// </summary>
        public override string Name { get; protected set; }

        /// <summary>
        /// 获取关联的参数信息
        /// </summary>
        public override ParameterInfo Member { get; protected set; }

        /// <summary>
        /// 获取参数索引
        /// </summary>
        public override int Index { get; protected set; }

        /// <summary>
        /// 获取参数类型
        /// </summary>
        public override Type ParameterType { get; protected set; }

        /// <summary>
        /// 获取关联的参数特性
        /// </summary>
        public override IReadOnlyList<IApiParameterAttribute> Attributes { get; protected set; }

        /// <summary>
        /// 获取关联的ValidationAttribute特性
        /// </summary>
        public override IReadOnlyList<ValidationAttribute> ValidationAttributes { get; protected set; }

        /// <summary>
        /// 请求Api的参数描述
        /// </summary>
        /// <param name="parameterFluentMetadata">参数信息</param>
        /// <exception cref="ArgumentNullException"></exception>
        public FluentConfigureApiParameterDescriptor(ParameterFluentMetadata parameterFluentMetadata)
            : this(parameterFluentMetadata, defaultAttribute)
        {
        }

        /// <summary>
        /// 请求Api的参数描述
        /// </summary>
        /// <param name="parameterFluentMetadata">参数信息</param>
        /// <param name="defaultAtribute">缺省特性时使用的默认特性</param>
        /// <exception cref="ArgumentNullException"></exception>
        public FluentConfigureApiParameterDescriptor(ParameterFluentMetadata parameterFluentMetadata, IApiParameterAttribute defaultAtribute)
        {
            if (parameterFluentMetadata == null)
            {
                throw new ArgumentNullException(nameof(parameterFluentMetadata));
            }
            var patameterInfo = parameterFluentMetadata.Member;
            var parameterAttributes = patameterInfo.GetCustomAttributes().ToArray();
            var parameterType = parameterFluentMetadata.ParameterType;
            var parameterAlias = parameterAttributes.OfType<AliasAsAttribute>().FirstOrDefault();
            var parameterName = parameterAlias == null ? parameterFluentMetadata.Name : parameterAlias.Name;
            var validationAttributes = parameterFluentMetadata.ValidationAttributes.Concat(parameterAttributes.OfType<ValidationAttribute>()).ToReadOnlyList();
            this.Member = patameterInfo;
            this.Name = parameterName ?? string.Empty;
            this.Index = parameterFluentMetadata.Index;
            this.ParameterType = parameterType;
            this.ValidationAttributes = validationAttributes;
            var attributes = this.GetAttributes(parameterFluentMetadata, parameterAttributes).ToArray();
            if (attributes.Length == 0)
            {
                this.Attributes = new[] { defaultAtribute }.ToReadOnlyList();
            }
            else
            {
                this.Attributes = attributes.ToReadOnlyList();
            }
        }

        /// <summary>
        /// 获取参数的特性
        /// </summary>
        /// <param name="parameterFluentMetadata">参数</param>
        /// <param name="attributes">参数声明的所有特性</param> 
        /// <returns></returns>
        protected virtual IEnumerable<IApiParameterAttribute> GetAttributes(ParameterFluentMetadata parameterFluentMetadata, Attribute[] attributes)
        {
            var parameterType = parameterFluentMetadata.ParameterType;
            if (parameterType.IsInheritFrom<HttpContent>() == true)
            {
                return RepeatOne<HttpContentTypeAttribute>();
            }

            if (parameterType.IsInheritFrom<IApiParameter>() || parameterType.IsInheritFrom<IEnumerable<IApiParameter>>())
            {
                return RepeatOne<ApiParameterTypeAttribute>();
            }

            if (parameterType == typeof(CancellationToken) || parameterType.IsInheritFrom<IEnumerable<CancellationToken>>())
            {
                return RepeatOne<CancellationTokenTypeAttribute>();
            }

            if (parameterType == typeof(FileInfo) || parameterType.IsInheritFrom<IEnumerable<FileInfo>>())
            {
                return RepeatOne<FileInfoTypeAttribute>();
            }

            return parameterFluentMetadata.ApiParameterAttributes.Concat(attributes.OfType<IApiParameterAttribute>());
        }

        /// <summary>
        /// 返回单次的迭代器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IEnumerable<T> RepeatOne<T>() where T : new()
        {
            return Enumerable.Repeat(new T(), 1);
        }
    }
}
