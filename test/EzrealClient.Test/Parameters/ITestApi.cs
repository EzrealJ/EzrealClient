using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EzrealClient.Test.Parameters
{
    public interface ITestApi 
    {
        Task<HttpResponseMessage> PostAsync(object value);
    }
}
