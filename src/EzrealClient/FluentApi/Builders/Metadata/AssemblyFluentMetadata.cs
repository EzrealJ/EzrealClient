using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzrealClient.FluentApi.Builders.Metadata
{
   public class AssemblyFluentMetadata:IFluentMetadata
    {
        public AssemblyFluentMetadata(Assembly assembly)
        {
            Assembly = assembly;
            ApiActionAttributes = new List<IApiActionAttribute>();
            ApiFilterAttributes = new List<IApiFilterAttribute>();
            ApiReturnAttributes = new List<IApiReturnAttribute>();
            Properties = new Dictionary<object, object>();
            NameSpaces = new List<NameSpaceFluentMetadata>();
        }

        public virtual IApiCacheAttribute? CacheAttribute { get; protected set; }

        public virtual IEnumerable<IApiActionAttribute> ApiActionAttributes { get; protected set; }

        public virtual IEnumerable<IApiFilterAttribute> ApiFilterAttributes { get; protected set; }

        public virtual IEnumerable<IApiReturnAttribute> ApiReturnAttributes { get; protected set; }

        public virtual Dictionary<object, object>? Properties { get; protected set; }

        public virtual Assembly Assembly { get; protected set; }

  
        public virtual AssemblyName AssemblyName => Assembly.GetName();


        /// <summary>
        /// 命名空间
        /// </summary>
        public virtual IEnumerable<NameSpaceFluentMetadata> NameSpaces { get; protected set; }
    }
}
