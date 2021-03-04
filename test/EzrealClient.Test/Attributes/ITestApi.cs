using System.Net.Http;
using System.Threading.Tasks;

namespace EzrealClient.Test.Attributes
{
    public interface ITestApi : IHttpApi
    {
        Task<HttpResponseMessage> PostAsync(object value);
    }
}
