using EzrealClient.FluentConfigure.Metadata;
using EzrealClient.Implementations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentConfigure.Descriptors
{
    public class FluentConfigureApiActionDescriptor : ApiActionDescriptor
    {
        /// <summary>
        /// 获取所在接口类型
        /// 这个值不一定是声明方法的接口类型
        /// </summary>
        public override Type InterfaceType { get; protected set; }

        /// <summary>
        /// 获取Api名称
        /// </summary>
        public override string Name { get; protected set; }

        /// <summary>
        /// 获取关联的方法信息
        /// </summary>
        public override MethodInfo Member { get; protected set; }

        /// <summary>
        /// 获取Api关联的缓存特性
        /// </summary>
        public override IApiCacheAttribute? CacheAttribute { get; protected set; }

        /// <summary>
        /// 获取Api关联的特性
        /// </summary>
        public override IReadOnlyList<IApiActionAttribute> Attributes { get; protected set; }

        /// <summary>
        /// 获取Api关联的过滤器特性
        /// </summary>
        public override IReadOnlyList<IApiFilterAttribute> FilterAttributes { get; protected set; }


        /// <summary>
        /// 获取Api的参数描述
        /// </summary>
        public override IReadOnlyList<ApiParameterDescriptor> Parameters { get; protected set; }

        /// <summary>
        /// 获取Api的返回描述
        /// </summary>
        public override ApiReturnDescriptor Return { get; protected set; }

        /// <summary>
        /// 获取自定义数据存储的字典
        /// </summary>
        public override ConcurrentDictionary<object, object> Properties { get; protected set; }

        /// <summary>
        /// 请求Api描述
        /// for test only
        /// </summary>
        internal FluentConfigureApiActionDescriptor(MethodFluentMetadata methodFluentMetadata)
            : this(methodFluentMetadata, methodFluentMetadata.InterfaceType)
        {
        }

        /// <summary>
        /// 请求Api描述
        /// </summary>
        /// <param name="methodFluentMetadata"></param>
        /// <param name="interfaceType">接口类型</param> 
        public FluentConfigureApiActionDescriptor(MethodFluentMetadata methodFluentMetadata, Type interfaceType)
        {
            var method = methodFluentMetadata.Member;
            var methodAttributes = method.GetCustomAttributes().ToArray();
            var interfaceAttributes = interfaceType.GetInterfaceCustomAttributes();

            var fluentConfigureFluentMetadata = methodFluentMetadata.InterfaceMetadata.NameSpaceMetadata.AssemblyMetadata.FluentMetadata;
            var fluentConfigureAssemblyMetadata = methodFluentMetadata.InterfaceMetadata.NameSpaceMetadata.AssemblyMetadata;
            var fluentConfigureNameSpaceMetadata = methodFluentMetadata.InterfaceMetadata.NameSpaceMetadata;
            var fluentConfigureInterfaceMetadata = methodFluentMetadata.InterfaceMetadata;

            var fluentConfigureApiActionAttributes = methodFluentMetadata.ApiActionAttributes
                .Concat(fluentConfigureInterfaceMetadata.ApiActionAttributes)
                 .Distinct(MultiplableComparer<IApiActionAttribute>.Instance)
                 .Concat(fluentConfigureNameSpaceMetadata.ApiActionAttributes)
                 .Distinct(MultiplableComparer<IApiActionAttribute>.Instance)
                 .Concat(fluentConfigureAssemblyMetadata.ApiActionAttributes)
                 .Distinct(MultiplableComparer<IApiActionAttribute>.Instance)
                 .Concat(fluentConfigureFluentMetadata.ApiActionAttributes)
                 .Distinct(MultiplableComparer<IApiActionAttribute>.Instance);

            // 接口特性优先于方法所在类型的特性
            var actionAttributes = methodAttributes
                .OfType<IApiActionAttribute>()
                .Concat(interfaceAttributes.OfType<IApiActionAttribute>())
                .Distinct(MultiplableComparer<IApiActionAttribute>.Instance)
                .Concat(fluentConfigureApiActionAttributes)
                .Distinct(MultiplableComparer<IApiActionAttribute>.Instance)
                .OrderBy(item => item.OrderIndex)
                .ToReadOnlyList();

            var fluentConfigureApiFilterAttributes = methodFluentMetadata.ApiFilterAttributes
                 .Concat(fluentConfigureInterfaceMetadata.ApiFilterAttributes)
                 .Distinct(MultiplableComparer<IApiFilterAttribute>.Instance)
                 .Concat(fluentConfigureNameSpaceMetadata.ApiFilterAttributes)
                 .Distinct(MultiplableComparer<IApiFilterAttribute>.Instance)
                 .Concat(fluentConfigureAssemblyMetadata.ApiFilterAttributes)
                 .Distinct(MultiplableComparer<IApiFilterAttribute>.Instance)
                 .Concat(fluentConfigureFluentMetadata.ApiFilterAttributes)
                 .Distinct(MultiplableComparer<IApiFilterAttribute>.Instance);

            var filterAttributes = methodAttributes
                .OfType<IApiFilterAttribute>()
                .Concat(interfaceAttributes.OfType<IApiFilterAttribute>())
                .Distinct(MultiplableComparer<IApiFilterAttribute>.Instance)
                .Concat(fluentConfigureApiFilterAttributes)
                .Distinct(MultiplableComparer<IApiFilterAttribute>.Instance)
                .OrderBy(item => item.OrderIndex)
                .Where(item => item.Enable)
                .ToReadOnlyList();

            this.InterfaceType = interfaceType;

            this.Member = method;
            this.Name = method.Name;
            this.Attributes = actionAttributes;


            var fluentConfigureCacheAttribute = methodFluentMetadata.CacheAttribute
                  ?? fluentConfigureInterfaceMetadata.CacheAttribute
                  ?? fluentConfigureNameSpaceMetadata.CacheAttribute
                  ?? fluentConfigureAssemblyMetadata.CacheAttribute
                  ?? fluentConfigureFluentMetadata.CacheAttribute;
            this.CacheAttribute = methodAttributes.OfType<IApiCacheAttribute>().FirstOrDefault() ?? fluentConfigureCacheAttribute;
            this.FilterAttributes = filterAttributes;
            this.Properties = new ConcurrentDictionary<object, object>();

            methodFluentMetadata.Properties.Select(kv => this.Properties.TryAdd(kv.Key, kv.Value));
            fluentConfigureInterfaceMetadata.Properties.Select(kv => this.Properties.TryAdd(kv.Key, kv.Value));
            fluentConfigureNameSpaceMetadata.Properties.Select(kv => this.Properties.TryAdd(kv.Key, kv.Value));
            fluentConfigureAssemblyMetadata.Properties.Select(kv => this.Properties.TryAdd(kv.Key, kv.Value));
            fluentConfigureFluentMetadata.Properties.Select(kv => this.Properties.TryAdd(kv.Key, kv.Value));



            var fluentConfigureApiReturnAttributes = methodFluentMetadata.ApiReturnAttributes
                .Concat(fluentConfigureInterfaceMetadata.ApiReturnAttributes)
                 .Distinct(MultiplableComparer<IApiReturnAttribute>.Instance)
                 .Concat(fluentConfigureNameSpaceMetadata.ApiReturnAttributes)
                 .Distinct(MultiplableComparer<IApiReturnAttribute>.Instance)
                 .Concat(fluentConfigureAssemblyMetadata.ApiReturnAttributes)
                 .Distinct(MultiplableComparer<IApiReturnAttribute>.Instance)
                 .Concat(fluentConfigureFluentMetadata.ApiReturnAttributes)
                 .Distinct(MultiplableComparer<IApiReturnAttribute>.Instance);


            this.Return = new FluentConfigureApiReturnDescriptor(method.ReturnType, methodAttributes, interfaceAttributes, fluentConfigureApiReturnAttributes);
            this.Parameters = method.GetParameters().Select(p =>
            {
                var parameterMetadata = methodFluentMetadata.GetOrAddParameterMetadata(p);
                return new FluentConfigureApiParameterDescriptor(parameterMetadata);
            }).ToReadOnlyList();
        }
    }
}
