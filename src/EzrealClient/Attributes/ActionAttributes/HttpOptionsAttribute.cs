using System.Net.Http;

namespace EzrealClient.Attributes
{
    /// <summary>
    /// 表示Options请求
    /// </summary>
    public class HttpOptionsAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Options请求
        /// </summary>
        public HttpOptionsAttribute()
           : this(path: null)
        {
        }

        /// <summary>
        /// Options请求
        /// </summary>
        /// <param name="path">相对路径</param>
        public HttpOptionsAttribute(string? path)
            : base(HttpMethod.Options, path)
        {
        }
    }
}
