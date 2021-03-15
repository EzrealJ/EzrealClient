using System.Threading.Tasks;

namespace EzrealClient.Extensions.OAuths
{
    /// <summary>
    /// 定义token提供者的接口
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// 设置别名
        /// </summary>
        string Name { set; }

        /// <summary>
        /// 强制清除token以支持下次获取到新的token
        /// </summary>
        void ClearToken();

        /// <summary>
        /// 获取token信息
        /// </summary> 
        /// <returns></returns>
        System.Threading.Tasks.Task<TokenResult> GetTokenAsync();
    }
}