using System.Net.Http;
using System.Threading.Tasks;

namespace EzrealClient.Test.Attributes.ReturnAttributes
{
    public interface ITestApi
    {
        Task<HttpResponseMessage> HttpResponseMessageAsync();

        Task<string> StringAsync();

        Task<byte[]> ByteArrayAsync();

        Task<TestModel> JsonXmlAsync();
    }
}
