using System.Net.Http;
using System.Threading.Tasks;

namespace EzrealClient.Test.Attributes
{
    public interface ITestApi : IHttpApi
    {
        System.Threading.Tasks.Task<HttpResponseMessage> PostAsync(object value);
    }
}
