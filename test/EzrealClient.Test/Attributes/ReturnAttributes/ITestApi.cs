using System.Net.Http;
using System.Threading.Tasks;

namespace EzrealClient.Test.Attributes.ReturnAttributes
{
    public interface ITestApi
    {
        System.Threading.Tasks.Task<HttpResponseMessage> HttpResponseMessageAsync();

        System.Threading.Tasks.Task<string> StringAsync();

        System.Threading.Tasks.Task<byte[]> ByteArrayAsync();

        System.Threading.Tasks.Task<TestModel> JsonXmlAsync();
    }
}
