using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EzrealClient.Test.Parameters
{
    public interface ITestApi 
    {
        System.Threading.Tasks.Task<HttpResponseMessage> PostAsync(object value);
    }
}
