namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// EzrealClient全局配置的Builder接口
    /// </summary>
    public interface IEzrealClientBuilder
    {
        /// <summary>
        /// 获取服务集合
        /// </summary>
        IServiceCollection Services { get; }
    }
}
