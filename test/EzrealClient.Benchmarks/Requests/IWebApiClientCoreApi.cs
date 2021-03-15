using System.Threading.Tasks;
using EzrealClient.Attributes;

namespace EzrealClient.Benchmarks.Requests
{
    public interface IEzrealClientApi
    {
        [HttpGet("/benchmarks/{id}")]
        System.Threading.Tasks.Task<Model> GetAsyc(string id);

        [HttpPost("/benchmarks")]
        System.Threading.Tasks.Task<Model> PostJsonAsync([JsonContent] Model model);

        [HttpPut("/benchmarks/{id}")]
        System.Threading.Tasks.Task<Model> PutFormAsync(string id, [FormContent] Model model);
    }
}
