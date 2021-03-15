using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EzrealClient;
using EzrealClient.Attributes;
using EzrealClient.Parameters;

namespace App.Clients
{
    /// <summary>
    /// 用户操作接口
    /// </summary>    
    [LoggingFilter]
    [OAuthToken]
    public interface IUserApi : IHttpApi
    {
        [HttpGet("api/users/{account}")]
        EzrealClient.Task<HttpResponseMessage> GetAsync([Required] string account);

        [HttpGet("api/users/{account}")]
        EzrealClient.Task<string> GetAsStringAsync([Required] string account, CancellationToken token = default);


        [HttpGet("api/users/{account}")]
        [JsonReturn]
        EzrealClient.Task<string> GetExpectJsonAsync([Required] string account, CancellationToken token = default);


        [HttpGet("api/users/{account}")]
        [XmlReturn]
        EzrealClient.Task<string> GetExpectXmlAsync([Required] string account, CancellationToken token = default);



        [HttpGet("api/users/{account}")]
        EzrealClient.Task<byte[]> GetAsByteArrayAsync([Required] string account, CancellationToken token = default);

        [HttpGet("api/users/{account}")]
        EzrealClient.Task<Stream> GetAsStreamAsync([Required] string account, CancellationToken token = default);

        [HttpGet("api/users/{account}")]
        EzrealClient.Task<User> GetAsModelAsync([Required] string account, CancellationToken token = default);


        [HttpPost("api/users/body")]
        System.Threading.Tasks.Task<User> PostByJsonAsync([Required, JsonContent] User user, CancellationToken token = default);

        [HttpPost("api/users/body")]
        System.Threading.Tasks.Task<User> PostByXmlAsync([Required, XmlContent] User user, CancellationToken token = default);



        [HttpPost("api/users/form")]
        System.Threading.Tasks.Task<User> PostByFormAsync([Required, FormContent] User user, CancellationToken token = default);

        [HttpPost("api/users/formdata")]
        System.Threading.Tasks.Task<User> PostByFormDataAsync([Required, FormDataContent] User user, FormDataFile file, CancellationToken token = default);



        [HttpDelete("api/users/{account}")]
        Task DeleteAsync([Required] string account);
    }
}
